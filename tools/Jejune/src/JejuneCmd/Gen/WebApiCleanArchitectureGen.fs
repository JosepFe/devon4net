module JejuneCmd.Gen.WebApiCleanArchitectureGen

open System.Text.Json
open JejuneCmd
open JsonData
open JejuneCmd.Utils

let generate_crud_clean_architecture (entitiesList: JejuneEntity list) (fromPath: string) (toPath: string) =

    let repo = load_template fromPath "{{entity}}Repository.cs.hbs"
    let repoInterface = load_template fromPath "I{{entity}}Repository.cs.hbs"
    let dto = load_template fromPath "{{entity}}Management/Dto/{{entity}}Dto.cs.hbs"
    let converter = load_template fromPath "{{entity}}Management/Converters/{{entity}}Converter.cs.hbs"

    for entity in entitiesList do

        let json = JsonSerializer.Serialize entity

        let entityData = match deserialize json with
                            | Ok o -> jsonpropsToDict o
                            | Error e -> failwith e.Message

        expand_write_file entityData repo toPath "Data/Repositories/{{entity}}Repository.cs"
        expand_write_file entityData repoInterface toPath "Domain/RepositoryInterfaces/I{{entity}}Repository.cs"
        expand_write_file entityData dto toPath "Business/{{entity}}Management/Dto/{{entity}}Dto.cs"
        expand_write_file entityData converter toPath "Business/{{entity}}Management/Converters/{{entity}}Converter.cs"