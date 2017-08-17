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
                Name = "Guest"
            };

            Role role2 = new Role()
            {
                Id = 2,
                IsDeleted = false,
                Name = "User"
            };

            Role role3 = new Role()
            {
                Id = 3,
                IsDeleted = false,
                Name = "Vendor"
            };

            Role role4 = new Role()
            {
                Id = 4,
                IsDeleted = false,
                Name = "Company"
            };

            Role role5 = new Role()
            {
                Id = 5,
                IsDeleted = false,
                Name = "Admin"
            };

            context.Roles.AddRange(new List<Role>() { role1, role2, role3, role4, role5 });

            Account account1 = new Account()
            {
                Id = 1,
                Email = "cleanok_company@gmail.com",
                EmailConfirmed = true,
                DateCreated = new DateTime(2017, 08, 10, 22, 38, 15),
                Avatar = "https://image.flaticon.com/icons/png/512/78/78373.png",
                Rating = 4.7,
                IsDeleted = false,
                Role = role3
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
                Role = role1
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
                Role = role2

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
                Role = role3
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
                Role = role1
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
                Role = role3
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
                Role = role4
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
                Role = role2
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
                Role = role1
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
                Role = role5
            };

            context.Accounts.AddRange(new List<Account>() { account1, account2, account3, account4, account5, account6, account7, account8, account9, account10 });

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
                Account = account8
            };

            SocialAccount socialAccount9 = new SocialAccount()
            {
                Id = 7,
                IsDeleted = false,
                Uid = "9",
                Provider = "Twitter",
                Account = account9
            };

            SocialAccount socialAccount10 = new SocialAccount()
            {
                Id = 8,
                IsDeleted = false,
                Uid = "10",
                Provider = "Google",
                Account = account10
            };

            context.SocialAccounts.AddRange(new List<SocialAccount>() { socialAccount1, socialAccount2, socialAccount3, socialAccount4, socialAccount5, socialAccount6, socialAccount7, socialAccount8, socialAccount9, socialAccount10 });

            Permission permision1 = new Permission()
            {
                Id = 1,
                Name = "Show all vendors(include search)",
                IsDeleted = false,
                Accounts = new List<Account> { account1, account2, account3, account4, account5, account6, account3, account8, account9, account10 }
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

            context.Permissions.AddRange(new List<Permission>() { permision1, permision2, permision3, permision4, permision5, permision6, permision7, permision8, permision9, permision10, permision11 });

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

            context.Categories.AddRange(new List<Category>() { category1, category2, category3, category4, category5, category6 });

            Subcategory subcategory1 = new Subcategory()
            {
                Id = 1,
                Description = "Wash your clothes and dry clothes",
                Name = "Washing",
                IsDeleted = false,
                Category = category1
            };

            Subcategory subcategory2 = new Subcategory()
            {
                Id = 2,
                Description = "care with lots of love about your cat",
                Name = "Cats care",
                IsDeleted = false,
                Category = category2
            };

            Subcategory subcategory3 = new Subcategory()
            {
                Id = 3,
                Description = "Subacategory lory service",
                Name = "Lory service",
                IsDeleted = false,
                Category = category3
            };

            Subcategory subcategory4 = new Subcategory()
            {
                Id = 4,
                Description = "Your appartment repair",
                Name = "Painting and plastering work",
                IsDeleted = false,
                Category = category4
            };

            Subcategory subcategory5 = new Subcategory()
            {
                Id = 5,
                Description = "Make your own photoset",
                Name = "Photosession",
                IsDeleted = false,
                Category = category5
            };

            Subcategory subcategory6 = new Subcategory()
            {
                Id = 6,
                Description = "Web Development",
                Name = "Web Development",
                IsDeleted = false,
                Category = category6
            };

            context.Subcategories.AddRange(new List<Subcategory>() { subcategory1, subcategory2, subcategory3, subcategory4, subcategory5, subcategory6 });

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

            context.Works.AddRange(new List<Work>() { work1, work2, work3, work4, work5, work6 });

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
                    Latitude = 50.437,
                    Longitude = 30.439,
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
                Location = new Location()
                {
                    Adress = "Akademica Palladina 9 pr.",
                    City = "Kiev",
                    Latitude = 50.461199,
                    Longitude = 30.355651,
                    IsDeleted = false,
                    PostIndex = "03179"
                },
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
                Location = new Location()
                {
                    Adress = "Main Square 8",
                    City = "Lviv",
                    Latitude = 38.148089,
                    Longitude = 63.114765,
                    IsDeleted = false,
                    PostIndex = "79013"
                },
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
                    Latitude = 50.461199,
                    Longitude = 30.355651,
                    IsDeleted = false,
                    PostIndex = "79053"
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
                Location = new Location()
                {
                    Adress = "Yawornitskogo st.8",
                    City = "Kharkiv",
                    Longitude = 69.651308,
                    Latitude = 75.084123,
                    IsDeleted = false,
                    PostIndex = "54136"
                },
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
                    Latitude = 50.461199,
                    Longitude = 30.355651,
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
                Location = new Location()
                {
                    Adress = "Puliya 7 pr.",
                    City = "Dnipro",
                    Longitude = 44.119637,
                    Latitude = 99.131470,
                    IsDeleted = false,
                    PostIndex = "09304"
                },
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
                Location = new Location()
                {
                    Adress = "Zamarstynivska 15a",
                    City = "Lviv",
                    Longitude = 16.312059,
                    Latitude = 22.963471,
                    IsDeleted = false,
                    PostIndex = "96341"
                },
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
                Location = new Location()
                {
                    Adress = "Dom Kolotushkina",
                    City = "Boston",
                    Longitude = 19.16348,
                    Latitude = 99.11074,
                    IsDeleted = false,
                    PostIndex = "013279"
                },
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
                {
                    Adress = "Center 2a",
                    City = "Madrid",
                    Longitude = 75.944219,
                    Latitude = 71.124861,
                    IsDeleted = false,
                    PostIndex = "99304"
                },
            };

            context.Persons.AddRange(new List<Person>() { person1, person2, person3, person4, person5, person6, person7, person8, person9, person10 });



            ContactProvider skype = new ContactProvider()
            {
                Name = "Skype",
                IsDeleted = false,
                Type = "Messenger"
            };

            ContactProvider telegram = new ContactProvider()
            {
                Name = "Telegram",
                IsDeleted = false,
                Type = "Messenger"
            };

            ContactProvider viber = new ContactProvider()
            {
                Name = "Viber",
                IsDeleted = false,
                Type = "Messenger"
            };

            ContactProvider facebook = new ContactProvider()
            {
                Name = "Facebook",
                IsDeleted = false,
                Type = "Social"
            };

            ContactProvider vk = new ContactProvider()
            {
                Name = "Vk",
                IsDeleted = false,
                Type = "Social"
            };

            ContactProvider linkedin = new ContactProvider()
            {
                Name = "LinkedIn",
                IsDeleted = false,
                Type = "Social"
            };

            ContactProvider email = new ContactProvider()
            {
                Name = "Email",
                IsDeleted = false,
                Type = "Email"
            };

            ContactProvider phone = new ContactProvider()
            {
                Name = "Phone",
                IsDeleted = false,
                Type = "Phone"
            };

            context.ContactProviders.AddRange(new List<ContactProvider>() { phone, email, skype, telegram, facebook, viber, vk, linkedin });

            Contact contact1 = new Contact
            {
                IsDeleted = false,
                Provider = phone,
                Value = "+1 415 670 9788"
            };
            Contact contact2 = new Contact
            {
                IsDeleted = false,
                Provider = phone,
                Value = "+1 415 670 9788"
            };
            Contact contact3 = new Contact
            {
                IsDeleted = false,
                Provider = phone,
                Value = "+1 415 670 9788"
            };
            Contact contact4 = new Contact
            {
                IsDeleted = false,
                Provider = email,
                Value = "turbocat_9000@gmail.com"
            };
            Contact contact5 = new Contact
            {
                IsDeleted = false,
                Provider = skype,
                Value = "turbocat_9000"
            };
            Contact contact6 = new Contact
            {
                IsDeleted = false,
                Provider = telegram,
                Value = "turbocat_9000"
            };
            Contact contact7 = new Contact
            {
                IsDeleted = false,
                Provider = facebook,
                Value = "turbocat_9000"
            };

            context.Contacts.AddRange(new List<Contact>() { contact1, contact2, contact3, contact4, contact5, contact6, contact7 });

            PortfolioItem portfolioItem1 = new PortfolioItem()
            {
                Image = "https://camo.githubusercontent.com/f8ea5eab7494f955e90f60abc1d13f2ce2c2e540/68747470733a2f2f662e636c6f75642e6769746875622e636f6d2f6173736574732f323037383234352f3235393331332f35653833313336322d386362612d313165322d383435332d6536626439353663383961342e706e67",
                IsDeleted = false,
                Subcategory = subcategory1,
                WorkType = work1
            };

            context.PortfolioItems.AddRange(new List<PortfolioItem>() { portfolioItem1 });

            Vendor vendor1 = new Vendor()
            {
                Id = 1,
                Experience = 7.5,
                IsDeleted = false,
                Position = "Head",
                Works = new List<Work>() { work1, work2, work3 },
                ExWork = "Dishwasher",
                Person = person1,
                Contacts = new List<Contact>()
                {
                    new Contact
                    {
                        IsDeleted = false,
                        Provider = phone,
                        Value = "+1 415 670 9788"
                    }
                },
                WorkLetter = "My name is Randy Patterson, and I’m currently looking for a job in youth services. I have 10 years of experience working with youth agencies. I have a bachelor’s degree in outdoor education. I raise money, train leaders, and organize units. I have raised over $100,000 each of the last six years. I consider myself a good public speaker, and I have a good sense of humor.",
                PortfolioItems = new List<PortfolioItem>() { portfolioItem1 }
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
                Contacts = new List<Contact>()
                {
                    contact1,
                    contact2,
                    contact3,
                    contact4,
                    contact5,
                    contact6,
                    contact7
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
                Person = person3,
                Contacts = new List<Contact>()
                {
                    contact1,
                    contact2,
                    contact3,
                    contact4,
                    contact5,
                    contact6,
                    contact7
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
                Person = person4,
                Contacts = new List<Contact>()
                {
                    contact1,
                    contact2,
                    contact3,
                    contact4,
                    contact5,
                    contact6,
                    contact7
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
                Person = person5,
                Contacts = new List<Contact>()
                {
                    contact1,
                    contact2,
                    contact3,
                    contact4,
                    contact5,
                    contact6,
                    contact7
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
                Person = person6,
                Contacts = new List<Contact>()
                {
                    contact1,
                    contact2,
                    contact3,
                    contact4,
                    contact5,
                    contact6,
                    contact7
                }
            };

            context.Vendors.AddRange(new List<Vendor>() { vendor1, vendor2, vendor3, vendor4, vendor5, vendor6 });

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
                    Adress = "Stepana Bandery 7",
                    City = "Lviv",
                    Latitude = 56.357890,
                    Longitude = 118.13458,
                    IsDeleted = false,
                    PostIndex = "79054"
                },
                Vendors = new List<Vendor>() { vendor1 }
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
                    Latitude = 50.461199,
                    Longitude = 30.355651,
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
                Location = new Location()
                {
                    Id = 6,
                    Adress = "Shevchenko str.",
                    City = "Boryspol",
                    Latitude = 50.461199,
                    Longitude = 30.355651,
                    IsDeleted = false,
                    PostIndex = "27123"
                },
                Vendors = new List<Vendor>() { vendor5 }
            };

            context.Companies.AddRange(new List<Company>() { company1, company2, company3 });

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

            context.Customers.AddRange(new List<Customer>() { customer1, customer2, customer3 });

            Book book1 = new Book()
            {
                Id = 1,
                Date = new DateTime(2017, 08, 12, 16, 46, 16),
                IsDeleted = false,
                Work = work1,
                Description = "clean clothes and irom it, output 3 spots ",
                Company = company1,
                Status = "in process",
                Customer = customer1
            };

            Book book2 = new Book()
            {
                Id = 2,
                Date = new DateTime(2017, 08, 17, 5, 30, 00),
                IsDeleted = false,
                Work = work2,
                Description = "Make a photoset when sunrise",
                Status = "Search",
                Customer = customer2
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
                Customer = customer2
            };

            Book book4 = new Book()
            {
                Id = 4,
                Date = new DateTime(2017, 08, 15, 18, 04, 00),
                IsDeleted = false,
                Work = work4,
                Description = "rebuild 3 walls",
                Status = "Confirmed",
                Customer = customer3,
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
                Customer = customer1,
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
                Customer = customer1
            };

            context.Books.AddRange(new List<Book>() { book1, book2, book3, book4, book5, book6 });

            Review review1 = new Review()
            {
                BookId = 1,
                Description = "qwert",
                From = "NameSurname",
                FromAccountId = 2,
                To = "NameSurname",
                ToAccountId = 6,
                IsDeleted = false,
                Grade = 4,
                Date = DateTime.Now,
                Avatar = "https://image.flaticon.com/icons/png/512/78/78373.png"
            };
            Review review2 = new Review()
            {
                BookId = 1,
                Description = "qwert",
                From = "NameSurname",
                FromAccountId = 2,
                To = "NameSurname",
                ToAccountId = 1,
                IsDeleted = false,
                Grade = 4,
                Date = DateTime.Now,
                Avatar = "https://image.flaticon.com/icons/png/512/78/78373.png"
            };
            Review review3 = new Review()
            {
                BookId = 1,
                Description = "qwert",
                From = "NameSurname",
                FromAccountId = 2,
                To = "NameSurname",
                ToAccountId = 6,
                IsDeleted = false,
                Grade = 4,
                Date = DateTime.Now,
                Avatar = "https://image.flaticon.com/icons/png/512/78/78373.png"
            };
            Review review4 = new Review()
            {
                BookId = 1,
                Description = "qwert",
                From = "NameSurname",
                FromAccountId = 2,
                To = "NameSurname",
                ToAccountId = 6,
                IsDeleted = false,
                Grade = 4,
                Date = DateTime.Now,
                Avatar = "https://image.flaticon.com/icons/png/512/78/78373.png"
            };
            Review review5 = new Review()
            {
                BookId = 1,
                Description = "qwert",
                From = "NameSurname",
                FromAccountId = 2,
                To = "NameSurname",
                ToAccountId = 1,
                IsDeleted = false,
                Grade = 4,
                Date = DateTime.Now,
                Avatar = "https://image.flaticon.com/icons/png/512/78/78373.png"
            };

            context.Reviews.AddRange(new List<Review>(){review1, review2, review3, review4, review5});
            
            context.SaveChanges();

            context.SaveChanges();

            base.Seed(context);
        }
    }
}
