using MessageBoard.API.Infrastructure.Entites;
using MessageBoard.API.Repositories;
using MessageBoard.API.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace MessageBoard.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; private set; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<MessageDBContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("MessageDBContext")));

            services.AddScoped<IMsgService, MsgService>();
            services.AddScoped<IMsgRepository, MsgRepository>();

            services.AddCors(options => {
                options.AddPolicy("CorsPolicy", policy =>
                {
                    policy.WithOrigins("https://localhost:5001")
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowCredentials();
                });
            });
            services.AddControllers()
                .ConfigureApiBehaviorOptions(options =>
                {
                    // 關閉驗證失敗 HTTP 400
                   options.SuppressModelStateInvalidFilter = true;
                });

            services.AddSwaggerDocument(config =>
            {
                config.PostProcess = document =>
                {
                    document.Info.Version = "v1";
                    document.Info.Title = "Message API";
                    document.Info.Description = "A simple ASP.NET Core web API";
                    document.Info.TermsOfService = "None";
                    document.Info.Contact = new NSwag.OpenApiContact
                    {
                        Name = "聯絡人 搬去金星了喔",
                        Email = string.Empty,
                        Url = ""
                    };
                    document.Info.License = new NSwag.OpenApiLicense
                    {
                        Name = "授權編譯器可以讀取包含授權資訊的文字檔，並產生可內嵌於通用語言執行平台可執行檔的二進位檔案做為資源。",
                        Url = "https://docs.microsoft.com/zh-tw/dotnet/framework/tools/lc-exe-license-compiler"
                    };
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseOpenApi();
            app.UseSwaggerUi3();
            
            app.UseCors("CorsPolicy");
            app.UseHttpsRedirection();
            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
