using System;
using System.Collections.Generic;
using System.Data.Entity;
using Unicorn.DataAccess.Entities;

namespace Unicorn.DataAccess.Context
{
    public class UnicornDbInitializer : DropCreateDatabaseIfModelChanges<AppContext>
    {
        protected override void Seed(AppContext context)
        {

            Role role1 = new Role()
            {
                Id = 1,
                IsDeleted = false,
                Name = "Vendor"
            };

            Role role2 = new Role()
            {
                Id = 2,
                IsDeleted = false,
                Name = "Simple User"
            };

            Role role3 = new Role()
            {
                Id = 3,
                IsDeleted = false,
                Name = "Vendor Company"
            };

            Role role4 = new Role()
            {
                Id =4,
                IsDeleted = false,
                Name = "Admin"
            };

            Role role5 = new Role()
            {
                Id = 5,
                IsDeleted = false,
                Name = "Unregistered person"
            };

            context.Roles.Add(role1);
            context.SaveChanges();
            context.Roles.Add(role2);
            context.SaveChanges();
            context.Roles.Add(role3);
            context.SaveChanges();
            context.Roles.Add(role4);
            context.SaveChanges();
            context.Roles.Add(role5);
            context.SaveChanges();

            Account account1 = new Account()
            {
                Id = 1,
                Email = "cleanok_company@gmail.com",
                EmailConfirmed = true,
                DateCreated = new DateTime(2017, 08, 10, 22, 38, 15),
                Avatar = "../../../assets/images/company_logo.png",
                Rating = 4.7,
                IsDeleted = false,
                Role = new Role() { Id = role3.Id, Name = role3.Name, IsDeleted = false }
            };

            Account account2 = new Account()
            {
                Id = 2,
                Email = "andriy_vendor@gmail.com",
                EmailConfirmed = true,
                DateCreated = new DateTime(2017, 08, 11, 00, 55, 16),
                Avatar = "andriy.jpg",
                Rating = 5,
                IsDeleted = false,
                Role = new Role() { Id = role1.Id, Name = role1.Name, IsDeleted = false }
            };

            Account account3 = new Account()
            {
                Id = 3,
                Email = "shnurenko_worker@gmail.com",
                EmailConfirmed = true,
                DateCreated = new DateTime(2017, 08, 11, 00, 57, 16),
                Avatar = "shnurenko.jpg",
                Rating = 2,
                IsDeleted = false,
                Role = new Role() { Id = role2.Id, Name = role2.Name, IsDeleted = false }

            };

            Account account4 = new Account()
            {
                Id = 4,
                Email = "catcare_company@gmail.com",
                EmailConfirmed = true,
                DateCreated = new DateTime(2017, 08, 11, 00, 58, 16),
                Avatar = "../../../assets/images/company_logo.png",
                Rating = 3.7,
                IsDeleted = false,
                Role = new Role() { Id = role3.Id, Name = role3.Name, IsDeleted = false }
            };

            Account account5 = new Account()
            {
                Id = 5,
                Email = "abkprostir_vendor@gmail.com",
                EmailConfirmed = true,
                DateCreated = new DateTime(2017, 08, 11, 01, 00, 16),
                Avatar = "abkprostir.jpg",
                Rating = 1,
                IsDeleted = false,
                Role = new Role() { Id = role1.Id, Name = role1.Name, IsDeleted = false }
            };

            Account account6 = new Account()
            {
                Id = 6,
                Email = "driving_company@gmail.com",
                EmailConfirmed = true,
                DateCreated = new DateTime(2017, 08, 11, 22, 57, 15),
                Avatar = "../../../assets/images/company_logo.png",
                Rating = 2.7,
                IsDeleted = false,
                Role = new Role() { Id = role3.Id, Name = role3.Name, IsDeleted = false }
            };

            Account account7 = new Account()
            {
                Id = 7,
                Email = "andrewsany_worker@gmail.com",
                EmailConfirmed = true,
                DateCreated = new DateTime(2017, 08, 11, 22, 57, 15),
                Avatar = "andrewsany_worker.jpg",
                Rating = 4,
                IsDeleted = false,
                Role = new Role() { Id = role2.Id, Name = role2.Name, IsDeleted = false }
            };

            Account account8 = new Account()
            {
                Id = 8,
                Email = "shmirmasha_worker@gmail.com",
                EmailConfirmed = true,
                DateCreated = new DateTime(2017, 08, 12, 02, 14, 15),
                Avatar = "shmirmasha_worker.jpg",
                Rating = 3,
                IsDeleted = false,
                Role = new Role() { Id = role2.Id, Name = role2.Name, IsDeleted = false }
            };

            Account account9 = new Account()
            {
                Id = 9,
                Email = "vitykostyuban_vendor@gmail.com",
                EmailConfirmed = true,
                DateCreated = new DateTime(2017, 08, 11, 22, 57, 15),
                Avatar = "vitykostyuban_vendor.jpg",
                Rating = 5,
                IsDeleted = false,
                Role = new Role() { Id = role1.Id, Name = role1.Name, IsDeleted = false }
            };

            Account account10 = new Account()
            {
                Id = 10,
                Email = "admin@gmail.com",
                EmailConfirmed = true,
                DateCreated = new DateTime(2017, 08, 11, 22, 00, 00),
                Avatar = "admin.jpg",
                Rating = 5,
                IsDeleted = false,
                Role = new Role() { Id = role4.Id, Name = role4.Name, IsDeleted = false }
            };

            context.Accounts.Add(account1);
            context.SaveChanges();
            context.Accounts.Add(account2);
            context.SaveChanges();
            context.Accounts.Add(account3);
            context.SaveChanges();
            context.Accounts.Add(account4);
            context.SaveChanges();
            context.Accounts.Add(account5);
            context.SaveChanges();
            context.Accounts.Add(account6);
            context.SaveChanges();
            context.Accounts.Add(account7);
            context.SaveChanges();
            context.Accounts.Add(account8);
            context.SaveChanges();
            context.Accounts.Add(account9);
            context.SaveChanges();
            context.Accounts.Add(account10);
            context.SaveChanges();

            SocialAccount socialAccount1 = new SocialAccount()
            {
                Id = 1,
                IsDeleted = false,
                Uid = "1",
                Provider = "Microsoft",
                Account = account1
            };

            SocialAccount socialAccount2 = new SocialAccount()
            {
                Id = 2,
                IsDeleted = false,
                Uid = "2",
                Provider = "Facebook",
                Account = account2
            };

            SocialAccount socialAccount3 = new SocialAccount()
            {
                Id = 3,
                IsDeleted = false,
                Uid = "3",
                Provider = "Google",
                Account = account3
            };

            SocialAccount socialAccount4 = new SocialAccount()
            {
                Id = 4,
                IsDeleted = false,
                Uid = "4",
                Provider = "Google",
                Account = account4
            };

            SocialAccount socialAccount5 = new SocialAccount()
            {
                Id = 5,
                IsDeleted = false,
                Uid = "5",
                Provider = "Google",
                Account = account5
            };

            SocialAccount socialAccount6 = new SocialAccount()
            {
                Id = 6,
                IsDeleted = false,
                Uid = "6",
                Provider = "Facebook",
                Account = account6
            };

            SocialAccount socialAccount7 = new SocialAccount()
            {
                Id = 7,
                IsDeleted = false,
                Uid = "7",
                Provider = "Google",
                Account = account7
            };

            SocialAccount socialAccount8 = new SocialAccount()
            {
                Id = 7,
                IsDeleted = false,
                Uid = "8",
                Provider = "Facbook",
                Account = account7
            };

            SocialAccount socialAccount9 = new SocialAccount()
            {
                Id = 7,
                IsDeleted = false,
                Uid = "9",
                Provider = "Twitter",
                Account = account7
            };

            SocialAccount socialAccount10 = new SocialAccount()
            {
                Id = 8,
                IsDeleted = false,
                Uid = "10",
                Provider = "Google",
                Account = account8
            };

            SocialAccount socialAccount11 = new SocialAccount()
            {
                Id = 9,
                IsDeleted = false,
                Uid = "11",
                Provider = "Facebook",
                Account = account9
            };

            context.SocialAccounts.Add(socialAccount1);
            context.SaveChanges();
            context.SocialAccounts.Add(socialAccount2);
            context.SaveChanges();
            context.SocialAccounts.Add(socialAccount3);
            context.SaveChanges();
            context.SocialAccounts.Add(socialAccount4);
            context.SaveChanges();
            context.SocialAccounts.Add(socialAccount5);
            context.SaveChanges();
            context.SocialAccounts.Add(socialAccount6);
            context.SaveChanges();
            context.SocialAccounts.Add(socialAccount7);
            context.SaveChanges();
            context.SocialAccounts.Add(socialAccount8);
            context.SaveChanges();
            context.SocialAccounts.Add(socialAccount9);
            context.SaveChanges();
            context.SocialAccounts.Add(socialAccount10);
            context.SaveChanges();
            context.SocialAccounts.Add(socialAccount11);
            context.SaveChanges();

            // simple user          account3, account7, account8
            // vendor               account2, account5, account9
            // vendor company       account1, account4, account6
            // admi                 acount10

            Permission permision1 = new Permission()
            {
                Id = 1,
                Name = "Show all vendors(include search)",
                IsDeleted = false,
                Accounts = new List<Account> { account1, account2, account3, account4, account5 , account6, account3, account8, account9, account10 }
            };

            Permission permision2 = new Permission()
            {
                Id = 2,
                Name = "Show all users(include search)",
                IsDeleted = false,
                Accounts = new List<Account> { account1, account2, account3, account4, account5, account6, account7, account8, account9, account10 }
            };

            Permission permision3 = new Permission()
            {
                Id = 3,
                Name = "Edit main vendor list",
                IsDeleted = false,
                Accounts = new List<Account> { account10 }
            };

            Permission permision4 = new Permission()
            {
                Id = 4,
                Name = "Edit main vendor list",
                IsDeleted = false,
                Accounts = new List<Account> { account10 }
            };

            Permission permision5 = new Permission()
            {
                Id = 5,
                Name = "Chat with users",
                IsDeleted = false,
                Accounts = new List<Account> { account10, account2, account5, account9, account1, account4, account6 }
            };

            Permission permision6 = new Permission()
            {
                Id = 6,
                Name = "Chat with vendors",
                IsDeleted = false,
                Accounts = new List<Account> { account10, account3, account7, account8, account1, account4, account6 }
            };

            Permission permision7 = new Permission()
            {
                Id = 7,
                Name = "Edit user info",
                IsDeleted = false,
                Accounts = new List<Account> { account10, account3, account7, account8 }
            };

            Permission permision8 = new Permission()
            {
                Id = 8,
                Name = "Edit vendor info",
                IsDeleted = false,
                Accounts = new List<Account> { account10, account2, account5, account9, account1, account4, account6 }
            };

            Permission permision9 = new Permission()
            {
                Id = 9,
                Name = "Book vendor",
                IsDeleted = false,
                Accounts = new List<Account> { account10, account3, account7, account8 }
            };

            Permission permision10 = new Permission()
            {
                Id = 10,
                Name = "Describe vendor skills and services",
                IsDeleted = false,
                Accounts = new List<Account> { account10, account2, account5, account9, account1, account4, account6 }
            };

            Permission permision11 = new Permission()
            {
                Id = 11,
                Name = "Edit own vendor list(for vendor company)",
                IsDeleted = false,
                Accounts = new List<Account> { account10, account1, account4, account6 }
            };

            context.Permissions.Add(permision1);
            context.SaveChanges();
            context.Permissions.Add(permision2);
            context.SaveChanges();
            context.Permissions.Add(permision3);
            context.SaveChanges();
            context.Permissions.Add(permision4);
            context.SaveChanges();
            context.Permissions.Add(permision5);
            context.SaveChanges();
            context.Permissions.Add(permision6);
            context.SaveChanges();
            context.Permissions.Add(permision7);
            context.SaveChanges();
            context.Permissions.Add(permision8);
            context.SaveChanges();
            context.Permissions.Add(permision9);
            context.SaveChanges();
            context.Permissions.Add(permision10);
            context.SaveChanges();
            context.Permissions.Add(permision11);
            context.SaveChanges();




            Category category1 = new Category()
            {
                Id = 1,
                IsDeleted = false,
                Description = "in this categoty vendors help other clean their dirty clothes",
                Name = "Care of clothes",
            };

            Category category2 = new Category()
            {
                Id = 2,
                IsDeleted = false,
                Description = "in this categoty vendors help other care for their pets",
                Name = "Pets care"
            };

            Category category3 = new Category()
            {
                Id = 3,
                IsDeleted = false,
                Description = "in this categoty Vendors help other to get into destination",
                Name = "Car services"
            };

            Category category4 = new Category()
            {
                Id = 4,
                IsDeleted = false,
                Description = "in this categoty vendor help other to make your appartment greate",
                Name = "Buider services"
            };

            Category category5 = new Category()
            {
                Id = 5,
                IsDeleted = false,
                Description = "Photo and video processing",
                Name = "Photo and Video"
            };

            Category category6 = new Category()
            {
                Id = 6,
                IsDeleted = false,
                Description = "Deveoping",
                Name = "Developer Service"
            };



            context.Categories.Add(category1);
            context.SaveChanges();
            context.Categories.Add(category2);
            context.SaveChanges();
            context.Categories.Add(category3);
            context.SaveChanges();
            context.Categories.Add(category4);
            context.SaveChanges();
            context.Categories.Add(category5);
            context.SaveChanges();
            context.Categories.Add(category6);
            context.SaveChanges();


            Subcategory subcategory1 = new Subcategory()
            {
                Id = 1,
                Description = "Wash your clothes and dry clothes",
                Name = "Washing",
                IsDeleted = false,
                Category = new Category() { Id = category1.Id, IsDeleted = false, Name = category1.Name, Description = category1.Description }
            };

            Subcategory subcategory2 = new Subcategory()
            {
                Id = 2,
                Description = "care with lots of love about your cat",
                Name = "Cats care",
                IsDeleted = false,
                Category = new Category() { Id = category2.Id, IsDeleted = false, Name = category2.Name, Description = category2.Description }
            };

            Subcategory subcategory3 = new Subcategory()
            {
                Id = 3,
                Description = "Subacategory lory service",
                Name = "Lory service",
                IsDeleted = false,
                Category = new Category() { Id = category3.Id, IsDeleted = false, Name = category3.Name, Description = category3.Description }
            };

            Subcategory subcategory4 = new Subcategory()
            {
                Id = 4,
                Description = "Your appartment repair",
                Name = "Painting and plastering work",
                IsDeleted = false,
                Category = new Category() { Id = category4.Id, IsDeleted = false, Name = category4.Name, Description = category4.Description }
            };

            Subcategory subcategory5 = new Subcategory()
            {
                Id = 5,
                Description = "Make your own photoset",
                Name = "Photosession",
                IsDeleted = false,
                Category = new Category() { Id = category5.Id, IsDeleted = false, Name = category5.Name, Description = category5.Description }
            };

            Subcategory subcategory6 = new Subcategory()
            {
                Id = 6,
                Description = "Web Development",
                Name = "Web Development",
                IsDeleted = false,
                Category = new Category() { Id = category6.Id, IsDeleted = false, Name = category6.Name, Description = category6.Description }
            };


            context.Subcategories.Add(subcategory1);
            context.SaveChanges();
            context.Subcategories.Add(subcategory2);
            context.SaveChanges();
            context.Subcategories.Add(subcategory3);
            context.SaveChanges();
            context.Subcategories.Add(subcategory4);
            context.SaveChanges();
            context.Subcategories.Add(subcategory5);
            context.SaveChanges();
            context.Subcategories.Add(subcategory6);
            context.SaveChanges();

            Work work1 = new Work()
            {
                Id = 1,
                Description = "Care of clothes",
                IsDeleted = false,
                Name = "laundress",
                Subcategory = subcategory1,
            };

            Work work2 = new Work()
            {
                Id = 2,
                Description = "Photosession, wedding photosessions etc.",
                IsDeleted = false,
                Name = "Photographer",
                Subcategory = subcategory5
            };

            Work work3 = new Work()
            {
                Id = 3,
                Description = "Care of your pets",
                IsDeleted = false,
                Name = "Vet",
                Subcategory = subcategory2
            };

            Work work4 = new Work()
            {
                Id = 4,
                Description = "Appartmen repair",
                IsDeleted = false,
                Name = "Builder",
                Subcategory = subcategory4
            };

            Work work5 = new Work()
            {
                Id = 5,
                Description = "Driving services",
                IsDeleted = false,
                Name = "Driver",
                Subcategory = subcategory3
            };

            Work work6 = new Work()
            {
                Id = 6,
                Description = "C# senior dev",
                IsDeleted = false,
                Name = "Developer",
                Subcategory = subcategory6
            };

            context.Works.Add(work1);
            context.SaveChanges();
            context.Works.Add(work2);
            context.SaveChanges();
            context.Works.Add(work3);
            context.SaveChanges();
            context.Works.Add(work4);
            context.SaveChanges();
            context.Works.Add(work5);
            context.SaveChanges();
            context.Works.Add(work6);
            context.SaveChanges();


            Person person1 = new Person()
            {
                Id = 1,
                Account = account1,
                Name = "Vasylysa",
                MiddleName = "Sergeevna",
                Surname = "Kuchma",
                Birthday = new DateTime(1976, 08, 08),
                Gender = "female",
                IsDeleted = false,
                Location = new Location()
                {
                    Id = 1,
                    Adress = "Lebedeva-Kumacha 7a str.",
                    City = "Kiev",
                    CoordinateX = 50.437,
                    CoordinateY = 30.439,
                    IsDeleted = false,
                    PostIndex = "03110"
                },
                Phone = "+38-044-401-13-13"
            };

            Person person2 = new Person()
            {
                Id = 2,
                Account = account2,
                Name = "Andriy",
                MiddleName = "Myloyovych",
                Surname = "Kravchuk",
                Birthday = new DateTime(1989, 08, 11),
                Gender = "male",
                IsDeleted = false,
                Location = new Location(),
                Phone = "+38-099-222-22-22"
            };

            Person person3 = new Person()
            {
                Id = 3,
                Account = account3,
                Name = "Shnurenko",
                MiddleName = "Oleksandrovych",
                Surname = "Vasil",
                Birthday = new DateTime(1995, 01, 10),
                Gender = "male",
                IsDeleted = false,
                Location = new Location(),
                Phone = "+38-044-333-33-33"
            };

            Person person4 = new Person()
            {
                Id = 4,
                Account = account4,
                Name = "Misha",
                MiddleName = "Petrovych",
                Surname = "Bakun",
                Birthday = new DateTime(1985, 05, 12),
                Gender = "male",
                IsDeleted = false,
                Location = new Location()
                {
                    Id = 4,
                    Adress = "Akademica Palladina 7 pr.",
                    City = "Kiev",
                    CoordinateX = 50.461199,
                    CoordinateY = 30.355651,
                    IsDeleted = false,
                    PostIndex = "03179"
                },
                Phone = "+38-044-444-44-44",
            };

            Person person5 = new Person()
            {
                Id = 5,
                Account = account5,
                Name = "Larysa",
                MiddleName = "Alexandrovna",
                Surname = "Yashkina",
                Birthday = new DateTime(1989, 09, 11),
                Gender = "female",
                IsDeleted = false,
                Location = new Location(),
                Phone = "+38-044-555-55-55"
            };

            Person person6 = new Person()
            {
                Id = 6,
                Account = account6,
                Name = "Sergey",
                MiddleName = "Mykolayovich",
                Surname = "Stasenko",
                Birthday = new DateTime(1977, 09, 23),
                Gender = "male",
                IsDeleted = false,
                Location = new Location()
                {
                    Id = 6,
                    Adress = "Shevchenko str.",
                    City = "Boryspol",
                    CoordinateX = 50.461199,
                    CoordinateY = 30.355651,
                    IsDeleted = false,
                    PostIndex = "27123"
                },
                Phone = "+38-044-666-66-66",
            };

            Person person7 = new Person()
            {
                Id = 7,
                Account = account7,
                Name = "Andrey",
                MiddleName = "Ivanovich",
                Surname = "Panchuk",
                Birthday = new DateTime(1998, 11, 10),
                Gender = "male",
                IsDeleted = false,
                Location = new Location(),
                Phone = "+38-044-777-77-77",
            };

            Person person8 = new Person()
            {
                Id = 8,
                Account = account8,
                Name = "Masha",
                MiddleName = "Edgarivna",
                Surname = "Shmir",
                Birthday = new DateTime(1995, 2, 27),
                Gender = "male",
                IsDeleted = false,
                Location = new Location(),
                Phone = "+38-044-888-88-88"
            };

            Person person9 = new Person()
            {
                Id = 9,
                Account = account9,
                Name = "Victor",
                MiddleName = "Olexsandrovych",
                Surname = "Kotsyuban",
                Birthday = new DateTime(1995, 2, 27),
                Gender = "male",
                IsDeleted = false,
                Location = new Location(),
                Phone = "+38-044-999-99-99"
            };

            Person person10 = new Person()
            {
                Id = 10,
                Account = account10,
                Name = "Admin",
                MiddleName = "Admin",
                Surname = "Admin",
                Birthday = new DateTime(1992, 1, 20),
                Gender = "male",
                IsDeleted = false,
                Phone = "+38-095-444-44-44",
                Location = new Location()
            };

            context.Persons.Add(person1);
            context.SaveChanges();
            context.Persons.Add(person2);
            context.SaveChanges();
            context.Persons.Add(person3);
            context.SaveChanges();
            context.Persons.Add(person4);
            context.SaveChanges();
            context.Persons.Add(person5);
            context.SaveChanges();
            context.Persons.Add(person6);
            context.SaveChanges();
            context.Persons.Add(person7);
            context.SaveChanges();
            context.Persons.Add(person8);
            context.SaveChanges();
            context.Persons.Add(person9);
            context.SaveChanges();
            context.Persons.Add(person10);
            context.SaveChanges();

            Vendor vendor1 = new Vendor()
            {
                Id = 1,
                Experience = 7.5,
                IsDeleted = false,
                Position = "Head",
                Works = new List<Work>() { work1 },
                ExWork = "Dishwasher",
                Person = person1,
                Location = new Location()
                {
                    Id = 1,
                    Adress = "Lebedeva-Kumacha 7a str.",
                    City = "Kiev",
                    CoordinateX = 50.437,
                    CoordinateY = 30.439,
                    IsDeleted = false,
                    PostIndex = "03110"
                }
            };

            Vendor vendor2 = new Vendor()
            {
                Id = 2,
                Experience = 6.7,
                IsDeleted = false,
                Position = "Photographer",
                Works = new List<Work>() { work2 },
                ExWork = "Photographer",
                Person = person2,
                Location = new Location()
                {
                    Id = 1,
                    Adress = "Lebedeva-Kumacha 7a str.",
                    City = "Kiev",
                    CoordinateX = 50.437,
                    CoordinateY = 30.439,
                    IsDeleted = false,
                    PostIndex = "03110"
                }
            };

            Vendor vendor3 = new Vendor()
            {
                Id = 3,
                Experience = 8.7,
                IsDeleted = false,
                Position = "Vet",
                Works = new List<Work>() { work3 },
                ExWork = "Vet",
                Person = person4,
                Location = new Location()
                {
                    Id = 1,
                    Adress = "Lebedeva-Kumacha 7a str.",
                    City = "Kiev",
                    CoordinateX = 50.437,
                    CoordinateY = 30.439,
                    IsDeleted = false,
                    PostIndex = "03110"
                }
            };

            Vendor vendor4 = new Vendor()
            {
                Id = 4,
                Experience = 4.2,
                IsDeleted = false,
                Position = "Builder",
                Works = new List<Work>() { work4 },
                ExWork = "Painter",
                Person = person5,
                Location = new Location()
                {
                    Id = 1,
                    Adress = "Lebedeva-Kumacha 7a str.",
                    City = "Kiev",
                    CoordinateX = 50.437,
                    CoordinateY = 30.439,
                    IsDeleted = false,
                    PostIndex = "03110"
                }
            };

            Vendor vendor5 = new Vendor()
            {
                Id = 5,
                Experience = 8.8,
                IsDeleted = false,
                Position = "Uber Select driver",
                Works = new List<Work>() { work5 },
                ExWork = "lory driver",
                Person = person6,
                Location = new Location()
                {
                    Id = 1,
                    Adress = "Lebedeva-Kumacha 7a str.",
                    City = "Kiev",
                    CoordinateX = 50.437,
                    CoordinateY = 30.439,
                    IsDeleted = false,
                    PostIndex = "03110"
                }
            };

            Vendor vendor6 = new Vendor()
            {
                Id = 6,
                Experience = 9.9,
                IsDeleted = false,
                Position = "C# Senior Dev",
                Works = new List<Work>() { work6 },
                ExWork = "Middle dev",
                Person = person9,
                Location = new Location()
                {
                    Id = 1,
                    Adress = "Lebedeva-Kumacha 7a str.",
                    City = "Kiev",
                    CoordinateX = 50.437,
                    CoordinateY = 30.439,
                    IsDeleted = false,
                    PostIndex = "03110"
                }
            };

            context.Vendors.Add(vendor1);
            context.SaveChanges();
            context.Vendors.Add(vendor2);
            context.SaveChanges();
            context.Vendors.Add(vendor3);
            context.SaveChanges();
            context.Vendors.Add(vendor4);
            context.SaveChanges();
            context.Vendors.Add(vendor5);
            context.SaveChanges();
            context.Vendors.Add(vendor6);
            context.SaveChanges();

            Company company1 = new Company()
            {
                Id = 1,
                Account = account1,
                Name = "TURBOCAT 9000 Inc.",
                Description = "Lorem ipsum dolor sit amet," +
                              "consectetur adipiscing elit. Sed dignissim maximus fringilla.Ut tortor lectus," +
                              "consequat sit amet ultricies ac, feugiat in libero.Sed augue diam," +
                              "lacinia tincidunt sollicitudin sed, rhoncus id neque." +
                              "Phasellus eu velit imperdiet, congue felis non," +
                              "condimentum velit.Lorem ipsum dolor sit amet," +
                              "consectetur adipiscing elit.Mauris pretium arcu vitae mauris rutrum," +
                              "et tempor est congue.",
                FoundationDate = new DateTime(2015, 08, 11, 00, 58, 16),
                Director = account1,
                Staff = 7,
                IsDeleted = false,
                Location = new Location()
                {
                    Id = 1,
                    Adress = "Lebedeva-Kumacha 7a str.",
                    City = "Kiev",
                    CoordinateX = 50.437,
                    CoordinateY = 30.439,
                    IsDeleted = false,
                    PostIndex = "03110"
                },
                Vendors = new List<Vendor>() {vendor1}
            };

            Company company2 = new Company()
            {
                Id = 2,
                Name = "CAT Inc.",
                Description = "Lorem ipsum dolor sit amet," +
                              "consectetur adipiscing elit. Sed dignissim maximus fringilla.Ut tortor lectus," +
                              "consequat sit amet ultricies ac, feugiat in libero.Sed augue diam," +
                              "lacinia tincidunt sollicitudin sed, rhoncus id neque." +
                              "Phasellus eu velit imperdiet, congue felis non," +
                              "condimentum velit.Lorem ipsum dolor sit amet," +
                              "consectetur adipiscing elit.Mauris pretium arcu vitae mauris rutrum," +
                              "et tempor est congue.",
                Account = account4,
                FoundationDate = new DateTime(2017, 08, 12, 10, 36, 16),
                Director = account1,
                Staff = 36,
                IsDeleted = false,
                Location = new Location()
                {
                    Adress = "Akademica Palladina 7 pr.",
                    City = "Kiev",
                    CoordinateX = 50.461199,
                    CoordinateY = 30.355651,
                    IsDeleted = false,
                    PostIndex = "03179"
                },
                Vendors = new List<Vendor>() { vendor4 }
            };

            Company company3 = new Company()
            {
                Id = 3,
                Name = "Tommy Catfilger Inc.",
                Description = "Lorem ipsum dolor sit amet," +
                              "consectetur adipiscing elit. Sed dignissim maximus fringilla.Ut tortor lectus," +
                              "consequat sit amet ultricies ac, feugiat in libero.Sed augue diam," +
                              "lacinia tincidunt sollicitudin sed, rhoncus id neque." +
                              "Phasellus eu velit imperdiet, congue felis non," +
                              "condimentum velit.Lorem ipsum dolor sit amet," +
                              "consectetur adipiscing elit.Mauris pretium arcu vitae mauris rutrum," +
                              "et tempor est congue.",
                Account = account6,
                FoundationDate = new DateTime(2012, 08, 08, 13, 11, 16),
                Director = account1,
                Staff = 2,
                IsDeleted = false,
                Location =new Location()
                {
                    Id = 6,
                    Adress = "Shevchenko str.",
                    City = "Boryspol",
                    CoordinateX = 50.461199,
                    CoordinateY = 30.355651,
                    IsDeleted = false,
                    PostIndex = "27123"
                },
                Vendors = new List<Vendor>() { vendor5 }
            };

            context.Companies.Add(company1);
            context.SaveChanges();
            context.Companies.Add(company2);
            context.SaveChanges();
            context.Companies.Add(company3);
            context.SaveChanges();

            Customer customer1 = new Customer()
            {
                Id = 1,
                IsDeleted = false,
                Person = person3
            };

            Customer customer2 = new Customer()
            {
                Id = 2,
                IsDeleted = false,
                Person = person7
            };

            Customer customer3 = new Customer()
            {
                Id = 3,
                IsDeleted = false,
                Person = person8
            };

            context.Customers.Add(customer1);
            context.SaveChanges();
            context.Customers.Add(customer2);
            context.SaveChanges();
            context.Customers.Add(customer3);
            context.SaveChanges();


            Book book1 = new Book()
            {
                Id = 1,
                Date = new DateTime(2017, 08, 12, 16, 46, 16),
                IsDeleted = false,
                Work = work1,
                Description = "clean clothes and irom it, output 3 spots ",
                Company = company1,
                Status = "in process",
                Customer = new Customer() { Id = customer1.Id, IsDeleted = false, Person = customer1.Person}
            };

            Book book2 = new Book()
            {
                Id = 2,
                Date = new DateTime(2017, 08, 17, 5, 30, 00),
                IsDeleted = false,
                Work = work2,
                Description = "Make a photoset when sunrise",
                Status = "Search",
                Customer = new Customer() { Id = customer1.Id, IsDeleted = false, Person = customer1.Person }
            };

            Book book3 = new Book()
            {
                Id = 3,
                Date = new DateTime(2017, 08, 15, 18, 04, 00),
                IsDeleted = false,
                Work = work3,
                Description = "Care about cat",
                Status = "Recorded at the reception",
                Vendor = vendor3,
                Customer = new Customer() { Id = customer2.Id, IsDeleted = false, Person = customer2.Person }
            };

            Book book4 = new Book()
            {
                Id = 4,
                Date = new DateTime(2017, 08, 15, 18, 04, 00),
                IsDeleted = false,
                Work = work4,
                Description = "rebuild 3 walls",
                Status = "Confirmed",
                Customer = new Customer() { Id = customer2.Id, IsDeleted = false, Person = customer2.Person },
                Company = company2
            };


            Book book5 = new Book()
            {
                Id = 5,
                Date = new DateTime(2017, 08, 16, 08, 00, 00),
                IsDeleted = false,
                Work = work5,
                Description = "Take out the trash",
                Status = "Confirmed",
                Customer = new Customer() { Id = customer3.Id, IsDeleted = false, Person = customer3.Person },
                Company = company3
            };

            Book book6 = new Book()
            {
                Id = 6,
                Date = new DateTime(2017, 08, 20, 18, 04, 00),
                IsDeleted = false,
                Work = work6,
                Description = "Make asp.net core website",
                Location = new Location(),
                Status = "Confirmed",
                Vendor = vendor6,
                Customer = new Customer() { Id = customer3.Id, IsDeleted = false, Person = customer3.Person },
            };

            context.Books.Add(book1);
            context.SaveChanges();
            context.Books.Add(book2);
            context.SaveChanges();
            context.Books.Add(book3);
            context.SaveChanges();
            context.Books.Add(book4);
            context.SaveChanges();
            context.Books.Add(book5);
            context.SaveChanges();
            context.Books.Add(book6);
            context.SaveChanges();



            context.SaveChanges();
            base.Seed(context);
        }
    }
}
