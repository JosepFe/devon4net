namespace Devon4NetCli.Apps

open Devon4NetCli.Utils.Utils
open Devon4NetCli.Consts.Consts
open System.Text.Json
open System
open System.IO

module CleanArchitectureApp =

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
        printfn "Choose the components for your Clean Architecture Application: "
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
                   kafka = Kafka.Contains("+");
                   grpc = Grpc.Contains("+");
                   nexus = Nexus.Contains("+") |}
            )

        printf "Output path: "
        let output_path = Console.ReadLine()

        printf "Database Connection String: "
        let connectionString = Console.ReadLine()

        printf "ContextName: "
        let contextName = Console.ReadLine()

        generate_clean_architecture_scaffolding_crud connectionString contextName output_path (Path.Combine(output_path, devon4netAppPath, "Devon4Net.Application.WebAPI.csproj"))

        printf "Completed, press any key to close"
        Console.ReadLine()

    let rec webapi_clean_architecture_menu () =
        Console.Clear();
        printMenu()
        match getInput() with
        | true, 1 ->
            CircuitBreaker <- choice(CircuitBreaker)
            Console.Clear();
            webapi_clean_architecture_menu()
        | true, 2 ->
            Jwt <- choice(Jwt)
            Console.Clear();
            webapi_clean_architecture_menu()
        | true, 3 ->
            AntiForgery <- choice(AntiForgery)
            Console.Clear();
            webapi_clean_architecture_menu()
        | true, 4 ->
            RabbitMq <- choice(RabbitMq)
            Console.Clear();
            webapi_clean_architecture_menu()
        | true, 5 ->
            Kafka <- choice(Kafka)
            Console.Clear();
            webapi_clean_architecture_menu()
        | true, 6 ->
            Grpc <- choice(Grpc)
            Console.Clear();
            webapi_clean_architecture_menu()
        | true, 7 ->
            Nexus <- choice(Nexus)
            Console.Clear();
            webapi_clean_architecture_menu()
        | true, 9 ->
            createWebApiProject() |> ignore
            exit 0
        | _ -> webapi_clean_architecture_menu()

    webapi_clean_architecture_menu ()