// Learn more about F# at http://docs.microsoft.com/dotnet/fsharp

open System
// Define a function to construct a message to print

open JejuneCmd
open System.IO
open Utils
open JejuneCmd.Gen.WebApiMonolithicAppGen.WebApiMonolithicAppGen

[<EntryPoint>]
let main argv =
    let baseDirectory = Directory.GetParent(__SOURCE_DIRECTORY__)
    let entities_path = Path.Combine(baseDirectory.ToString(), "Entities")
    let entities = load_entities(Path.Combine(entities_path, "entities.txt"))

    let templates_path = Path.Combine(baseDirectory.ToString(),"Templates", "Crud")

    printJejuneLogo()

    Console.Write("Provide the path to where you want to generate the code: ")
    let source_path = Console.ReadLine()
    copyAndExpandFiles entities entities_path  templates_path source_path
    0