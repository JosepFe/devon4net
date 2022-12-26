module Utils

open System.Diagnostics
open System

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