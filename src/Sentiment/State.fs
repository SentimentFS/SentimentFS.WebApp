module Sentiment.State
open Types
open Elmish

let init () : Model * Cmd<Msg> =
  { text = ""; score = ([] |> Map.ofList) }, []

let update msg model : Model * Cmd<Msg> =
  match msg with
  | Classify msg ->
      model, []

