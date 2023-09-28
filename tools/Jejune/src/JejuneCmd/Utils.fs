module JejuneCmd.Utils

open System.IO
open System
open System.Collections
open HandlebarsDotNet

let rec directoryCopy srcPath dstPath copySubDirs =

    if not <| System.IO.Directory.Exists(srcPath) then
        let msg = System.String.Format("Source directory does not exist or could not be found: {0}", srcPath)
        raise (System.IO.DirectoryNotFoundException(msg))

    if not <| System.IO.Directory.Exists(dstPath) then
        System.IO.Directory.CreateDirectory(dstPath) |> ignore

    let srcDir = new System.IO.DirectoryInfo(srcPath)

    for file in srcDir.GetFiles() do
        let temppath = System.IO.Path.Combine(dstPath, file.Name)
        file.CopyTo(temppath, true) |> ignore

    if copySubDirs then
        for subdir in srcDir.GetDirectories() do
            let dstSubDir = System.IO.Path.Combine(dstPath, subdir.Name)
            directoryCopy subdir.FullName dstSubDir copySubDirs

let rec deleteFilesAndDirs srcPath pattern includeSubDirs =
    if not <| System.IO.Directory.Exists(srcPath) then
        let msg = System.String.Format("Source directory does not exist or could not be found: {0}", srcPath)
        raise (System.IO.DirectoryNotFoundException(msg))

    // Delete files in the current directory
    for file in System.IO.Directory.EnumerateFiles(srcPath, pattern) do
        let tempPath = System.IO.Path.Combine(srcPath, file)
        System.IO.File.Delete(tempPath)

    if includeSubDirs then
        // Delete files and directories in subdirectories
        let srcDir = new System.IO.DirectoryInfo(srcPath)
        for subdir in srcDir.GetDirectories() do
            deleteFilesAndDirs subdir.FullName pattern includeSubDirs

    // After deleting files, delete the current directory itself
    System.IO.Directory.Delete(srcPath, true)

let printJejuneLogo() =
    printfn "%s" """
    __        _
   \ \  ___ (_)_   _ _ __   ___
    \ \/ _ \| | | | | '_ \ / _ \
 /\_/ /  __/| | |_| | | | |  __/
 \___/ \___|/ |\__,_|_| |_|\___|
          |__/

"""

let findLineNumber (lines:string array) (searchString:string) =
    let lineNumber = Seq.tryFindIndex (fun (line: string) -> line.Contains(searchString)) lines
    match lineNumber with
    | Some num -> num + 1
    | None -> -1

let instertLine (srcPath: string) (newLine: string) (lineToFind: string) (lineAddition: int) =

    let lines = File.ReadAllLines srcPath
    let lineNumber = findLineNumber lines lineToFind + lineAddition
    let exists = findLineNumber lines newLine
    if exists < 0 then
        let updatedLines = Array.insertAt lineNumber newLine lines
        File.WriteAllLines (srcPath, updatedLines)

let getEntities (srcPath: string) =

    let mutable entities: string list = []
    let lines = File.ReadAllLines srcPath
    for line in lines do
        if line.Contains("DbSet<") then
            let entity = line.Split("<").[1].Split(">").[0]
            entities <- [$"{entity}"] |> List.append entities
    entities

type JejuneVars = { ``type`` : string ; name : string }
type JejuneEntity = { context : string ; entity : string ; vars: JejuneVars list }

let generateJejuneEntities (srcPath: string) (contextName: string) =

    let contextPath = Path.Combine(srcPath, "Domain", "Database", $"{contextName}.cs")
    let entities = getEntities contextPath

    let mutable jejuneEntites: JejuneEntity list = []

    for entity in entities do
        let entityPath = Path.Combine(srcPath, "Domain", "Entities", $"{entity}.cs" )
        let mutable jejuneVars: JejuneVars list = []

        let lines = File.ReadAllLines entityPath

        for line in lines do
            if line.Contains("{ get; set; }") && not (line.Contains("virtual")) then
                let entity = line.Split("{ get; set; }").[0].Split("public").[1].Split(" ")
                let jejuneVar = { ``type`` = entity.[1] ; name = entity.[2] }
                jejuneVars <- [jejuneVar] |> List.append jejuneVars

        let jejuneEntity = { context = contextName ; entity = entity ; vars = jejuneVars }
        jejuneEntites <- [jejuneEntity] |> List.append jejuneEntites
    jejuneEntites

//Gen Utils

let load_entities path =

        use f = File.OpenText(path)
        let txt = (f.ReadToEnd ()).Trim()
        let lst = List.ofArray(txt.Split( Environment.NewLine, StringSplitOptions.None))
        List.map (fun (x: string) -> x.Trim()) lst

let load_components path =

        use f = File.OpenText(path)
        let txt = (f.ReadToEnd ()).Trim()
        let lst = List.ofArray(txt.Split( Environment.NewLine, StringSplitOptions.None))
        List.map (fun (x: string) -> x.Trim()) lst

let load_template base_path path =
    use f = File.OpenText(Path.Combine(base_path, path))
    Handlebars.Compile(f.ReadToEnd())

let expand_write_file (entity: Generic.IDictionary<string,obj>) (template: HandlebarsTemplate<obj,obj>) base_path (path: string) =
    let _path = Path.Combine(base_path, path.Replace("{{entity}}", entity.["entity"].ToString()))

    let dir = Path.GetDirectoryName(_path)
    if not (File.Exists(dir)) then
        Directory.CreateDirectory(dir) |> ignore

    File.WriteAllText(_path, template.Invoke(entity))

let component_apply_template (componentToApply: Generic.IDictionary<string,obj>) (template: HandlebarsTemplate<obj,obj>) base_path (path: string) =
    let _path = Path.Combine(base_path, path)

    let dir = Path.GetDirectoryName(_path)
    if not (File.Exists(dir)) then
        Directory.CreateDirectory(dir) |> ignore

    File.WriteAllText(_path, template.Invoke(componentToApply))