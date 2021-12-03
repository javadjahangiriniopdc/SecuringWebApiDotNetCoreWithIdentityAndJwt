using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SecuringWebApiDotNetCoreWithIdentityAndJwt.Areas.Identity.Data;
using SecuringWebApiDotNetCoreWithIdentityAndJwt.Data;

[assembly: HostingStartup(typeof(SecuringWebApiDotNetCoreWithIdentityAndJwt.Areas.Identity.IdentityHostingStartup))]
namespace SecuringWebApiDotNetCoreWithIdentityAndJwt.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<SecuringWebApiDotNetCoreWithIdentityAndJwtContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("SecuringWebApiDotNetCoreWithIdentityAndJwtContextConnection")));

                services.AddDefaultIdentity<SecuringWebApiDotNetCoreWithIdentityAndJwtUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<SecuringWebApiDotNetCoreWithIdentityAndJwtContext>();
            });
        }
    }
}