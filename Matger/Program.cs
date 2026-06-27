using System.Text;
using System.Text.Json;
using Matger.Core;
using Matger.Core.Entity.Identity;
using Matger.Core.Repository;
using Matger.Core.Services;
using Matger.Errors;
using Matger.Helpers;
using Matger.Middlewares;
using Matger.Repository;
using Matger.Repository.Data;
using Matger.Repository.Identity;
using Matger.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using StackExchange.Redis;

namespace Matger
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<StoreContext>(Options =>
            {
                Options.UseSqlServer(builder.Configuration.GetConnectionString("DefualtConnection")
                    );
            });

            builder.Services.AddDbContext<AppIdentityDbContext>(Options =>
            {
                Options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection")
                    );

            });

            builder.Services.AddSingleton<IConnectionMultiplexer>(Option =>
            {

                var Connection = builder.Configuration.GetConnectionString("RedisConnection");

                return ConnectionMultiplexer.Connect(Connection);
            }
            );

            builder.Services.AddControllers();
            


            builder.Services.AddScoped<ITokenSevice, TokenSevice>();

            builder.Services.AddIdentity<AppUser, IdentityRole>().AddEntityFrameworkStores<AppIdentityDbContext>();
            builder.Services.AddAuthentication(Options =>
            {
                Options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                Options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(Options =>
                {
                    Options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
                        ValidateAudience = true,
                        ValidAudience = builder.Configuration["JWT:ValidAudience"],
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"])),
                    };
                });

            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped(typeof(IOrderService), typeof(OrderService));
            builder.Services.AddScoped(typeof(IGenericRepository<>),typeof( GenericRepository<>));
            builder.Services.AddScoped(typeof(IBasketRepository), typeof(BasketRepository));
            builder.Services.AddScoped<IPaymentService, PaymentService>();


            builder.Services.AddAutoMapper(typeof(MappingProfiles));

            builder.Services.Configure<ApiBehaviorOptions>(Option =>
            {
                Option.InvalidModelStateResponseFactory = (ActionContext) =>
                {
                    var errors = ActionContext.ModelState.Where(p => p.Value.Errors.Count() > 0)  //to hold validation errors 
                                                        .SelectMany(p => p.Value.Errors)          //statuscode=400 
                                                        .Select(e => e.ErrorMessage)              //message= badrequest
                                                        .ToArray();
                    var validationErrorRespose = new ApiValidationErrorRespose()
                    {
                        Errors = errors
                    };

                    return new BadRequestObjectResult(validationErrorRespose);

                };

            });
            builder.Services.AddOpenApi();
            #endregion

            var app = builder.Build();

            #region Update-Database
            var scope = app.Services.CreateScope();
            var services = scope.ServiceProvider;
            var loggerFactory = services.GetRequiredService<ILoggerFactory>();
            try
            {
                var dbcontext = services.GetRequiredService<StoreContext>();
                await dbcontext.Database.MigrateAsync();

                var Identitydbcontext = services.GetRequiredService<AppIdentityDbContext>();
                await Identitydbcontext.Database.MigrateAsync();

                var _userManger = services.GetRequiredService<UserManager<AppUser>>();
                await AppIdentityDbContextSeed.SeedUserAsynk(_userManger);
                await StoreContextSeed.SeedAsynk(dbcontext);    
            }
            catch (Exception ex)
            {
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "An Error During Migartion");
            }
            #endregion

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMiddleware<ExceptionMiddleware>();
                app.MapOpenApi();
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseStaticFiles();

            app.UseStatusCodePagesWithReExecute("/errors/{0}");//if endpoint is not found

            app.UseHttpsRedirection();
            app.UseAuthentication();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
