module Sentiment.State
open Types
open Elmish
open System
open Fable.PowerPack
open Fable.PowerPack.Fetch.Fetch_types
open System.Net.Http
open System.Net.Http.Headers
open Fable.Core.JsInterop
open System.Collections.Generic

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

let classifyCmd  (msg: Classify) =
    Cmd.ofPromise classify msg Classified Error

let init () : Model * Cmd<Msg> =
  { text = ""; classificationResult = { text = ""; score = [||] } }, []

let update msg model : Model * Cmd<Msg> =
  match msg with
  | ChangeText text ->
    { model with text = text}, []
  | Classify ->
    model, classifyCmd { text = model.text }
  | Classified msg ->
    printfn "%A" msg
    { model with classificationResult = msg }, []
  | Error ex ->
    printfn "%A" ex
    model, []

