module Devon4netCli.Program
open Devon4NetCli.Utils
open System
open Devon4NetCli.Apps

let mutable WebapiMonolithic = "\t1. WebAPI Monolithic"
let mutable WebApiCleanArchitecture = "\t2. WebAPI Clean Architecture"
let mutable ConsoleApplication = "\t3. Console"
let mutable AwsLambda = "\t4. Aws Lambda"
let mutable AzureAppService = "\t5. Azure AppService"

let printMenu () =
    Utils.printLogo()
    printfn "Choose your application type: "
    printfn ""
    printfn $"%s{WebapiMonolithic}"
    printfn $"%s{WebApiCleanArchitecture}"
    printfn $"%s{ConsoleApplication}"
    printfn $"%s{AwsLambda}"
    printfn $"%s{AzureAppService}"
    printfn ""
    printf "Enter your choice: "

let getInput () = Int32.TryParse (Console.ReadLine())

let WebApiGen() =
    WebApiMonolithicApp.webapi_monolithic_menu()

let CleanArchitectureGen() =
    CleanArchitectureApp.webapi_clean_architecture_menu()

let ConsoleGen() =
    ConsoleApp.console_menu()

let AwsLambdaGen() =
    AwsLambdaApp.aws_lambda_menu()

let AzureAppServiceGen() =
    AzureAppServiceApp.azure_appService_menu()

let rec menu () =
    printMenu()
    match getInput() with
    | true, 1 ->
        WebApiGen()
    | true, 2 ->
        CleanArchitectureGen()
    | true, 3 ->
        ConsoleGen()
    | true, 4 ->
        AwsLambdaGen()
    | true, 5 ->
        AzureAppServiceGen()
    | _ -> menu()

Console.Clear();
menu ()