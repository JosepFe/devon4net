module Devon4NetCli.Apps.CleanArchitectureApp

open Devon4NetCli.Utils.Utils
open Devon4NetCli.Consts.Consts
open System.Text.Json
open JejuneCmd.Gen
open System.IO
open System

let mutable CircuitBreaker = "\t1. CircuitBreaker"
let mutable Jwt = "\t2. Jwt"
let mutable AntiForgery = "\t3. AntiForgery"
let mutable RabbitMq = "\t4. RabbitMq"
let mutable Kafka = "\t6. Kafka"
let mutable Grpc = "\t7. Grpc"
let mutable Nexus = "\t8. Nexus"
let mutable Done = "\t9. Done"

let printMenu () =
    printLogo()
    printfn "Choose the components for your Clean Architecure Application: "
    printfn ""
    printfn $"%s{CircuitBreaker}"
    printfn $"%s{Jwt}"
    printfn $"%s{AntiForgery}"
    printfn $"%s{RabbitMq}"
    printfn $"%s{Kafka}"
    printfn $"%s{Grpc}"
    printfn $"%s{Nexus}"
    printfn ""
    printfn $"%s{Done}"
    printfn ""
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
    
    printf "Completed, press any key to close"
    Console.ReadLine()