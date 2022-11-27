module JejuneCmd.Gen
open System
open System.Collections
open System.IO
open HandlebarsDotNet
open FSharp.Data
open JsonData

let load_entities path = 

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

        
(*
Data/Repositories/{{entity}}Repository.cs
Domain/RepositoryInterfaces/I{{entity}}Repository.cs

Business/{{entity}}Management/Controllers/{{entity}}Controller.cs
Business/{{entity}}Management/Converters/{{entity}}Converter.cs
Business/{{entity}}Management/Dto/{{entity}}Dto.cs
Business/{{entity}}Management/Services/{{entity}}Service.cs
Business/{{entity}}Management/Services/I{{entity}}Service.cs
        
*)