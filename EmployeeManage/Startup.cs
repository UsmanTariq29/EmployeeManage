using EmployeeManage.Controllers;
using EmployeeManage.Model;
using EmployeeManage.Repository;
using EmployeeManage.Repository.Interface;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.FileProviders;
using Microsoft.AspNetCore.Session;

using EmployeeManage.Utilities.FileUpload;
using EmployeeManage.Models;

namespace EmployeeManage
{
    public class Startup
    {
        private IConfiguration _confiq;

        public Startup(IConfiguration config)
        {
            _confiq = config;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        { 
            // register root upload path as file provider
            var fileProvider = new PhysicalFileProvider(_confiq.GetValue<string>("FileUpload:RootPath"));
            services.AddSingleton<IFileProvider>(fileProvider);
            // register file upload configurations as options
            foreach (FileUploadConfigTypeEnum config in Enum.GetValues(typeof(FileUploadConfigTypeEnum)))
            {
                services.Configure<FileUploadConfig>(config.ToString(),
                    _confiq.GetSection($"FileUpload:Config:{config}")
                );
            }
            // services.AddDbContextPool<EmployeesDBContext>(options => options.UseSqlServer(_confiq.GetConnectionString("ConnectionStringName")));
            // services.AddDbContextPool<SPToCoreContext>(options => options.UseSqlServer(_confiq.GetConnectionString("EmployeeDbConnection")));
            services.AddDbContext<EmployeesDBContext>(options => options.UseSqlServer(
               _confiq.GetConnectionString(_confiq.GetValue<string>("ConnectionStringNameSuffix"))));

            services.AddMvc(options =>options.EnableEndpointRouting = false).AddXmlDataContractSerializerFormatters() ;

            services.AddScoped<IEmployeeRepo,EmployeeRepo>();
            services.AddTransient<IDepartmentRepo, DepartmentRepo>();
            services.AddTransient<IDocumentCount, DocumentCountRepo>();
            services.AddTransient<IRegionCountryCity, RegionCountryCityRepo>();
            services.AddTransient<IDocument, DocumentRepo>();
            services.AddScoped<IUser, UserRepo>();
            services.AddScoped<IBarcodeRepo, BarcodeRepo>();
            services.AddScoped<IDashboardRepo, DashboardRepo>();
            services.AddScoped<IPromotionRepo, PromotionRepo>();
            services.AddScoped<ISupplierRepo,SupplierRepo >();
            services.AddScoped<IPurchaseRepo,PurchaseRepo>();
           
            services.AddScoped<IItemGroupRepo , ItemGroupRepo>();
            services.AddDistributedMemoryCache();

            services.AddHttpContextAccessor();
            services.AddSession(options => 
            {
                options.IOTimeout = TimeSpan.FromMinutes(5);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseStatusCodePagesWithReExecute("/Error/{0}");
            }

            app.UseSession();
            app.UseStaticFiles();
            app.UseMvc(routes =>
            {
                 routes.MapRoute("default", "{controller=Login}/{action=Index}/{id?}");
     //           routes.MapRoute("default", "{controller=Barcode}/{action=BarcodeSearch}/{id?}");
            });

        }
    }
}
