module Sitelet.Nav

open IntelliFactory.Html
open IntelliFactory.WebSharper.Sitelets
open System

let navToggle =
    Button [
        Attributes.Type "button"
        Class "navbar-toggle"
        HTML5.Data "target" ".navbar-ex1-collapse"
        HTML5.Data "toggle" "collapse"
    ] -< [
        Span [Class "sr-only"] -< [Text "Toggle Navigation"]
        Span [Class "icon-bar"]
        Span [Class "icon-bar"]
        Span [Class "icon-bar"]
    ]

let navHeader = 
    Div [Class "navbar-header"] -< [
        navToggle
        A [
            Class "navbar-brand"
            HRef "/"
        ] -< [Text "FsLight"]
    ]

let li activeLiOption href txt = 
    let link = A [HRef href] -< [Text txt]
    match activeLiOption with
    | Some activeLi when txt = activeLi -> LI [Class "active"] -< [link]
    | _ -> LI [link]

let navDiv activeLi = 
    Div [Class "collapse navbar-collapse navbar-ex1-collapse"] -< [
        UL [Class "nav navbar-nav"] -< [
            li activeLi "/" "Home"
            li activeLi "/about" "About"
        ]
    ]

let navElt activeLi : Content.HtmlElement = 
    HTML5.Nav [
        Class "navbar navbar-inverse navbar-fixed-top"
        NewAttribute "role" "navigation"
    ] -< [
        Div [Class "container"] -< [
            navHeader
            navDiv activeLi
        ]
    ]