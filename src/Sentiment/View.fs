module Sentiment.View

open Fable.Core
open Fable.Core.JsInterop
open Fable.Helpers.React
open Fable.Helpers.React.Props
open Types
open System.Collections.Generic

let prepareData(score: Sentiment array ) =
    score.Length

let root (model: Model) dispatch =
  div
    [ ]
    [ p
        [ ClassName "control" ]
        [ input
            [ ClassName "input"
              Type "text"
              Placeholder "Type"
              DefaultValue model.text
              AutoFocus true
              OnChange (fun ev -> !!ev.target?value |> ChangeText |> dispatch ) ] ]
      br [ ]
      button [ClassName "test"; OnClick (fun _ -> Classify |> dispatch)] [  ]
      span
        [ ]
        [ str (sprintf "Hello %A" (model.classificationResult.score |> prepareData )) ] ]
