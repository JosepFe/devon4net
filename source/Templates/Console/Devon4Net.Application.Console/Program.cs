// See https://aka.ms/new-console-template for more information

using Devon4Net.Application.Console;
using Microsoft.Extensions.DependencyInjection;

var consoleExtension = new ConsoleExtension();
consoleExtension.GetConfigurationObjects(out var configuration, serviceCollection: out IServiceCollection serviceCollection);

