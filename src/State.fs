module App.State

open Elmish
open Elmish.Browser.Navigation
open Elmish.Browser.UrlParser
open Fable.Import.Browser
open Global
open Types

let pageParser: Parser<Page->Page,Page> =
  oneOf [
    map About (s "about")
    map Counter (s "counter")
    map Sentiment (s "sentiment")
  ]

let urlUpdate (result: Option<Page>) model =
  match result with
  | None ->
    console.error("Error parsing url")
    model,Navigation.modifyUrl (toHash model.currentPage)
  | Some page ->
      { model with currentPage = page }, []

let init result =
  let (counter, counterCmd) = Counter.State.init()
  let (sentiment, homeCmd) = Sentiment.State.init()
  let (model, cmd) =
    urlUpdate result
      { currentPage = Sentiment
        counter = counter
        sentiment = sentiment }
  model, Cmd.batch [ cmd
                     Cmd.map CounterMsg counterCmd
                     Cmd.map SentimentMsg homeCmd ]

let update msg model =
  match msg with
  | CounterMsg msg ->
      let (counter, counterCmd) = Counter.State.update msg model.counter
      { model with counter = counter }, Cmd.map CounterMsg counterCmd
  | SentimentMsg msg ->
      let (sentiment, sentimentCmd) = Sentiment.State.update msg model.sentiment
      { model with sentiment = sentiment }, Cmd.map SentimentMsg sentimentCmd
