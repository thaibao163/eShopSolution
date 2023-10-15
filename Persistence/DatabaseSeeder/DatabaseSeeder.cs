using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using OfficeOpenXml.FormulaParsing.Excel.Functions.RefAndLookup;
using Persistence.Constants;
using Persistence.Contexts;
using Persistence.Interfaces;

namespace Persistence.DatabaseSeeder
{
    public class DatabaseSeeder : IDatabaseSeeder
    {
        private readonly ILogger<DatabaseSeeder> _logger;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;

        public DatabaseSeeder(ILogger<DatabaseSeeder> logger, ApplicationDbContext context, UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            _logger = logger;
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public void Initialize()
        {
            AddAdministrator();
            AddMenu();
            AddPermisson();
            AddCategory();
            AddProduct();
            AddColor();
            AddCapacityProduct();
            _context.SaveChanges();
        }

        private void AddAdministrator()
        {
            Task.Run(async () =>
            {
                //Admin
                var adminRole = new Role()
                {
                    Name = RoleConstants.AdministratorRole,
                    Description = "Administrator role with full permission"
                };
                var adminRoleInDb = await _roleManager.FindByNameAsync(RoleConstants.AdministratorRole);
                if (adminRoleInDb == null)
                {
                    await _roleManager.CreateAsync(adminRole);
                    _logger.LogInformation("Seeded Administrator Role.");
                }

                //Customer
                var customerRole = new Role()
                {
                    Name = RoleConstants.CustomerRole,
                    Description = "Customer role with custom permission"
                };
                var employeeRoleInDb = await _roleManager.FindByNameAsync(RoleConstants.CustomerRole);
                if (employeeRoleInDb == null)
                {
                    await _roleManager.CreateAsync(customerRole);
                    _logger.LogInformation("Seeded Customer Role.");
                }

                //Visiting Guests
                var visiting_guestsRole = new Role()
                {
                    Name = RoleConstants.Visiting_GuestsRole,
                    Description = "Visiting guests role with custom permission"
                };
                var visiting_guestsRoleInDb = await _roleManager.FindByNameAsync(RoleConstants.Visiting_GuestsRole);
                if (visiting_guestsRoleInDb == null)
                {
                    await _roleManager.CreateAsync(visiting_guestsRole);
                    _logger.LogInformation("Seeded Visiting guests Role.");
                }

                //Check if User Exists
                var hasher = new PasswordHasher<User>();
                var superUser = new User()
                {
                    FullName = "Superadmin",
                    UserName = "superadmin",
                    Email = "superadmin@gmail.com",
                    NormalizedEmail = "superadmin@gmail.com",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true,
                    CreatedOn = DateTime.Now,
                };
                var superUserInDb = await _userManager.FindByNameAsync(superUser.UserName);
                if (superUserInDb == null)
                {
                    await _userManager.CreateAsync(superUser, UserConstants.DefaultPassword);
                    var result = await _userManager.AddToRoleAsync(superUser, RoleConstants.AdministratorRole);
                    if (result.Succeeded)
                    {
                        _logger.LogInformation("Seeded Default SuperAdmin User.");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            _logger.LogError(error.Description);
                        }
                    }
                }
            }).GetAwaiter().GetResult();
        }

        private void AddMenu()
        {
            if (!_context.Menus.Any())
            {
                _context.Menus.Add(new Menu() { Name = "Product", Url = "/product" });
                _context.Menus.Add(new Menu() { Name = "Category", Url = "/category" });
                _context.Menus.Add(new Menu() { Name = "User", Url = "/user" });
                _context.Menus.Add(new Menu() { Name = "Order", Url = "/order" });
                _context.Menus.Add(new Menu() { Name = "Role", Url = "/role" });
                _context.Menus.Add(new Menu() { Name = "Visiting Guests", Url = "/visiting_guests" });
                _context.Menus.Add(new Menu() { Name = "Cart", Url = "/cart" });
            }
        }

        private void AddCategory()
        {
            if (!_context.Categories.Any())
            {
                _context.Categories.Add(new Category() { Name = "Samsung", CreatedOn = DateTime.Now, CreatedBy = "38ac54ad-d3c7-488c-a2aa-77bb501c919a" });
                _context.Categories.Add(new Category() { Name = "IPhone", CreatedOn = DateTime.Now, CreatedBy = "38ac54ad-d3c7-488c-a2aa-77bb501c919a" });
                _context.Categories.Add(new Category() { Name = "Xiaomi", CreatedOn = DateTime.Now, CreatedBy = "38ac54ad-d3c7-488c-a2aa-77bb501c919a" });
                _context.Categories.Add(new Category() { Name = "Oppo", CreatedOn = DateTime.Now, CreatedBy = "38ac54ad-d3c7-488c-a2aa-77bb501c919a" });
                _context.Categories.Add(new Category() { Name = "Vivo", CreatedOn = DateTime.Now, CreatedBy = "38ac54ad-d3c7-488c-a2aa-77bb501c919a" });
                _context.Categories.Add(new Category() { Name = "Nokia", CreatedOn = DateTime.Now, CreatedBy = "38ac54ad-d3c7-488c-a2aa-77bb501c919a" });
            }
        }

        private void AddColor()
        {
            if (!_context.Colors.Any())
            {
                _context.Colors.Add(new Color() { Name = "Trắng", CreatedOn = DateTime.Now, CreatedBy = "38ac54ad-d3c7-488c-a2aa-77bb501c919a" });
                _context.Colors.Add(new Color() { Name = "Vàng", CreatedOn = DateTime.Now, CreatedBy = "38ac54ad-d3c7-488c-a2aa-77bb501c919a" });
                _context.Colors.Add(new Color() { Name = "Hồng", CreatedOn = DateTime.Now, CreatedBy = "38ac54ad-d3c7-488c-a2aa-77bb501c919a" });
                _context.Colors.Add(new Color() { Name = "Xám", CreatedOn = DateTime.Now, CreatedBy = "38ac54ad-d3c7-488c-a2aa-77bb501c919a" });
                _context.Colors.Add(new Color() { Name = "Đỏ", CreatedOn = DateTime.Now, CreatedBy = "38ac54ad-d3c7-488c-a2aa-77bb501c919a" });
                _context.Colors.Add(new Color() { Name = "Đen", CreatedOn = DateTime.Now, CreatedBy = "38ac54ad-d3c7-488c-a2aa-77bb501c919a" });
                _context.Colors.Add(new Color() { Name = "Nâu", CreatedOn = DateTime.Now, CreatedBy = "38ac54ad-d3c7-488c-a2aa-77bb501c919a" });
                _context.Colors.Add(new Color() { Name = "Tím", CreatedOn = DateTime.Now, CreatedBy = "38ac54ad-d3c7-488c-a2aa-77bb501c919a" });
            }
        }

        private void AddCapacityProduct()
        {
            if (!_context.Capacities.Any())
            {
                _context.Capacities.Add(new Capacity() { Name = "16GB", CreatedOn = DateTime.Now, CreatedBy = "38ac54ad-d3c7-488c-a2aa-77bb501c919a" });
                _context.Capacities.Add(new Capacity() { Name = "32GB", CreatedOn = DateTime.Now, CreatedBy = "38ac54ad-d3c7-488c-a2aa-77bb501c919a" });
                _context.Capacities.Add(new Capacity() { Name = "64GB", CreatedOn = DateTime.Now, CreatedBy = "38ac54ad-d3c7-488c-a2aa-77bb501c919a" });
                _context.Capacities.Add(new Capacity() { Name = "128GB", CreatedOn = DateTime.Now, CreatedBy = "38ac54ad-d3c7-488c-a2aa-77bb501c919a" });
                _context.Capacities.Add(new Capacity() { Name = "256GB", CreatedOn = DateTime.Now, CreatedBy = "38ac54ad-d3c7-488c-a2aa-77bb501c919a" });
                _context.Capacities.Add(new Capacity() { Name = "512GB", CreatedOn = DateTime.Now, CreatedBy = "38ac54ad-d3c7-488c-a2aa-77bb501c919a" });
                _context.Capacities.Add(new Capacity() { Name = "1T", CreatedOn = DateTime.Now, CreatedBy = "38ac54ad-d3c7-488c-a2aa-77bb501c919a" });
            }
        }

        private void AddProduct()
        {
            if (!_context.Products.Any())
            {
                _context.Products.Add(new Product() { CategoryId = 1, Name = "Samsung", Description = "", Price = 150000, Quantity = 15, CapacityID = 1, ColorID = 1, CreatedOn = DateTime.Now, CreatedBy = "38ac54ad-d3c7-488c-a2aa-77bb501c919a" });
                _context.Products.Add(new Product() { CategoryId = 1, Name = "IPhone", Description = "", Price = 160000, Quantity = 16, CapacityID = 2, ColorID = 2, CreatedOn = DateTime.Now, CreatedBy = "38ac54ad-d3c7-488c-a2aa-77bb501c919a" });
                _context.Products.Add(new Product() { CategoryId = 1, Name = "Realme", Description = "", Price = 170000, Quantity = 17, CapacityID = 3, ColorID = 3, CreatedOn = DateTime.Now, CreatedBy = "38ac54ad-d3c7-488c-a2aa-77bb501c919a" });
                _context.Products.Add(new Product() { CategoryId = 1, Name = "Xiaomi", Description = "", Price = 180000, Quantity = 18, CapacityID = 4, ColorID = 4, CreatedOn = DateTime.Now, CreatedBy = "38ac54ad-d3c7-488c-a2aa-77bb501c919a" });
                _context.Products.Add(new Product() { CategoryId = 1, Name = "Oppo", Description = "", Price = 190000, Quantity = 19, CapacityID = 5, ColorID = 5, CreatedOn = DateTime.Now, CreatedBy = "38ac54ad-d3c7-488c-a2aa-77bb501c919a" });
                _context.Products.Add(new Product() { CategoryId = 1, Name = "Vivo", Description = "", Price = 200000, Quantity = 20, CapacityID = 6, ColorID = 6, CreatedOn = DateTime.Now, CreatedBy = "38ac54ad-d3c7-488c-a2aa-77bb501c919a" });
                _context.Products.Add(new Product() { CategoryId = 1, Name = "Nokia", Description = "", Price = 220000, Quantity = 22, CapacityID = 7, ColorID = 7, CreatedOn = DateTime.Now, CreatedBy = "38ac54ad-d3c7-488c-a2aa-77bb501c919a" });
            }
        }

        private void AddPermisson()
        {
            if (!_context.Permissons.Any())
            {
                //role
                _context.Permissons.Add(new Permission() { RoleId = "38ac54ad-d3c7-488c-a2aa-77bb501c919a", MenuId = 1, CanAccess = true, CanAdd = true, CanDelete = true, CanUpdate = true });
                _context.Permissons.Add(new Permission() { RoleId = "38ac54ad-d3c7-488c-a2aa-77bb501c919a", MenuId = 2, CanAccess = true, CanAdd = true, CanDelete = true, CanUpdate = true });
                _context.Permissons.Add(new Permission() { RoleId = "38ac54ad-d3c7-488c-a2aa-77bb501c919a", MenuId = 3, CanAccess = true, CanAdd = true, CanDelete = true, CanUpdate = true });
                _context.Permissons.Add(new Permission() { RoleId = "38ac54ad-d3c7-488c-a2aa-77bb501c919a", MenuId = 4, CanAccess = true, CanAdd = true, CanDelete = true, CanUpdate = true });
                _context.Permissons.Add(new Permission() { RoleId = "38ac54ad-d3c7-488c-a2aa-77bb501c919a", MenuId = 5, CanAccess = true, CanAdd = true, CanDelete = true, CanUpdate = true });
                _context.Permissons.Add(new Permission() { RoleId = "38ac54ad-d3c7-488c-a2aa-77bb501c919a", MenuId = 6, CanAccess = true, CanAdd = true, CanDelete = true, CanUpdate = true });
                _context.Permissons.Add(new Permission() { RoleId = "38ac54ad-d3c7-488c-a2aa-77bb501c919a", MenuId = 7, CanAccess = true, CanAdd = true, CanDelete = true, CanUpdate = true });

                //customer
                _context.Permissons.Add(new Permission() { RoleId = "e95944d2-b18d-40a6-b0ea-3a66b181adbe", MenuId = 1, CanAccess = true, CanAdd = false, CanDelete = false, CanUpdate = false });
                _context.Permissons.Add(new Permission() { RoleId = "e95944d2-b18d-40a6-b0ea-3a66b181adbe", MenuId = 2, CanAccess = true, CanAdd = false, CanDelete = false, CanUpdate = false });
                _context.Permissons.Add(new Permission() { RoleId = "e95944d2-b18d-40a6-b0ea-3a66b181adbe", MenuId = 3, CanAccess = true, CanAdd = false, CanDelete = false, CanUpdate = true });
                _context.Permissons.Add(new Permission() { RoleId = "e95944d2-b18d-40a6-b0ea-3a66b181adbe", MenuId = 4, CanAccess = false, CanAdd = true, CanDelete = true, CanUpdate = true });
                _context.Permissons.Add(new Permission() { RoleId = "e95944d2-b18d-40a6-b0ea-3a66b181adbe", MenuId = 5, CanAccess = false, CanAdd = false, CanDelete = false, CanUpdate = false });
                _context.Permissons.Add(new Permission() { RoleId = "e95944d2-b18d-40a6-b0ea-3a66b181adbe", MenuId = 6, CanAccess = true, CanAdd = true, CanDelete = true, CanUpdate = true }); 
                _context.Permissons.Add(new Permission() { RoleId = "e95944d2-b18d-40a6-b0ea-3a66b181adbe", MenuId = 7, CanAccess = true, CanAdd = true, CanDelete = true, CanUpdate = true });

                //visiting guests
                _context.Permissons.Add(new Permission() { RoleId = "1919e06e-d4c4-4c8e-b250-32eefb569502", MenuId = 1, CanAccess = true, CanAdd = false, CanDelete = false, CanUpdate = false });
                _context.Permissons.Add(new Permission() { RoleId = "1919e06e-d4c4-4c8e-b250-32eefb569502", MenuId = 2, CanAccess = true, CanAdd = false, CanDelete = false, CanUpdate = false });
                _context.Permissons.Add(new Permission() { RoleId = "1919e06e-d4c4-4c8e-b250-32eefb569502", MenuId = 3, CanAccess = false, CanAdd = false, CanDelete = false, CanUpdate = false });
                _context.Permissons.Add(new Permission() { RoleId = "1919e06e-d4c4-4c8e-b250-32eefb569502", MenuId = 4, CanAccess = false, CanAdd = false, CanDelete = false, CanUpdate = false });
                _context.Permissons.Add(new Permission() { RoleId = "1919e06e-d4c4-4c8e-b250-32eefb569502", MenuId = 5, CanAccess = false, CanAdd = false, CanDelete = false, CanUpdate = false });
                _context.Permissons.Add(new Permission() { RoleId = "1919e06e-d4c4-4c8e-b250-32eefb569502", MenuId = 6, CanAccess = false, CanAdd = false, CanDelete = false, CanUpdate = false }); 
                _context.Permissons.Add(new Permission() { RoleId = "1919e06e-d4c4-4c8e-b250-32eefb569502", MenuId = 7, CanAccess = false, CanAdd = false, CanDelete = false, CanUpdate = false });
            }
        } 
    }
}