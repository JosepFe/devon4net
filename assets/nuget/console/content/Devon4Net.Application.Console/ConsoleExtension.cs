using Devon4Net.Infrastructure.Common.Application.ApplicationTypes.Console;
using Devon4Net.Infrastructure.Logger;
using Microsoft.Extensions.DependencyInjection;

namespace Devon4Net.Application.Console
{
    public class ConsoleExtension : DevonfwConsole
    {
        protected override void ConfigureServices(IServiceCollection services)
        {
            services.SetupLog(DevonfwConfigurationBuilder.Configuration);
        }
    }
}
