using System;
using System.Collections.Generic;
using System.Data.Entity;

using Unicorn.DataAccess.Entities;
using Unicorn.DataAccess.Entities.Enum;

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
                },
                Type = Entities.Enum.RoleType.Guest
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
                },
                Type = Entities.Enum.RoleType.Customer
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
                },
                Type = Entities.Enum.RoleType.Vendor
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
                },
                Type = Entities.Enum.RoleType.Company
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
                },
                Type = Entities.Enum.RoleType.Admin
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
            
            Contact contact92 = new Contact { IsDeleted = false, Provider = phone, Value = "+1 415 670 9788" };
            Contact contact93 = new Contact { IsDeleted = false, Provider = phone, Value = "+1 415 670 9788" };
            Contact contact94 = new Contact { IsDeleted = false, Provider = phone, Value = "+1 415 670 9788" };
            Contact contact95 = new Contact { IsDeleted = false, Provider = email, Value = "turbocat_9000@gmail.com" };
            Contact contact96 = new Contact { IsDeleted = false, Provider = skype, Value = "turbocat_9000" };
            Contact contact97 = new Contact { IsDeleted = false, Provider = telegram, Value = "turbocat_9000" };
            Contact contact98 = new Contact { IsDeleted = false, Provider = facebook, Value = "turbocat_9000" };

            Contact contact99 = new Contact { IsDeleted = false, Provider = phone, Value = "+1 415 670 9788" };
            Contact contact100 = new Contact { IsDeleted = false, Provider = phone, Value = "+1 415 670 9788" };
            Contact contact101 = new Contact { IsDeleted = false, Provider = phone, Value = "+1 415 670 9788" };
            Contact contact102 = new Contact { IsDeleted = false, Provider = email, Value = "turbocat_9000@gmail.com" };
            Contact contact103 = new Contact { IsDeleted = false, Provider = skype, Value = "turbocat_9000" };
            Contact contact104 = new Contact { IsDeleted = false, Provider = telegram, Value = "turbocat_9000" };
            Contact contact105 = new Contact { IsDeleted = false, Provider = facebook, Value = "turbocat_9000" };

            Contact contact106 = new Contact { IsDeleted = false, Provider = phone, Value = "+1 415 670 9788" };
            Contact contact107 = new Contact { IsDeleted = false, Provider = phone, Value = "+1 415 670 9788" };
            Contact contact108 = new Contact { IsDeleted = false, Provider = phone, Value = "+1 415 670 9788" };
            Contact contact109 = new Contact { IsDeleted = false, Provider = email, Value = "turbocat_9000@gmail.com" };
            Contact contact110 = new Contact { IsDeleted = false, Provider = skype, Value = "turbocat_9000" };
            Contact contact111 = new Contact { IsDeleted = false, Provider = telegram, Value = "turbocat_9000" };
            Contact contact112 = new Contact { IsDeleted = false, Provider = facebook, Value = "turbocat_9000" };

            Contact contact113 = new Contact { IsDeleted = false, Provider = phone, Value = "+1 415 670 9788" };
            Contact contact114 = new Contact { IsDeleted = false, Provider = phone, Value = "+1 415 670 9788" };
            Contact contact115 = new Contact { IsDeleted = false, Provider = phone, Value = "+1 415 670 9788" };
            Contact contact116 = new Contact { IsDeleted = false, Provider = email, Value = "turbocat_9000@gmail.com" };
            Contact contact117 = new Contact { IsDeleted = false, Provider = skype, Value = "turbocat_9000" };
            Contact contact118 = new Contact { IsDeleted = false, Provider = telegram, Value = "turbocat_9000" };
            Contact contact119 = new Contact { IsDeleted = false, Provider = facebook, Value = "turbocat_9000" };

            Contact contact120 = new Contact { IsDeleted = false, Provider = phone, Value = "+1 415 670 9788" };
            Contact contact121 = new Contact { IsDeleted = false, Provider = phone, Value = "+1 415 670 9788" };
            Contact contact122 = new Contact { IsDeleted = false, Provider = phone, Value = "+1 415 670 9788" };
            Contact contact123 = new Contact { IsDeleted = false, Provider = email, Value = "turbocat_9000@gmail.com" };
            Contact contact124 = new Contact { IsDeleted = false, Provider = skype, Value = "turbocat_9000" };
            Contact contact125 = new Contact { IsDeleted = false, Provider = telegram, Value = "turbocat_9000" };
            Contact contact126 = new Contact { IsDeleted = false, Provider = facebook, Value = "turbocat_9000" };

            context.Contacts.AddRange(new List<Contact>()
            {
                contact1,  contact11, contact21, contact31, contact41, contact51, contact61, contact71, contact81, contact91,
                contact2,  contact12, contact22, contact32, contact42, contact52, contact62, contact72, contact82, contact92,
                contact3,  contact13, contact23, contact33, contact43, contact53, contact63, contact73, contact83, contact93,
                contact4,  contact14, contact24, contact34, contact44, contact54, contact64, contact74, contact84, contact94,
                contact5,  contact15, contact25, contact35, contact45, contact55, contact65, contact75, contact85, contact95,
                contact6,  contact16, contact26, contact36, contact46, contact56, contact66, contact76, contact86, contact96,
                contact7,  contact17, contact27, contact37, contact47, contact57, contact67, contact77, contact87, contact97,
                contact8,  contact18, contact28, contact38, contact48, contact58, contact68, contact78, contact88, contact98,
                contact9,  contact19, contact29, contact39, contact49, contact59, contact69, contact79, contact89, contact99,
                contact10, contact20, contact30, contact40, contact50, contact60, contact70, contact80, contact90, contact100,

                contact101, contact111, contact121,
                contact102, contact112, contact122,
                contact103, contact113, contact123,
                contact104, contact114, contact124,
                contact105, contact115, contact125,
                contact106, contact116, contact126,
                contact107, contact117,
                contact108, contact118,
                contact109, contact119,
                contact110, contact120,
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
                IsDeleted = false,
                Role = role3,
                Contacts = new List<Contact> { contact8, contact9, contact10, contact11, contact12, contact13, contact14 }
            };

            Account account3 = new Account()
            {
                Id = 3,
                Email = "shnurenko_worker@gmail.com",
                EmailConfirmed = true,
                DateCreated = new DateTime(2017, 08, 11, 00, 57, 16),
                Avatar = "http://revivre.ee/wp-content/uploads/2013/07/photodune-4276142-smiling-portraits-xl_411.jpg",
                IsDeleted = false,
                Role = role3,
                Contacts = new List<Contact> { contact15, contact16, contact17, contact18, contact19, contact20, contact21 }

            };

            Account account4 = new Account()
            {
                Id = 4,
                Email = "catcare_company@gmail.com",
                EmailConfirmed = true,
                DateCreated = new DateTime(2017, 08, 11, 00, 58, 16),
                Avatar = "http://coodiv.net/project/skyhost/HTML/img/quote/44-3.jpg",
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
                IsDeleted = false,
                Role = role3,
                Contacts = new List<Contact> { contact29, contact30, contact31, contact32, contact33, contact34, contact35 }
            };

            Account account6 = new Account()
            {
                Id = 6,
                Email = "driving_company@gmail.com",
                EmailConfirmed = true,
                DateCreated = new DateTime(2017, 08, 11, 22, 57, 15),
                Avatar = "http://1.bp.blogspot.com/_A1-gDagxbUM/S_lmQ5ZiIwI/AAAAAAAAAH4/tfLxn-zR314/s1600/Tom+Cruise.jpg",
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
                IsDeleted = false,
                Role = role3,
                Contacts = new List<Contact> { contact43, contact44, contact45, contact46, contact47, contact48, contact49 }
            };

            Account account8 = new Account()
            {
                Id = 8,
                Email = "shmirmasha_worker@gmail.com",
                EmailConfirmed = true,
                DateCreated = new DateTime(2017, 08, 12, 02, 14, 15),
                Avatar = "https://articlemanager.blob.core.windows.net/article-images/2_your-logo-design.jpg",
                IsDeleted = false,
                Role = role4,
                Contacts = new List<Contact> { contact50, contact51, contact52, contact53, contact54, contact55, contact56 }
            };

            Account account9 = new Account()
            {
                Id = 9,
                Email = "vitykostyuban_vendor@gmail.com",
                EmailConfirmed = true,
                DateCreated = new DateTime(2017, 08, 11, 22, 57, 15),
                Avatar = "https://tribkcpq.files.wordpress.com/2016/07/s032965871-300.jpg?quality=85&strip=all&w=770",
                IsDeleted = false,
                Role = role4,
                Contacts = new List<Contact> { contact57, contact58, contact59, contact60, contact61, contact62, contact63 }
            };

            Account account10 = new Account()
            {
                Id = 10,
                Email = "admin@gmail.com",
                EmailConfirmed = true,
                DateCreated = new DateTime(2017, 08, 11, 22, 00, 00),
                Avatar = "http://www.nmc-corp.com/uploadedImages/NebraskaMachineryCompany/Logo_Library/NMC%20CAT%20Power%20Systems%20Mark.png",
                IsDeleted = false,
                Role = role4,
                Contacts = new List<Contact> { contact64, contact65, contact66, contact67, contact68, contact69, contact70 }
            };

            Account account11 = new Account()
            {
                Id = 11,
                Email = "some@gmail.com",
                EmailConfirmed = true,
                DateCreated = new DateTime(2017, 08, 11, 22, 00, 00),
                Avatar = "https://www.ifak.eu/sites/www.ifak.eu/files/Mitarbeiterfotos/Steinmann.jpg",
                IsDeleted = false,
                Role = role2,
                Contacts = new List<Contact> { contact71, contact72, contact73, contact74, contact75, contact76, contact77 }
            };

            Account account12 = new Account()
            {
                Id = 12,
                Email = "other@gmail.com",
                EmailConfirmed = true,
                DateCreated = new DateTime(2017, 08, 11, 22, 00, 00),
                Avatar = "https://www.kellerag.com/fileadmin/_processed_/csm_KAUSSEN-Sascha_95ed03675a.jpg",
                IsDeleted = false,
                Role = role2,
                Contacts = new List<Contact> { contact78, contact79, contact80, contact81, contact82, contact83, contact84 }
            };

            Account account13 = new Account()
            {
                Id = 13,
                Email = "mine@gmail.com",
                EmailConfirmed = true,
                DateCreated = new DateTime(2017, 08, 11, 22, 00, 00),
                Avatar = "http://top101news.com/wp-content/uploads/2016/10/Paul-Walker-Top-Most-Popular-Persons-Who-Died-in-Road-Accident-2018.jpg",
                IsDeleted = false,
                Role = role2,
                Contacts = new List<Contact> { contact85, contact86, contact87, contact88, contact89, contact90, contact91 }
            };
            
            Account account14 = new Account()
            {
                Id = 14,
                Email = "google@gmail.com",
                EmailConfirmed = true,
                DateCreated = new DateTime(2017, 08, 01, 20, 00, 00),
                Avatar = "http://techbox.dennikn.sk/wp-content/uploads/2016/02/Google-logo-01.jpg",
                IsDeleted = false,
                Role = role4,
                Contacts = new List<Contact> { contact92, contact93, contact94, contact95, contact96, contact97, contact98 }
            };

            Account account15 = new Account()
            {
                Id = 15,
                Email = "siemens@gmail.com",
                EmailConfirmed = true,
                DateCreated = new DateTime(2017, 08, 23, 20, 00, 00),
                Avatar = "http://vneconomictimes.com/cache/uploads/media/images/2016/MAY/Siemens_logo_640_auto.jpg",
                IsDeleted = false,
                Role = role4,
                Contacts = new List<Contact> { contact99, contact100, contact101, contact102, contact103, contact104, contact105 }
            };

            Account account16 = new Account()
            {
                Id = 16,
                Email = "dell@gmail.com",
                EmailConfirmed = true,
                DateCreated = new DateTime(2017, 08, 19, 06, 00, 00),
                Avatar = "http://www.geekets.com/wp-content/uploads/2013/01/dell_logo020307.jpg",
                IsDeleted = false,
                Role = role4,
                Contacts = new List<Contact> { contact106, contact107, contact108, contact109, contact110, contact111, contact112 }
            };

            Account account17 = new Account()
            {
                Id = 17,
                Email = "mcdonalds@gmail.com",
                EmailConfirmed = true,
                DateCreated = new DateTime(2017, 08, 14, 16, 00, 00),
                Avatar = "http://cdn.playbuzz.com/cdn/21053f7d-f84d-4cb5-85d5-387fbb08692a/1c8e0442-5d13-435e-aa93-85b3804cfbc5.jpg",
                IsDeleted = false,
                Role = role4,
                Contacts = new List<Contact> { contact113, contact114, contact115, contact116, contact117, contact118, contact119 }
            };

            Account account18 = new Account()
            {
                Id = 18,
                Email = "novaposhta@gmail.com",
                EmailConfirmed = true,
                DateCreated = new DateTime(2017, 08, 11, 12, 00, 00),
                Avatar = "http://open4business.com.ua/wp-content/uploads/2015/07/NovaPoshta-1-800x494.jpg",
                IsDeleted = false,
                Role = role4,
                Contacts = new List<Contact> { contact120, contact121, contact122, contact123, contact124, contact125, contact126 }
            };

            context.Accounts.AddRange(new List<Account>()
            {
                account1, account2, account3, account4, account5, account6, account7, account8, account9, account10,
                account11, account12, account13, account14, account15, account16, account17, account18,
            });


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
            
            SocialAccount socialAccount14 = new SocialAccount()
            {
                Id = 14,
                IsDeleted = false,
                Uid = "14",
                Provider = "Google",
                Account = account14
            };

            SocialAccount socialAccount15 = new SocialAccount()
            {
                Id = 15,
                IsDeleted = false,
                Uid = "15",
                Provider = "Google",
                Account = account15
            };

            SocialAccount socialAccount16 = new SocialAccount()
            {
                Id = 16,
                IsDeleted = false,
                Uid = "16",
                Provider = "Google",
                Account = account16
            };

            SocialAccount socialAccount17 = new SocialAccount()
            {
                Id = 17,
                IsDeleted = false,
                Uid = "17",
                Provider = "Google",
                Account = account17
            };

            SocialAccount socialAccount18 = new SocialAccount()
            {
                Id = 18,
                IsDeleted = false,
                Uid = "18",
                Provider = "Google",
                Account = account18
            };
            context.SocialAccounts.AddRange(new List<SocialAccount>()
            {
                socialAccount1,  socialAccount11,
                socialAccount2,  socialAccount12,
                socialAccount3,  socialAccount13,
                socialAccount4,  socialAccount14,
                socialAccount5,  socialAccount15,
                socialAccount6,  socialAccount16,
                socialAccount7,  socialAccount17,
                socialAccount8,  socialAccount18,
                socialAccount9,
                socialAccount10,   
            });

            #endregion     

            #region Categories

            Category category1 = new Category()
            {
                Id = 1,
                Icon = "http://www.noorderpoort-samendoen.nl/wp-content/uploads/2015/10/naaispullen.jpg",
                IsDeleted = false,
                Description = "Sewing",
                Name = "Care of clothes",
                Tags = @""
            };

            Category category2 = new Category()
            {
                Id = 2,
                Icon = "https://phrasee.co/wp-content/uploads/2016/06/pets.jpg",
                IsDeleted = false,
                Description = "Pet",
                Name = "Pets care",
                Tags = @"care,scratch,caregiver,infirmary,healthcare,health,caring,caregiving,elderly,paramedic,
                        clinic,nursery,convalescence,lactation,profession,infant,retirement,breastfeeding,medic,resting,
                        accommodation,pension,feeding,corpsman,lactating,rest,suckler,attendant,weaning,conservation,
                        endowment,ranger,upbringing,orderly,guarding,infancy,attentionbreast-feeding,
                        health-care,nursing"
            };

            Category category3 = new Category()
            {
                Id = 3,
                Icon = "http://www.portlandgroupbd.com/wp-content/uploads/2016/06/t1.jpg",
                IsDeleted = false,
                Description = "Transportations",
                Name = "Car services",
                Tags = @""
            };

            Category category4 = new Category()
            {
                Id = 4,
                Icon = "http://pop.h-cdn.co/assets/cm/15/05/54cb5d94587ec_-_tallest-buildings-05-1214-lgn.jpg",
                IsDeleted = false,
                Description = "Buildings",
                Name = "Buider services",
                Tags = @""
            };

            Category category5 = new Category()
            {
                Id = 5,
                Icon = "http://photolifeway.com/images/futaji.png",
                IsDeleted = false,
                Description = "Media processing",
                Name = "Photo and Video",
                Tags = @""
            };

            Category category6 = new Category()
            {
                Id = 6,
                Icon = "https://ieg.worldbankgroup.org/Data/styles/inner_page_style/public/Evaluation/images/financial-viability-electricity.jpg?itok=CvUNQtjh",
                IsDeleted = false,
                Description = "Developing",
                Name = "Developer Service",
                Tags = @""
            };

            Category category7 = new Category()
            {
                Id = 7,
                Icon = "http://www.shopcusa.com/wp-content/uploads/2015/07/Computer-parts.jpg",
                IsDeleted = false,
                Description = "Electronic parts supplier",
                Name = "Electronic parts",
                Tags = @""
            };

            Category category8 = new Category()
            {
                Id = 8,
                Icon = "http://images.all-free-download.com/images/graphicthumb/fast_food_icons_set_vector_graphics_548786.jpg",
                IsDeleted = false,
                Description = "Food and beverages",
                Name = "Food service",
                Tags = @""
            };

            Category category9 = new Category()
            {
                Id = 9,
                Icon = "https://cdn.dribbble.com/users/12733/screenshots/2705729/delivery_truck_icon_1x.png",
                IsDeleted = false,
                Description = "Package delivery",
                Name = "Delivery service",
                Tags = @""
            };

            context.Categories.AddRange(new List<Category>()
            {
                category1, category2, category3, category4, category5, category6, category7, category8, category9
            });


            #endregion

            #region Subcategories

            Subcategory subcategory1 = new Subcategory()
            {
                Id = 1,
                Description = "Wash your clothes and dry clothes",
                Name = "Washing",
                IsDeleted = false,
                Category = category1,
                Tags = @""
            };

            Subcategory subcategory2 = new Subcategory()
            {
                Id = 2,
                Description = "care with lots of love about your cat",
                Name = "Cats care",
                IsDeleted = false,
                Category = category2,
                Tags = @"cat,kitten,puss,pussycat,pot,pool,pussy,jackpot,kitty-cat,pet,cats,poodle,cub,sweetie,beaver,
                        Koko,bimbo,commie,coconut,baby,bucket,spade,coco,geezer,mom,little,boy,playboy,Cathy,rug,
                        Katie,bogeyman,wuss,petit,cold,nice,kath,hood,chat,stop"
            };

            Subcategory subcategory3 = new Subcategory()
            {
                Id = 3,
                Description = "Subacategory lory service",
                Name = "Lory service",
                IsDeleted = false,
                Category = category3,
                Tags = @""
            };

            Subcategory subcategory4 = new Subcategory()
            {
                Id = 4,
                Description = "Your appartment repair",
                Name = "Painting and plastering work",
                IsDeleted = false,
                Category = category4,
                Tags = @""
            };

            Subcategory subcategory5 = new Subcategory()
            {
                Id = 5,
                Description = "Make your own photoset",
                Name = "Photosession",
                IsDeleted = false,
                Category = category5,
                Tags = @""
            };

            Subcategory subcategory6 = new Subcategory()
            {
                Id = 6,
                Description = "Web Development",
                Name = "Web Development",
                IsDeleted = false,
                Category = category6,
                Tags = @""
            };

            Subcategory subcategory7 = new Subcategory()
            {
                Id = 7,
                Description = "Computer parts",
                Name = "Computer parts",
                IsDeleted = false,
                Category = category7,
                Tags = @""
            };

            Subcategory subcategory8 = new Subcategory()
            {
                Id = 8,
                Description = "Fast food",
                Name = "Fast food",
                IsDeleted = false,
                Category = category8,
                Tags = @""
            };

            Subcategory subcategory9 = new Subcategory()
            {
                Id = 9,
                Description = "Package express delivery",
                Name = "Express delivery",
                IsDeleted = false,
                Category = category9,
                Tags = @""
            };

            context.Subcategories.AddRange(new List<Subcategory>()
            {
                subcategory1, subcategory2, subcategory3, subcategory4, subcategory5, subcategory6, subcategory7, subcategory8, subcategory9
            });


            #endregion

            #region Works

            Work work1 = new Work()
            {
                Id = 1,
                Icon = "http://homecareservices.ru/blog/wp-content/uploads/2014/10/CollectionShot.jpg",
                Description = "Care of clothes",
                IsDeleted = false,
                Name = "laundress",
                Subcategory = subcategory1,
                Orders = 10
            };

            Work work2 = new Work()
            {
                Id = 2,
                Icon = "http://blog.creativelive.com/wp-content/uploads/2015/06/photographer-692035_640.jpg",
                Description = "Photosession, wedding photosessions etc.",
                IsDeleted = false,
                Name = "Photographer",
                Subcategory = subcategory5,
                Orders = 20
            };

            Work work3 = new Work()
            {
                Id = 3,
                Icon = "http://www.sagevets.com.au/images/vet-and-cat.jpg",
                Description = "Care of your pets",
                IsDeleted = false,
                Name = "Vet",
                Subcategory = subcategory2,
                Orders = 30
            };

            Work work4 = new Work()
            {
                Id = 4,
                Icon = "http://www.samuraibuilders.co.nz/wp-content/uploads/2015/05/topslider2-a1.jpg",
                Description = "Appartmen repair",
                IsDeleted = false,
                Name = "Builder",
                Subcategory = subcategory4,
                Orders = 40
            };

            Work work5 = new Work()
            {
                Id = 5,
                Icon = "https://www.fmcsa.dot.gov/sites/fmcsa.dot.gov/files/TruckDriver1%5B1%5D.jpg",
                Description = "Driving services",
                IsDeleted = false,
                Name = "Driver",
                Subcategory = subcategory3,
                Orders = 50
            };

            Work work6 = new Work()
            {
                Id = 6,
                Icon = "http://www.startupguys.net/wp-content/uploads/2016/06/how-to-find-good-software-developer.jpg",
                Description = "C# senior dev",
                IsDeleted = false,
                Name = "Developer",
                Subcategory = subcategory6,
                Orders = 60
            };

            Work work7 = new Work()
            {
                Id = 7,
                Icon = "http://www.qualix.co.in/wp-content/uploads/2016/01/servicesInformationTechnologyBanner.jpg",
                Description = "Services and products development",
                IsDeleted = false,
                Name = "Services and products",
                Subcategory = subcategory6
            };

            Work work8 = new Work()
            {
                Id = 8,
                Icon = "https://www.misedesigns.com/wp-content/uploads/2016/09/restaurant-equipment-supplier.jpg",
                Description = "Supply industrial equipment and machines",
                IsDeleted = false,
                Name = "Equipment supplier",
                Subcategory = subcategory6
            };

            Work work9 = new Work()
            {
                Id = 9,
                Icon = "https://www.easypacelearning.com/design/images/content/personalcomputerhardwareparts.png",
                Description = "Computer parts supplier",
                IsDeleted = false,
                Name = "Computer parts supplier",
                Subcategory = subcategory7
            };

            Work work10 = new Work()
            {
                Id = 10,
                Icon = "https://cdn.shutterstock.com/shutterstock/videos/1811057/thumb/10.jpg",
                Description = "Сooking food",
                IsDeleted = false,
                Name = "Сooking food",
                Subcategory = subcategory8
            };

            Work work11 = new Work()
            {
                Id = 11,
                Icon = "http://www.phatinvestor.com/wp-content/uploads/2017/04/blog41.jpg",
                Description = "Courier service delivery",
                IsDeleted = false,
                Name = "Delivery",
                Subcategory = subcategory9
            };

            context.Works.AddRange(new List<Work>() { work1, work2, work3, work4, work5, work6, work7, work8, work9, work10, work11 });


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
                Works = new List<Work> { work1, work2}

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
                Works = new List<Work> { work3, work4 }
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
                Works = new List<Work> { work5, work6 }
            };

            Company company4 = new Company()
            {
                Id = 4,
                Name = "Google Inc.",
                Description = "Don't be evil",
                Account = account14,
                FoundationDate = new DateTime(2010, 08, 02, 10, 11, 16),
                Director = "Sergey Grin",
                Staff = 1,
                IsDeleted = false,
                Location = new Location()
                {
                    Adress = "Soborna St",
                    City = "Vinnitsa",
                    Latitude = 49.233256,                    
                    Longitude = 28.467229,
                    IsDeleted = false,
                    PostIndex = "21021"
                },
                Vendors = new List<Vendor>(),
                Works = new List<Work> { work7 }
            };

            Company company5 = new Company()
            {
                Id = 5,
                Name = "Siemens",
                Description = "Engenuity for life",
                Account = account15,
                FoundationDate = new DateTime(2012, 08, 08, 13, 11, 16),
                Director = "Rob Morris",
                Staff = 1,
                IsDeleted = false,
                Location = new Location()
                {
                    Adress = "Balkivs'ka St",
                    City = "Odessa",
                    Latitude = 46.44124,
                    Longitude = 30.715554,
                    IsDeleted = false,
                    PostIndex = "23445"
                },
                Vendors = new List<Vendor>(),
                Works = new List<Work> { work8 }
            };

            Company company6 = new Company()
            {
                Id = 6,
                Name = "Dell",
                Description = "The power to do more",
                Account = account16,
                FoundationDate = new DateTime(2009, 08, 09, 13, 11, 16),
                Director = "Alok Ohrie",
                Staff = 1,
                IsDeleted = false,
                Location = new Location()
                {
                    Adress = "Peremohy Ave, 82",
                    City = "Chernihiv",
                    Latitude = 51.492639,                    
                    Longitude = 31.292565,
                    IsDeleted = false,
                    PostIndex = "24326"
                },
                Vendors = new List<Vendor>(),
                Works = new List<Work> { work9 }
            };

            Company company7 = new Company()
            {
                Id = 7,
                Name = "McDonalds",
                Description = "I'm lovin' it",
                Account = account17,
                FoundationDate = new DateTime(2006, 01, 01, 13, 11, 16),
                Director = "Don Thompson",
                Staff = 1,
                IsDeleted = false,
                Location = new Location()
                {
                    Adress = "Pushkina Ave, 39А",
                    City = "Dnipro",
                    Latitude = 48.466027,
                    Longitude = 35.025498,
                    IsDeleted = false,
                    PostIndex = "27000"
                },
                Vendors = new List<Vendor>(),
                Works = new List<Work> { work10 }
            };

            Company company8 = new Company()
            {
                Id = 8,
                Name = "Nova poshta",
                Description = "Express delivery lider",
                Account = account18,
                FoundationDate = new DateTime(2014, 08, 08, 13, 11, 16),
                Director = "Sergey Kovalenko",
                Staff = 1,
                IsDeleted = false,
                Location = new Location()
                {
                    Adress = "Rus'ka St, 18",
                    City = "Ternopil",
                    Latitude = 49.550626,
                    Longitude = 25.590897,
                    IsDeleted = false,
                    PostIndex = "26911"
                },
                Vendors = new List<Vendor>(),
                Works = new List<Work> { work11 }
            };

            context.Companies.AddRange(new List<Company>()
            {
                company1, company2, company3, company4, company5, company6, company7, company8
            });


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
                Customer = customer1,               
                CustomerPhone = "+123 456 789",
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
                },
                CustomerPhone = "+380 111 222 333"
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
                Customer = customer3,
                CustomerPhone = "8 800 555 3535"
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
                Company = company2,
                CustomerPhone = "+380 50 40 30 20 10",
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


            Book book5 = new Book()
            {
                Id = 5,
                Date = new DateTime(2017, 08, 16, 08, 00, 00),
                IsDeleted = false,
                Work = work5,
                Description = "Take out the trash",
                Status = BookStatus.Accepted,
                Customer = customer1,
                Company = company3,
                CustomerPhone = "02",
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
                },
                CustomerPhone = "+100 500"
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
                ToAccountId = 11,
                IsDeleted = false,
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

            #region Ratings
            Rating rating1 = new Rating()
            {
                Id = 1,
                Grade = 4,
                IsDeleted = false,
                Reciever = account11,
                Sender = account1
            };
            Rating rating2 = new Rating()
            {
                Id = 2,
                Grade = 3,
                IsDeleted = false,
                Reciever = account11,
                Sender = account2
            };

            Rating rating3 = new Rating()
            {
                Id = 3,
                Grade = 5,
                IsDeleted = false,
                Reciever = account2,
                Sender = account1
            };

            Rating rating4 = new Rating()
            {
                Id = 4,
                Grade = 1,
                IsDeleted = false,
                Reciever = account2,
                Sender = account3
            };

            Rating rating5 = new Rating()
            {
                Id = 5,
                Grade = 4,
                IsDeleted = false,
                Reciever = account9,
                Sender = account2
            };

            Rating rating6 = new Rating()
            {
                Id = 6,
                Grade = 3,
                IsDeleted = false,
                Reciever = account10,
                Sender = account3
            };
            context.Ratings.AddRange(new List<Rating>() { rating1, rating2, rating3, rating4, rating5, rating6 });
            #endregion
            context.SaveChanges();

            base.Seed(context);
        }
    }
}
