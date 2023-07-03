module JejuneCmd.JsonData
open System.IO
open FSharp.Data
open System.Collections.Generic

let rec jsonArray (arr : JsonValue array):  obj array  =
        let s : (obj) seq = seq {
            for e in arr do match e with
                                        | JsonValue.Null -> yield null
                                        | JsonValue.Boolean b -> yield b
                                        | JsonValue.String s -> yield s
                                        | JsonValue.Float f -> yield f
                                        | JsonValue.Number n -> yield n
                                        | JsonValue.Array a -> yield jsonArray a :> obj
                                        | JsonValue.Record r -> yield jsonRecord r :> obj
        }
        s |> Seq.toArray

    and jsonRecord (record : (string * JsonValue) array): IDictionary<string, obj>  =

        let s : (string * obj) seq = seq {
                for n, v in record do match v with
                                        | JsonValue.Null -> yield (n, null)
                                        | JsonValue.Boolean b -> yield (n, b :> obj)
                                        | JsonValue.String s -> yield (n, s :> obj)
                                        | JsonValue.Float f -> yield (n, f :> obj)
                                        | JsonValue.Number d -> yield (n, d :> obj)
                                        | JsonValue.Array a -> yield (n, jsonArray a :> obj)
                                        | JsonValue.Record r ->yield  (n, jsonRecord r :> obj)
        }
        s |> dict

let jsonpropsToDict (jsonvalue: JsonValue) : IDictionary<string, obj>=

    match jsonvalue with
        | JsonValue.Record r -> jsonRecord (jsonvalue.Properties())
        | _ -> failwith "Data can only be an JsonObject"

let deserialize<'a> json =
    try
        JsonValue.Parse(json) |> Result.Ok
    with
      // catch all exceptions and convert to Result
      | ex -> Result.Error ex  