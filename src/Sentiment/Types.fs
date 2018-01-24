module Sentiment.Types
open Fable.Core
open System.Collections.Generic

type Emotion =
    | VeryNegative = -2
    | Negative = -1
    | Neutral = 0
    | Positive = 1
    | VeryPositive = 2

let getEmotionName emotion =
    match emotion with
    | Emotion.VeryNegative -> "Bardzo Negatywny"
    | Emotion.Negative -> "Negatywny"
    | Emotion.Neutral -> "Neutralny"
    | Emotion.Positive -> "Pozytywny"
    | Emotion.VeryPositive -> "Bardzo Pozytywny"
    | _ -> "Nieznany"

[<Pojo>]
type Classify = { text : string }

[<Pojo>]
type Train = { value: string; category: Emotion; weight : int option }

[<Pojo>]
type Sentiment = { emotion: Emotion; probability: float }

[<Pojo>]
type ClassificationResult = { text: string; score: Sentiment array }

[<Pojo>]
type ClassificatorState = { categories: Map<Emotion, Map<string, int>> }

type ChartData = { name: string; prob: float }

type Model = { text: string; classificationResult: ClassificationResult }

type Msg =
    | Classify
    | Classified of ClassificationResult
    | ChangeText of string
    | Train of Train
    | GetState
    | Error of exn
