module Devon4netCli.ConsoleAppComponents
open System
open System.Text.Json
open Utils
open System.IO
open JejuneCmd.Gen

let mutable CircuitBreaker = "1. CircuitBreaker"
let mutable RabbitMq = "2. RabbitMq"
let mutable MediatR = "3. MediatR"
let mutable Kafka = "4. Kafka"
let mutable Nexus = "5. Nexus"
let mutable Done = "6. Done"

let printMenu () =
    printfn "Menu: "
    printfn "%s" CircuitBreaker
    printfn "%s" RabbitMq
    printfn "%s" MediatR
    printfn "%s" Kafka
    printfn "%s" Nexus
    printfn "%s" Done
    printf "Enter your choise: "

let createWebApiProject() =
    let componentInfo =
        JsonSerializer.Serialize(
            {| circuitbreaker = CircuitBreaker.Contains("+"); 
               rabbitmq =RabbitMq.Contains("+"); 
               mediatr = MediatR.Contains("+"); 
               kafka = Kafka.Contains("+"); 
               nexus = Nexus.Contains("+") |}
        )

    printf "Output path: "
    let output_path = Console.ReadLine()

    let launchTemplateProcess = executeProcess output_path Devon4netCliConsts.devon4netConsoleTemplate_launch
    launchTemplateProcess.WaitForExit()

    let destinationPath = Path.Combine(output_path, Devon4netCliConsts.devon4netConsoleAppPath)

    devon4net_console_components_generation componentInfo Devon4netCliConsts.webapi_console_path destinationPath

    printfn "Completed"

let rec console_app_components_menu () =
    System.Console.Clear();
    printfn "Console Application"
    printMenu()
    match getInput() with
    | true, 1 -> 
        CircuitBreaker <- choice(CircuitBreaker)
        System.Console.Clear();
        console_app_components_menu()
    | true, 2 -> 
        RabbitMq <- choice(RabbitMq)
        System.Console.Clear();
        console_app_components_menu()
    | true, 3 -> 
        MediatR <- choice(MediatR)
        System.Console.Clear();
        console_app_components_menu()
    | true, 4 -> 
        Kafka <- choice(Kafka)
        System.Console.Clear();
        console_app_components_menu()
    | true, 5 -> 
        Nexus <- choice(Nexus)
        System.Console.Clear();
        console_app_components_menu()
    | true, 6 -> 
        createWebApiProject()
        exit 0
    | _ -> console_app_components_menu()

console_app_components_menu ()