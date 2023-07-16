module Devon4netCli.Apps.AwsLambdaComponents
open System

let mutable DynamoDb = "1. DynamoDb"
let mutable S3 = "2. S3"
let mutable Sqs = "3. Sqs"
let mutable SecretManager = "4. SecretManager"
let mutable Parameters = "5. Parameters"
let mutable Sns = "6. Sns"
let mutable Done = "7. Done"

let printMenu () =
    printfn "Menu: "
    printfn $"%s{DynamoDb}"
    printfn $"%s{S3}"
    printfn $"%s{Sqs}"
    printfn $"%s{SecretManager}"
    printfn $"%s{Parameters}"
    printfn $"%s{Sns}"
    printfn $"%s{Done}"
    printf "Enter your choice: "

let getInput () = Int32.TryParse (Console.ReadLine())

let doThis () = printfn "Do this..."
let doThat () = printfn "Do that..."

let choice (devonComponent:string) = 
    if devonComponent.Contains("+") then 
        devonComponent.Remove(devonComponent.Length - 2)
    else 
        devonComponent + " +"

let rec aws_lambda_components_menu () =
    Console.Clear();
    printfn "WebAPI Application"
    printMenu()
    match getInput() with
    | true, 1 -> 
        DynamoDb <- choice(DynamoDb)
        Console.Clear();
        aws_lambda_components_menu()
    | true, 2 -> 
        S3 <- choice(S3)
        Console.Clear();
        aws_lambda_components_menu()
    | true, 3 -> 
        Sqs <- choice(Sqs)
        Console.Clear();
        aws_lambda_components_menu()
    | true, 4 -> 
        SecretManager <- choice(SecretManager)
        Console.Clear();
        aws_lambda_components_menu()
    | true, 5 -> 
        Parameters <- choice(Parameters)
        Console.Clear();
        aws_lambda_components_menu()
    | true, 6 -> 
        Sns <- choice(Sns)
        Console.Clear();
        aws_lambda_components_menu()
    | true, 7 -> ()
    | _ -> aws_lambda_components_menu()

aws_lambda_components_menu ()