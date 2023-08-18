namespace JejuneCmd.Gen.WebApiMonolithicAppGen

open JejuneCmd.Utils
open System.IO
open JejuneCmd.JsonData
open System.Text.Json
open JejuneCmd
open FSharp.Data
open System

module WebApiMonolithicAppGen =

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

    let generate_crud_files (entitielist: JejuneEntity list) (frompath: string) (topath: string) =

        let repo = load_template frompath "Data/Repositories/{{entity}}Repository.cs.hbs"
        let irepo = load_template frompath "Domain/RepositoryInterfaces/I{{entity}}Repository.cs.hbs"
        let dto = load_template frompath "Business/{{entity}}Management/Dto/{{entity}}Dto.cs.hbs"
        let controller = load_template frompath "Business/{{entity}}Management/Controllers/{{entity}}Controller.cs.hbs"
        let converter = load_template frompath "Business/{{entity}}Management/Converters/{{entity}}Converter.cs.hbs"

        let service = load_template frompath "Business/{{entity}}Management/Services/{{entity}}Service.cs.hbs"
        let iservice = load_template frompath "Business/{{entity}}Management/Services/I{{entity}}Service.cs.hbs"

        for entity in entitielist do

            let json = JsonSerializer.Serialize entity

            let entitydata = match deserialize json with
                                | Ok o -> JsonData.jsonpropsToDict o
                                | Error e -> failwith e.Message

            expand_write_file entitydata repo topath "Data/Repositories/{{entity}}Repository.cs"
            expand_write_file entitydata irepo topath "Domain/RepositoryInterfaces/I{{entity}}Repository.cs"
            expand_write_file entitydata dto topath "Business/{{entity}}Management/Dto/{{entity}}Dto.cs"
            expand_write_file entitydata controller topath "Business/{{entity}}Management/Controllers/{{entity}}Controller.cs"
            expand_write_file entitydata converter topath "Business/{{entity}}Management/Converters/{{entity}}Converter.cs"

            expand_write_file entitydata service topath "Business/{{entity}}Management/Services/{{entity}}Service.cs"
            expand_write_file entitydata iservice topath "Business/{{entity}}Management/Services/I{{entity}}Service.cs"

    let devon4net_webapi_components_generation (componentInfo: string) (frompath: string) (topath: string) =

        let program = load_template frompath "Program.cs.hbs"
        let appsettings = load_template frompath "Appsettings.Development.json.hbs"
        let csproj = load_template frompath "Devon4Net.Application.WebAPI.csproj.hbs"
        let devonConfiguration = load_template frompath "DevonConfiguration.cs.hbs"

        let webapiComponentData = jsonpropsToDict (JsonValue.Parse(componentInfo))

        let jwt = Convert.ToBoolean(webapiComponentData.Item("jwt"))
        let antiForgery = Convert.ToBoolean(webapiComponentData.Item("antiForgery"))
        let rabbitmq = Convert.ToBoolean(webapiComponentData.Item("rabbitmq"))
        let mediatr = Convert.ToBoolean(webapiComponentData.Item("mediatr"))

        let authPath = Path.Combine(topath, "Business","AuthManagement")
        let antiForgeryPath = Path.Combine(topath, "Business","AntiForgeryTokenManagement")
        let mediatrPath = Path.Combine(topath, "Business","MediatRManagement")
        let rabbitMqPath = Path.Combine(topath, "Business","RabbitMqManagement")

        component_apply_template webapiComponentData program topath "Program.cs"
        component_apply_template webapiComponentData appsettings topath "Appsettings.Development.json"
        component_apply_template webapiComponentData csproj topath "Devon4Net.Application.WebAPI.csproj"
        component_apply_template webapiComponentData devonConfiguration topath "Configuration/DevonConfiguration.cs"

        if not jwt then
            deleteFiles authPath "*.*" true
        if not antiForgery then
            deleteFiles antiForgeryPath "*.*" true
        if not mediatr then
            deleteFiles mediatrPath "*.*" true
        if not rabbitmq then
            deleteFiles rabbitMqPath "*.*" true