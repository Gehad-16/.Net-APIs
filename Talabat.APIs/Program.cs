using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Talabat.APIs.Helpers;
using Talabat.Core.Entities.Identity;
using Talabat.Core.Repositories;
using Talabat.Core.Services;
using Talabat.Repository;
using Talabat.Repository.Data;
using Talabat.Repository.Identity;
using Talabat.Service;

namespace Talabat.APIs
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Configure Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddDbContext<StoreContext>(Options =>
            {
                Options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            }
            );

            builder.Services.AddDbContext<AppIdentityDBContext>(Options =>
            {
                Options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection"));
            }
           );

            // Allow dependency injection for all classes
            builder.Services.AddScoped(typeof(IGenericRepository<>),typeof(GenericRepository<>));
            builder.Services.AddAutoMapper(M => M.AddProfile(new MappingProfiles()));
            //for Identity
            builder.Services.AddScoped<ITokenService, TokenService>();
            builder.Services.AddIdentity<AppUser, IdentityRole>()
                            .AddEntityFrameworkStores<AppIdentityDBContext>();

            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
                Options =>
                {
                    Options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer= builder.Configuration["JWT:ValidIssuer"],
                        ValidAudience=builder.Configuration["JWT:ValidAudience"],
                        IssuerSigningKey= new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"])),
                    };
                }
                );
            #endregion

           var app = builder.Build();

            #region To Seed Data
            // Resolve the DbContext and call the static method in MyStaticClass
            using (var scope = app.Services.CreateScope())
            {
                var dbcontext = scope.ServiceProvider.GetRequiredService<StoreContext>();
                // Call the static method, passing the DbContext
                await StoreContextSeed.SeedAsync(dbcontext);


                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
                // Call the static method, passing the DbContext
                await AppIdentityDBContextSeed.SeedUserAsync(userManager);


            }

            #endregion

            #region  Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseStaticFiles();
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.UseAuthorization();
            app.MapControllers();

            app.Run(); 
            #endregion
        }
    }
}
