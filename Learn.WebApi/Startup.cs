using Autofac;
using Learn.IService;
using Learn.Repository.Base;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Learn.WebApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        private const string ProjectName = "Learn.WebApi";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            #region Swagger

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = ProjectName,
                    Version = "v1",
                    Description = $"{ProjectName} HTTP API ",
                    TermsOfService = new Uri("https://github.com/dingshuanglei"),
                    Contact = new OpenApiContact { Name = "丁双磊", Email = "shuangleiding@163.com", Url = new Uri("https://github.com/dingshuanglei") }
                });

                var basePath = Path.GetDirectoryName(typeof(Program).Assembly.Location);
                var xmlPath = Path.Combine(basePath, $"{ProjectName}.xml");
                c.IncludeXmlComments(xmlPath, true);
                var modelXmlPath = Path.Combine(basePath, "Learn.Model.xml");
                c.IncludeXmlComments(modelXmlPath, true);

                // Add security definitions
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Description = "Please enter into field the word 'Bearer' followed by a space and the JWT value",
                    Name = "token",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference()
                            {
                                Id = "Bearer",
                                Type = ReferenceType.SecurityScheme
                            }
                        }, Array.Empty<string>()
                    }
                });
            });

            #endregion

            #region db init

            InitDbTable.InitTable();
            
            #endregion

            services.AddControllers();
        }


        /// <summary>
        /// autofac ioc
        /// </summary>
        /// <param name="builder"></param>
        public void ConfigureContainer(ContainerBuilder builder)
        {
            //使用单例模式
            //builder.RegisterType<Test>().As<ITest>().SingleInstance().PropertiesAutowired();

            //单接口多实现
            //builder.RegisterType<TestOne>().Named<ITest>(nameof(TestOne));
            //builder.RegisterType<TestTwo>().Named<ITest>(nameof(TestTwo));

            Assembly[] assembliesService = new Assembly[] { Assembly.Load("Learn.Service") };
            builder.RegisterAssemblyTypes(assembliesService).Where(type => !type.IsAbstract && typeof(IocService).IsAssignableFrom(type)).AsImplementedInterfaces().PropertiesAutowired();

            Assembly[] assembliesRepository = new Assembly[] { Assembly.Load("Learn.Repository") };
            builder.RegisterAssemblyTypes(assembliesRepository).Where(type => !type.IsAbstract).AsImplementedInterfaces().PropertiesAutowired();
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            #region Swagger
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", $"v1");
                c.RoutePrefix = string.Empty;
            });
            #endregion

            app.UseAuthorization();

            //使用静态文件
            app.UseStaticFiles();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
