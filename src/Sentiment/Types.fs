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

type Model = ClassificationResult

type Msg =
    | Classify of Classify
    | Train of Train
    | GetState
