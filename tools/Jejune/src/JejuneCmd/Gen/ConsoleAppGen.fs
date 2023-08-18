namespace JejuneCmd.Gen.ConsoleAppGen

open JejuneCmd.JsonData
open JejuneCmd.Utils
open FSharp.Data

module ConsoleAppGen =

    let devon4net_console_components_generation (componentInfo: string) (frompath: string) (topath: string) =

        let appsettings = load_template frompath "Appsettings.Development.json.hbs"
        let csproj = load_template frompath "Devon4Net.Application.Console.csproj.hbs"
        let consoleExtension = load_template frompath "ConsoleExtension.cs.hbs"

        let webapiComponentData = jsonpropsToDict (JsonValue.Parse(componentInfo))

        component_apply_template webapiComponentData appsettings topath "Appsettings.Development.json"
        component_apply_template webapiComponentData csproj topath "Devon4Net.Application.Console.csproj"
        component_apply_template webapiComponentData consoleExtension topath "ConsoleExtension.cs"