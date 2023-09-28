namespace Devon4NetCli.Apps

open Devon4NetCli.Utils.Utils
open Devon4NetCli.Consts.Consts
open System.Text.Json
open JejuneCmd.Gen
open System.IO
open System
open JejuneCmd.Gen.WebApiMonolithicAppGen.WebApiMonolithicAppGen

module WebApiMonolithicApp =

    let mutable CircuitBreaker = "\t1. CircuitBreaker"
    let mutable Jwt = "\t2. Jwt"
    let mutable AntiForgery = "\t3. AntiForgery"
    let mutable RabbitMq = "\t4. RabbitMq"
    let mutable MediatR = "\t5. MediatR"
    let mutable Kafka = "\t6. Kafka"
    let mutable Grpc = "\t7. Grpc"
    let mutable Nexus = "\t8. Nexus"
    let mutable Done = "\t9. Done"

    let private printMenu () =
        printLogo()
        printfn "Choose your components for your WebApi Application: "
        printfn ""
        printfn $"%s{CircuitBreaker}"
        printfn $"%s{Jwt}"
        printfn $"%s{AntiForgery}"
        printfn $"%s{RabbitMq}"
        printfn $"%s{MediatR}"
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

        //let installTemplateProcess = executeProcess output_path devon4net_webapi_template_installation
        //installTemplateProcess.WaitForExit()
        let launchTemplateProcess = executeProcess output_path devon4netWebApiMonolithicAppTemplate_launch
        launchTemplateProcess.WaitForExit()

        let destinationPath = Path.Combine(output_path, devon4netAppPath)

        devon4net_webapi_components_generation componentInfo webapi_templates_path destinationPath

        printf "Database Connection String: "
        let connectionString = Console.ReadLine()

        printf "ContextName: "
        let contextName = Console.ReadLine()

        if not (String.IsNullOrWhiteSpace(connectionString)) then

            generate_webapi_scaffolding_crud connectionString contextName output_path (Path.Combine(output_path, devon4netAppPath, "Devon4Net.Application.WebAPI.csproj"))

        printf "Completed, press any key to close"
        Console.ReadLine()

    let rec webapi_monolithic_menu () =
        Console.Clear();
        printMenu()
        match getInput() with
        | true, 1 ->
            CircuitBreaker <- choice(CircuitBreaker)
            Console.Clear();
            webapi_monolithic_menu()
        | true, 2 ->
            Jwt <- choice(Jwt)
            Console.Clear();
            webapi_monolithic_menu()
        | true, 3 ->
            AntiForgery <- choice(AntiForgery)
            Console.Clear();
            webapi_monolithic_menu()
        | true, 4 ->
            RabbitMq <- choice(RabbitMq)
            Console.Clear();
            webapi_monolithic_menu()
        | true, 5 ->
            MediatR <- choice(MediatR)
            Console.Clear();
            webapi_monolithic_menu()
        | true, 6 ->
            Kafka <- choice(Kafka)
            Console.Clear();
            webapi_monolithic_menu()
        | true, 7 ->
            Grpc <- choice(Grpc)
            Console.Clear();
            webapi_monolithic_menu()
        | true, 8 ->
            Nexus <- choice(Nexus)
            Console.Clear();
            webapi_monolithic_menu()
        | true, 9 ->
            createWebApiProject() |> ignore
            exit 0
        | _ -> webapi_monolithic_menu()

    webapi_monolithic_menu ()