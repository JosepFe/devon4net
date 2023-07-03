module Devon4netCli.Utils
open System.Diagnostics
open System
open System.IO

let executeProcess (source_path: string) (command: string) =
    let launchTemplate = new ProcessStartInfo "cmd.exe"
    launchTemplate.WindowStyle <- ProcessWindowStyle.Normal;
    launchTemplate.WorkingDirectory <- source_path
    launchTemplate.Arguments <- "/c " + command;
    launchTemplate.CreateNoWindow <- true;
    launchTemplate.UseShellExecute <- false;
    // *** Redirect the output ***
    launchTemplate.RedirectStandardError <- true;
    launchTemplate.RedirectStandardOutput <- true;
    let launchTemplateResult = Process.Start(launchTemplate);
    launchTemplateResult

let choice (devonComponent:string) =
    if devonComponent.Contains("+") then
        devonComponent.Remove(devonComponent.Length - 2)
    else
        devonComponent + " +"

let getInput () = Int32.TryParse (Console.ReadLine())

let printLogo() =
    printfn "%s" """
  ____                        _  _              _    ____ _ _
 |  _ \  _____   _____  _ __ | || |  _ __   ___| |_ / ___| (_)
 | | | |/ _ \ \ / / _ \| '_ \| || |_| '_ \ / _ \ __| |   | | |
 | |_| |  __/\ V / (_) | | | |__   _| | | |  __/ |_| |___| | |
 |____/ \___| \_/ \___/|_| |_|  |_| |_| |_|\___|\__|\____|_|_|

"""

let generate_webapi_scaffolding_crud (connectionString: string) (contextName: string) (outputPath: string) (csprojPath: string) =

    JejuneCmd.Utils.instertLine csprojPath Devon4netCliConsts.entityFrameworkCore_tools_package_insert Devon4netCliConsts.entityFrameworkCore_design_package_index 0
    JejuneCmd.Utils.instertLine csprojPath Devon4netCliConsts.entityFrameworkCore_design_package_insert Devon4netCliConsts.entityFrameworkCore_design_package_index 0

    let command = Devon4netCliConsts.scaffold_sqlServer_comand connectionString contextName

    let commandResult = executeProcess (Path.Combine(outputPath, Devon4netCliConsts.devon4netAppPath)) command
    commandResult.WaitForExit()

    JejuneCmd.Utils.instertLine (Devon4netCliConsts.appsettingsPath outputPath) (Devon4netCliConsts.appsettings_database_index contextName connectionString) Devon4netCliConsts.appsettings_database_insert 0
    JejuneCmd.Utils.instertLine (Devon4netCliConsts.devonConfigurationPath outputPath) (Devon4netCliConsts.setup_database_insert contextName) Devon4netCliConsts.setup_database_index 1

    let jejuneEntities = JejuneCmd.Utils.generateJejuneEntities (Path.Combine(outputPath, Devon4netCliConsts.devon4netAppPath)) contextName

    JejuneCmd.Gen.generate_crud_files jejuneEntities Devon4netCliConsts.jejune_templates_path (Path.Combine(outputPath, Devon4netCliConsts.devon4netAppPath))