// Learn more about F# at http://docs.microsoft.com/dotnet/fsharp

open System
// Define a function to construct a message to print

open JejuneCmd
open Gen
open System.IO
    

[<EntryPoint>]
let main argv =
    let baseDirectory = Directory.GetParent(__SOURCE_DIRECTORY__)
    let entities_path = Path.Combine(baseDirectory.ToString(), "Entities")
    let entities = load_entities(Path.Combine(entities_path, "entities.txt"))

    let templates_path = Path.Combine(baseDirectory.ToString(),"Templates")

    let components_path = Path.Combine(baseDirectory.ToString(), "Components")
    let components = load_components(Path.Combine(components_path, "components.txt"))

    Console.Write("Output path: ")
    let source_path = Console.ReadLine()
    //copyAndExpandFiles entities entities_path  templates_path source_path
    devon4net_webapi_components_generation components components_path templates_path source_path
    0
    
    