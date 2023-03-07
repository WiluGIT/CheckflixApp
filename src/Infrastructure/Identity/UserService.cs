using System.Security.Cryptography.X509Certificates;
using Ardalis.Specification.EntityFrameworkCore;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using CheckflixApp.Application.Common.Exceptions;
using CheckflixApp.Application.Common.FileStorage;
using CheckflixApp.Application.Common.Interfaces;
using CheckflixApp.Application.Common.Models;
using CheckflixApp.Application.Common.Specification;
using CheckflixApp.Application.Identity.Common;
using CheckflixApp.Application.Identity.Interfaces;
using CheckflixApp.Application.Identity.Users.Commands.ToggleUserStatus;
using CheckflixApp.Application.Mailing;
using CheckflixApp.Infrastructure.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;

namespace CheckflixApp.Infrastructure.Identity;
internal partial class UserService : IUserService
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly RoleManager<IdentityRole> _roleManager;
    private readonly IStringLocalizer<UserService> _localizer;
    private readonly SecuritySettings _securitySettings;
    private readonly IMailService _mailService;
    private readonly IJobService _jobService;
    private readonly IEmailTemplateService _templateService;
    private readonly IFileStorageService _fileStorage;
    private readonly IMapper _mapper;

    public UserService(
        UserManager<ApplicationUser> userManager,
        IMapper mapper,
        IStringLocalizer<UserService> localizer,
        RoleManager<IdentityRole> roleManager,
        IOptions<SecuritySettings> securitySettings,
        SignInManager<ApplicationUser> signInManager,
        IMailService mailService,
        IJobService jobService,
        IEmailTemplateService templateService,
        IFileStorageService fileStorage
        )
    {
        _userManager = userManager;
        _mapper = mapper;
        _localizer = localizer;
        _roleManager = roleManager;
        _securitySettings = securitySettings.Value;
        _signInManager = signInManager;
        _mailService = mailService;
        _jobService = jobService;
        _templateService = templateService;
        _fileStorage = fileStorage;
    }

    public async Task<int> GetCountAsync(CancellationToken cancellationToken) =>
        await _userManager.Users.AsNoTracking().CountAsync(cancellationToken);

    public async Task<bool> ExistsWithNameAsync(string name) =>
        await _userManager.FindByNameAsync(name) is not null;

    public async Task<bool> ExistsWithEmailAsync(string email, string? exceptId = null) =>
        await _userManager.FindByEmailAsync(email.Normalize()) is ApplicationUser user && user.Id != exceptId;

    public async Task<bool> ExistsWithPhoneNumberAsync(string phoneNumber, string? exceptId = null) =>
        await _userManager.Users.FirstOrDefaultAsync(x => x.PhoneNumber == phoneNumber) is ApplicationUser user && user.Id != exceptId;

    public async Task<List<UserDetailsDto>> GetListAsync(CancellationToken cancellationToken)
        => await _userManager.Users
                .AsNoTracking()
                .Select(x => new UserDetailsDto 
                { 
                    Id = x.Id,
                    Email= x.Email,
                    EmailConfirmed= x.EmailConfirmed,
                    ImageUrl= x.ImageUrl,
                    IsActive = x.IsActive,
                    PhoneNumber = x.PhoneNumber,
                    UserName= x.UserName,
                })
                .ToListAsync();

    public async Task<UserDetailsDto> GetAsync(string userId, CancellationToken cancellationToken)
    {
        var userDetailsDto = await _userManager.Users
            .AsNoTracking()
            .Where(u => u.Id == userId)
            .Select(x => new UserDetailsDto
            {
                Id = x.Id,
                Email = x.Email,
                EmailConfirmed = x.EmailConfirmed,
                ImageUrl = x.ImageUrl,
                IsActive = x.IsActive,
                PhoneNumber = x.PhoneNumber,
                UserName = x.UserName,
            })
            .FirstOrDefaultAsync(cancellationToken);

        _ = userDetailsDto ?? throw new NotFoundException(_localizer["User Not Found."]);

        return userDetailsDto;
    }

    public async Task<PaginatedList<UserDetailsDto>> SearchAsync(UserListFilter filter, CancellationToken cancellationToken)
    {
        var spec = new EntitiesByPaginationFilterSpec<ApplicationUser>(filter);

        var users = await _userManager.Users
            .WithSpecification(spec)
            .Select(x => new UserDetailsDto
            {
                Id = x.Id,
                Email = x.Email,
                EmailConfirmed = x.EmailConfirmed,
                ImageUrl = x.ImageUrl,
                IsActive = x.IsActive,
                PhoneNumber = x.PhoneNumber,
                UserName = x.UserName,
            })
            .ToListAsync(cancellationToken);

        int count = await _userManager.Users
            .CountAsync(cancellationToken);

        return new PaginatedList<UserDetailsDto>(users, count, filter.PageNumber, filter.PageSize);
    }

    public async Task ToggleUserStatusAsync(ToggleUserStatusCommand command, CancellationToken cancellationToken)
    {
        var user = await _userManager.Users.Where(u => u.Id == command.Id).FirstOrDefaultAsync(cancellationToken);

        _ = user ?? throw new NotFoundException(_localizer["User Not Found."]);

        bool isAdmin = await _userManager.IsInRoleAsync(user, SystemRoles.Administrator);
        if (isAdmin)
        {
            throw new InternalServerException(_localizer["Administrators Profile's Status cannot be toggled"]);
        }

        user.IsActive = command.ActivateUser;

        await _userManager.UpdateAsync(user);

        //await _events.PublishAsync(new ApplicationUserUpdatedEvent(user.Id));
    }
}
