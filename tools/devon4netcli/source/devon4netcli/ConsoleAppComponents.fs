module Devon4netCli.ConsoleAppComponents
open System
open System.Text.Json
open Utils
open System.IO

let mutable CircuitBreaker = "1. CircuitBreaker"
let mutable Jwt = "2. Jwt"
let mutable RabbitMq = "3. RabbitMq"
let mutable MediatR = "4. MediatR"
let mutable Kafka = "5. Kafka"
let mutable Grpc = "6. Grpc"
let mutable Nexus = "7. Nexus"
let mutable Done = "8. Done"

let printMenu () =
    printfn "Menu: "
    printfn "%s" CircuitBreaker
    printfn "%s" Jwt
    printfn "%s" RabbitMq
    printfn "%s" MediatR
    printfn "%s" Kafka
    printfn "%s" Grpc
    printfn "%s" Nexus
    printfn "%s" Done
    printf "Enter your choise: "

let createWebApiProject() =
    //let componentInfo =
    //    JsonSerializer.Serialize(
    //        {| circuitbreaker = CircuitBreaker.Contains("+"); 
    //           jwt = Jwt.Contains("+"); 
    //           rabbitmq =RabbitMq.Contains("+"); 
    //           mediatr = MediatR.Contains("+"); 
    //           kafka = Kafka.Contains("+"); 
    //           grpc = Grpc.Contains("+"); 
    //           nexus = Nexus.Contains("+") |}
    //    )

    printf "Output path: "
    let output_path = Console.ReadLine()

    let launchTemplateProcess = executeProcess output_path Devon4netCliConsts.devon4netConsoleTemplate_launch
    launchTemplateProcess.WaitForExit()

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
        Jwt <- choice(Jwt)
        System.Console.Clear();
        console_app_components_menu()
    | true, 3 -> 
        RabbitMq <- choice(RabbitMq)
        System.Console.Clear();
        console_app_components_menu()
    | true, 4 -> 
        MediatR <- choice(MediatR)
        System.Console.Clear();
        console_app_components_menu()
    | true, 5 -> 
        Kafka <- choice(Kafka)
        System.Console.Clear();
        console_app_components_menu()
    | true, 6 -> 
        Grpc <- choice(Grpc)
        System.Console.Clear();
        console_app_components_menu()
    | true, 7 -> 
        Nexus <- choice(Nexus)
        System.Console.Clear();
        console_app_components_menu()
    | true, 8 -> 
        createWebApiProject()
        exit 0
    | _ -> console_app_components_menu()

console_app_components_menu ()