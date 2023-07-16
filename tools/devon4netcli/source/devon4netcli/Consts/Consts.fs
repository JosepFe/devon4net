module Devon4NetCli.Consts.Consts

open System.IO

let componentsJsonFileName = "components.json"

let devon4net_webapi_template_instalation = "dotnet new -i Devon4Net.WebAPI.Template::6.0.11"
let devon4net_console_template_instalation = "dotnet new -i Devon4Net.WebAPI.Console::6.0.11"
let devon4netWebApiTemplate_launch = "dotnet new Devon4NetAPI"

let devon4netConsoleTemplate_launch = "dotnet new Devon4NetConsole"

let devon4netAppPath = Path.Combine("Templates","WebAPI","Devon4Net.Application.WebAPI")
let devon4netConsoleAppPath = "Devon4Net.Application.Console"

let baseDirectory = Directory.GetParent(__SOURCE_DIRECTORY__)
let jejuneDirectory = Path.Combine (baseDirectory.Parent.Parent.Parent.ToString(), "Jejune", "src")
let webapi_templates_path = Path.Combine(jejuneDirectory.ToString(), "Templates", "Apps", "WebApi")
let webapi_console_path = Path.Combine(jejuneDirectory.ToString(), "Templates", "Apps", "Console")

let jejune_templates_path = Path.Combine(jejuneDirectory.ToString(), "Templates", "Crud")

let appsettingsPath (outputPath: string) = Path.Combine(outputPath, devon4netAppPath, "appsettings.Development.json")
let devonConfigurationPath (outputPath: string) = Path.Combine(outputPath, devon4netAppPath, "Configuration", "DevonConfiguration.cs")

//scaffold + database crud creation
let scaffold_postgres_comand (connectionString: string) (contextName: string) =  $"dotnet ef dbcontext scaffold \"{connectionString}\" Npgsql.EntityFrameworkCore.PostgreSQL --context-dir \"Domain\Database\" --output-dir \"Domain\Entities\" -c \"{contextName}\""
let scaffold_sqlServer_comand (connectionString: string) (contextName: string) =  $"dotnet ef dbcontext scaffold \"{connectionString}\" Microsoft.EntityFrameworkCore.SqlServer --context-dir \"Domain\Database\" --output-dir \"Domain\Entities\" -c \"{contextName}\""

let entityFrameworkCore_tools_package_insert = "    <PackageReference Include=\"Microsoft.EntityFrameworkCore.Tools\" Version=\"6.0.11\"/>"
let entityFrameworkCore_design_package_insert = "    <PackageReference Include=\"Microsoft.EntityFrameworkCore.Design\" Version=\"6.0.11\"/>"
let entityFrameworkCore_design_package_index = "<ItemGroup>"

let appsettings_database_index (contextName: string) (connectionString: string) = $"    \"{contextName}\": \"{connectionString}\","
let appsettings_database_insert = "ConnectionStrings"

let setup_database_index= "private static void SetupDatabase"
let setup_database_insert (contextName: string) = $"            services.SetupDatabase<{contextName}>(configuration, \"{contextName}\", DatabaseType.PostgreSQL, migrate: true);"