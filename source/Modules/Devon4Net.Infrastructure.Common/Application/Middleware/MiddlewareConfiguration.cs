using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Devon4Net.Infrastructure.Common.Handlers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Devon4Net.Infrastructure.Common.Application.Options;
using Devon4Net.Infrastructure.Common.Application.Middleware.KillSwicth;
using Devon4Net.Infrastructure.Common.Application.Middleware.Headers;
using Devon4Net.Infrastructure.Common.Application.Middleware.Certificates;
using Devon4Net.Infrastructure.Common.Application.Middleware.CorrelationId;

namespace Devon4Net.Infrastructure.Common.Application.Middleware
{
    public static class MiddlewareConfiguration
    {
        public static void SetupMiddleware(this IApplicationBuilder app, IServiceCollection services)
        {
            using var serviceProvider = services.BuildServiceProvider();

            app.UseMiddleware<CorrelationIdMiddleware>();
            app.UseMiddleware<CustomHeadersMiddleware>();

            var killSwitch = serviceProvider.GetService<IOptions<KillSwitchOptions>>()?.Value;
            var certificates = serviceProvider.GetService<IOptions<CertificatesOptions>>()?.Value;

            if (killSwitch?.UseKillSwitch == true) app.UseMiddleware<KillSwicthMiddleware>();
            if (certificates?.ClientCertificate?.EnableClientCertificateCheck == true || certificates?.ClientCertificate?.RequireClientCertificate== true) app.UseMiddleware<ClientCertificatesMiddleware>();
        }

        public static void SetupMiddleware(this IServiceCollection services, IConfiguration configuration)
        {
            configuration.SetupHeaders();

            services.GetTypedOptions<KillSwitchOptions>(configuration, "KillSwitch");
            services.GetTypedOptions<CertificatesOptions>(configuration, "Certificates");
        }
    }
}
