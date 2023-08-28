using System;
using AutoMapper;
using System.Collections.Generic;
using System.Threading.Tasks;
using SmartOffice.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SmartOffice.Hubs;
using SmartOffice.Models;
using SmartOffice.Class;
using System.Globalization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Localization;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.FileProviders;
using System.IO;
using SmartOffice.ModelsEsmartOffice;
using SmartOffice.ModelsDocControl;
using SmartOffice.ModelsHRMSLocal;
using Microsoft.AspNetCore.Http.Features;
using SmartOffice.IResponsitory;
using SmartOffice.Controllers;
using SmartOffice.EmailCore.Models;
using SmartOffice.EmailCore.IResponsitory;
using SmartOffice.EmailCore;
using SmartOffice.EReservation;
using SmartOffice.EReservation.IResponsitory;
using SmartOffice.EAppointment;
using SmartOffice.eReservation.ModelsDocControl;
using SmartOffice.EAppointment.IResponsitory;
using SmartOffice.PRApprove;
using SmartOffice.eManagement.ModelsManagementControl;
using SmartOffice.EManagement.IResponsitory;
using SmartOffice.eManagement;
using SmartOffice.Services;
using SmartOffice.EHelpdesk.Mappings;
using SmartOffice.EHelpdesk.Models;
using SmartOffice.EHelpdesk.Models.ViewModels;
using SmartOffice.EHelpdesk.Helpers;
using SmartOffice.SurveyApp.IResponsitory;
using SmartOffice.SurveyApp;
using Microsoft.AspNetCore.HttpOverrides;
using SmartOffice.eManagement.IResponsitory;
using Microsoft.AspNetCore.DataProtection;
using SmartOffice.Models.DataProtectModel;
using SmartOffice.Responsitory;
using SmartOffice.eManagement.Class;
using SmartOffice.Persistence;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Internal;
using Ducksoft.NetCore.Razor.Sitemap.Models;
using Microsoft.AspNetCore.Mvc.ViewEngines;

namespace SmartOffice
{
    public class Startup
    {

        public Startup(IConfiguration configuration)
        {
           
            Configuration = configuration;
        }


        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
         
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });



            services.AddDbContext<ApplicationDbContext>(options =>
          options.UseSqlServer(
              Configuration.GetConnectionString("DefaultConnection")));

            services.AddDbContext<ESmartOfficeContext>(options =>
              options.UseSqlServer(
                  Configuration.GetConnectionString("DefaultConnection1")));

            services.AddDbContext<MyKeysContext>(options =>
            options.UseSqlServer(
                Configuration.GetConnectionString("DefaultConnection1")));

            services.AddDbContext<DocumentControlContext>(options =>
              options.UseSqlServer(
                  Configuration.GetConnectionString("DefaultConnection2")));


            services.AddDbContext<EReservationContext>(options =>
           options.UseSqlServer(
               Configuration.GetConnectionString("DefaultConnection2")));

            services.AddDbContext<HRMSLocalContext>(options =>
             options.UseSqlServer(
                 Configuration.GetConnectionString("DefaultConnection3")));

            services.AddDbContext<SendmailContext>(options =>
            options.UseSqlServer(
                Configuration.GetConnectionString("DefaultConnection4")));

            services.AddDbContext<PR_APPROVEContext>(options =>
           options.UseSqlServer(
               Configuration.GetConnectionString("DefaultConnection5")));


            services.AddDbContext<ManagementControlContext>(options =>
           options.UseSqlServer(
               Configuration.GetConnectionString("DefaultConnection7")));


            services.AddDbContext<SurveyApp.ModelsManagementControl.ManagementControlContext>(options =>
          options.UseSqlServer(
              Configuration.GetConnectionString("DefaultConnection7")));


            services.AddDbContext<EAppointment.ModelsForm.DocumentControlContext>(options =>
          options.UseSqlServer(
              Configuration.GetConnectionString("DefaultConnection2")));



            // using Microsoft.AspNetCore.DataProtection;
            // All user accounts on the machine can decrypt the keys ProtectKeysWithDpapi(protectToLocalMachine: true)
            services.AddDataProtection().PersistKeysToDbContext<MyKeysContext>().ProtectKeysWithDpapi(protectToLocalMachine: true).SetApplicationName("SharedCookieApp");
            

            services.ConfigureApplicationCookie(options =>
            {
                ////Cookie settings
                //options.ExpireTimeSpan = TimeSpan.FromMinutes(180);
                options.Cookie.Name = ".AspNet.SharedCookie";
                options.Cookie.Path = "/";
                options.LoginPath = "/Identity/Account/Login";
                options.AccessDeniedPath = "/Identity/Account/AccessDenied";
                options.SlidingExpiration = true;

            });

            services.AddDefaultIdentity<ApplicationUser>()
                .AddRoles<IdentityRole>()
                .AddDefaultUI(UIFramework.Bootstrap4)
                .AddEntityFrameworkStores<ApplicationDbContext>();
                         

            // Auto Mapper Configurations
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new MessageProfile());
                mc.AddProfile(new RoomProfile());
                mc.AddProfile(new UserProfile());
            });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<MessageModel, MessageViewModel>()
              .ForMember(dst => dst.From, opt => opt.MapFrom(x =>
                    CultureInfo.CurrentCulture.TextInfo.ToTitleCase(x.FromUser.Namempe.ToLower())))
                 .ForMember(dst => dst.To, opt => opt.MapFrom(x => x.ToRoom.Name.ToString()))
                 .ForMember(dst => dst.Avatar, opt => opt.MapFrom(x => "~/../../image/User/" + x.FromUser.Codempid.Trim() + ".jpg"))
                 .ForMember(dst => dst.Content, opt => opt.MapFrom(x => BasicEmojis.ParseEmojis(x.Content)))
                 .ForMember(dst => dst.Timestamp, opt => opt.MapFrom(x => new DateTime(long.Parse(x.Timestamp)).ToLongTimeString()));
                cfg.CreateMap<MessageViewModel, MessageModel>();
            });
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll", builder =>
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader()
                           .AllowCredentials());
            });

            services.AddSignalR(o =>
            {
                o.EnableDetailedErrors = true;
            });


            services.AddSingleton<IUserConnectionManager, UserConnectionManager>();
            services.AddTransient<ISurveyApp, SurveyAppControl>();
            services.AddTransient<ISendEmail, SendEmail>();
            services.AddTransient<IEReservationRoom, EReservationRoomControl>();
            services.AddTransient<IEReservationRoomSetting, EReservationRoomSettingControl>();
            services.AddTransient<IEReservationAsset, EReservationAssetControl>();
            services.AddTransient<IEAppointment, EAppointmentControl>();
            services.AddTransient<IDynamicFormService, DynamicFormService>();
            services.AddTransient<IEJounalVoucher, EJounalVoucher>();
            services.AddTransient<ISearchEngineService, SearchEngineService>();
            services.AddTransient<IMachineOperation, MachineOperation>();
            services.AddTransient<IEForm, EForm>();
            services.AddTransient<IDashboard,Dashboard>();
            services.AddTransient<IEUser, EUser>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddTransient<IPDFFormService, PDFFormService>();
            services.AddTransient<IInqueryDataService,InqueryDataService>();
            services.AddTransient<IConnForm, ConnForm>();
            services.AddSingleton<IHitControl, HitControl>();

            //dependency injection
            services.AddDbContext<ESmartNotiContext>(options => options.UseSqlServer(Configuration.GetConnectionString("TableDependency")));
            services.AddSingleton<IESmartNotiRepository, ESmartNotiRepository>();
            services.AddSingleton<EsmartDatabaseSubscription, EsmartDatabaseSubscription>();



            services.Configure<FormOptions>(options =>
            {
                options.ValueCountLimit = int.MaxValue;
                options.ValueLengthLimit = int.MaxValue;

                options.MultipartBodyLengthLimit = int.MaxValue;

                options.MultipartHeadersLengthLimit = int.MaxValue;
                options.MultipartBodyLengthLimit = 1048576000;

            });
       
            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequiredLength = 6;
                options.Password.RequiredUniqueChars = 1;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                options.Lockout.MaxFailedAccessAttempts = 10;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = false;
            });

            services.AddLocalization(opts => { opts.ResourcesPath = "Resources"; });
            services.AddMvc()
                .AddViewLocalization(
                    LanguageViewLocationExpanderFormat.Suffix,
                    opts => { opts.ResourcesPath = "Resources"; })
                .AddDataAnnotationsLocalization();


            services.Configure<RequestLocalizationOptions>(
        opts =>
        {
            var supportedCultures = new List<CultureInfo>
            {
                new CultureInfo("en-GB"),
             
            };

            opts.DefaultRequestCulture = new RequestCulture("en-GB");
            // Formatting numbers, dates, etc.
            opts.SupportedCultures = supportedCultures;
            // UI strings that we have localized.
            opts.SupportedUICultures = supportedCultures;
        });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);


            services.AddTransient<MenuMasterService, MenuMasterService>();
            services.AddTransient<MenuMasterService2, MenuMasterService2>();

            //var connection = @"server=(local)\SQLEXPRESS;Database=ESmart;Integrated Security = True";

            //services.AddDbContext<ESmartContext>(options => options.UseSqlServer(connection));

            services.AddSingleton<IFileProvider>(
            new PhysicalFileProvider(
                Path.Combine(Directory.GetCurrentDirectory(), "wwwroot")));

            services.Configure<ForwardedHeadersOptions>(options => options.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto);
           
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
                //app.UseExceptionHandler("/Home/Error");
            }

      

            var options = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(options.Value);
            app.UseAuthentication();
            app.UseCors("AllowAll");
            app.UseMvc();
            //loggerFactory.AddLog4Net();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "/{controller=Home}/{action=Welcome}/{id?}");
            });
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            //app.UseSession();

            app.UseCookiePolicy();

           
            
            app.UseSignalR(routes =>
            {
                routes.MapHub<NotiHub>("/NotiHub");
                routes.MapHub<ChatHub>("/ChatHub");
            });
           
            //var webSocketOptions = new WebSocketOptions()
            //{
            //    KeepAliveInterval = TimeSpan.FromSeconds(2),
            //    ReceiveBufferSize = 6 * 1024
            //};
            ////app.UseMiddleware(typeof(VisitorCounterMiddleware));
            //app.UseWebSockets(webSocketOptions);
          
            app.UseSqlTableDependency<EsmartDatabaseSubscription>(Configuration.GetConnectionString("TableDependency"));
            app.UseSitemapMiddleware();
            //loggerFactory.AddLog4Net();
            //app.UseFastReport();
            //CreateUserRoles(serviceProvider).Wait();
        }
        private async Task CreateUserRoles(IServiceProvider serviceProvider)
        {
            var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var UserManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();

            IdentityResult roleResult;
            //Adding Addmin Role  
            var roleCheck = await RoleManager.RoleExistsAsync("Admin");
            if (!roleCheck)
            {
                //create the roles and seed them to the database  
                roleResult = await RoleManager.CreateAsync(new IdentityRole("Admin"));
            }

            roleCheck = await RoleManager.RoleExistsAsync("Manager");
            if (!roleCheck)
            {
                //create the roles and seed them to the database  
                roleResult = await RoleManager.CreateAsync(new IdentityRole("Manager"));
            }
            roleCheck = await RoleManager.RoleExistsAsync("User");
            if (!roleCheck)
            {
                //create the roles and seed them to the database  
                roleResult = await RoleManager.CreateAsync(new IdentityRole("User"));
            }


        }


      

    }
}