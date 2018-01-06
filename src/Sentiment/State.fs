module Sentiment.State
open Types
open Elmish
open System
open Fable.PowerPack
open Fable.PowerPack.Fetch.Fetch_types
open System.Net.Http
open System.Net.Http.Headers
open Fable.Core.JsInterop

let serverUrl = Environment.GetEnvironmentVariable("AnalysisServerUrl")

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
  { text = ""; score = ([] |> Map.ofList) }, []

let update msg model : Model * Cmd<Msg> =
  match msg with
  | Classify msg ->
      model, classifyCmd msg
  | Classified msg ->
      msg, []

