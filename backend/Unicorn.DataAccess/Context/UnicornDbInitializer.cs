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
            #region Permissions
            Permission permision1 = new Permission()
            {
                Id = 1,
                Name = "Show all vendors(include search)",
                IsDeleted = false
            };

            Permission permision2 = new Permission()
            {
                Id = 2,
                Name = "Show all users(include search)",
                IsDeleted = false
            };

            Permission permision3 = new Permission()
            {
                Id = 3,
                Name = "Edit main vendor list",
                IsDeleted = false
            };

            Permission permision4 = new Permission()
            {
                Id = 4,
                Name = "Edit main vendor list",
                IsDeleted = false
            };

            Permission permision5 = new Permission()
            {
                Id = 5,
                Name = "Chat with users",
                IsDeleted = false
            };

            Permission permision6 = new Permission()
            {
                Id = 6,
                Name = "Chat with vendors",
                IsDeleted = false
            };

            Permission permision7 = new Permission()
            {
                Id = 7,
                Name = "Edit user info",
                IsDeleted = false
            };

            Permission permision8 = new Permission()
            {
                Id = 8,
                Name = "Edit vendor info",
                IsDeleted = false
            };

            Permission permision9 = new Permission()
            {
                Id = 9,
                Name = "Book vendor",
                IsDeleted = false
            };

            Permission permision10 = new Permission()
            {
                Id = 10,
                Name = "Describe vendor skills and services",
                IsDeleted = false
            };

            Permission permision11 = new Permission()
            {
                Id = 11,
                Name = "Edit own vendor list(for vendor company)",
                IsDeleted = false
            };
        
            context.Permissions.AddRange(new List<Permission>() { permision1, permision2, permision3, permision4, permision5, permision6, permision7, permision8, permision9, permision10, permision11 });

            #endregion

            #region Roles
            Role role1 = new Role()
            {
                Id = 1,
                IsDeleted = false,
                Name = "Guest",
                Permissions = new List<Permission>
                {
                    permision1,
                    permision2
                }
            };

            Role role2 = new Role()
            {
                Id = 2,
                IsDeleted = false,
                Name = "Customer",
                Permissions = new List<Permission>
                {
                    permision1,
                    permision2,
                    permision6,
                    permision7,
                    permision9
                }
            };

            Role role3 = new Role()
            {
                Id = 3,
                IsDeleted = false,
                Name = "Vendor",
                Permissions = new List<Permission>
                {
                    permision1,
                    permision2,
                    permision5,
                    permision8,
                    permision10
                }
            };

            Role role4 = new Role()
            {
                Id = 4,
                IsDeleted = false,
                Name = "Company",
                Permissions = new List<Permission>
                {
                    permision1,
                    permision2,
                    permision5,
                    permision6,
                    permision8,
                    permision10,
                    permision11
                }
            };

            Role role5 = new Role()
            {
                Id = 5,
                IsDeleted = false,
                Name = "Admin",
                Permissions = new List<Permission>
                {
                    permision1,
                    permision2,
                    permision3,
                    permision4,
                    permision5,
                    permision6,
                    permision7,
                    permision8,
                    permision9,
                    permision10,
                    permision11
                }
            };

            context.Roles.AddRange(new List<Role>() { role1, role2, role3, role4, role5 });

            #endregion

            #region ContactProviders

            ContactProvider skype = new ContactProvider()
            {
                Name = "skype",
                IsDeleted = false,
                Type = "Messenger"
            };

            ContactProvider telegram = new ContactProvider()
            {
                Name = "telegram",
                IsDeleted = false,
                Type = "Messenger"
            };

            ContactProvider viber = new ContactProvider()
            {
                Name = "viber",
                IsDeleted = false,
                Type = "Messenger"
            };

            ContactProvider facebook = new ContactProvider()
            {
                Name = "facebook",
                IsDeleted = false,
                Type = "Social"
            };

            ContactProvider vk = new ContactProvider()
            {
                Name = "vk",
                IsDeleted = false,
                Type = "Social"
            };

            ContactProvider linkedin = new ContactProvider()
            {
                Name = "linkedIn",
                IsDeleted = false,
                Type = "Social"
            };

            ContactProvider email = new ContactProvider()
            {
                Name = "email",
                IsDeleted = false,
                Type = "Email"
            };

            ContactProvider phone = new ContactProvider()
            {
                Name = "phone",
                IsDeleted = false,
                Type = "Phone"
            };

            context.ContactProviders.AddRange(new List<ContactProvider>() { phone, email, skype, telegram, facebook, viber, vk, linkedin });


            #endregion

            #region Contacts

            Contact contact1 = new Contact { IsDeleted = false, Provider = phone, Value = "+1 415 670 9788" };
            Contact contact2 = new Contact { IsDeleted = false, Provider = phone, Value = "+1 415 670 9788" };
            Contact contact3 = new Contact { IsDeleted = false, Provider = phone, Value = "+1 415 670 9788" };
            Contact contact4 = new Contact { IsDeleted = false, Provider = email, Value = "turbocat_9000@gmail.com" };
            Contact contact5 = new Contact { IsDeleted = false, Provider = skype, Value = "turbocat_9000" };
            Contact contact6 = new Contact { IsDeleted = false, Provider = telegram, Value = "turbocat_9000" };
            Contact contact7 = new Contact { IsDeleted = false, Provider = facebook, Value = "turbocat_9000" };

            Contact contact8 = new Contact { IsDeleted = false, Provider = phone, Value = "+1 415 670 9788" };
            Contact contact9 = new Contact { IsDeleted = false, Provider = phone, Value = "+1 415 670 9788" };
            Contact contact10 = new Contact { IsDeleted = false, Provider = phone, Value = "+1 415 670 9788" };
            Contact contact11 = new Contact { IsDeleted = false, Provider = email, Value = "turbocat_9000@gmail.com" };
            Contact contact12 = new Contact { IsDeleted = false, Provider = skype, Value = "turbocat_9000" };
            Contact contact13 = new Contact { IsDeleted = false, Provider = telegram, Value = "turbocat_9000" };
            Contact contact14 = new Contact { IsDeleted = false, Provider = facebook, Value = "turbocat_9000" };

            Contact contact15 = new Contact { IsDeleted = false, Provider = phone, Value = "+1 415 670 9788" };
            Contact contact16 = new Contact { IsDeleted = false, Provider = phone, Value = "+1 415 670 9788" };
            Contact contact17 = new Contact { IsDeleted = false, Provider = phone, Value = "+1 415 670 9788" };
            Contact contact18 = new Contact { IsDeleted = false, Provider = email, Value = "turbocat_9000@gmail.com" };
            Contact contact19 = new Contact { IsDeleted = false, Provider = skype, Value = "turbocat_9000" };
            Contact contact20 = new Contact { IsDeleted = false, Provider = telegram, Value = "turbocat_9000" };
            Contact contact21 = new Contact { IsDeleted = false, Provider = facebook, Value = "turbocat_9000" };

            Contact contact22 = new Contact { IsDeleted = false, Provider = phone, Value = "+1 415 670 9788" };
            Contact contact23 = new Contact { IsDeleted = false, Provider = phone, Value = "+1 415 670 9788" };
            Contact contact24 = new Contact { IsDeleted = false, Provider = phone, Value = "+1 415 670 9788" };
            Contact contact25 = new Contact { IsDeleted = false, Provider = email, Value = "turbocat_9000@gmail.com" };
            Contact contact26 = new Contact { IsDeleted = false, Provider = skype, Value = "turbocat_9000" };
            Contact contact27 = new Contact { IsDeleted = false, Provider = telegram, Value = "turbocat_9000" };
            Contact contact28 = new Contact { IsDeleted = false, Provider = facebook, Value = "turbocat_9000" };

            Contact contact29 = new Contact { IsDeleted = false, Provider = phone, Value = "+1 415 670 9788" };
            Contact contact30 = new Contact { IsDeleted = false, Provider = phone, Value = "+1 415 670 9788" };
            Contact contact31 = new Contact { IsDeleted = false, Provider = phone, Value = "+1 415 670 9788" };
            Contact contact32 = new Contact { IsDeleted = false, Provider = email, Value = "turbocat_9000@gmail.com" };
            Contact contact33 = new Contact { IsDeleted = false, Provider = skype, Value = "turbocat_9000" };
            Contact contact34 = new Contact { IsDeleted = false, Provider = telegram, Value = "turbocat_9000" };
            Contact contact35 = new Contact { IsDeleted = false, Provider = facebook, Value = "turbocat_9000" };

            Contact contact36 = new Contact { IsDeleted = false, Provider = phone, Value = "+1 415 670 9788" };
            Contact contact37 = new Contact { IsDeleted = false, Provider = phone, Value = "+1 415 670 9788" };
            Contact contact38 = new Contact { IsDeleted = false, Provider = phone, Value = "+1 415 670 9788" };
            Contact contact39 = new Contact { IsDeleted = false, Provider = email, Value = "turbocat_9000@gmail.com" };
            Contact contact40 = new Contact { IsDeleted = false, Provider = skype, Value = "turbocat_9000" };
            Contact contact41 = new Contact { IsDeleted = false, Provider = telegram, Value = "turbocat_9000" };
            Contact contact42 = new Contact { IsDeleted = false, Provider = facebook, Value = "turbocat_9000" };

            Contact contact43 = new Contact { IsDeleted = false, Provider = phone, Value = "+1 415 670 9788" };
            Contact contact44 = new Contact { IsDeleted = false, Provider = phone, Value = "+1 415 670 9788" };
            Contact contact45 = new Contact { IsDeleted = false, Provider = phone, Value = "+1 415 670 9788" };
            Contact contact46 = new Contact { IsDeleted = false, Provider = email, Value = "turbocat_9000@gmail.com" };
            Contact contact47 = new Contact { IsDeleted = false, Provider = skype, Value = "turbocat_9000" };
            Contact contact48 = new Contact { IsDeleted = false, Provider = telegram, Value = "turbocat_9000" };
            Contact contact49 = new Contact { IsDeleted = false, Provider = facebook, Value = "turbocat_9000" };

            Contact contact50 = new Contact { IsDeleted = false, Provider = phone, Value = "+1 415 670 9788" };
            Contact contact51 = new Contact { IsDeleted = false, Provider = phone, Value = "+1 415 670 9788" };
            Contact contact52 = new Contact { IsDeleted = false, Provider = phone, Value = "+1 415 670 9788" };
            Contact contact53 = new Contact { IsDeleted = false, Provider = email, Value = "turbocat_9000@gmail.com" };
            Contact contact54 = new Contact { IsDeleted = false, Provider = skype, Value = "turbocat_9000" };
            Contact contact55 = new Contact { IsDeleted = false, Provider = telegram, Value = "turbocat_9000" };
            Contact contact56 = new Contact { IsDeleted = false, Provider = facebook, Value = "turbocat_9000" };

            Contact contact57 = new Contact { IsDeleted = false, Provider = phone, Value = "+1 415 670 9788" };
            Contact contact58 = new Contact { IsDeleted = false, Provider = phone, Value = "+1 415 670 9788" };
            Contact contact59 = new Contact { IsDeleted = false, Provider = phone, Value = "+1 415 670 9788" };
            Contact contact60 = new Contact { IsDeleted = false, Provider = email, Value = "turbocat_9000@gmail.com" };
            Contact contact61 = new Contact { IsDeleted = false, Provider = skype, Value = "turbocat_9000" };
            Contact contact62 = new Contact { IsDeleted = false, Provider = telegram, Value = "turbocat_9000" };
            Contact contact63 = new Contact { IsDeleted = false, Provider = facebook, Value = "turbocat_9000" };

            Contact contact64 = new Contact { IsDeleted = false, Provider = phone, Value = "+1 415 670 9788" };
            Contact contact65 = new Contact { IsDeleted = false, Provider = phone, Value = "+1 415 670 9788" };
            Contact contact66 = new Contact { IsDeleted = false, Provider = phone, Value = "+1 415 670 9788" };
            Contact contact67 = new Contact { IsDeleted = false, Provider = email, Value = "turbocat_9000@gmail.com" };
            Contact contact68 = new Contact { IsDeleted = false, Provider = skype, Value = "turbocat_9000" };
            Contact contact69 = new Contact { IsDeleted = false, Provider = telegram, Value = "turbocat_9000" };
            Contact contact70 = new Contact { IsDeleted = false, Provider = facebook, Value = "turbocat_9000" };

            Contact contact71 = new Contact { IsDeleted = false, Provider = phone, Value = "+1 415 670 9788" };
            Contact contact72 = new Contact { IsDeleted = false, Provider = phone, Value = "+1 415 670 9788" };
            Contact contact73 = new Contact { IsDeleted = false, Provider = phone, Value = "+1 415 670 9788" };
            Contact contact74 = new Contact { IsDeleted = false, Provider = email, Value = "turbocat_9000@gmail.com" };
            Contact contact75 = new Contact { IsDeleted = false, Provider = skype, Value = "turbocat_9000" };
            Contact contact76 = new Contact { IsDeleted = false, Provider = telegram, Value = "turbocat_9000" };
            Contact contact77 = new Contact { IsDeleted = false, Provider = facebook, Value = "turbocat_9000" };

            Contact contact78 = new Contact { IsDeleted = false, Provider = phone, Value = "+1 415 670 9788" };
            Contact contact79 = new Contact { IsDeleted = false, Provider = phone, Value = "+1 415 670 9788" };
            Contact contact80 = new Contact { IsDeleted = false, Provider = phone, Value = "+1 415 670 9788" };
            Contact contact81 = new Contact { IsDeleted = false, Provider = email, Value = "turbocat_9000@gmail.com" };
            Contact contact82 = new Contact { IsDeleted = false, Provider = skype, Value = "turbocat_9000" };
            Contact contact83 = new Contact { IsDeleted = false, Provider = telegram, Value = "turbocat_9000" };
            Contact contact84 = new Contact { IsDeleted = false, Provider = facebook, Value = "turbocat_9000" };

            Contact contact85 = new Contact { IsDeleted = false, Provider = phone, Value = "+1 415 670 9788" };
            Contact contact86 = new Contact { IsDeleted = false, Provider = phone, Value = "+1 415 670 9788" };
            Contact contact87 = new Contact { IsDeleted = false, Provider = phone, Value = "+1 415 670 9788" };
            Contact contact88 = new Contact { IsDeleted = false, Provider = email, Value = "turbocat_9000@gmail.com" };
            Contact contact89 = new Contact { IsDeleted = false, Provider = skype, Value = "turbocat_9000" };
            Contact contact90 = new Contact { IsDeleted = false, Provider = telegram, Value = "turbocat_9000" };
            Contact contact91 = new Contact { IsDeleted = false, Provider = facebook, Value = "turbocat_9000" };

            context.Contacts.AddRange(new List<Contact>()
            {
                contact1,  contact11, contact21, contact31, contact41, contact51, contact61, contact71, contact81, contact91,
                contact2,  contact12, contact22, contact32, contact42, contact52, contact62, contact72, contact82, 
                contact3,  contact13, contact23, contact33, contact43, contact53, contact63, contact73, contact83, 
                contact4,  contact14, contact24, contact34, contact44, contact54, contact64, contact74, contact84, 
                contact5,  contact15, contact25, contact35, contact45, contact55, contact65, contact75, contact85, 
                contact6,  contact16, contact26, contact36, contact46, contact56, contact66, contact76, contact86, 
                contact7,  contact17, contact27, contact37, contact47, contact57, contact67, contact77, contact87, 
                contact8,  contact18, contact28, contact38, contact48, contact58, contact68, contact78, contact88, 
                contact9,  contact19, contact29, contact39, contact49, contact59, contact69, contact79, contact89, 
                contact10, contact20, contact30, contact40, contact50, contact60, contact70, contact80, contact90 
            });

            #endregion

            #region Accounts

            Account account1 = new Account()
            {
                Id = 1,
                Email = "cleanok_company@gmail.com",
                EmailConfirmed = true,
                DateCreated = new DateTime(2017, 08, 10, 22, 38, 15),
                Avatar = "http://www.arteinterattiva.it/wp-content/uploads/2014/08/013.png",
                Rating = 4.7,
                IsDeleted = false,
                Role = role3,
                Contacts = new List<Contact> { contact1, contact2, contact3, contact4, contact5, contact6, contact7}
            };

            Account account2 = new Account()
            {
                Id = 2,
                Email = "andriy_vendor@gmail.com",
                EmailConfirmed = true,
                DateCreated = new DateTime(2017, 08, 11, 00, 55, 16),
                Avatar = "https://www.xing.com/image/3_4_a_99233ef95_25270136_1/mehmet-altun-foto.1024x1024.jpg",
                Rating = 4.7,
                IsDeleted = false,
                Role = role1,
                Contacts = new List<Contact> { contact8, contact9, contact10, contact11, contact12, contact13, contact14 }
            };

            Account account3 = new Account()
            {
                Id = 3,
                Email = "shnurenko_worker@gmail.com",
                EmailConfirmed = true,
                DateCreated = new DateTime(2017, 08, 11, 00, 57, 16),
                Avatar = "http://revivre.ee/wp-content/uploads/2013/07/photodune-4276142-smiling-portraits-xl_411.jpg",
                Rating = 2,
                IsDeleted = false,
                Role = role2,
                Contacts = new List<Contact> { contact15, contact16, contact17, contact18, contact19, contact20, contact21 }

            };

            Account account4 = new Account()
            {
                Id = 4,
                Email = "catcare_company@gmail.com",
                EmailConfirmed = true,
                DateCreated = new DateTime(2017, 08, 11, 00, 58, 16),
                Avatar = "http://coodiv.net/project/skyhost/HTML/img/quote/44-3.jpg",
                Rating = 3.7,
                IsDeleted = false,
                Role = role3,
                Contacts = new List<Contact> { contact22, contact23, contact24, contact25, contact26, contact27, contact28 }
            };

            Account account5 = new Account()
            {
                Id = 5,
                Email = "abkprostir_vendor@gmail.com",
                EmailConfirmed = true,
                DateCreated = new DateTime(2017, 08, 11, 01, 00, 16),
                Avatar = "http://www.actionagainstabduction.org/wp-content/uploads/2015/07/Lucy_Holmes.jpg",
                Rating = 1,
                IsDeleted = false,
                Role = role1,
                Contacts = new List<Contact> { contact29, contact30, contact31, contact32, contact33, contact34, contact35 }
            };

            Account account6 = new Account()
            {
                Id = 6,
                Email = "driving_company@gmail.com",
                EmailConfirmed = true,
                DateCreated = new DateTime(2017, 08, 11, 22, 57, 15),
                Avatar = "http://1.bp.blogspot.com/_A1-gDagxbUM/S_lmQ5ZiIwI/AAAAAAAAAH4/tfLxn-zR314/s1600/Tom+Cruise.jpg",
                Rating = 2.7,
                IsDeleted = false,
                Role = role3,
                Contacts = new List<Contact> { contact36, contact37, contact38, contact39, contact40, contact41, contact42 }
            };

            Account account7 = new Account()
            {
                Id = 7,
                Email = "andrewsany_worker@gmail.com",
                EmailConfirmed = true,
                DateCreated = new DateTime(2017, 08, 11, 22, 57, 15),
                Avatar = "https://s-media-cache-ak0.pinimg.com/736x/88/e3/b0/88e3b020ed58b7b5734b87f9661e010d--hugo-chávez-archaeology.jpg",
                Rating = 4,
                IsDeleted = false,
                Role = role4,
                Contacts = new List<Contact> { contact43, contact44, contact45, contact46, contact47, contact48, contact49 }
            };

            Account account8 = new Account()
            {
                Id = 8,
                Email = "shmirmasha_worker@gmail.com",
                EmailConfirmed = true,
                DateCreated = new DateTime(2017, 08, 12, 02, 14, 15),
                Avatar = "https://articlemanager.blob.core.windows.net/article-images/2_your-logo-design.jpg",
                Rating = 3,
                IsDeleted = false,
                Role = role2,
                Contacts = new List<Contact> { contact50, contact51, contact52, contact53, contact54, contact55, contact56 }
            };

            Account account9 = new Account()
            {
                Id = 9,
                Email = "vitykostyuban_vendor@gmail.com",
                EmailConfirmed = true,
                DateCreated = new DateTime(2017, 08, 11, 22, 57, 15),
                Avatar = "https://tribkcpq.files.wordpress.com/2016/07/s032965871-300.jpg?quality=85&strip=all&w=770",
                Rating = 5,
                IsDeleted = false,
                Role = role1,
                Contacts = new List<Contact> { contact57, contact58, contact59, contact60, contact61, contact62, contact63 }
            };

            Account account10 = new Account()
            {
                Id = 10,
                Email = "admin@gmail.com",
                EmailConfirmed = true,
                DateCreated = new DateTime(2017, 08, 11, 22, 00, 00),
                Avatar = "http://www.nmc-corp.com/uploadedImages/NebraskaMachineryCompany/Logo_Library/NMC%20CAT%20Power%20Systems%20Mark.png",
                Rating = 5,
                IsDeleted = false,
                Role = role5,
                Contacts = new List<Contact> { contact64, contact65, contact66, contact67, contact68, contact69, contact70 }
            };

            Account account11 = new Account()
            {
                Id = 11,
                Email = "some@gmail.com",
                EmailConfirmed = true,
                DateCreated = new DateTime(2017, 08, 11, 22, 00, 00),
                Avatar = "https://www.ifak.eu/sites/www.ifak.eu/files/Mitarbeiterfotos/Steinmann.jpg",
                Rating = 5,
                IsDeleted = false,
                Role = role5,
                Contacts = new List<Contact> { contact71, contact72, contact73, contact74, contact75, contact76, contact77 }
            };

            Account account12 = new Account()
            {
                Id = 12,
                Email = "other@gmail.com",
                EmailConfirmed = true,
                DateCreated = new DateTime(2017, 08, 11, 22, 00, 00),
                Avatar = "https://www.kellerag.com/fileadmin/_processed_/csm_KAUSSEN-Sascha_95ed03675a.jpg",
                Rating = 5,
                IsDeleted = false,
                Role = role5,
                Contacts = new List<Contact> { contact78, contact79, contact80, contact81, contact82, contact83, contact84 }
            };

            Account account13 = new Account()
            {
                Id = 13,
                Email = "mine@gmail.com",
                EmailConfirmed = true,
                DateCreated = new DateTime(2017, 08, 11, 22, 00, 00),
                Avatar = "http://top101news.com/wp-content/uploads/2016/10/Paul-Walker-Top-Most-Popular-Persons-Who-Died-in-Road-Accident-2018.jpg",
                Rating = 5,
                IsDeleted = false,
                Role = role5,
                Contacts = new List<Contact> { contact85, contact86, contact87, contact88, contact89, contact90, contact91 }
            };

            context.Accounts.AddRange(new List<Account>() { account1, account2, account3, account4, account5, account6, account7, account8, account9, account10, account11, account12, account13 });


            #endregion

            #region SocialAccounts

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
                Id = 8,
                IsDeleted = false,
                Uid = "8",
                Provider = "Facebook",
                Account = account8
            };

            SocialAccount socialAccount9 = new SocialAccount()
            {
                Id = 9,
                IsDeleted = false,
                Uid = "9",
                Provider = "Twitter",
                Account = account9
            };

            SocialAccount socialAccount10 = new SocialAccount()
            {
                Id = 10,
                IsDeleted = false,
                Uid = "10",
                Provider = "Google",
                Account = account10
            };

            SocialAccount socialAccount11 = new SocialAccount()
            {
                Id = 11,
                IsDeleted = false,
                Uid = "11",
                Provider = "Facebook",
                Account = account11
            };

            SocialAccount socialAccount12 = new SocialAccount()
            {
                Id = 12,
                IsDeleted = false,
                Uid = "12",
                Provider = "Facebook",
                Account = account12
            };

            SocialAccount socialAccount13 = new SocialAccount()
            {
                Id = 13,
                IsDeleted = false,
                Uid = "13",
                Provider = "Twitter",
                Account = account13
            };


            context.SocialAccounts.AddRange(new List<SocialAccount>() { socialAccount1, socialAccount2, socialAccount3, socialAccount4, socialAccount5, socialAccount6, socialAccount7, socialAccount8, socialAccount9, socialAccount10, socialAccount11, socialAccount12, socialAccount13 });

            #endregion     

            #region Categories

            Category category1 = new Category()
            {
                Id = 1,
                Icon = "http://www.noorderpoort-samendoen.nl/wp-content/uploads/2015/10/naaispullen.jpg",
                IsDeleted = false,
                Description = "Sewing",
                Name = "Care of clothes",
            };

            Category category2 = new Category()
            {
                Id = 2,
                Icon = "https://phrasee.co/wp-content/uploads/2016/06/pets.jpg",
                IsDeleted = false,
                Description = "Pet",
                Name = "Pets care"
            };

            Category category3 = new Category()
            {
                Id = 3,
                Icon = "http://www.portlandgroupbd.com/wp-content/uploads/2016/06/t1.jpg",
                IsDeleted = false,
                Description = "Transportations",
                Name = "Car services"
            };

            Category category4 = new Category()
            {
                Id = 4,
                Icon = "http://pop.h-cdn.co/assets/cm/15/05/54cb5d94587ec_-_tallest-buildings-05-1214-lgn.jpg",
                IsDeleted = false,
                Description = "Buildings",
                Name = "Buider services"
            };

            Category category5 = new Category()
            {
                Id = 5,
                Icon = "http://photolifeway.com/images/futaji.png",
                IsDeleted = false,
                Description = "Media processing",
                Name = "Photo and Video"
            };

            Category category6 = new Category()
            {
                Id = 6,
                Icon = "https://ieg.worldbankgroup.org/Data/styles/inner_page_style/public/Evaluation/images/financial-viability-electricity.jpg?itok=CvUNQtjh",
                IsDeleted = false,
                Description = "Developing",
                Name = "Developer Service"
            };

            context.Categories.AddRange(new List<Category>() { category1, category2, category3, category4, category5, category6 });


            #endregion

            #region Subcategories

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


            #endregion

            #region Works

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


            #endregion

            #region Persons

            Person person1 = new Person()
            {
                Id = 1,
                Account = account1,
                Name = "George M.",
                MiddleName = "Couture",
                Birthday = new DateTime(1976, 08, 08),
                Gender = "male",
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
                Phone = "803-604-7259"
            };

            Person person2 = new Person()
            {
                Id = 2,
                Account = account2,
                Name = "John A.",
                MiddleName = "Wagoner",
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
                Phone = "646-319-4092"
            };

            Person person3 = new Person()
            {
                Id = 3,
                Account = account3,
                Name = "Kim A.",
                MiddleName = "King",
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
                Phone = "206-237-6702"
            };

            Person person4 = new Person()
            {
                Id = 4,
                Account = account4,
                Name = "Misha",
                MiddleName = "Bakun",
                Surname = "Petrovych",
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
                Phone = "+38-095-366-11-37",
            };

            Person person5 = new Person()
            {
                Id = 5,
                Account = account5,
                Name = "Larysa",
                MiddleName = "Yashkina",
                Surname = "Alexandrovna",
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
                MiddleName = "Stasenko",
                Surname = "Mykolayovich",
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
                MiddleName = "Panchuk",
                Surname = "Ivanovich",
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
                Account = account11,
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
                Account = account12,
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
                Account = account13,
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

            #endregion
            
            #region PortfolioItems
            
            PortfolioItem portfolioItem1 = new PortfolioItem()
            {
                Image = "https://camo.githubusercontent.com/f8ea5eab7494f955e90f60abc1d13f2ce2c2e540/68747470733a2f2f662e636c6f75642e6769746875622e636f6d2f6173736574732f323037383234352f3235393331332f35653833313336322d386362612d313165322d383435332d6536626439353663383961342e706e67",
                IsDeleted = false,
                Subcategory = subcategory1,
                WorkType = work1
            };

            context.PortfolioItems.AddRange(new List<PortfolioItem>() { portfolioItem1 });

            #endregion

            #region Vendors            

            Vendor vendor1 = new Vendor()
            {
                Id = 1,
                Experience = 7.5,
                IsDeleted = false,
                Position = "Head",
                Works = new List<Work>() { work1, work2, work3 },
                ExWork = "Dishwasher",
                Person = person1,
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
            };

            Vendor vendor7 = new Vendor()
            {
                Id = 7,
                Experience = 0.5,
                IsDeleted = false,
                Position = "Python Junior Dev",
                Works = new List<Work>() { work6, work5},
                ExWork = "Cook",
                Person = person7,
            };

            context.Vendors.AddRange(new List<Vendor>() { vendor1, vendor2, vendor3, vendor4, vendor5, vendor6, vendor7 });


            #endregion

            #region Companies

            Company company1 = new Company()
            {
                Id = 1,
                Account = account8,
                Name = "TURBOCAT 9000 Inc.",
                Description = "We have been engaged in feline business for more than 10 years",
                FoundationDate = new DateTime(2006, 08, 11, 00, 58, 16),
                Director = "Alex Moren",
                Staff = 2,
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
                Vendors = new List<Vendor>() { vendor1, vendor2 },
                Categories = new List<Category> { category1, category2}

            };

            Company company2 = new Company()
            {
                Id = 2,
                Name = "CAT Inc.",
                Description = "After our services your pets will be the happiest",
                Account = account9,
                FoundationDate = new DateTime(2017, 08, 12, 10, 36, 16),
                Director = "John Snow",
                Staff = 2,
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
                Vendors = new List<Vendor>() { vendor4, vendor5},
                Categories = new List<Category> { category3, category4 }
            };

            Company company3 = new Company()
            {
                Id = 3,
                Name = "Tommy Catfilger Inc.",
                Description = "You will want to use our services more and more",
                Account = account10,
                FoundationDate = new DateTime(2012, 08, 08, 13, 11, 16),
                Director = "Jaime Lannister",
                Staff = 1,
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
                Vendors = new List<Vendor>() { vendor6 },
                Categories = new List<Category> { category5, category6 }
            };

            context.Companies.AddRange(new List<Company>() { company1, company2, company3 });


            #endregion

            #region Customers

            Customer customer1 = new Customer()
            {
                Id = 1,
                IsDeleted = false,
                Person = person8
            };

            Customer customer2 = new Customer()
            {
                Id = 2,
                IsDeleted = false,
                Person = person9
            };

            Customer customer3 = new Customer()
            {
                Id = 3,
                IsDeleted = false,
                Person = person10
            };

            context.Customers.AddRange(new List<Customer>() { customer1, customer2, customer3 });


            #endregion

            #region Books

            Book book1 = new Book()
            {
                Id = 1,
                Date = new DateTime(2017, 08, 12, 16, 46, 16),
                IsDeleted = false,
                Work = work1,
                Description = "clean clothes and irom it, output 3 spots ",
                Company = company1,
                Status = BookStatus.InProgress,
                Customer = customer1
            };

            Book book2 = new Book()
            {
                Id = 2,
                Date = new DateTime(2017, 08, 17, 5, 30, 00),
                IsDeleted = false,
                Work = work2,
                Vendor = vendor1,
                Description = "Make a photoset when sunrise",
                Status = BookStatus.Accepted,
                Customer = customer2,
                Location = new Location()
                {
                    Adress = "Lebedeva-Kumacha 7a str.",
                    City = "Kiev",
                    Latitude = 50.437,
                    Longitude = 30.439,
                    IsDeleted = false,
                    PostIndex = "03110"
                }
            };

            Book book3 = new Book()
            {
                Id = 3,
                Date = new DateTime(2017, 08, 15, 18, 04, 00),
                IsDeleted = false,
                Work = work3,
                Description = "Care about cat",
                Status = BookStatus.Finished,
                Vendor = vendor3,
                Location = new Location()
                {
                    Adress = "Lebedeva-Kumacha 7a str.",
                    City = "Kiev",
                    Latitude = 50.437,
                    Longitude = 30.439,
                    IsDeleted = false,
                    PostIndex = "03110"
                },
                Customer = customer3
            };

            Book book4 = new Book()
            {
                Id = 4,
                Date = new DateTime(2017, 08, 15, 18, 04, 00),
                IsDeleted = false,
                Work = work4,
                Description = "rebuild 3 walls",
                Status = BookStatus.Accepted,
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
                Status = BookStatus.Accepted,
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
                Status = BookStatus.InProgress,
                Vendor = vendor1,
                Customer = customer1,
                Location = new Location()
                {
                    Adress = "Lebedeva-Kumacha 7a str.",
                    City = "Kiev",
                    Latitude = 50.437,
                    Longitude = 30.439,
                    IsDeleted = false,
                    PostIndex = "03110"
                }
            };

            context.Books.AddRange(new List<Book>() { book1, book2, book3, book4, book5, book6 });


            #endregion

            #region Reviews

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

            context.Reviews.AddRange(new List<Review>() { review1, review2, review3, review4, review5 });


            #endregion

            #region History
           History history1 = new History()
            {
                Id = 1,
                Date = new DateTime(2017, 05, 12, 16, 46, 16),
                DateFinished = new DateTime(2017,05,13,11,11,11),
                BookDescription = "fix dripping tap toworrow",
                WorkDescription = "fix tap",
                CategoryName = "House Work",
                Vendor = vendor1,
                IsDeleted = false,
                Review = review1,
                SubcategoryName = "",
                Customer = customer1
            };
            context.Histories.AddRange(new List<History>() { history1 });

            #endregion
            context.SaveChanges();

            base.Seed(context);
        }
    }
}
