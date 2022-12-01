namespace uStora.Data.Migrations
{
    using Common;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using Model.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Validation;
    using System.Diagnostics;
    using System.Globalization;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<uStoraDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(uStoraDbContext context)
        {
            CreateUser(context);
            BrandDefault(context);
            CreateContactDetail(context);
            CreateSystemConfig(context);
            CreatePage(context);
            CreateGroup(context);
        }
        #region Methods
        private void CreateUser(uStoraDbContext context)
        {
            if (context.Users.Count() == 0)
            {
                var manager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new uStoraDbContext()));

                var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new uStoraDbContext()));

                var user = new ApplicationUser()
                {
                    UserName = "amdin",
                    Email = "admin@gmail.com",
                    EmailConfirmed = true,
                    Image = CommonConstants.DefaultAvatar,
                    CreatedDate = DateTime.ParseExact(DateTime.Now.ToShortDateString(), "dd/MM/yyyy", CultureInfo.InvariantCulture),
                    BirthDay = DateTime.ParseExact("25/10/2001", "dd/MM/yyyy", CultureInfo.InvariantCulture),
                    Gender = "Nam",
                    FullName = "admin",
                    Address = "HCM",
                    PhoneNumber = "01652130546",
                    IsDeleted = false,
                    IsViewed = true
                };
                if (manager.Users.Count() == 0)
                {
                    manager.Create(user, "admin");

                    if (!roleManager.Roles.Any())
                    {
                        roleManager.Create(new IdentityRole { Name = "Admin" });
                        roleManager.Create(new IdentityRole { Name = "User" });
                    }

                    var adminUser = manager.FindByEmail("admin@gmail.com");

                    manager.AddToRoles(adminUser.Id, new string[] { "Admin", "User" });

                }
            }
        }

        private void BrandDefault(uStoraDbContext context)
        {
            if (context.Brands.Count() == 0)
            {
                var brand = new Brand()
                {
                    Name = "Apple",
                    Alias = "apple",
                    CreatedBy = "system",
                    CreatedDate = DateTime.Now,
                    Status = true,
                    IsDeleted = false
                };
                context.Brands.Add(brand);
                context.SaveChanges();
            }
        }

        private void CreateContactDetail(uStoraDbContext context)
        {
            if (context.ContactDetails.Count() == 0)
            {
                try
                {
                    var contactDetail = new ContactDetail()
                    {
                        Name = "Shop online - uBook",
                        Address = "HCM",
                        Phone = "016 5213 0546",
                        Email = "huudiem@gmail.com",
                        Lat = 16.034562,
                        Lng = 108.222603,
                        Website = "http://uBook.somee.com",
                        Description = "",
                        Status = true
                    };
                    context.ContactDetails.Add(contactDetail);
                    context.SaveChanges();
                }
                catch
                {

                }
            }
        }

        private void CreateSystemConfig(uStoraDbContext context)
        {
            if (!context.SystemConfigs.Any(x => x.Code == "Hometitle"))
            {
                context.SystemConfigs.Add(new SystemConfig()
                {
                    Code = "HomeTitle",
                    ValueString = "Trang chủ uBook shop - nơi mua bán uy tín và chất lượng - uBook.com"
                });
            }
            if (!context.SystemConfigs.Any(x => x.Code == "HomeMetaKeyword"))
            {
                context.SystemConfigs.Add(new SystemConfig()
                {
                    Code = "HomeMetaKeyword",
                    ValueString = "Trang chủ uBook shop - nơi mua bán uy tín và chất lượng - uBook.com"
                });
            }
            if (!context.SystemConfigs.Any(x => x.Code == "HomeMetaDescription"))
            {
                context.SystemConfigs.Add(new SystemConfig()
                {
                    Code = "HomeMetaDescription",
                    ValueString = "Trang chủ uBook shop - nơi mua bán uy tín và chất lượng - uBook.com"
                });
            }
        }

        private void CreatePage(uStoraDbContext context)
        {
            if (context.Pages.Count() == 0)
            {
                try
                {
                    var page = new Page()
                    {
                        Name = "Giới thiệu",
                        Alias = "gioi-thieu",
                        CreatedDate = DateTime.Now,
                        MetaDescription = "Trang giới thiệu của uBook",
                        MetaKeyword = "Trang giới thiệu của uBook",
                        Content = @"Là trang web bán sách, chuyên cung cấp các thể loại sách hay nhất Việt Nam,... và một số dịch vụ kèm theo khi mua hàng.",
                        Status = true

                    };
                    context.Pages.Add(page);
                    context.SaveChanges();
                }
                catch (DbEntityValidationException ex)
                {
                    foreach (var eve in ex.EntityValidationErrors)
                    {
                        Trace.WriteLine($"Entity of type \"{eve.Entry.Entity.GetType().Name}\" in state \"{eve.Entry.State}\" has the following validation error.");
                        foreach (var ve in eve.ValidationErrors)
                        {
                            Trace.WriteLine($"- Property: \"{ve.PropertyName}\", Error: \"{ve.ErrorMessage}\"");
                        }
                    }
                }

            }
        }

        private void CreateGroup(uStoraDbContext context)
        {
            if (context.ApplicationGroups.Count() == 0)
            {
                try
                {
                    var appGroups = new List<ApplicationGroup>();

                    appGroups.Add(new ApplicationGroup()
                    {
                        Name = "Admin",
                        Description = "Ban quản trị",
                        IsDeleted = false
                    });
                    appGroups.Add(new ApplicationGroup()
                    {
                        Name = "Người quản lý",
                        Description = "Người quản lý",
                        IsDeleted = false
                    });
                    appGroups.Add(new ApplicationGroup()
                    {
                        Name = "Thành viên",
                        Description = "Thành viên",
                        IsDeleted = false
                    });
                    appGroups.Add(new ApplicationGroup()
                    {
                        Name = "Tài xế",
                        Description = "Tài xế",
                        IsDeleted = false
                    });

                    foreach (var appGroup in appGroups)
                    {
                        context.ApplicationGroups.Add(appGroup);
                        context.SaveChanges();
                    }
                    
                }
                catch (Exception ex)
                {
                    Console.Write(ex.Message);
                }
            }
        }
        #endregion
    }
}