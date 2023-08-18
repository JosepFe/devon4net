namespace Devon4NetCli.Apps

open System

module AzureAppServiceApp =

    let mutable BlobStorage = "1. BlobStorage"
    let mutable KeyVault = "2. KeyVault"
    let mutable Done = "3. Done"

    let printMenu () =
        printfn "Menu: "
        printfn $"%s{BlobStorage}"
        printfn $"%s{KeyVault}"
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

    let rec azure_appService_menu () =
        Console.Clear();
        printfn "WebAPI Application"
        printMenu()
        match getInput() with
        | true, 1 ->
            BlobStorage <- choice(BlobStorage)
            Console.Clear();
            azure_appService_menu()
        | true, 2 ->
            KeyVault <- choice(KeyVault)
            Console.Clear();
            azure_appService_menu()
        | true, 3 -> ()
        | _ -> azure_appService_menu()

    azure_appService_menu ()