module Sentiment.View

open Fable.Core
open Fable.Core.JsInterop
open Fable.Helpers.React
open Fable.Helpers.React.Props
open Types

let root model dispatch =
  div
    [ ]
    [ p
        [ ClassName "control" ]
        [ input
            [ ClassName "input"
              Type "text"
              Placeholder "Type"
              DefaultValue model
              AutoFocus true
              OnChange (fun ev -> !!ev.target?value |> ChangeText |> dispatch ) ] ]
      br [ ]
      span
        [ ]
        [ str (sprintf "Hello %s" model) ] ]
