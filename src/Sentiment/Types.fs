module Sentiment.Types
open Fable.Core

type Emotion =
    | VeryNegative = -2
    | Negative = -1
    | Neutral = 0
    | Positive = 1
    | VeryPositive = 2

[<Pojo>]
type Classify = { text : string }

[<Pojo>]
type Train = { value: string; category: Emotion; weight : int option }

[<Pojo>]
type ClassificationResult = { text: string; score: Map<Emotion, float> }

[<Pojo>]
type ClassificatorState = { categories: Map<Emotion, Map<string, int>> }

type Model = { text: string; classificationResult: ClassificationResult }

type Msg =
    | Classify
    | Classified of ClassificationResult
    | ChangeText of string
    | Train of Train
    | GetState
    | Error of exn
