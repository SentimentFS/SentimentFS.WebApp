module Global

type Page =
  | Sentiment
  | Counter
  | About

let toHash page =
  match page with
  | About -> "#about"
  | Counter -> "#counter"
  | Sentiment -> "#sentiment"
