module Devon4netCli.WebapiComponents
open System.Text.Json
open JejuneCmd.Gen
open System.IO
open Utils
open System

let mutable CircuitBreaker = "1. CircuitBreaker"
let mutable Jwt = "2. Jwt"
let mutable AntiForgery = "3. AntiForgery"
let mutable RabbitMq = "4. RabbitMq"
let mutable MediatR = "5. MediatR"
let mutable Kafka = "6. Kafka"
let mutable Grpc = "7. Grpc"
let mutable Nexus = "8. Nexus"
let mutable Done = "9. Done"

let printMenu () =
    printfn "Menu: "
    printfn "%s" CircuitBreaker
    printfn "%s" Jwt
    printfn "%s" AntiForgery
    printfn "%s" RabbitMq
    printfn "%s" MediatR
    printfn "%s" Kafka
    printfn "%s" Grpc
    printfn "%s" Nexus
    printfn "%s" Done
    printf "Enter your choice: "

let createWebApiProject() =
    let componentInfo =
        JsonSerializer.Serialize(
            {| circuitbreaker = CircuitBreaker.Contains("+"); 
               jwt = Jwt.Contains("+"); 
               antiForgery = AntiForgery.Contains("+"); 
               rabbitmq =RabbitMq.Contains("+"); 
               mediatr = MediatR.Contains("+"); 
               kafka = Kafka.Contains("+"); 
               grpc = Grpc.Contains("+"); 
               nexus = Nexus.Contains("+") |}
        )

    printf "Output path: "
    let output_path = Console.ReadLine()



    //let installTemplateProcess = executeProcess output_path Devon4netCliConsts.devon4netTemplate_instalation
    //installTemplateProcess.WaitForExit()
    let launchTemplateProcess = executeProcess output_path Devon4netCliConsts.devon4netWebApiTemplate_launch
    launchTemplateProcess.WaitForExit()

    let destinationPath = Path.Combine(output_path, Devon4netCliConsts.devon4netAppPath)

    devon4net_webapi_components_generation componentInfo Devon4netCliConsts.webapi_templates_path destinationPath

    printf "Completed, press any key to close"
    Console.ReadLine()

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
        AntiForgery <- choice(AntiForgery)
        System.Console.Clear();
        webapi_components_menu()
    | true, 4 -> 
        RabbitMq <- choice(RabbitMq)
        System.Console.Clear();
        webapi_components_menu()
    | true, 5 -> 
        MediatR <- choice(MediatR)
        System.Console.Clear();
        webapi_components_menu()
    | true, 6 -> 
        Kafka <- choice(Kafka)
        System.Console.Clear();
        webapi_components_menu()
    | true, 7 -> 
        Grpc <- choice(Grpc)
        System.Console.Clear();
        webapi_components_menu()
    | true, 8 -> 
        Nexus <- choice(Nexus)
        System.Console.Clear();
        webapi_components_menu()
    | true, 9 -> 
        createWebApiProject()
        exit 0
    | _ -> webapi_components_menu()

webapi_components_menu ()