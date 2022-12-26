//using Domain.Entities;
//using Microsoft.AspNetCore.Identity;
//using Microsoft.EntityFrameworkCore;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Persistence.DatabaseSeeder
//{
//    public static class ModelBuilderExtensions
//    {
//        public static void DatabaseSeeder(this ModelBuilder modelBuilder)
//        {

//            var hasher = new PasswordHasher<User>();
//            modelBuilder.Entity<User>().HasData(new User
//            {
//                FullName = "Nguyen Thai Bao",
//                UserName = "superadmin",
//                Email = "baonguyenthai2@gmail.com",
//                NormalizedEmail = "baonguyenthai2@gmail.com",
//                EmailConfirmed = true,
//                PasswordHash = hasher.HashPassword(null, "1234B@o"),
//                PhoneNumberConfirmed = true,
//                CreatedOn = DateTime.Now,
//                Dob = new DateTime(2001, 03, 16)
//            });
//        }
//    }
//}
