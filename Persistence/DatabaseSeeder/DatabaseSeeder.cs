using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
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
            AddCategory();
            AddColor();
            AddSize();
            //AddProduct();
            AddPermisson();
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

                //Seller
                var sellerRole = new Role()
                {
                    Name = RoleConstants.SellerRole,
                    Description = "Seller role with custom permission"
                };
                var sellerRoleInDb = await _roleManager.FindByNameAsync(RoleConstants.SellerRole);
                if (sellerRoleInDb == null)
                {
                    await _roleManager.CreateAsync(sellerRole);
                    _logger.LogInformation("Seeded Seller Role.");
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
                _context.Menus.Add(new Menu() { Name = "Administrator", Url = "/administrator" });
                _context.Menus.Add(new Menu() { Name = "Cart", Url = "/cart" });
                _context.Menus.Add(new Menu() { Name = "Category", Url = "/category" });
                _context.Menus.Add(new Menu() { Name = "Color", Url = "/color" });
                _context.Menus.Add(new Menu() { Name = "Mail", Url = "/mail" });
                _context.Menus.Add(new Menu() { Name = "Order", Url = "/order" });
                _context.Menus.Add(new Menu() { Name = "OrderDetail", Url = "/orderDetail" });
                _context.Menus.Add(new Menu() { Name = "Product", Url = "/product" });
                _context.Menus.Add(new Menu() { Name = "Review", Url = "/review" });
                _context.Menus.Add(new Menu() { Name = "Size", Url = "/size" });
                _context.Menus.Add(new Menu() { Name = "User", Url = "/user" });
                _context.Menus.Add(new Menu() { Name = "Role", Url = "/role" });
                _context.Menus.Add(new Menu() { Name = "Seller", Url = "/seller" });
            }
        }

        private void AddCategory()
        {
            if (!_context.Categories.Any())
            {
                var createdBy = "65d34211-66fc-489f-bf2e-d34c54a2285e";
                _context.Categories.Add(new Category() { Name = "Shirt", CreatedOn = DateTime.Now, CreatedBy = createdBy });
                _context.Categories.Add(new Category() { Name = "Hat", CreatedOn = DateTime.Now, CreatedBy = createdBy });
                _context.Categories.Add(new Category() { Name = "Sandal", CreatedOn = DateTime.Now, CreatedBy = createdBy });
            }
        }

        private void AddColor()
        {
            if (!_context.Colors.Any())
            {
                var createdBy = "65d34211-66fc-489f-bf2e-d34c54a2285e";
                _context.Colors.Add(new Color() { Name = "Trắng", CreatedOn = DateTime.Now, CreatedBy = createdBy });
                _context.Colors.Add(new Color() { Name = "Xanh da trời", CreatedOn = DateTime.Now, CreatedBy = createdBy });
                _context.Colors.Add(new Color() { Name = "Xanh lá cây", CreatedOn = DateTime.Now, CreatedBy = createdBy });
                _context.Colors.Add(new Color() { Name = "Vàng", CreatedOn = DateTime.Now, CreatedBy = createdBy });
                _context.Colors.Add(new Color() { Name = "Hồng", CreatedOn = DateTime.Now, CreatedBy = createdBy });
                _context.Colors.Add(new Color() { Name = "Xám", CreatedOn = DateTime.Now, CreatedBy = createdBy });
                _context.Colors.Add(new Color() { Name = "Đỏ", CreatedOn = DateTime.Now, CreatedBy = createdBy });
                _context.Colors.Add(new Color() { Name = "Đen", CreatedOn = DateTime.Now, CreatedBy = createdBy });
                _context.Colors.Add(new Color() { Name = "Nâu", CreatedOn = DateTime.Now, CreatedBy = createdBy });
                _context.Colors.Add(new Color() { Name = "Ghi", CreatedOn = DateTime.Now, CreatedBy = createdBy });
                _context.Colors.Add(new Color() { Name = "Tím", CreatedOn = DateTime.Now, CreatedBy = createdBy });
            }
        }

        private void AddSize()
        {
            if (!_context.Sizes.Any())
            {
                var createdBy = "65d34211-66fc-489f-bf2e-d34c54a2285e";
                _context.Sizes.Add(new Size() { Name = "S", CreatedOn = DateTime.Now, CreatedBy = createdBy });
                _context.Sizes.Add(new Size() { Name = "M", CreatedOn = DateTime.Now, CreatedBy = createdBy });
                _context.Sizes.Add(new Size() { Name = "L", CreatedOn = DateTime.Now, CreatedBy = createdBy });
                _context.Sizes.Add(new Size() { Name = "XL", CreatedOn = DateTime.Now, CreatedBy = createdBy });
                _context.Sizes.Add(new Size() { Name = "XXL", CreatedOn = DateTime.Now, CreatedBy = createdBy });
            }
        }

        private void AddProduct()
        {
            if (!_context.Products.Any())
            {
                var createdBy = "65d34211-66fc-489f-bf2e-d34c54a2285e";
                _context.Products.Add(new Product() { CategoryId = 1, Name = "Sweater", Description = "", Price = 150000, Quantity = 15, SizeId = 1, ColorId = 1, CreatedOn = DateTime.Now, CreatedBy = createdBy });
            }
        }

        private void AddPermisson()
        {
            if (!_context.Permissons.Any())
            {
                //admin
                var admin = "07eccb98-7897-44fc-9bf3-59ee78e15cad";
                _context.Permissons.Add(new Permission() { RoleId = admin, MenuId = 1, CanAccess = true, CanAdd = true, CanDelete = true, CanUpdate = true });
                _context.Permissons.Add(new Permission() { RoleId = admin, MenuId = 2, CanAccess = true, CanAdd = true, CanDelete = true, CanUpdate = true });
                _context.Permissons.Add(new Permission() { RoleId = admin, MenuId = 3, CanAccess = true, CanAdd = true, CanDelete = true, CanUpdate = true });
                _context.Permissons.Add(new Permission() { RoleId = admin, MenuId = 4, CanAccess = true, CanAdd = true, CanDelete = true, CanUpdate = true });
                _context.Permissons.Add(new Permission() { RoleId = admin, MenuId = 5, CanAccess = true, CanAdd = true, CanDelete = true, CanUpdate = true });
                _context.Permissons.Add(new Permission() { RoleId = admin, MenuId = 6, CanAccess = true, CanAdd = true, CanDelete = true, CanUpdate = true });
                _context.Permissons.Add(new Permission() { RoleId = admin, MenuId = 7, CanAccess = true, CanAdd = true, CanDelete = true, CanUpdate = true });
                _context.Permissons.Add(new Permission() { RoleId = admin, MenuId = 8, CanAccess = true, CanAdd = true, CanDelete = true, CanUpdate = true });
                _context.Permissons.Add(new Permission() { RoleId = admin, MenuId = 9, CanAccess = true, CanAdd = true, CanDelete = true, CanUpdate = true });
                _context.Permissons.Add(new Permission() { RoleId = admin, MenuId = 10, CanAccess = true, CanAdd = true, CanDelete = true, CanUpdate = true });
                _context.Permissons.Add(new Permission() { RoleId = admin, MenuId = 11, CanAccess = true, CanAdd = true, CanDelete = true, CanUpdate = true });
                _context.Permissons.Add(new Permission() { RoleId = admin, MenuId = 12, CanAccess = true, CanAdd = true, CanDelete = true, CanUpdate = true });
                _context.Permissons.Add(new Permission() { RoleId = admin, MenuId = 13, CanAccess = true, CanAdd = true, CanDelete = true, CanUpdate = true });

                //seller
                var seller = "65f4242f-7f4b-4bb6-b1d5-fd87aacc8d7c";
                _context.Permissons.Add(new Permission() { RoleId = seller, MenuId = 1, CanAccess = false, CanAdd = false, CanDelete = false, CanUpdate = false });
                _context.Permissons.Add(new Permission() { RoleId = seller, MenuId = 2, CanAccess = true, CanAdd = true, CanDelete = true, CanUpdate = true });
                _context.Permissons.Add(new Permission() { RoleId = seller, MenuId = 3, CanAccess = true, CanAdd = false, CanDelete = false, CanUpdate = false });
                _context.Permissons.Add(new Permission() { RoleId = seller, MenuId = 4, CanAccess = true, CanAdd = false, CanDelete = false, CanUpdate = false });
                _context.Permissons.Add(new Permission() { RoleId = seller, MenuId = 5, CanAccess = false, CanAdd = false, CanDelete = false, CanUpdate = false });
                _context.Permissons.Add(new Permission() { RoleId = seller, MenuId = 6, CanAccess = true, CanAdd = true, CanDelete = false, CanUpdate = false });
                _context.Permissons.Add(new Permission() { RoleId = seller, MenuId = 7, CanAccess = true, CanAdd = true, CanDelete = false, CanUpdate = false });
                _context.Permissons.Add(new Permission() { RoleId = seller, MenuId = 8, CanAccess = true, CanAdd = false, CanDelete = false, CanUpdate = false });
                _context.Permissons.Add(new Permission() { RoleId = seller, MenuId = 9, CanAccess = true, CanAdd = true, CanDelete = false, CanUpdate = false });
                _context.Permissons.Add(new Permission() { RoleId = seller, MenuId = 10, CanAccess = true, CanAdd = false, CanDelete = false, CanUpdate = false });
                _context.Permissons.Add(new Permission() { RoleId = seller, MenuId = 11, CanAccess = false, CanAdd = false, CanDelete = false, CanUpdate = true });
                _context.Permissons.Add(new Permission() { RoleId = seller, MenuId = 12, CanAccess = false, CanAdd = false, CanDelete = false, CanUpdate = false });
                _context.Permissons.Add(new Permission() { RoleId = seller, MenuId = 13, CanAccess = true, CanAdd = true, CanDelete = false, CanUpdate = true });

                //customer
                var customer = "e033fe92-fdf3-4a55-8efc-913e10aaaa99";
                _context.Permissons.Add(new Permission() { RoleId = customer, MenuId = 1, CanAccess = false, CanAdd = false, CanDelete = false, CanUpdate = false });
                _context.Permissons.Add(new Permission() { RoleId = customer, MenuId = 2, CanAccess = true, CanAdd = true, CanDelete = true, CanUpdate = true });
                _context.Permissons.Add(new Permission() { RoleId = customer, MenuId = 3, CanAccess = true, CanAdd = false, CanDelete = false, CanUpdate = false });
                _context.Permissons.Add(new Permission() { RoleId = customer, MenuId = 4, CanAccess = true, CanAdd = false, CanDelete = false, CanUpdate = false });
                _context.Permissons.Add(new Permission() { RoleId = customer, MenuId = 5, CanAccess = false, CanAdd = false, CanDelete = false, CanUpdate = false });
                _context.Permissons.Add(new Permission() { RoleId = customer, MenuId = 6, CanAccess = true, CanAdd = true, CanDelete = false, CanUpdate = false });
                _context.Permissons.Add(new Permission() { RoleId = customer, MenuId = 7, CanAccess = true, CanAdd = true, CanDelete = false, CanUpdate = false });
                _context.Permissons.Add(new Permission() { RoleId = customer, MenuId = 8, CanAccess = true, CanAdd = false, CanDelete = false, CanUpdate = false });
                _context.Permissons.Add(new Permission() { RoleId = customer, MenuId = 9, CanAccess = true, CanAdd = true, CanDelete = false, CanUpdate = false });
                _context.Permissons.Add(new Permission() { RoleId = customer, MenuId = 10, CanAccess = true, CanAdd = false, CanDelete = false, CanUpdate = false });
                _context.Permissons.Add(new Permission() { RoleId = customer, MenuId = 11, CanAccess = true, CanAdd = false, CanDelete = false, CanUpdate = true });
                _context.Permissons.Add(new Permission() { RoleId = customer, MenuId = 12, CanAccess = false, CanAdd = false, CanDelete = false, CanUpdate = false });
                _context.Permissons.Add(new Permission() { RoleId = customer, MenuId = 13, CanAccess = false, CanAdd = false, CanDelete = false, CanUpdate = false });
            }
        }
    }
}