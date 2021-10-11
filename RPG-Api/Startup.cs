using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using RPG.Api.Persistence;
using RPG.Api.Domain.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using RPG.Api.Domain.Repositories;
using RPG.Api.Persistence.Repositories;
using RPG.Api.Services;
using RPG.Api.Domain.Services.Security;
using RPG.Api.Services.Security.Tokens;
using RPG.Api.Domain.Services.Security.Tokens;
using RPG.Api.Domain.Repositories.Profile;
using RPG.Api.Persistence.Repositories.Profile;
using RPG.Api.Domain.Services.Profile;
using RPG.Api.Services.Profile;
using RPG.Api.Domain.Services.SGame;
using RPG.Api.Domain.Repositories.RGame;
using RPG.Api.Services.SGame;
using RPG.Api.Persistence.Repositories.RGame;
using RPG.Api.Domain.Repositories.RForum;
using RPG.Api.Domain.Services.SForum;
using RPG.Api.Persistence.Repositories.RForum;
using RPG.Api.Services.SForum;
using Microsoft.Extensions.FileProviders;
using System.IO;

namespace RPG.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        
        public void ConfigureServices(IServiceCollection services)
        {
            /*services.AddCors(options =>
            {
                options.AddPolicy("MyPolicy", builder => builder
                    .AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .SetIsOriginAllowed((host) => true)
                );
            });*/
            services.AddAutoMapper();
            services.AddMvc(options => options.EnableEndpointRouting = false).AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);
            services.AddDbContext<RpgDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Default")));

            services.AddSpaStaticFiles(configuration => {
                configuration.RootPath = "Client/dist";
            });

            services.AddScoped<IAuthorizeService, AuthorizeService>();
            services.AddSingleton<IPasswordHasher, PasswordHasher>();
            services.AddSingleton<ITokenHandler, TokenHandler>();

            services.Configure<TokenOptions>(Configuration.GetSection("TokenOptions"));
            var tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>();

            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IPersonalDataRepository, PersonalDataRepository>();
            services.AddScoped<IPersonalDataService, PersonalDataService>();

            services.AddScoped<IPhotoService, PhotoService>();
            services.AddScoped<IPhotoRepository, PhotoRepository>();

            services.AddScoped<IFriendRepository, FriendRepository>();
            services.AddScoped<IFriendService, FriendService>();

            services.AddScoped<IMessageRepository, MessageRepository>();
            services.AddScoped<IMessageService, MessageService>();
            services.AddScoped<INotificationRepository, NotificationRepository>();
            services.AddScoped<INotificationService, NotificationService>();

            services.AddScoped<IGameToPersonRepository, GameToPersonRepository>();
            services.AddScoped<IGameToPersonService, GameToPersonService>();
            services.AddScoped<IGameRepository, GameRepository>();
            services.AddScoped<IGameService, GameService>();

            services.AddScoped<IForumRepository, ForumRepository>();
            services.AddScoped<IForumService, ForumService>();

            services.AddScoped<ITopicToPersonRepository, TopicToPersonRepository>();
            services.AddScoped<ITopicToPersonService, TopicToPersonService>();
            services.AddScoped<ITopicRepository, TopicRepository>();
            services.AddScoped<ITopicService, TopicService>();

            services.AddScoped<IMessageForumRepository, MessageForumRepository>();
            services.AddScoped<IMessageForumService, MessageForumService>();

            services.AddScoped<ISkillRepository, SkillRepository>();
            services.AddScoped<ISkillService, SkillService>();
            services.AddScoped<IMySkillRepository, MySkillRepository>();
            services.AddScoped<IMySkillService, MySkillService>();

            services.AddScoped<IUnitOfWork, UnitOfWork>();
            
            services.AddScoped<DbRepository>();

            services.AddSignalR();

            var signingConfigurations = new SigningConfigurations();
            services.AddSingleton(signingConfigurations);

            services
               .AddAuthentication(
                   options =>
                   {
                       options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                       options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                   })
              .AddJwtBearer(options =>
              {
                  options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                  {
                      ValidateIssuer = true,
                      ValidateAudience = true,
                      ValidateLifetime = true,
                      ValidateIssuerSigningKey = true,
                      ValidIssuer = tokenOptions.Issuer,
                      ValidAudience = tokenOptions.Audience,
                      IssuerSigningKey = signingConfigurations.Key,
                      ClockSkew = TimeSpan.Zero
                      //ValidIssuer = Configuration["Jwt:Issuer"],
                      //ValidAudience = Configuration["Jwt:Issuer"],
                      // IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                  };
              });
        }
        
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //app.UseCors("MyPolicy");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(builder => builder
              .AllowAnyHeader()
              .AllowAnyMethod()
              .SetIsOriginAllowed((host) => true)
              .AllowCredentials()
             );

            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseSpaStaticFiles();
            app.UseSpa(spa =>
            {
                spa.Options.SourcePath = "Client/dist";
            });
        }
    }
}
