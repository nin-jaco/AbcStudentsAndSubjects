using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ABCSchool.Data;
using ABCSchool.Data.Interfaces;
using Microsoft.EntityFrameworkCore;
using ABCSchool.Data.Repositories;

namespace ABCSchool.WebApi
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
            services.AddControllers();
            string dbConn = Configuration.GetSection("ConnectionStrings").GetValue<string>("dbContext");
            services.AddDbContext<AbcSchoolDbContext>(options => options.UseSqlServer(dbConn));
            /*var db = new AbcSchoolDbContext(new DbContextOptionsBuilder<AbcSchoolDbContext>()
                .UseSqlServer(dbConn).Options);
            services.AddScoped<IStudentRepository, StudentRepository>(_ => new StudentRepository(db));
            services.AddScoped<ISubjectRepository, SubjectRepository>(_ => new SubjectRepository(db));*/
            services.AddScoped<StudentRepository>();
            services.AddScoped<SubjectRepository>();
            //services.AddScoped<StudentsSubjectsRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
