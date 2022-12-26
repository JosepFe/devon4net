module Devon4netCliConsts

open System.IO

let componentsJsonFileName = "components.json"

let devon4netTemplate_instalation = "dotnet new install Devon4Net.WebAPI.Template::6.0.11"
let devon4netTemplate_launch = "dotnet new Devon4NetAPI"

let devon4netAppPath = Path.Combine("Templates","WebAPI","Devon4Net.Application.WebAPI")

let baseDirectory = Directory.GetParent(__SOURCE_DIRECTORY__)
let jejuneDirectory = Path.Combine (baseDirectory.Parent.Parent.Parent.ToString(), "Jejune", "src")
let templates_path = Path.Combine(jejuneDirectory.ToString(),"Templates")
