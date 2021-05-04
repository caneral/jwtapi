using AppCore.Utilities.Security.Encryption;
using AppCore.Utilities.Security.Jwt;
using Jwt.Business.Abstract;
using Jwt.Business.Concrete;
using Jwt.DAL.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using PBS.Business.Abstract;
using PBS.Business.Concrete;
using System;

namespace Jwt.WebAPI
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
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder =>
                    // Servisi belirli bir adresten gelen taleplere ama
                    //builder.WithOrigins("http://localhost:3000","http://94.73.164.170:")
                    // Servisi tm taleplere ama
                     builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Jwt.WebAPI", Version = "v1" });
            });

            services.AddDbContext<JwtDbContext>();
            services.AddScoped<IPersonService, PersonService>();
            services.AddScoped<IAppUserService, AppUserService>();
            services.AddScoped<IAppClaimService, AppClaimService>();
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<ITokenHelper, TokenHelper>();

            var tokenOptions = Configuration.GetSection("TokenOptions").Get<TokenOptions>();
            services.Configure<TokenOptions>(Configuration.GetSection("TokenOptions"));
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(jwtopt =>
            {
                jwtopt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
                {
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    // Token Geçerlilik Süresini Kontrol Et.
                    ValidateLifetime = true,
                    // 
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = tokenOptions.Issuer,
                    ValidAudience = tokenOptions.Audience,
                    IssuerSigningKey = SecurityKeyHelper.CreateSecurityKey(tokenOptions.SecurityKey),
                    ClockSkew = TimeSpan.Zero
                };
            });


            //servisler acildi
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors("CorsPolicy");

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => {
                    c.SwaggerEndpoint("v1/swagger.json", "Jwt.WebAPI v1");
                    //c.SwaggerEndpoint("/test/swagger/v1/swagger.json", "Test.WebAPI v1");
                });

                app.UseCors(option =>
                option.AllowAnyOrigin()
                    .AllowAnyMethod()
                     .AllowAnyHeader()
                     );
            }


            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
