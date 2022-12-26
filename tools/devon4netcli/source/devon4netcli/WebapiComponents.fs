module Devon4netCli.WebapiComponents
open System.Text.Json
open JejuneCmd.Gen
open System.IO
open Utils
open System

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
    let componentInfo =
        JsonSerializer.Serialize(
            {| circuitbreaker = CircuitBreaker.Contains("+"); 
               jwt = Jwt.Contains("+"); 
               rabbitmq =RabbitMq.Contains("+"); 
               mediatr = MediatR.Contains("+"); 
               kafka = Kafka.Contains("+"); 
               grpc = Grpc.Contains("+"); 
               nexus = Nexus.Contains("+") |}
        )

    printf "Output path: "
    let output_path = Console.ReadLine()

    let installTemplateProcess = executeProcess output_path Devon4netCliConsts.devon4netTemplate_instalation
    installTemplateProcess.WaitForExit()
    let launchTemplateProcess = executeProcess output_path Devon4netCliConsts.devon4netTemplate_launch
    launchTemplateProcess.WaitForExit()

    let destinationPath = Path.Combine(output_path, Devon4netCliConsts.devon4netAppPath)

    devon4net_webapi_components_generation componentInfo Devon4netCliConsts.templates_path destinationPath

    printfn "Completed"

let rec webapi_components_menu () =
    System.Console.Clear();
    printfn "WebAPI Application"
    printMenu()
    match getInput() with
    | true, 1 -> 
        CircuitBreaker <- choice(CircuitBreaker)
        System.Console.Clear();
        webapi_components_menu()
    | true, 2 -> 
        Jwt <- choice(Jwt)
        System.Console.Clear();
        webapi_components_menu()
    | true, 3 -> 
        RabbitMq <- choice(RabbitMq)
        System.Console.Clear();
        webapi_components_menu()
    | true, 4 -> 
        MediatR <- choice(MediatR)
        System.Console.Clear();
        webapi_components_menu()
    | true, 5 -> 
        Kafka <- choice(Kafka)
        System.Console.Clear();
        webapi_components_menu()
    | true, 6 -> 
        Grpc <- choice(Grpc)
        System.Console.Clear();
        webapi_components_menu()
    | true, 7 -> 
        Nexus <- choice(Nexus)
        System.Console.Clear();
        webapi_components_menu()
    | true, 8 -> 
        createWebApiProject()
        exit 0
    | _ -> webapi_components_menu()

webapi_components_menu ()