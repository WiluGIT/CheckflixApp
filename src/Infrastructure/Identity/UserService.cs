using Ardalis.Specification.EntityFrameworkCore;
using AutoMapper;
using CheckflixApp.Application.Common.FileStorage;
using CheckflixApp.Application.Common.Interfaces;
using CheckflixApp.Application.Common.Models;
using CheckflixApp.Application.Common.Specification;
using CheckflixApp.Application.Followings.Common;
using CheckflixApp.Application.Identity.Common;
using CheckflixApp.Application.Identity.Interfaces;
using CheckflixApp.Application.Identity.Users.Commands.ToggleUserStatus;
using CheckflixApp.Application.Mailing;
using CheckflixApp.Domain.Common.Primitives;
using CheckflixApp.Domain.Common.Primitives.Result;
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
    private readonly IUnitOfWork _unitOfWork;
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
        IFileStorageService fileStorage,
        IUnitOfWork unitOfWork)
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
        _unitOfWork = unitOfWork;
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

    public async Task<UserFollowingsCountDto?> GetFollowingCountAsync(string userId, CancellationToken cancellationToken)
    {
        var userFollowingsCountDto = await _userManager.Users
            .Include(x => x.Followers)
            .Include(x => x.Following)
            .AsNoTracking()
            .Where(x => x.Id == userId)
            .Select(x => new UserFollowingsCountDto
            {
                FollowerCount = x.Followers.Count(),
                FollowingCount = x.Following.Count()
            })
            .FirstOrDefaultAsync(cancellationToken);

        return userFollowingsCountDto;
    }

    public async Task<UserDetailsDto?> GetAsync(string userId, CancellationToken cancellationToken) =>
        await _userManager.Users
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

    public async Task<IdentityUser?> GetUserByEmailAsync(string email) =>
        await _userManager.FindByEmailAsync(email.Trim().Normalize());

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

    public async Task<Result> ToggleUserStatusAsync(ToggleUserStatusCommand command, CancellationToken cancellationToken)
    {
        var user = await _userManager.Users.Where(u => u.Id == command.Id).FirstOrDefaultAsync(cancellationToken);

        if (user == null)
        {
            return Error.NotFound(description: _localizer["User Not Found."]);
        }

        bool isAdmin = await _userManager.IsInRoleAsync(user, SystemRoles.Administrator);
        if (isAdmin)
        {
            return Error.Validation(description: _localizer["Administrators Profile's Status cannot be toggled"]);
        }

        user.IsActive = command.ActivateUser;

        await _userManager.UpdateAsync(user);

        //await _events.PublishAsync(new ApplicationUserUpdatedEvent(user.Id));

        return Result.From();
    }
}
