using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Todo.Domain.Handlers;
using Todo.Domain.Infra.Contexts;
using Todo.Domain.Infra.Repositories;
using Todo.Domain.Repositories;

namespace Todo.Domain.Api
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
            services.AddControllers();

            // services.AddDbContext<TodoContext>(option => option.UseInMemoryDatabase("Database"));
            services.AddDbContext<TodoContext>(option =>
                option.UseSqlServer(Configuration.GetConnectionString("todoContext")));
            services.AddTransient<ITodoRepository, TodoRepository>();
            services.AddTransient<TodoHandler, TodoHandler>();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.Authority = "https://securetoken.google.com/todos-90b10";
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidIssuer = "https://securetoken.google.com/todos-90b10",
                        ValidateAudience = true,
                        ValidAudience = "todos-90b10",
                        ValidateLifetime = true
                    };
                });
        }

        // apiKey: "AIzaSyBjmp2G2A5c_dtYrbl9JBkTFAkMeocb2r0",
        // authDomain: "todos-90b10.firebaseapp.com",
        // projectId: "todos-90b10",
        // storageBucket: "todos-90b10.appspot.com",
        // messagingSenderId: "871340090750",
        // appId: "1:871340090750:web:d36911bd203765c51fff2f"

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}