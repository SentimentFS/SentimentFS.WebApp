module App.Types

open Global

type Msg =
  | CounterMsg of Counter.Types.Msg
  | SentimentMsg of Sentiment.Types.Msg

type Model = {
    currentPage: Page
    counter: Counter.Types.Model
    sentiment: Sentiment.Types.Model
  }
