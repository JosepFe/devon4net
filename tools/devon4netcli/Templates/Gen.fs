module JejuneCmd.Gen
open System
open System.Collections
open System.IO
open HandlebarsDotNet
open JsonData

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
        
let copyAndExpandFiles (entitielist: string list) (entities_path: string) (frompath: string) (topath: string) =
            
    let repo = load_template frompath "Data/Repositories/{{entity}}Repository.cs.hbs" 
    let irepo = load_template frompath "Domain/RepositoryInterfaces/I{{entity}}Repository.cs.hbs"
    let dto = load_template frompath "Business/{{entity}}Management/Dto/{{entity}}Dto.cs.hbs"
    let controller = load_template frompath "Business/{{entity}}Management/Controllers/{{entity}}Controller.cs.hbs" 
    let converter = load_template frompath "Business/{{entity}}Management/Converters/{{entity}}Converter.cs.hbs"
     
    let service = load_template frompath "Business/{{entity}}Management/Services/{{entity}}Service.cs.hbs"
    let iservice = load_template frompath "Business/{{entity}}Management/Services/I{{entity}}Service.cs.hbs" 
        
    for entity in entitielist do
         
        let entityfile = Path.Combine(entities_path, entity + ".json")       
        let entitydata = match deserialize entityfile with       
                            | Ok o -> jsonpropsToDict o
                            | Error e -> failwith e.Message
                              
        expand_write_file entitydata repo topath "Data/Repositories/{{entity}}Repository.cs"
        expand_write_file entitydata irepo topath "Domain/RepositoryInterfaces/I{{entity}}Repository.cs"
        expand_write_file entitydata dto topath "Business/{{entity}}Management/Dto/{{entity}}Dto.cs"
        expand_write_file entitydata controller topath "Business/{{entity}}Management/Controllers/{{entity}}Controller.cs"
        expand_write_file entitydata converter topath "Business/{{entity}}Management/Converters/{{entity}}Converter.cs"

        expand_write_file entitydata service topath "Business/{{entity}}Management/Services/{{entity}}Service.cs"
        expand_write_file entitydata iservice topath "Business/{{entity}}Management/Services/I{{entity}}Service.cs"

let expand_write_file_component (entity: Generic.IDictionary<string,obj>) (template: HandlebarsTemplate<obj,obj>) base_path (path: string) =
    let _path = Path.Combine(base_path, path)

    let dir = Path.GetDirectoryName(_path)
    if not (File.Exists(dir)) then        
        Directory.CreateDirectory(dir) |> ignore

    File.WriteAllText(_path, template.Invoke(entity)) 

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

let devon4net_webapi_components_generation (devon4net_webapi_components: string list) (components_path: string) (frompath: string) (topath: string) =

    let program = load_template frompath "Program.cs.hbs" 
    let appsettings = load_template frompath "Appsettings.Development.json.hbs" 
    let csproj = load_template frompath "Devon4Net.Application.WebAPI.csproj.hbs"
    let devonConfiguration = load_template frompath "DevonConfiguration.cs.hbs"

    for devon4net_webapi_component in devon4net_webapi_components do
         
        Console.WriteLine("Component is " + devon4net_webapi_component)
        let entityfile = Path.Combine(components_path, devon4net_webapi_component + ".json")       
        let entitydata = match deserialize entityfile with       
                            | Ok o -> jsonpropsToDict o
                            | Error e -> failwith e.Message

        let rabbitmq = Convert.ToBoolean(entitydata.Item("rabbitmq"))
        let mediatr = Convert.ToBoolean(entitydata.Item("mediatr"))

        let mediatrPath = Path.Combine(frompath, "Business","MediatRManagement")
        let mediatrOutputPath = Path.Combine(topath, "Business","MediatRManagement")
        
        let rabbitMqPath = Path.Combine(frompath, "Business","RabbitMqManagement")
        let rabbitMqPathOutputPath = Path.Combine(topath, "Business","RabbitMqManagement")

        expand_write_file_component entitydata program topath "Program.cs"
        expand_write_file_component entitydata appsettings topath "Appsettings.Development.json"
        expand_write_file_component entitydata csproj topath "Devon4Net.Application.WebAPI.csproj"
        expand_write_file_component entitydata devonConfiguration topath "Configuration/DevonConfiguration.cs"

        if mediatr then
            directoryCopy mediatrPath mediatrOutputPath true
        if rabbitmq then  
            directoryCopy rabbitMqPath rabbitMqPathOutputPath true
