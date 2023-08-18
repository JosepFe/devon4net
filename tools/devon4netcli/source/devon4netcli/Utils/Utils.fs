namespace Devon4NetCli.Utils

open Devon4NetCli.Consts.Consts
open System.Diagnostics
open System
open System.IO
open JejuneCmd.Gen.WebApiMonolithicAppGen.WebApiMonolithicAppGen
open JejuneCmd.Gen.WebApiCleanArchitectureGen

module Utils =

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

        JejuneCmd.Utils.instertLine csprojPath entityFrameworkCore_tools_package_insert entityFrameworkCore_design_package_index 0
        JejuneCmd.Utils.instertLine csprojPath entityFrameworkCore_design_package_insert entityFrameworkCore_design_package_index 0

        let command = scaffold_sqlServer_comand connectionString contextName

        let commandResult = executeProcess (Path.Combine(outputPath, devon4netAppPath)) command
        commandResult.WaitForExit()

        JejuneCmd.Utils.instertLine (appsettingsPath outputPath) (appsettings_database_index contextName connectionString) appsettings_database_insert 0
        JejuneCmd.Utils.instertLine (devonConfigurationPath outputPath) (setup_database_insert contextName) setup_database_index 1

        let jejuneEntities = JejuneCmd.Utils.generateJejuneEntities (Path.Combine(outputPath, devon4netAppPath)) contextName

        generate_crud_files jejuneEntities jejune_templates_path (Path.Combine(outputPath, devon4netAppPath))

    let generate_clean_architecture_scaffolding_crud (connectionString: string) (contextName: string) (outputPath: string) (csprojPath: string) =

        JejuneCmd.Utils.instertLine csprojPath entityFrameworkCore_tools_package_insert entityFrameworkCore_design_package_index 0
        JejuneCmd.Utils.instertLine csprojPath entityFrameworkCore_design_package_insert entityFrameworkCore_design_package_index 0

        let command = scaffold_sqlServer_comand connectionString contextName

        let commandResult = executeProcess (Path.Combine(outputPath, devon4netAppPath)) command
        commandResult.WaitForExit()

        JejuneCmd.Utils.instertLine (appsettingsPath outputPath) (appsettings_database_index contextName connectionString) appsettings_database_insert 0
        JejuneCmd.Utils.instertLine (devonConfigurationPath outputPath) (setup_database_insert contextName) setup_database_index 1

        let jejuneEntities = JejuneCmd.Utils.generateJejuneEntities (Path.Combine(outputPath, devon4netAppPath)) contextName

        generate_crud_clean_architecture jejuneEntities jejune_templates_path (Path.Combine(outputPath, devon4netAppPath))