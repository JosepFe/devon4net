module Devon4NetCli.Apps.ConsoleAppComponents
open Devon4NetCli.Consts.Consts
open Devon4NetCli.Utils.Utils
open System
open System.Text.Json
open System.IO
open JejuneCmd.Gen

let mutable CircuitBreaker = "\t1. CircuitBreaker"
let mutable RabbitMq = "\t2. RabbitMq"
let mutable MediatR = "\t3. MediatR"
let mutable Kafka = "\t4. Kafka"
let mutable Nexus = "\t5. Nexus"
let mutable Done = "\t6. Done"

let printMenu () =
    printLogo()
    printfn "Choose your components for your Console App: "
    printfn ""
    printfn $"%s{CircuitBreaker}"
    printfn $"%s{RabbitMq}"
    printfn $"%s{MediatR}"
    printfn $"%s{Kafka}"
    printfn $"%s{Nexus}"
    printfn ""
    printfn $"%s{Done}ç"
    printfn ""
    printf "Enter your choice: "

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

    let installTemplateProcess = executeProcess output_path devon4net_console_template_instalation
    installTemplateProcess.WaitForExit()
    let launchTemplateProcess = executeProcess output_path devon4netConsoleTemplate_launch
    launchTemplateProcess.WaitForExit()

    let destinationPath = Path.Combine(output_path, devon4netConsoleAppPath)

    devon4net_console_components_generation componentInfo webapi_console_path destinationPath

    printf "Completed, press any key to close"
    Console.ReadLine()

let rec console_app_components_menu () =
    Console.Clear();
    printMenu()
    match getInput() with
    | true, 1 -> 
        CircuitBreaker <- choice(CircuitBreaker)
        Console.Clear();
        console_app_components_menu()
    | true, 2 -> 
        RabbitMq <- choice(RabbitMq)
        Console.Clear();
        console_app_components_menu()
    | true, 3 -> 
        MediatR <- choice(MediatR)
        Console.Clear();
        console_app_components_menu()
    | true, 4 -> 
        Kafka <- choice(Kafka)
        Console.Clear();
        console_app_components_menu()
    | true, 5 -> 
        Nexus <- choice(Nexus)
        Console.Clear();
        console_app_components_menu()
    | true, 6 -> 
        createWebApiProject() |> ignore
        exit 0
    | _ -> console_app_components_menu()

console_app_components_menu ()