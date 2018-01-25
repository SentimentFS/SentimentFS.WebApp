module Sentiment.State
open Types
open Elmish
open Fable.PowerPack
open Fable.PowerPack.Fetch.Fetch_types
open Fable.Core.JsInterop
open Fable.PowerPack
open Fable.PowerPack

let serverUrl = "http://localhost:5000"

let classify (msg: Classify) =
    promise {
        let url = sprintf "%s/%s" serverUrl "api/sentiment/classification"
        let body = msg |> toJson
        let props =
            [
                RequestProperties.Method HttpMethod.POST
                Fetch.requestHeaders [
                    HttpRequestHeaders.ContentType "application/json"
                ]
                RequestProperties.Body !^body
            ]
        return! Fetch.fetchAs<ClassificationResult> url props
    }

let train (msg: Train) =
    promise {
        let url = sprintf "%s/%s" serverUrl "api/sentiment/trainer"
        let body = msg |> toJson
        let props =
            [
                RequestProperties.Method HttpMethod.PUT
                Fetch.requestHeaders [
                    HttpRequestHeaders.ContentType "application/json"
                ]
                RequestProperties.Body !^body
            ]
        let! response = Fetch.fetch url props
        return response.Ok
    }

let classifyCmd  (msg: Classify) =
    Cmd.ofPromise classify msg Classified Error

let trainCmd(msg: Train) =
    Cmd.ofPromise train msg Trained Error

let init () : Model * Cmd<Msg> =
  { text = ""; classificationResult = { text = ""; score = [||]; }; trainData = None }, []

let update msg model : Model * Cmd<Msg> =
  match msg with
  | ChangeText text ->
    { model with text = text}, []
  | Classify ->
    model, classifyCmd { text = model.text }
  | Classified msg ->
    printfn "%A" msg
    { model with classificationResult = msg }, []
  | Train ->
    match model.trainData with
    | Some data ->
        { model with trainData = None }, trainCmd data
    | None ->
        printfn "Empty train model"
        model, []
  | Error ex ->
    printfn "%A" ex
    model, []

