module Devon4netCli.Program
open System

let mutable Webapi = "1. WebAPI Application"
let mutable ConsoleApp = "2. Console Application"
let mutable AwsLambda = "3. AwsLambda"
let mutable AzureAppService = "4. AzureAppService"

let printMenu () =
    printfn "Menu: "
    printfn "%s" Webapi
    printfn "%s" ConsoleApp
    printfn "%s" AwsLambda
    printfn "%s" AzureAppService
    printf "Enter your choise: "

let getInput () = Int32.TryParse (Console.ReadLine())

let WebApiGen() =
    Devon4netCli.WebapiComponents.webapi_components_menu()

let ConsoleGen() =
    Devon4netCli.ConsoleAppComponents.console_app_components_menu()

let AwsLambdaGen() =
    Devon4netCli.AwsLambdaComponents.aws_lambda_components_menu()

let AzureAppServiceGen() =
    Devon4netCli.AzureAppServiceComponents.azure_appService_components_menu()


let rec menu () =
    printMenu()
    match getInput() with
    | true, 1 -> 
        WebApiGen()
    | true, 2 -> 
        ConsoleGen()
    | true, 3 -> 
        AwsLambdaGen()
    | true, 4 -> 
        AzureAppServiceGen()
    | _ -> menu()

System.Console.Clear();
menu ()