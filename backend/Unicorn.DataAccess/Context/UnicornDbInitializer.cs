using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unicorn.DataAccess.Entities;

namespace Unicorn.DataAccess.Context
{
    public class UnicornDbInitializer : DropCreateDatabaseAlways<AppContext>
    {
        protected override void Seed(AppContext context)
        {

            Account account1 = new Account()
            {
                Id = 1,
                Email = "cleanok@gmail.com",
                EmailConfirmed = true,
                DateCreated = new DateTime(2017, 08, 10, 22, 38, 15),
                Avatar = "cleanok.jpg",
                Rating = 5,
                IsDeleted = false,
            };

            Account account2 = new Account()
            {
                Id = 2,
                Email = "andriy@gmail.com",
                EmailConfirmed = true,
                DateCreated = new DateTime(2017, 08, 11, 00, 55, 16),
                Avatar = "andriy.jpg",
                Rating = 5,
                IsDeleted = false,
            };

            Account account3 = new Account()
            {
                Id = 3,
                Email = "shnurenko@gmail.com",
                EmailConfirmed = true,
                DateCreated = new DateTime(2017, 08, 11, 00, 57, 16),
                Avatar = "shnurenko.jpg",
                Rating = 2,
                IsDeleted = false,

            };

            Account account4 = new Account()
            {
                Id = 4,
                Email = "catcare@gmail.com",
                EmailConfirmed = true,
                DateCreated = new DateTime(2017, 08, 11, 00, 58, 16),
                Avatar = "catcare.jpg",
                Rating = 4,
                IsDeleted = false,
            };

            Account account5 = new Account()
            {
                Id = 5,
                Email = "abkprostir@gmail.com",
                EmailConfirmed = true,
                DateCreated = new DateTime(2017, 08, 11, 01, 00, 16),
                Avatar = "abkprostir.jpg",
                Rating = 1,
                IsDeleted = false,
            };

            SocialAccount socialAccount1 = new SocialAccount()
            {
                Id = 1,
                IsDeleted = false,
                Uid = 1,
                Provider = "Microsoft",
                Account = account1
            };

            SocialAccount socialAccount2 = new SocialAccount()
            {
                Id = 2,
                IsDeleted = false,
                Uid = 2,
                Provider = "Facebook",
                Account = account2
            };

            SocialAccount socialAccount3 = new SocialAccount()
            {
                Id = 3,
                IsDeleted = false,
                Uid = 2,
                Provider = "Google",
                Account = account2
            };

            SocialAccount socialAccount4 = new SocialAccount()
            {
                Id = 4,
                IsDeleted = false,
                Uid = 3,
                Provider = "Google",
                Account = account4
            };

            SocialAccount socialAccount5 = new SocialAccount()
            {
                Id = 5,
                IsDeleted = false,
                Uid = 4,
                Provider = "Google",
                Account = account5
            };

            SocialAccount socialAccount6 = new SocialAccount()
            {
                Id = 6,
                IsDeleted = false,
                Uid = 5,
                Provider = "Google"
            };

            context.SocialAccounts.Add(socialAccount1);
            context.SocialAccounts.Add(socialAccount2);
            context.SocialAccounts.Add(socialAccount3);
            context.SocialAccounts.Add(socialAccount4);
            context.SocialAccounts.Add(socialAccount5);
            context.SocialAccounts.Add(socialAccount6);

      

            //Permission permision1 = new Permission()
            //{
            //    Id = 1,
            //    Name = "Comment",
            //    IsDeleted = false,
            //    Accounts = { account1, account2, account3, account4, account5 }

            //};

            //Permission permision2 = new Permission()
            //{
            //    Id = 2,
            //    Name = "Edit",
            //    IsDeleted = false,
            //    Accounts = { account1, account2, account3 }
            //};

            //Permission permision3 = new Permission()
            //{
            //    Id = 3,
            //    Name = "Add",
            //    IsDeleted = false,
            //    Accounts = { account1, account4, account5 }
            //};

            //Role role1 = new Role()
            //{
            //    Id = 1,
            //    Accounts = { account2 },
            //    IsDeleted = false,
            //    Name = "vendor",
            //};

            //Role role2 = new Role()
            //{
            //    Id = 2,
            //    Accounts = { account2, account4 },
            //    IsDeleted = false,
            //    Name = "worker"
            //};

            //Role role3 = new Role()
            //{
            //    Id = 3,
            //    Accounts = { account1, account5 },
            //    IsDeleted = false,
            //    Name = "company",
            //};

           

            //Company company1 = new Company()
            //{
            //    Id = 1,
            //    Account = account1,
            //    FoundationDate = new DateTime(2015, 08, 11, 00, 58, 16),
            //    Staff = 7,
            //    IsDeleted = false,
            //};

            //Company company2 = new Company()
            //{
            //    Id = 2,
            //    Account = account5,
            //    FoundationDate = new DateTime(2012, 08, 11, 00, 58, 16),
            //    Staff = 9,
            //    IsDeleted = false,
            //};


            Location location = new Location()
            {
                Id = 1,
                Adress = "smth",
                City = "Kiev",
                CoordinateX =1,
                CoordinateY =2,
                IsDeleted = false,
                PostIndex = "sadasd"
            };

    

            context.Accounts.Add(account1);
            context.Accounts.Add(account2);
            context.Accounts.Add(account3);
            context.Accounts.Add(account4);
            context.Accounts.Add(account5);




            //context.Permissions.Add(permision1);
            //context.Permissions.Add(permision2);
            //context.Permissions.Add(permision3);

            //context.Locations.Add(location);
            //context.Accounts.Add(account1);
            //context.Accounts.Add(account2);
            //context.Accounts.Add(account3);
            //context.Accounts.Add(account4);
            //context.Accounts.Add(account5);

            //context.Roles.Add(role1);
            //context.Roles.Add(role2);
            //context.Roles.Add(role3);




            //context.Companies.Add(company1);
            //context.Companies.Add(company2);



            context.SaveChanges();
            base.Seed(context);
        }
        
    }
}
