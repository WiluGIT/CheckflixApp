using CheckflixApp.Domain.Entities;
using CheckflixApp.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CheckflixApp.Infrastructure.Persistence;
public class ApplicationDbContextInitialiser
{
    private readonly ILogger<ApplicationDbContextInitialiser> _logger;
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public ApplicationDbContextInitialiser(ILogger<ApplicationDbContextInitialiser> logger, ApplicationDbContext context, UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _logger = logger;
        _context = context;
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task InitialiseAsync()
    {
        try
        {
            if (_context.Database.IsSqlServer())
            {
                await _context.Database.MigrateAsync();
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while initialising the database.");
            throw;
        }
    }

    public async Task SeedAsync()
    {
        try
        {
            await TrySeedAsync();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while seeding the database.");
            throw;
        }
    }

    public async Task TrySeedAsync()
    {
        // Default roles
        var administratorRole = new IdentityRole(SystemRoles.Administrator);
        var basicRole = new IdentityRole(SystemRoles.Basic);

        if (_roleManager.Roles.All(r => r.Name != administratorRole.Name))
        {
            await _roleManager.CreateAsync(administratorRole);
        }

        if (_roleManager.Roles.All(r => r.Name != basicRole.Name))
        {
            await _roleManager.CreateAsync(basicRole);
        }

        // Default users
        var administrator = ApplicationUser.Create(
            username: "administrator@localhost", 
            email: "administrator@localhost", 
            isActive: true,
            emailConfirmed: true);

        var basic = ApplicationUser.Create(
            username: "basic@localhost",
            email: "basic@localhost",
            isActive: true,
            emailConfirmed: true);

        if (_userManager.Users.All(u => u.UserName != administrator.UserName))
        {
            await _userManager.CreateAsync(administrator, "Administrator1!");
            await _userManager.AddToRolesAsync(administrator, new[] { administratorRole.Name });
        }

        if (_userManager.Users.All(u => u.UserName != basic.UserName))
        {
            await _userManager.CreateAsync(basic, "Basic1!");
            await _userManager.AddToRolesAsync(basic, new[] { basicRole.Name });
        }

        // Default data
        // Seed, if necessary
        if (!_context.Set<Genre>().Any())
        {
            var defaultGenres = new List<Genre>()
            {
                Genre.Create("Action"),
                Genre.Create("Adventure"),
                Genre.Create("Animation"),
                Genre.Create("Comedy"),
                Genre.Create("Crime"),
                Genre.Create("Documentary"),
                Genre.Create("Drama"),
                Genre.Create("Family"),
                Genre.Create("Fantasy"),
                Genre.Create("Fiction"),
                Genre.Create("Foreign"),
                Genre.Create("History"),
                Genre.Create("Horror"),
                Genre.Create("Movie"),
                Genre.Create("Music"),
                Genre.Create("Mystery"),
                Genre.Create("Romance"),
                Genre.Create("Science"),
                Genre.Create("TV"),
                Genre.Create("Thriller"),
                Genre.Create("War"),
                Genre.Create("Western"),
            };

            _context.Set<Genre>().AddRange(defaultGenres);

            await _context.SaveChangesAsync();
        }
    }
}
