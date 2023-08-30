using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using UpdateUIWithSignalR.BL;
using UpdateUIWithSignalR.Hubs;

var builder = Host.CreateDefaultBuilder(args)
    .ConfigureWebHostDefaults(webBuilder =>
    {
        webBuilder.ConfigureServices(services =>
        {
            services.AddControllersWithViews();
            services.AddSignalR();
            services.AddSingleton<AdminHub>();
            services.AddSingleton<AdminJobs>();
        });

        webBuilder.Configure(app =>
        {
            if (!app.ApplicationServices.GetRequiredService<IWebHostEnvironment>().IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<AdminHub>("/adminHub");
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Admin}/{action=Index}/{id?}");
            });
        });
    });

var app = builder.Build();
app.Run();