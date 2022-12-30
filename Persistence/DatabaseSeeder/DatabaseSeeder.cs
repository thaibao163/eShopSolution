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
            AddSizeProduct();
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
                _context.Menus.Add(new Menu() { Name = "Product", Url = "/product" });
                _context.Menus.Add(new Menu() { Name = "Category", Url = "/category" });
                _context.Menus.Add(new Menu() { Name = "User", Url = "/user" });
                _context.Menus.Add(new Menu() { Name = "Order", Url = "/order" });
                _context.Menus.Add(new Menu() { Name = "Role", Url = "/role" });
                _context.Menus.Add(new Menu() { Name = "Seller", Url = "/seller" });
                _context.Menus.Add(new Menu() { Name = "Cart", Url = "/cart" });
            }
        }

        private void AddCategory()
        {
            if (!_context.Categories.Any())
            {
                _context.Categories.Add(new Category() { Name = "Shirt", CreatedOn = DateTime.Now, CreatedBy = "65d34211-66fc-489f-bf2e-d34c54a2285e" });
                _context.Categories.Add(new Category() { Name = "Hat", CreatedOn = DateTime.Now, CreatedBy = "65d34211-66fc-489f-bf2e-d34c54a2285e" });
                _context.Categories.Add(new Category() { Name = "Sandal", CreatedOn = DateTime.Now, CreatedBy = "65d34211-66fc-489f-bf2e-d34c54a2285e" });
            }
        }

        private void AddColor()
        {
            if (!_context.Colors.Any())
            {
                _context.Colors.Add(new Color() { Name = "White", CreatedOn = DateTime.Now, CreatedBy = "65d34211-66fc-489f-bf2e-d34c54a2285e" });
                _context.Colors.Add(new Color() { Name = "Blue", CreatedOn = DateTime.Now, CreatedBy = "65d34211-66fc-489f-bf2e-d34c54a2285e" });
                _context.Colors.Add(new Color() { Name = "Green", CreatedOn = DateTime.Now, CreatedBy = "65d34211-66fc-489f-bf2e-d34c54a2285e" });
                _context.Colors.Add(new Color() { Name = "Yellow", CreatedOn = DateTime.Now, CreatedBy = "65d34211-66fc-489f-bf2e-d34c54a2285e" });
                _context.Colors.Add(new Color() { Name = "Pink", CreatedOn = DateTime.Now, CreatedBy = "65d34211-66fc-489f-bf2e-d34c54a2285e" });
                _context.Colors.Add(new Color() { Name = "Gray", CreatedOn = DateTime.Now, CreatedBy = "65d34211-66fc-489f-bf2e-d34c54a2285e" });
                _context.Colors.Add(new Color() { Name = "Red", CreatedOn = DateTime.Now, CreatedBy = "65d34211-66fc-489f-bf2e-d34c54a2285e" });
                _context.Colors.Add(new Color() { Name = "Black", CreatedOn = DateTime.Now, CreatedBy = "65d34211-66fc-489f-bf2e-d34c54a2285e" });
                _context.Colors.Add(new Color() { Name = "Brown", CreatedOn = DateTime.Now, CreatedBy = "65d34211-66fc-489f-bf2e-d34c54a2285e" });
            }
        }

        private void AddSizeProduct()
        {
            if (!_context.Sizes.Any())
            {
                _context.Sizes.Add(new SizeProduct() { Name = "S", CreatedOn = DateTime.Now, CreatedBy = "65d34211-66fc-489f-bf2e-d34c54a2285e" });
                _context.Sizes.Add(new SizeProduct() { Name = "M", CreatedOn = DateTime.Now, CreatedBy = "65d34211-66fc-489f-bf2e-d34c54a2285e" });
                _context.Sizes.Add(new SizeProduct() { Name = "L", CreatedOn = DateTime.Now, CreatedBy = "65d34211-66fc-489f-bf2e-d34c54a2285e" });
                _context.Sizes.Add(new SizeProduct() { Name = "XL", CreatedOn = DateTime.Now, CreatedBy = "65d34211-66fc-489f-bf2e-d34c54a2285e" });
                _context.Sizes.Add(new SizeProduct() { Name = "XXL", CreatedOn = DateTime.Now, CreatedBy = "65d34211-66fc-489f-bf2e-d34c54a2285e" });
            }
        }

        private void AddProduct()
        {
            if (!_context.Products.Any())
            {
                _context.Products.Add(new Product() { CategoryId = 1, Name = "Sweater", Description = "", Price = 150000, Quantity = 15, SizeID = 1, ColorID = 1, CreatedOn = DateTime.Now, CreatedBy = "65d34211-66fc-489f-bf2e-d34c54a2285e" });
                _context.Products.Add(new Product() { CategoryId = 2, Name = "Cap", Description = "", Price = 75000, Quantity = 10, SizeID = 2, ColorID = 2, CreatedOn = DateTime.Now, CreatedBy = "65d34211-66fc-489f-bf2e-d34c54a2285e" });
            }
        }

        private void AddPermisson()
        {
            if (!_context.Permissons.Any())
            {
                //role
                _context.Permissons.Add(new Permission() { RoleId = "1a648e07-91a0-476e-9fe2-f398009fe175", MenuId = 1, CanAccess = true, CanAdd = true, CanDelete = true, CanUpdate = true });
                _context.Permissons.Add(new Permission() { RoleId = "1a648e07-91a0-476e-9fe2-f398009fe175", MenuId = 2, CanAccess = true, CanAdd = true, CanDelete = true, CanUpdate = true });
                _context.Permissons.Add(new Permission() { RoleId = "1a648e07-91a0-476e-9fe2-f398009fe175", MenuId = 3, CanAccess = true, CanAdd = true, CanDelete = true, CanUpdate = true });
                _context.Permissons.Add(new Permission() { RoleId = "1a648e07-91a0-476e-9fe2-f398009fe175", MenuId = 4, CanAccess = true, CanAdd = true, CanDelete = true, CanUpdate = true });
                _context.Permissons.Add(new Permission() { RoleId = "1a648e07-91a0-476e-9fe2-f398009fe175", MenuId = 5, CanAccess = true, CanAdd = true, CanDelete = true, CanUpdate = true });
                _context.Permissons.Add(new Permission() { RoleId = "1a648e07-91a0-476e-9fe2-f398009fe175", MenuId = 6, CanAccess = true, CanAdd = true, CanDelete = true, CanUpdate = true });
                _context.Permissons.Add(new Permission() { RoleId = "1a648e07-91a0-476e-9fe2-f398009fe175", MenuId = 7, CanAccess = true, CanAdd = true, CanDelete = true, CanUpdate = true });

                //customer
                _context.Permissons.Add(new Permission() { RoleId = "01fa6a70-60d3-4445-8131-247224a4b3dd", MenuId = 1, CanAccess = true, CanAdd = false, CanDelete = false, CanUpdate = false });
                _context.Permissons.Add(new Permission() { RoleId = "01fa6a70-60d3-4445-8131-247224a4b3dd", MenuId = 2, CanAccess = true, CanAdd = false, CanDelete = false, CanUpdate = false });
                _context.Permissons.Add(new Permission() { RoleId = "01fa6a70-60d3-4445-8131-247224a4b3dd", MenuId = 3, CanAccess = false, CanAdd = false, CanDelete = false, CanUpdate = false });
                _context.Permissons.Add(new Permission() { RoleId = "01fa6a70-60d3-4445-8131-247224a4b3dd", MenuId = 4, CanAccess = false, CanAdd = true, CanDelete = true, CanUpdate = true });
                _context.Permissons.Add(new Permission() { RoleId = "01fa6a70-60d3-4445-8131-247224a4b3dd", MenuId = 5, CanAccess = false, CanAdd = false, CanDelete = false, CanUpdate = false });
                _context.Permissons.Add(new Permission() { RoleId = "01fa6a70-60d3-4445-8131-247224a4b3dd", MenuId = 6, CanAccess = true, CanAdd = true, CanDelete = true, CanUpdate = true }); 
                _context.Permissons.Add(new Permission() { RoleId = "01fa6a70-60d3-4445-8131-247224a4b3dd", MenuId = 7, CanAccess = true, CanAdd = true, CanDelete = true, CanUpdate = true });


                //seller
                _context.Permissons.Add(new Permission() { RoleId = "21c3773c-0959-44e7-bf46-92aef9d4db6f", MenuId = 1, CanAccess = true, CanAdd = true, CanDelete = true, CanUpdate = true });
                _context.Permissons.Add(new Permission() { RoleId = "21c3773c-0959-44e7-bf46-92aef9d4db6f", MenuId = 2, CanAccess = true, CanAdd = false, CanDelete = false, CanUpdate = false });
                _context.Permissons.Add(new Permission() { RoleId = "21c3773c-0959-44e7-bf46-92aef9d4db6f", MenuId = 3, CanAccess = false, CanAdd = false, CanDelete = false, CanUpdate = false });
                _context.Permissons.Add(new Permission() { RoleId = "21c3773c-0959-44e7-bf46-92aef9d4db6f", MenuId = 4, CanAccess = true, CanAdd = true, CanDelete = true, CanUpdate = true });
                _context.Permissons.Add(new Permission() { RoleId = "21c3773c-0959-44e7-bf46-92aef9d4db6f", MenuId = 5, CanAccess = false, CanAdd = false, CanDelete = false, CanUpdate = false });
                _context.Permissons.Add(new Permission() { RoleId = "21c3773c-0959-44e7-bf46-92aef9d4db6f", MenuId = 6, CanAccess = true, CanAdd = true, CanDelete = true, CanUpdate = true });
                _context.Permissons.Add(new Permission() { RoleId = "21c3773c-0959-44e7-bf46-92aef9d4db6f", MenuId = 7, CanAccess = true, CanAdd = true, CanDelete = true, CanUpdate = true });
            }
        } 
    }
}