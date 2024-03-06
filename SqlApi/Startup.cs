using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Serialization;
using SqlApi.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using SqlApi.Services;
using System.Net;

namespace SqlApi
{
    public class Startup
    {

        public string ConnectionString { get; set; }
        public string ConnectionStringStok { get; set; }
        public string ConnectionStringFav { get; set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            ConnectionString = Configuration.GetConnectionString("Connn");
            ConnectionStringStok = Configuration.GetConnectionString("Conn");
            ConnectionStringFav = Configuration.GetConnectionString("Con");
        }


        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)

        {
            services.AddResponseCaching();
            services.AddRazorPages();
            services.AddResponseCompression();
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateLifetime= true,
                    ValidateIssuerSigningKey=true,
                    IssuerSigningKey=new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.GetSection("AppSettings:Secret").Value)),
                    ValidateIssuer=false,
                    ValidateAudience=false
                };
            });
            services.AddCors(options =>
            {
                options.AddPolicy(name: "AllowOrigin",
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:44332", "http://localhost:50331", "http://192.168.2.13", "http://192.168.2.168:8080", "http://nova.efece.com", "http://192.168.2.209", "http://192.168.2.202", "http://192.168.2.13:81", "http://192.168.2.13:85", "http://192.168.35.40")
                                            .AllowAnyHeader()
                                            .AllowAnyMethod();
                    });
            });
            services.AddControllers();
           
            var appSettingsSection = Configuration.GetSection("FcmNotification");
            services.Configure<FcmNotificationSetting>(appSettingsSection);

            // Register the swagger generator
            services.AddSwaggerGen(options => {
                options.SwaggerDoc("v1", new Swashbuckle.AspNetCore.Swagger.Info
                {
                    Version = "v1",
                    Title = ".NET Core Swagger"
                });
            });
            services.AddDbContext<FavContext>(options => options.UseSqlServer(ConnectionStringFav));
            services.AddDbContext<StokContext>(options => options.UseSqlServer(ConnectionStringStok));
            services.AddDbContext<UserRoleContext>(options => options.UseSqlServer(ConnectionString));
            services.AddDbContext<TokenExampleContext>(options => options.UseSqlServer(ConnectionString));
            services.AddDbContext<UserContext>(options => options.UseSqlServer(ConnectionString));
            services.AddControllersWithViews().AddNewtonsoftJson(options=>options.SerializerSettings.ReferenceLoopHandling=Newtonsoft.Json.ReferenceLoopHandling.Ignore).AddNewtonsoftJson(options=>options.SerializerSettings.ContractResolver=new DefaultContractResolver());
            services.AddControllers();
            services.Configure<MongoDbSettings>(Configuration.GetSection("MongoDB"));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseResponseCompression();
            app.UseResponseCaching();
            /*app.UseCors(options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });*/
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            // Enable middleware to serve generated Swagger as a JSON endpoint
            app.UseSwagger();

            app.UseSwaggerUI(options => {
                options.DocumentTitle = "NOVA_API";
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "NOVA_API");
            });
            //app.UseIPFilter();
            app.UseCors("AllowOrigin");
            app.UseCors(bldr => bldr.WithOrigins("http://192.168.2.13:80").AllowAnyMethod()
   .AllowAnyHeader().AllowAnyOrigin());
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
        }
    }
}
