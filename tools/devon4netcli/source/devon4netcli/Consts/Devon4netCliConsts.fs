module Devon4netCliConsts

open System.IO

let componentsJsonFileName = "components.json"

let devon4netTemplate_instalation = "dotnet new -i Devon4Net.WebAPI.Template::6.0.11"
let devon4netWebApiTemplate_launch = "dotnet new Devon4NetAPI"

let devon4netConsoleTemplate_launch = "dotnet new Devon4NetConsole"

let devon4netAppPath = Path.Combine("Templates","WebAPI","Devon4Net.Application.WebAPI")
let devon4netConsoleAppPath = "Devon4Net.Application.Console"

let baseDirectory = Directory.GetParent(__SOURCE_DIRECTORY__)
let jejuneDirectory = Path.Combine (baseDirectory.Parent.Parent.Parent.ToString(), "Jejune", "src")
let webapi_templates_path = Path.Combine(jejuneDirectory.ToString(), "Templates", "Apps", "WebApi")
let webapi_console_path = Path.Combine(jejuneDirectory.ToString(), "Templates", "Apps", "Console")
