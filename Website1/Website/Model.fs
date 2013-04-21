namespace Website

open IntelliFactory.WebSharper.Sitelets.Content

module Model =

    type PageId = int

    type Action =
        | About
        | Error
        | Home