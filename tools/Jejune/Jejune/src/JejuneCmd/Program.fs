// Learn more about F# at http://docs.microsoft.com/dotnet/fsharp

open System
open HandlebarsDotNet
// Define a function to construct a message to print

open JejuneCmd
open Gen
open System.IO
    

[<EntryPoint>]
let main argv =
    
    let currentDir = Directory.GetCurrentDirectory();
    let projectDirectory = Directory.GetParent(currentDir).Parent.Parent.Parent.Parent.Parent.FullName
    let templates_path = Path.Combine(projectDirectory, "Templates", "template")
    let entities_path = Path.Combine(projectDirectory, "Templates", "entities")

    let entities = load_entities(Path.Combine(entities_path, "entities.txt"))
    Console.Write("Output path: ")
    let source_path = Console.ReadLine()

    copyAndExpandFiles entities entities_path  templates_path source_path
    0
    
    