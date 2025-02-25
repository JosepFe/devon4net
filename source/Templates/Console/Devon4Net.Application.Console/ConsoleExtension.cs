namespace Devon4Net.Application.Console
{
    using Devon4Net.Infrastructure.Common.Application.ApplicationTypes.Console;
    using Devon4Net.Infrastructure.Logger;
    using Microsoft.Extensions.DependencyInjection;

    public class ConsoleExtension : DevonfwConsole
    {
        protected override void ConfigureServices(IServiceCollection services)
        {
            services.SetupLogging(DevonfwConfigurationBuilder.Configuration);
        }
    }
}
