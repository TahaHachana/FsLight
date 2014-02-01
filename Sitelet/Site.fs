module Sitelet.Site

open IntelliFactory.WebSharper.Sitelets
open Sitelet.Controller
open Sitelet.Model

let router : Router<Action> = 
    Router.Table [
        About, "about"
        Error, "error"
        Home, "/"
    ] <|> Router.Infer()
    
let Main = 
    {Controller = controller
     Router = router}

type Website() = 
    interface IWebsite<Action> with
        member this.Sitelet = Main
        member this.Actions = []

[<WebsiteAttribute(typeof<Website>)>]
do ()