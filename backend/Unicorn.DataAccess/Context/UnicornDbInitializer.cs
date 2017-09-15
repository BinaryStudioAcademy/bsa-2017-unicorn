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
                Name = "linkedin",
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

            ContactProvider instagram = new ContactProvider()
            {
                Name = "instagram",
                IsDeleted = false,
                Type = "Social"
            };

            ContactProvider twitter = new ContactProvider()
            {
                Name = "twitter",
                IsDeleted = false,
                Type = "Social"
            };

            context.ContactProviders.AddRange(new List<ContactProvider>() { phone, email, skype, telegram, facebook, vk, linkedin });


            #endregion

            #region Contacts

            Contact contact1 = new Contact { IsDeleted = false, Provider = phone, Value = "+1 415 670 9788" };
            Contact contact2 = new Contact { IsDeleted = false, Provider = phone, Value = "+1 415 670 9788" };
            Contact contact3 = new Contact { IsDeleted = false, Provider = phone, Value = "+1 415 670 9788" };
            Contact contact4 = new Contact { IsDeleted = false, Provider = email, Value = "turbocat_9000@gmail.com" };
            Contact contact5 = new Contact { IsDeleted = false, Provider = skype, Value = "turbocat_9000" };
            Contact contact6 = new Contact { IsDeleted = false, Provider = telegram, Value = "turbocat_9000" };
            Contact contact7 = new Contact { IsDeleted = false, Provider = facebook, Value = "http://www.facebook.com/turbocat_9000" };

            Contact contact8 = new Contact { IsDeleted = false, Provider = phone, Value = "+1 234 567 0001" };
            Contact contact9 = new Contact { IsDeleted = false, Provider = phone, Value = "+1 234 567 0002" };
            Contact contact10 = new Contact { IsDeleted = false, Provider = phone, Value = "+1 234 567 0003" };
            Contact contact11 = new Contact { IsDeleted = false, Provider = email, Value = "Bused1983@superrito.com" };
            Contact contact12 = new Contact { IsDeleted = false, Provider = skype, Value = "Bused1983" };
            Contact contact13 = new Contact { IsDeleted = false, Provider = telegram, Value = "Bused1983" };
            Contact contact14 = new Contact { IsDeleted = false, Provider = facebook, Value = "http://www.facebook.com/Bused1983" };

            Contact contact15 = new Contact { IsDeleted = false, Provider = phone, Value = "+1 919-382-8350" };
            Contact contact16 = new Contact { IsDeleted = false, Provider = phone, Value = "+1 216-312-5238" };
            Contact contact17 = new Contact { IsDeleted = false, Provider = phone, Value = "+1 704-373-1282" };
            Contact contact18 = new Contact { IsDeleted = false, Provider = email, Value = "MaryJBartley@dayrep.com" };
            Contact contact19 = new Contact { IsDeleted = false, Provider = skype, Value = "MaryJBartley" };
            Contact contact20 = new Contact { IsDeleted = false, Provider = telegram, Value = "MaryJBartley" };
            Contact contact21 = new Contact { IsDeleted = false, Provider = facebook, Value = "http://www.facebook.com/MaryJBartley" };

            Contact contact22 = new Contact { IsDeleted = false, Provider = phone, Value = "+1 (816)567-3906" };
            Contact contact23 = new Contact { IsDeleted = false, Provider = phone, Value = "+1 (228)263-8701" };
            Contact contact24 = new Contact { IsDeleted = false, Provider = phone, Value = "+1 (207)438-4503" };
            Contact contact25 = new Contact { IsDeleted = false, Provider = email, Value = "LynnJCrouch@dayrep.com" };
            Contact contact26 = new Contact { IsDeleted = false, Provider = skype, Value = "LynnJCrouch" };
            Contact contact27 = new Contact { IsDeleted = false, Provider = telegram, Value = "LynnJCrouch" };
            Contact contact28 = new Contact { IsDeleted = false, Provider = facebook, Value = "http://www.facebook.com/LynnJCrouch" };

            Contact contact29 = new Contact { IsDeleted = false, Provider = phone, Value = "+1 (408)613-5255" };
            Contact contact30 = new Contact { IsDeleted = false, Provider = phone, Value = "+1 (702)397-9066" };
            Contact contact31 = new Contact { IsDeleted = false, Provider = phone, Value = "+1 (631)857-1783" };
            Contact contact32 = new Contact { IsDeleted = false, Provider = email, Value = "JeremyKHodgson@dayrep.com" };
            Contact contact33 = new Contact { IsDeleted = false, Provider = skype, Value = "JeremyKHodgson" };
            Contact contact34 = new Contact { IsDeleted = false, Provider = telegram, Value = "JeremyKHodgson" };
            Contact contact35 = new Contact { IsDeleted = false, Provider = facebook, Value = "http://www.facebook.com/JeremyKHodgson" };

            Contact contact36 = new Contact { IsDeleted = false, Provider = phone, Value = "+1 (210)960-3447" };
            Contact contact37 = new Contact { IsDeleted = false, Provider = phone, Value = "+1 (331)214-9586" };
            Contact contact38 = new Contact { IsDeleted = false, Provider = phone, Value = "+1 (731)414-6120" };
            Contact contact39 = new Contact { IsDeleted = false, Provider = email, Value = "JoaquinLSmith@armyspy.com" };
            Contact contact40 = new Contact { IsDeleted = false, Provider = skype, Value = "JoaquinLSmith" };
            Contact contact41 = new Contact { IsDeleted = false, Provider = telegram, Value = "JoaquinLSmith" };
            Contact contact42 = new Contact { IsDeleted = false, Provider = facebook, Value = "http://www.facebook.com/JoaquinLSmith" };

            Contact contact43 = new Contact { IsDeleted = false, Provider = phone, Value = "+1 (503)549-7864" };
            Contact contact44 = new Contact { IsDeleted = false, Provider = phone, Value = "+1 (405)779-9099" };
            Contact contact45 = new Contact { IsDeleted = false, Provider = phone, Value = "+1 (410)245-8385" };
            Contact contact46 = new Contact { IsDeleted = false, Provider = email, Value = "MarvinCKelley@dayrep.com" };
            Contact contact47 = new Contact { IsDeleted = false, Provider = skype, Value = "MarvinCKelley" };
            Contact contact48 = new Contact { IsDeleted = false, Provider = telegram, Value = "MarvinCKelley" };
            Contact contact49 = new Contact { IsDeleted = false, Provider = facebook, Value = "http://www.facebook.com/MarvinCKelley" };

            Contact contact50 = new Contact { IsDeleted = false, Provider = phone, Value = "+1 (509)435-1920" };
            Contact contact51 = new Contact { IsDeleted = false, Provider = phone, Value = "+1 (619)988-6607" };
            Contact contact52 = new Contact { IsDeleted = false, Provider = phone, Value = "+1 (586)201-8800" };
            Contact contact53 = new Contact { IsDeleted = false, Provider = email, Value = "MaryTCook@teleworm.us" };
            Contact contact54 = new Contact { IsDeleted = false, Provider = skype, Value = "MaryTCook" };
            Contact contact55 = new Contact { IsDeleted = false, Provider = telegram, Value = "MaryTCook" };
            Contact contact56 = new Contact { IsDeleted = false, Provider = facebook, Value = "http://www.facebook.com/MaryTCook" };

            Contact contact57 = new Contact { IsDeleted = false, Provider = phone, Value = "+1 (813)732-7204" };
            Contact contact58 = new Contact { IsDeleted = false, Provider = phone, Value = "+1 (281)872-1672" };
            Contact contact59 = new Contact { IsDeleted = false, Provider = phone, Value = "+1 (484)556-2961" };
            Contact contact60 = new Contact { IsDeleted = false, Provider = email, Value = "JosephineDBarnes@teleworm.us" };
            Contact contact61 = new Contact { IsDeleted = false, Provider = skype, Value = "JosephineDBarnes" };
            Contact contact62 = new Contact { IsDeleted = false, Provider = telegram, Value = "JosephineDBarnes" };
            Contact contact63 = new Contact { IsDeleted = false, Provider = facebook, Value = "http://www.facebook.com/JosephineDBarnes" };

            Contact contact64 = new Contact { IsDeleted = false, Provider = phone, Value = "+1 (423)525-9676" };
            Contact contact65 = new Contact { IsDeleted = false, Provider = phone, Value = "+1 (574)826-4167" };
            Contact contact66 = new Contact { IsDeleted = false, Provider = phone, Value = "+1 (508)805-8606" };
            Contact contact67 = new Contact { IsDeleted = false, Provider = email, Value = "PaulLFairbanks@dayrep.com" };
            Contact contact68 = new Contact { IsDeleted = false, Provider = skype, Value = "PaulLFairbanks" };
            Contact contact69 = new Contact { IsDeleted = false, Provider = telegram, Value = "PaulLFairbanks" };
            Contact contact70 = new Contact { IsDeleted = false, Provider = facebook, Value = "http://www.facebook.com/PaulLFairbanks" };

            Contact contact71 = new Contact { IsDeleted = false, Provider = phone, Value = "+1 (551)333-8972" };
            Contact contact72 = new Contact { IsDeleted = false, Provider = phone, Value = "+1 (402)883-3209" };
            Contact contact73 = new Contact { IsDeleted = false, Provider = phone, Value = "+1 (336)567-8609" };
            Contact contact74 = new Contact { IsDeleted = false, Provider = email, Value = "OllieJDurgan@armyspy.com" };
            Contact contact75 = new Contact { IsDeleted = false, Provider = skype, Value = "OllieJDurgan" };
            Contact contact76 = new Contact { IsDeleted = false, Provider = telegram, Value = "OllieJDurgan" };
            Contact contact77 = new Contact { IsDeleted = false, Provider = facebook, Value = "http://www.facebook.com/OllieJDurgan" };

            Contact contact78 = new Contact { IsDeleted = false, Provider = phone, Value = "+1 (503)549-8462" };
            Contact contact79 = new Contact { IsDeleted = false, Provider = phone, Value = "+1 (580)467-4669" };
            Contact contact80 = new Contact { IsDeleted = false, Provider = phone, Value = "+1 (206)358-3625" };
            Contact contact81 = new Contact { IsDeleted = false, Provider = email, Value = "CourtneyCGardner@jourrapide.com" };
            Contact contact82 = new Contact { IsDeleted = false, Provider = skype, Value = "CourtneyCGardner" };
            Contact contact83 = new Contact { IsDeleted = false, Provider = telegram, Value = "CourtneyCGardner" };
            Contact contact84 = new Contact { IsDeleted = false, Provider = facebook, Value = "http://www.facebook.com/CourtneyCGardner" };

            Contact contact85 = new Contact { IsDeleted = false, Provider = phone, Value = "+1 (408)873-6972" };
            Contact contact86 = new Contact { IsDeleted = false, Provider = phone, Value = "+1 (510)491-6720" };
            Contact contact87 = new Contact { IsDeleted = false, Provider = phone, Value = "+1 (660)323-7792" };
            Contact contact88 = new Contact { IsDeleted = false, Provider = email, Value = "CharleneLGarza@teleworm.us" };
            Contact contact89 = new Contact { IsDeleted = false, Provider = skype, Value = "CharleneLGarza" };
            Contact contact90 = new Contact { IsDeleted = false, Provider = telegram, Value = "CharleneLGarza" };
            Contact contact91 = new Contact { IsDeleted = false, Provider = facebook, Value = "http://www.facebook.com/CharleneLGarza" };
            
            Contact contact92 = new Contact { IsDeleted = false, Provider = phone, Value = "+1 (432)337-1105" };
            Contact contact93 = new Contact { IsDeleted = false, Provider = phone, Value = "+1 (214)694-8133" };
            Contact contact94 = new Contact { IsDeleted = false, Provider = phone, Value = "+1 (631)772-3167" };
            Contact contact95 = new Contact { IsDeleted = false, Provider = email, Value = "JuanaPRivas@jourrapide.com" };
            Contact contact96 = new Contact { IsDeleted = false, Provider = skype, Value = "JuanaPRivas" };
            Contact contact97 = new Contact { IsDeleted = false, Provider = telegram, Value = "JuanaPRivas" };
            Contact contact98 = new Contact { IsDeleted = false, Provider = facebook, Value = "http://www.facebook.com/JuanaPRivas" };

            Contact contact99 = new Contact { IsDeleted = false, Provider = phone, Value = "+1 (402)251-7214" };
            Contact contact100 = new Contact { IsDeleted = false, Provider = phone, Value = "+1 (269)787-9158" };
            Contact contact101 = new Contact { IsDeleted = false, Provider = phone, Value = "+1 (306)799-7775" };
            Contact contact102 = new Contact { IsDeleted = false, Provider = email, Value = "MaiBSmit@rhyta.com" };
            Contact contact103 = new Contact { IsDeleted = false, Provider = skype, Value = "MaiBSmit" };
            Contact contact104 = new Contact { IsDeleted = false, Provider = telegram, Value = "MaiBSmit" };
            Contact contact105 = new Contact { IsDeleted = false, Provider = facebook, Value = "http://www.facebook.com/MaiBSmit" };

            Contact contact106 = new Contact { IsDeleted = false, Provider = phone, Value = "+1 (609)807-8815" };
            Contact contact107 = new Contact { IsDeleted = false, Provider = phone, Value = "+1 (541)894-2265" };
            Contact contact108 = new Contact { IsDeleted = false, Provider = phone, Value = "+1 (234)601-9671" };
            Contact contact109 = new Contact { IsDeleted = false, Provider = email, Value = "DannyBShetler@armyspy.com" };
            Contact contact110 = new Contact { IsDeleted = false, Provider = skype, Value = "DannyBShetler" };
            Contact contact111 = new Contact { IsDeleted = false, Provider = telegram, Value = "DannyBShetler" };
            Contact contact112 = new Contact { IsDeleted = false, Provider = facebook, Value = "http://www.facebook.com/DannyBShetler" };

            Contact contact113 = new Contact { IsDeleted = false, Provider = phone, Value = "+1 (276)447-3655" };
            Contact contact114 = new Contact { IsDeleted = false, Provider = phone, Value = "+1 (724)448-4345" };
            Contact contact115 = new Contact { IsDeleted = false, Provider = phone, Value = "+1 (206)830-2452" };
            Contact contact116 = new Contact { IsDeleted = false, Provider = email, Value = "WilliamPFlores@armyspy.com" };
            Contact contact117 = new Contact { IsDeleted = false, Provider = skype, Value = "WilliamPFlores" };
            Contact contact118 = new Contact { IsDeleted = false, Provider = telegram, Value = "WilliamPFlores" };
            Contact contact119 = new Contact { IsDeleted = false, Provider = facebook, Value = "http://www.facebook.com/WilliamPFlores" };

            Contact contact120 = new Contact { IsDeleted = false, Provider = phone, Value = "+1 (475)675-8208" };
            Contact contact121 = new Contact { IsDeleted = false, Provider = phone, Value = "+1 (307)890-7566" };
            Contact contact122 = new Contact { IsDeleted = false, Provider = phone, Value = "+1 (414)267-4010" };
            Contact contact123 = new Contact { IsDeleted = false, Provider = email, Value = "BuddyGLivingston@jourrapide.com" };
            Contact contact124 = new Contact { IsDeleted = false, Provider = skype, Value = "BuddyGLivingston" };
            Contact contact125 = new Contact { IsDeleted = false, Provider = telegram, Value = "BuddyGLivingston" };
            Contact contact126 = new Contact { IsDeleted = false, Provider = facebook, Value = "http://www.facebook.com/BuddyGLivingston" };

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

            #region Locations
            Location location1 = new Location()
            {
                Id = 1,
                Adress = "Instytutska 13",
                Latitude = 50.447252,
                Longitude = 30.532936,
                IsDeleted = false,
                City = "Kiev",
                PostIndex = "61002"
            };

            Location location2 = new Location()
            {
                Id = 2,
                Adress = "Plosha Rinok 1",
                Latitude = 49.841377,                
                Longitude = 24.031699,
                IsDeleted = false,
                City = "Lviv",
                PostIndex = "71002"
            };

            Location location3 = new Location()
            {
                Id = 3,
                Adress = "Soborna 10",
                Latitude = 49.233213, 
                Longitude = 28.473971,
                IsDeleted = false,
                City = "Vinnitsya",
                PostIndex = "21002"
            };

            Location location4 = new Location()
            {
                Id = 4,
                Adress = "Svobodu 48/1",
                Latitude = 49.421336,
                Longitude = 26.988326,
                IsDeleted = false,
                City = "Khmel'nyts'kyi",
                PostIndex = "51002"
            };

            Location location5 = new Location()
            {
                Id = 5,
                Adress = "Zamkova 7",
                Latitude = 49.551702,
                Longitude = 25.58925,
                IsDeleted = false,
                City = "Ternopil",
                PostIndex = "41002"
            };

            Location location6 = new Location()
            {
                Id = 6,
                Adress = "Shevchenka 195",
                Latitude = 49.442816,
                Longitude = 32.062539,
                IsDeleted = false,
                City = "Cherkasy",
                PostIndex = "31002"
            };

            Location location7 = new Location()
            {
                Id = 7,
                Adress = "Yarmakova 15",
                Latitude = 49.794918,
                Longitude = 30.124247,
                IsDeleted = false,
                City = "Bila Tserkva",
                PostIndex = "81002"
            };

            Location location8 = new Location()
            {
                Id = 8,
                Adress = "Barikadna 4A",
                Latitude = 48.462562,                
                Longitude = 35.052371,
                IsDeleted = false,
                City = "Dnipro",
                PostIndex = "91002"
            };

            Location location9 = new Location()
            {
                Id = 9,
                Adress = "Universitetska 23",
                Latitude = 49.989473,
                Longitude = 36.230405,
                IsDeleted = false,
                City = "Kharkiv",
                PostIndex = "71102"
            };

            Location location10 = new Location()
            {
                Id = 10,
                Adress = "Kipornosa 23",
                Latitude = 51.491648,
                Longitude = 31.293969,
                IsDeleted = false,
                City = "Chernihiv",
                PostIndex = "41032"
            };

            Location location11 = new Location()
            {
                Id = 11,
                Adress = "Kondratiyka 5",
                Latitude = 50.750931,
                Longitude = 25.320614,
                IsDeleted = false,
                City = "Luts'k",
                PostIndex = "11502"
            };

            Location location12 = new Location()
            {
                Id = 12,
                Adress = "Chervona 11A",
                Latitude = 49.823970,
                Longitude = 24.006666,
                IsDeleted = false,
                City = "Lviv",
                PostIndex = "71014"
            };


            Location location13 = new Location()
            {
                Id = 13,
                Adress = "Zelena 21",
                Latitude = 49.833720,
                Longitude = 24.038185,
                IsDeleted = false,
                City = "Lviv",
                PostIndex = "71054"
            };

            Location location14 = new Location()
            {
                Id = 14,
                Adress = "Krytogirna 7",
                Latitude = 49.791558,
                Longitude = 30.141250,
                IsDeleted = false,
                City = "Bila Tserkva",
                PostIndex = "81032"
            };

            Location location15 = new Location()
            {
                Id = 15,
                Adress = "Borova 10A",
                Latitude = 50.413609,
                Longitude = 30.670130,
                IsDeleted = false,
                City = "Kiev",
                PostIndex = "61302"
            };
            Location location16 = new Location()
            {
                Id = 16,
                Adress = "Kioto 27",
                Latitude = 50.467732,
                Longitude = 30.650091,
                IsDeleted = false,
                City = "Kiev",
                PostIndex = "65002"
            };
            Location location17 = new Location()
            {
                Id = 17,
                Adress = "Nagirna 14",
                Latitude = 50.473794,
                Longitude = 30.481694,
                IsDeleted = false,
                City = "Kiev",
                PostIndex = "61562"
            };
            Location location18 = new Location()
            {
                Id = 18,
                Adress = "Festivalna 63",
                Latitude = 50.346039,
                Longitude = 30.547797,
                IsDeleted = false,
                City = "Kiev",
                PostIndex = "61882"
            };

            context.Locations.AddRange(new List<Location>() { location1, location2, location3, location4, location5, location6, location7, location8, location9, location10, location11, location12, location13, location14, location15, location16, location17, location18 });

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
                Location = location1,
                Contacts = new List<Contact> { contact1, contact2, contact3, contact4, contact5, contact6, contact7},
                IsBanned = false
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
                Location = location2,
                Contacts = new List<Contact> { contact8, contact9, contact10, contact11, contact12, contact13, contact14 },
                IsBanned = false
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
                Location = location3,
                Contacts = new List<Contact> { contact15, contact16, contact17, contact18, contact19, contact20, contact21 },
                IsBanned = false

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
                Location = location4,
                Contacts = new List<Contact> { contact22, contact23, contact24, contact25, contact26, contact27, contact28 },
                IsBanned = false
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
                Location = location5,
                Contacts = new List<Contact> { contact29, contact30, contact31, contact32, contact33, contact34, contact35 },
                IsBanned = false
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
                Location = location6,
                Contacts = new List<Contact> { contact36, contact37, contact38, contact39, contact40, contact41, contact42 },
                IsBanned = false
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
                Location = location7,
                Contacts = new List<Contact> { contact43, contact44, contact45, contact46, contact47, contact48, contact49 },
                IsBanned = false
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
                Location = location8,
                Contacts = new List<Contact> { contact50, contact51, contact52, contact53, contact54, contact55, contact56 },
                IsBanned = false
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
                Location = location9,
                Contacts = new List<Contact> { contact57, contact58, contact59, contact60, contact61, contact62, contact63 },
                IsBanned = false
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
                Location = location10,
                Contacts = new List<Contact> { contact64, contact65, contact66, contact67, contact68, contact69, contact70 },
                IsBanned = false
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
                Location = location11,
                Contacts = new List<Contact> { contact71, contact72, contact73, contact74, contact75, contact76, contact77 },
                IsBanned = false
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
                Location = location12,
                Contacts = new List<Contact> { contact78, contact79, contact80, contact81, contact82, contact83, contact84 },
                IsBanned = false
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
                Location = location13,
                Contacts = new List<Contact> { contact85, contact86, contact87, contact88, contact89, contact90, contact91 },
                IsBanned = false
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
                Location = location14,
                Contacts = new List<Contact> { contact92, contact93, contact94, contact95, contact96, contact97, contact98 },
                IsBanned = false
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
                Location = location15,
                Contacts = new List<Contact> { contact99, contact100, contact101, contact102, contact103, contact104, contact105 },
                IsBanned = false
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
                Location = location16,
                Contacts = new List<Contact> { contact106, contact107, contact108, contact109, contact110, contact111, contact112 },
                IsBanned = false
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
                Location = location17,
                Contacts = new List<Contact> { contact113, contact114, contact115, contact116, contact117, contact118, contact119 },
                IsBanned = false
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
                Location = location18,
                Contacts = new List<Contact> { contact120, contact121, contact122, contact123, contact124, contact125, contact126 },
                IsBanned = false
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
                Tags = "care of closes,care,clothes,sewing,stitchery,stitching,needlework,dressmaking,seamstress,stitch,tailoring,couture," +
                       "fashion,thread,design,seam,intersection,knitting,quilting,crocheting,embroidery,needlecraft,serger,sewing machine," +
                       "embroidering,smocking,felting,woodworking,needlepoint,handwork,quilters,tatting,woodcarving,scrapbooking," +
                       "darning,beading,crocheter,crewel,embroiderers,stitchers,bobbin lace,quilter,bookbinding,woodcraft"
            };

            Category category2 = new Category()
            {
                Id = 2,
                Icon = "https://phrasee.co/wp-content/uploads/2016/06/pets.jpg",
                IsDeleted = false,
                Description = "Pet",
                Name = "Pets care",
                Tags = "pets care,pet,pets,scratch,caregiver,infirmary,healthcare,health,caring,caregiving,elderly,paramedic," +
                       "clinic,nursery,convalescence,lactation,profession,infant,retirement,breastfeeding,medic,resting," +
                       "accommodation,pension,feeding,corpsman,lactating,rest,suckler,attendant,weaning,conservation," +
                       "endowment,ranger,upbringing,orderly,guarding,infancy,attentionbreast-feeding,health-care,nursing"
            };

            Category category3 = new Category()
            {
                Id = 3,
                Icon = "http://www.portlandgroupbd.com/wp-content/uploads/2016/06/t1.jpg",
                IsDeleted = false,
                Description = "Transportations",
                Name = "Car services",
                Tags = "car services,car,service,transportation,transportations,transport,transit,conveyance,shipping," +
                       "department of transportation,transfer,transferral,transportation system,freight,rail," +
                       "trucking,commuter,commuting,transporting,travel,cargo,services,communications,transports,conveyances," +
                       "cartage,communication,haulage,traffic,mobility,maritime,passenger,airlift,delivery,ride," +
                       "hauling,transmission,relocation,transported,locomotion,transporter,road,haul,vehicle,limousine,"
            };

            Category category4 = new Category()
            {
                Id = 4,
                Icon = "http://pop.h-cdn.co/assets/cm/15/05/54cb5d94587ec_-_tallest-buildings-05-1214-lgn.jpg",
                IsDeleted = false,
                Description = "Buildings",
                Name = "Builder services",
                Tags = "builder services,build,builder,service,create,reconstruct," + 
                       "augmenting,constructing,constructed,construct,rebuild,reconstructing,creating,reinforcing,establishing," +
                       "forming,cultivating,develop,consolidating,establish,consolidate,expand,furthering,enhancing," +
                       "achieving,bolstering,providing,reinforce,capitalizing,generate,boosting,created,strengthen,improve," +
                       "placing,integrate,promoting,preparing,enhance,basing,acquire,encourage,relying,introducing"
            };

            Category category5 = new Category()
            {
                Id = 5,
                Icon = "http://photolifeway.com/images/futaji.png",
                IsDeleted = false,
                Description = "Media processing",
                Name = "Photo and Video",
                Tags = "photo and video,photo,video,media,processing,myspace,avatar,camera,closeup,footage,frame," +
                       "shoot,alien,cam,file,images,map,snap,archive,caption,essay,foreground,graph,image,mat,microdot," +
                       "mosaic,panel,photo-electric,photoelectric,photography,pic,picture,pictures,print," +
                       "radiograph,reportage,retake,scan,scenic,shot,speed,still,thumbnail,toner"
            };

            Category category6 = new Category()
            {
                Id = 6,
                Icon = "https://www.cyberworx.in/images/services/web-developement.png",
                IsDeleted = false,
                Description = "Developing",
                Name = "Developer Service",
                Tags = "developer service,developer,service,development,developingunderdeveloped,nonindustrial,creating," +
                       "producing,designing,implementing,emerging,establishing,expanding,strengthening,upgrading," +
                       "formulating,enhancing,devising,acquiring,promoting,cultivating,fostering,constructing,improving," +
                       "introducing,providing,evolving,forming,crafting,growing,build,evolve,forging,advancing," +
                       "nurturing,increasing,create,expand,finalizing,formulate,enhance,developmental,creation"
            };

            Category category7 = new Category()
            {
                Id = 7,
                Icon = "http://www.shopcusa.com/wp-content/uploads/2015/07/Computer-parts.jpg",
                IsDeleted = false,
                Description = "Electronic parts supplier",
                Name = "Electronic parts",
                Tags = "electronic parts,electronic,parts,supplier,computerized,automated,digital,paperless,online,computer,computerization,computerised," +
                       "opto,electric,wire,informatics,internet,virtual,mailing,magnetic,cyber,document,desktop,web,computing,electron,electrical," +
                       "information,email,edr,electrons,website,esd,broadcast,numerical,line,computerisation,e-business,e-commerce,audiovisual," +
                       "e -finance,e-government,e-health,e-learning"
            };

            Category category8 = new Category()
            {
                Id = 8,
                Icon = "http://images.all-free-download.com/images/graphicthumb/fast_food_icons_set_vector_graphics_548786.jpg",
                IsDeleted = false,
                Description = "Food and beverages",
                Name = "Food service",
                Tags = "food service,food,service,eat,beverages,feeds,recharge,restoring,restore,powering,buy,served,eaten," +
                       "ate,drink,prepare,consumed,cook,grow,preparing,supplied,contaminated,stored,serving," +
                       "digested,consume,sharing,tasted,processed,imported,obtaining,ingested,prepared,cooked," +
                       "buying,purchase,store,deprived,taste,digest,beg,searching,chew,begging"
            };

            Category category9 = new Category()
            {
                Id = 9,
                Icon = "https://cdn.dribbble.com/users/12733/screenshots/2705729/delivery_truck_icon_1x.png",
                IsDeleted = false,
                Description = "Package delivery",
                Name = "Delivery service",
                Tags = "delivery service,delivery,service,package,delivering,delivered,deliver,delivers,transporting,providing,distributing," +
                       "rendered,provides,render,achieving,disseminate,implement,carrying,administer,obtaining,execute,provided,submitting," +
                       "remitting,handing,fulfilling,placing,realizing,enforcing,recognizing,submit,granting,carried,perform,shed," +
                       "recognize,extradite,ensure,improve,allow,facilitatedistribute,provide,implementing"
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
                Tags = "washing,wash,clothes,dry,apparel,wear,vesture,wearing apparel,garments,dresses,fashions," +
                       "shirts,gowns,costumes,outerwear,garment,linens,robes,jackets,garbs,duds,dressing,vests," +
                       "fatigues,suits,textiles,naked,washing,habits,children,straitjackets,threads,woodworking," +
                       "plainclothes,claws,clones,inflatable,cases,affairs,guises,plain-clothes,attire,uniforms,outfits,"
            };

            Subcategory subcategory2 = new Subcategory()
            {
                Id = 2,
                Description = "care with lots of love about your cat",
                Name = "Cats care",
                IsDeleted = false,
                Category = category2,
                Tags = "cats care,cat,cats,care,kitten,puss,pussycat,pot,pool,pussy,jackpot,kitty-cat,pet,cats,poodle,cub,sweetie,beaver," +
                       "Koko,bimbo,commie,coconut,baby,bucket,spade,coco,geezer,mom,little,boy,playboy,Cathy,rug," +
                       "Katie,bogeyman,wuss,petit,cold,nice,kath,hood,chat,stop"
            };

            Subcategory subcategory3 = new Subcategory()
            {
                Id = 3,
                Description = "Subacategory lory service",
                Name = "Lorry service",
                IsDeleted = false,
                Category = category3,
                Tags = "lorry service,lorry,service,camion,hgv,truck,van,tractor,bus,vehicle,car,tanker," +
                       "minivan,cargo,freight,flatbed,highway,road,trunk,pickup,truckload,rig," +
                       "trucking,semi,firecracker,heavy,juggernaut,heavyweight,coach,heavy-duty," +
                       "tractor -trailer,minibus,haulage,tractor trailer,haulier,horsebox," +
                       "concrete mixer,forklift,hackney carriage,dumper,lgv,wagon,container"
            };

            Subcategory subcategory4 = new Subcategory()
            {
                Id = 4,
                Description = "Your appartment repair",
                Name = "Painting and plastering work",
                IsDeleted = false,
                Category = category4,
                Tags = "painting and plastering work,painting,plastering,work,appartment,house,building,repair," + 
                       "primed,ground,grounded,rendering,case,float,floating,fur,lath,mortarboard," +
                       "plasterwork,staff,baseboard,let,bronzing,daub,fresco,gesso,lay,painter," +
                       "secco,sgraffito,size,browning,calcimine,cat,compost,dots,doy,dpt,furring,key," +
                       "keying,keys,lime,lock,locking,locks,mastic,mortar"
            };

            Subcategory subcategory5 = new Subcategory()
            {
                Id = 5,
                Description = "Make your own photoset",
                Name = "Photosession",
                IsDeleted = false,
                Category = category5,
                Tags = "photosession,make,photoset,shoot,hoot,reload,bath,camera,archive,block,blur," +
                       "caption,chart,epic,figure,footage,foreground,frame,gallery,graph,image,imagery," +
                       "inset,locket,map,mat,mosaic,ops,panel,picture,pictures,retake,scan," +
                       "sepia,shot,snap,snapshot,speed,sunset,after,before,charts,copy"
            };

            Subcategory subcategory6 = new Subcategory()
            {
                Id = 6,
                Description = "Web Development",
                Name = "Web Development",
                IsDeleted = false,
                Category = category6,
                Tags = "web development,web,www,development,network,world wide web,net,vane,entanglement,website," +
                       "websites,webpage,online,homepage,internet,portal,site,intranet,sites,page,webcast,cyberspace," +
                       "spider,networks,cyber,virtual,networking,posted,available,electronic,enabled,update,Webb,weave," +
                       "fabric,trophic,weft,chain,address,retina,sling,nets,system"
            };

            Subcategory subcategory7 = new Subcategory()
            {
                Id = 7,
                Description = "Computer parts",
                Name = "Computer parts",
                IsDeleted = false,
                Category = category7,
                Tags = "computer parts,computer,parts,pc,busbar,card,computer architecture,fragmentation,hardware,kludge,link," +
                       "multiprocessing,panel,Intel,Opengl,cognitive science,abacus,adapter,alphabet,architecture,area," +
                       "array,assembler,assembly,background,binary,bits,block,box,brain,branch,bridge,bush,button,cards," +
                       "chassis,clock,configuration,core,coring,cracker,linux,machine,motherboard,"
            };

            Subcategory subcategory8 = new Subcategory()
            {
                Id = 8,
                Description = "Fast food",
                Name = "Fast food",
                IsDeleted = false,
                Category = category8,
                Tags = "fast food,fast,food,fries,snack,bite,diet,snacks,cola,taco,Togo,soda,fryer,spork," +
                       "deli,Mac,arch,wrap,combo,fry,tempura,wahoo,blt,breakfast,donut,malt,sauce," +
                       "steam,toy,chain,emu,teens,cheeseburger,diner,foodie,pizza,eater,burrito," +
                       "enchiladas,escargot,falafelautomat,chili"
            };

            Subcategory subcategory9 = new Subcategory()
            {
                Id = 9,
                Description = "Package express delivery",
                Name = "Express delivery",
                IsDeleted = false,
                Category = category9,
                Tags = "express delivery,express,delivery,package,bundle,packet,parcel,box,plan,deal,proposal,bundles,pkg,kit,program,bailout,scheme,packs," +
                       "solution,wrapper,toolkit,measures,suite,bag,piece,envelope,packets,pact,pouch,crate," +
                       "omnibus,toolbox,kits,contract,pack,shipment,carton,array,module,wrap,combination," +
                       "plans,document,parcels"
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

            Work work12 = new Work()
            {
                Icon = "http://homecareservices.ru/blog/wp-content/uploads/2014/10/CollectionShot.jpg",
                Description = "Care of clothes",
                IsDeleted = false,
                Name = "laundress",
                Subcategory = subcategory1,
                Orders = 10
            };

            Work work13 = new Work()
            {
                Icon = "http://blog.creativelive.com/wp-content/uploads/2015/06/photographer-692035_640.jpg",
                Description = "Photosession, wedding photosessions etc.",
                IsDeleted = false,
                Name = "Photographer",
                Subcategory = subcategory5,
                Orders = 20
            };

            Work work14 = new Work()
            {
                Icon = "http://www.sagevets.com.au/images/vet-and-cat.jpg",
                Description = "Care of your pets",
                IsDeleted = false,
                Name = "Vet",
                Subcategory = subcategory2,
                Orders = 30
            };

            Work work15 = new Work()
            {
                Icon = "http://www.samuraibuilders.co.nz/wp-content/uploads/2015/05/topslider2-a1.jpg",
                Description = "Appartmen repair",
                IsDeleted = false,
                Name = "Builder",
                Subcategory = subcategory4,
                Orders = 40
            };

            Work work16 = new Work()
            {
                Icon = "https://www.fmcsa.dot.gov/sites/fmcsa.dot.gov/files/TruckDriver1%5B1%5D.jpg",
                Description = "Driving services",
                IsDeleted = false,
                Name = "Driver",
                Subcategory = subcategory3,
                Orders = 50
            };

            Work work17 = new Work()
            {
                Icon = "http://www.startupguys.net/wp-content/uploads/2016/06/how-to-find-good-software-developer.jpg",
                Description = "C# senior dev",
                IsDeleted = false,
                Name = "Developer",
                Subcategory = subcategory6,
                Orders = 60
            };

            Work work18 = new Work()
            {
                Icon = "http://www.qualix.co.in/wp-content/uploads/2016/01/servicesInformationTechnologyBanner.jpg",
                Description = "Services and products development",
                IsDeleted = false,
                Name = "Services and products",
                Subcategory = subcategory6
            };

            Work work19 = new Work()
            {
                Icon = "https://www.misedesigns.com/wp-content/uploads/2016/09/restaurant-equipment-supplier.jpg",
                Description = "Supply industrial equipment and machines",
                IsDeleted = false,
                Name = "Equipment supplier",
                Subcategory = subcategory6
            };

            Work work20 = new Work()
            {
                Icon = "https://www.easypacelearning.com/design/images/content/personalcomputerhardwareparts.png",
                Description = "Computer parts supplier",
                IsDeleted = false,
                Name = "Computer parts supplier",
                Subcategory = subcategory7
            };

            Work work21 = new Work()
            {
                Icon = "https://cdn.shutterstock.com/shutterstock/videos/1811057/thumb/10.jpg",
                Description = "Сooking food",
                IsDeleted = false,
                Name = "Сooking food",
                Subcategory = subcategory8
            };

            Work work22 = new Work()
            {
                Icon = "http://www.phatinvestor.com/wp-content/uploads/2017/04/blog41.jpg",
                Description = "Courier service delivery",
                IsDeleted = false,
                Name = "Delivery",
                Subcategory = subcategory9
            };

            context.Works.AddRange(new List<Work>() { work1, work2, work3, work4, work5, work6, work7, work8, work9, work10, work11, work12, work13, work14, work15, work16, work17, work18, work19, work20, work21, work22 });


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
                Phone = "+8036047259"
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
                Phone = "+6463194092"
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
                Phone = "+2062376702"
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
                Phone = "+380953661137",
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
                Phone = "+380445555555"
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
                Phone = "+380446666666",
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
                Phone = "+380447777777",
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
                Phone = "+380448888888"
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
                Phone = "+380449999999"
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
                Phone = "+380954444444"
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

            #region Calendars

            Calendar calendar1 = new Calendar
            {
                Id = 1,
                IsDeleted = false,
                StartDate = DateTimeOffset.UtcNow,
                EndDate = null,
                ExtraDayOffs = new List<ExtraDay>(),
                ExtraWorkDays = new List<ExtraDay>(),
                WorkOnWeekend = false,
                SeveralTaskPerDay = true
            };

            Calendar calendar2 = new Calendar
            {
                Id = 2,
                IsDeleted = false,
                StartDate = DateTimeOffset.UtcNow,
                EndDate = null,
                ExtraDayOffs = new List<ExtraDay>(),
                ExtraWorkDays = new List<ExtraDay>(),
                WorkOnWeekend = false,
                SeveralTaskPerDay = true
            };

            Calendar calendar3 = new Calendar
            {
                Id = 3,
                IsDeleted = false,
                StartDate = DateTimeOffset.UtcNow,
                EndDate = null,
                ExtraDayOffs = new List<ExtraDay>(),
                ExtraWorkDays = new List<ExtraDay>(),
                WorkOnWeekend = false,
                SeveralTaskPerDay = true
            };

            Calendar calendar4 = new Calendar
            {
                Id = 4,
                IsDeleted = false,
                StartDate = DateTimeOffset.UtcNow,
                EndDate = null,
                ExtraDayOffs = new List<ExtraDay>(),
                ExtraWorkDays = new List<ExtraDay>(),
                WorkOnWeekend = false,
                SeveralTaskPerDay = true
            };

            Calendar calendar5 = new Calendar
            {
                Id = 5,
                IsDeleted = false,
                StartDate = DateTimeOffset.UtcNow,
                EndDate = null,
                ExtraDayOffs = new List<ExtraDay>(),
                ExtraWorkDays = new List<ExtraDay>(),
                WorkOnWeekend = false,
                SeveralTaskPerDay = true
            };

            Calendar calendar6 = new Calendar
            {
                Id = 6,
                IsDeleted = false,
                StartDate = DateTimeOffset.UtcNow,
                EndDate = null,
                ExtraDayOffs = new List<ExtraDay>(),
                ExtraWorkDays = new List<ExtraDay>(),
                WorkOnWeekend = false,
                SeveralTaskPerDay = true
            };

            Calendar calendar7 = new Calendar
            {
                Id = 7,
                IsDeleted = false,
                StartDate = DateTimeOffset.UtcNow,
                EndDate = null,
                ExtraDayOffs = new List<ExtraDay>(),
                ExtraWorkDays = new List<ExtraDay>(),
                WorkOnWeekend = false,
                SeveralTaskPerDay = true
            };

            Calendar calendar8 = new Calendar
            {
                Id = 8,
                IsDeleted = false,
                StartDate = DateTimeOffset.UtcNow,
                EndDate = null,
                ExtraDayOffs = new List<ExtraDay>(),
                ExtraWorkDays = new List<ExtraDay>(),
                WorkOnWeekend = false,
                SeveralTaskPerDay = true
            };

            Calendar calendar9 = new Calendar
            {
                Id = 9,
                IsDeleted = false,
                StartDate = DateTimeOffset.UtcNow,
                EndDate = null,
                ExtraDayOffs = new List<ExtraDay>(),
                ExtraWorkDays = new List<ExtraDay>(),
                WorkOnWeekend = false,
                SeveralTaskPerDay = true
            };

            Calendar calendar10 = new Calendar
            {
                Id = 10,
                IsDeleted = false,
                StartDate = DateTimeOffset.UtcNow,
                EndDate = null,
                ExtraDayOffs = new List<ExtraDay>(),
                ExtraWorkDays = new List<ExtraDay>(),
                WorkOnWeekend = false,
                SeveralTaskPerDay = true
            };

            Calendar calendar11 = new Calendar
            {
                Id = 11,
                IsDeleted = false,
                StartDate = DateTimeOffset.UtcNow,
                EndDate = null,
                ExtraDayOffs = new List<ExtraDay>(),
                ExtraWorkDays = new List<ExtraDay>(),
                WorkOnWeekend = false,
                SeveralTaskPerDay = true
            };

            Calendar calendar12 = new Calendar
            {
                Id = 12,
                IsDeleted = false,
                StartDate = DateTimeOffset.UtcNow,
                EndDate = null,
                ExtraDayOffs = new List<ExtraDay>(),
                ExtraWorkDays = new List<ExtraDay>(),
                WorkOnWeekend = false,
                SeveralTaskPerDay = true
            };

            Calendar calendar13 = new Calendar
            {
                Id = 13,
                IsDeleted = false,
                StartDate = DateTimeOffset.UtcNow,
                EndDate = null,
                ExtraDayOffs = new List<ExtraDay>(),
                ExtraWorkDays = new List<ExtraDay>(),
                WorkOnWeekend = false,
                SeveralTaskPerDay = true
            };

            Calendar calendar14 = new Calendar
            {
                Id = 14,
                IsDeleted = false,
                StartDate = DateTimeOffset.UtcNow,
                EndDate = null,
                ExtraDayOffs = new List<ExtraDay>(),
                ExtraWorkDays = new List<ExtraDay>(),
                WorkOnWeekend = false,
                SeveralTaskPerDay = true
            };

            Calendar calendar15 = new Calendar
            {
                Id = 15,
                IsDeleted = false,
                StartDate = DateTimeOffset.UtcNow,
                EndDate = null,
                ExtraDayOffs = new List<ExtraDay>(),
                ExtraWorkDays = new List<ExtraDay>(),
                WorkOnWeekend = false,
                SeveralTaskPerDay = true
            };

            context.Calendars.AddRange(new List<Calendar>() { calendar1, calendar2, calendar3, calendar4, calendar5, calendar6, calendar7, calendar8, calendar9, calendar10, calendar11, calendar12, calendar13, calendar14, calendar15 });

            #endregion

            #region Vendors            

            Vendor vendor1 = new Vendor()
            {
                Id = 1,
                Experience = 7.5,
                IsDeleted = false,
                Position = "Web designer",
                Works = new List<Work>() { work1, work2, work3 },
                ExWork = "Dishwasher",
                Person = person1,
                Calendar = calendar1,
                WorkLetter = "My name is Randy Patterson, and I’m currently looking for a job in youth services. I have 10 years of experience working with youth agencies. I have a bachelor’s degree in outdoor education. I raise money, train leaders, and organize units. I have raised over $100,000 each of the last six years. I consider myself a good public speaker, and I have a good sense of humor.",
            };

            Vendor vendor2 = new Vendor()
            {
                Id = 2,
                Experience = 6.7,
                IsDeleted = false,
                Position = "Photographer",
                Works = new List<Work>() { work2, work22 },
                ExWork = "Photographer",
                Person = person2,
                Calendar = calendar2,
                WorkLetter = "My job is to build your website so that it is functional and user-friendly but at the same time attractive. Moreover, I add personal touch to your product and make sure that is eye-catching and easy to use. My aim is to bring across your message and identity in the most creative way."
            };

            Vendor vendor3 = new Vendor()
            {
                Id = 3,
                Experience = 8.7,
                IsDeleted = false,
                Position = "Vet",
                Works = new List<Work>() { work3, work21 },
                ExWork = "Vet",
                Person = person3,
                Calendar = calendar3,
                WorkLetter = "I'm Jordan Vitanov, digital artist and designer, working in web development and print media. If you have a project that needs some creative injection then that’s where I come in!"
            };

            Vendor vendor4 = new Vendor()
            {
                Id = 4,
                Experience = 4.2,
                IsDeleted = false,
                Position = "Builder",
                Works = new List<Work>() { work4, work20 },
                ExWork = "Painter",
                Person = person4,
                Calendar = calendar4,
                WorkLetter = "Lia Griffith is an internationally renowned DIY designer. She built a large and loyal following within a year of blogging and teaching. We worked with Lia Griffith to craft a new site to match the creative energy and vision of her brand."
            };

            Vendor vendor5 = new Vendor()
            {
                Id = 5,
                Experience = 8.8,
                IsDeleted = false,
                Position = "Uber Select driver",
                Works = new List<Work>() { work5, work19 },
                ExWork = "lory driver",
                Person = person5,
                Calendar = calendar5,
                WorkLetter = "At first I intended to be an animator and went to design school fully motivated to become just that. One thing led to another and 2 years went by and I was a (almost) fully fledged freelance web designer without ever planning to become one"
            };

            Vendor vendor6 = new Vendor()
            {
                Id = 6,
                Experience = 9.9,
                IsDeleted = false,
                Position = "C# Senior Dev",
                Works = new List<Work>() { work6, work18, work17 },
                ExWork = "Middle dev",
                Person = person6,
                Calendar = calendar6,
                WorkLetter = "I have sucessfully been a freelance web designer now for a while and it has given me even more love for this work. I have worked with ad agencies, web developers, diaper makers, pension funds, furniture makers, businiess women & men, friends & family."
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
                Calendar = calendar7,
                WorkLetter = "I have coded websites with great respect to standards, because it makes life easier for everybody. My site even used to validate, honestly!"
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
                Calendar = calendar8,
                Vendors = new List<Vendor>() { vendor1, vendor2 },
                Works = new List<Work> { work1, work2, work22},

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
                Calendar = calendar9,
                Vendors = new List<Vendor>() { vendor4, vendor5},
                Works = new List<Work> { work3, work4, work20 }
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
                Calendar = calendar10,
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
                Calendar = calendar11,
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
                Calendar = calendar12,
                Vendors = new List<Vendor>(),
                Works = new List<Work> { work8, work12 }
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
                Calendar = calendar13,
                Vendors = new List<Vendor>(),
                Works = new List<Work> { work9, work13 }
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
                Calendar = calendar14,
                Vendors = new List<Vendor>(),
                Works = new List<Work> { work10, work14 }
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
                Calendar = calendar15,
                Vendors = new List<Vendor>(),
                Works = new List<Work> { work11, work16, work15 }
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

            Book book7 = new Book()
            {
                Date = new DateTime(2017, 08, 20, 18, 04, 00),
                IsDeleted = false,
                Work = work2,
                Description = "Make asp.net core website",
                Status = BookStatus.Accepted,
                Vendor = vendor1,
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
                CustomerPhone = "+100 500"
            };

            Book book8 = new Book()
            {
                Date = new DateTime(2017, 08, 20, 18, 04, 00),
                IsDeleted = false,
                Work = work3,
                Description = "Make asp.net core website",
                Status = BookStatus.Finished,
                Vendor = vendor1,
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
                CustomerPhone = "+100 500"
            };

            Book book9 = new Book()
            {
                Date = new DateTime(2017, 08, 20, 18, 04, 00),
                IsDeleted = false,
                Work = work2,
                Description = "Make asp.net core website",
                Status = BookStatus.InProgress,
                Company = company1,
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

            Book book10 = new Book()
            {
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

            context.Books.AddRange(new List<Book>() { book1, book2, book3, book4, book5, book6, book7, book8, book9, book10 });


            #endregion

            #region Reviews

            Review review1 = new Review()
            {
                BookId = 1,
                Description = "qwert",
                Sender = account11,
                To = "NameSurname",
                ToAccountId = 6,
                IsDeleted = false,
                Date = DateTime.Now,
            };

            context.Reviews.AddRange(new List<Review>() { review1 });


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
