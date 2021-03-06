module Sentiment.View

open Types
open Fable.Core.JsInterop
open Fable.Recharts
open Fable.Recharts.Props
open Fable.Helpers.React
open Fable.Helpers.React.Props
module SVG = Fable.Helpers.React.Props
module R = Fable.Helpers.React
module P = R.Props

let prepareData(score: Sentiment array ) =
    score |> Array.map(fun x -> { name = x.emotion |> getEmotionName; prob = x.probability})
let margin t r b l =
    Chart.Margin { top = t; bottom = b; right = r; left = l }

let emotionBarChart(chartData: ChartData array) =
    barChart
        [ margin 5. 20. 5. 0.
          Chart.Width 600.
          Chart.Height 300.
          Chart.Data chartData ]
        [ xaxis [Cartesian.DataKey "name"] []
          yaxis [] []
          tooltip [] []
          legend [] []
          cartesianGrid [StrokeDasharray "5 5"] []
          bar [Cartesian.DataKey "prob"; Cartesian.StackId "a"; P.Fill "#8884d8"] []
        ]

let emotionRadarChart(chartData: ChartData array) =
    radarChart
        [ margin 5. 20. 5. 0.
          Chart.Width 600.
          Chart.Height 300.
          Chart.Data chartData ]
        [ polarGrid [][]
          polarAngleAxis [ Polar.DataKey "name" ] []
          polarRadiusAxis [] []
          radar [ Name "Emotions"; Polar.DataKey "prob"; P.Stroke "#8884d8"; P.Fill "#8884d8"; P.FillOpacity 0.6; ] []
        ]

let root (model: Model) dispatch =
  let chartData = (model.classificationResult.score |> prepareData)
  div
    [ ]
    [ p
        [ ClassName "control" ]
        [ input
            [ ClassName "input"
              Type "text"
              Placeholder "Wpisz tekst"
              DefaultValue model.text
              AutoFocus true
              OnChange (fun ev -> !!ev.target?value |> ChangeText |> dispatch ) ] ]
      br [ ]
      button [ClassName "button"; Type "button"; Value "Szukaj"; OnClick (fun _ -> Classify |> dispatch)] [ str "Szukaj" ]
      div
        [ ]
        [ emotionRadarChart(chartData)
          emotionBarChart(chartData)
        ] ]
