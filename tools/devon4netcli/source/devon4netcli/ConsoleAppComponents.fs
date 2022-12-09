module Devon4netCli.ConsoleAppComponents
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

let getInput () = Int32.TryParse (Console.ReadLine())

let doThis () = printfn "Do this..."
let doThat () = printfn "Do that..."

let choice (devonComponent:string) = 
    if devonComponent.Contains("+") then 
        devonComponent.Remove(devonComponent.Length - 2)
    else 
        devonComponent + " +"

let rec console_app_components_menu () =
    System.Console.Clear();
    printfn "WebAPI Application"
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
    | true, 8 -> ()
    | _ -> console_app_components_menu()

console_app_components_menu ()