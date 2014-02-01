module Sitelet.Content

open IntelliFactory.Html
open IntelliFactory.WebSharper
open IntelliFactory.WebSharper.Sitelets.Content

module Home =
    
    let header =
        Div [Class "page-header"] -< [
            H1 [Text "F# Syntax Highlighting"]
            P [Text "Paste your FSharp code and press 'Highlight' to generate a properly formatted output."]
        ]

    let textarea id = 
        TextArea [
            Id id
            HTML5.SpellCheck "false"
        ]

    let tabLi isActive href text = 
        let li = 
            match isActive with
            | false -> LI []
            | true -> LI [Class "active"]
        li -< [
            A [
                HRef href
                HTML5.Data "toggle" "tab"
            ] -< [Text text]
        ]

    let btnsDiv =
        Div [Id "btns"] -< [
            Div [new Highlight.Control()]
            Div [
                Class "progress progress-striped active"
                Id "progress-bar"
            ] -< [
                Div [Class "progress-bar"]
            ]
        ]

    let tabsDiv =
        Div [Class "tabbable"] -< [
            UL [Class "nav nav-tabs"] -< [
                tabLi true "#html-tab" "HTML"
                tabLi false "#html-preview-tab" "HTML Preview"
            ]
            Div [Class "tab-content"] -< [
                Div [
                    Class "tab-pane active"
                    Id "html-tab"
                ] -< [textarea "html"]
                Div [
                    Class "tab-pane"
                    Id "html-preview-tab"
                ]
            ]
        ]
     
    let body _ =
        Div [Class "wrap"] -< [
            Nav.navElt (Some "Home")
            Div [Class "container"; Id "push"] -< [
                header
                H3 [Text "F# Code"]
                textarea "code" -< [HTML5.AutoFocus "autofocus"]
                btnsDiv
                Div [Style "height: 500px;"] -< [tabsDiv]
            ]
        ]

module About =
    
    let header =
        Div [Class "page-header"] -< [
            H1 [Text "About FsLight"]
        ]

    let body _ =
        Div [Class "wrap"] -< [
            Nav.navElt (Some "About")
            Div [Class "container"; Id "push"] -< [
                header
                P [Class "lead"] -< [
                    Text "FsLight is an online FSharp syntax highlighting tool powered by "
                    A [HRef "http://www.websharper.com/"] -< [Text "WebSharper"]
                    Text " and maintained by "
                    A [HRef "http://taha-hachana.apphb.com/"] -< [Text "Taha Hachana"]
                    Text "."
                ]
            ]
        ]

module Error =

    let body _ = 
        Div [Class "wrap"] -< [
            Nav.navElt None
            Div [
                Class "container"
                Id "push"
            ] -< [
                Div [Class "page-header"] -< [H1 [Text "Error"]]
                H2 [Text "Page Not Found"]
                P [Text "The requested URL doesn't exist."]]]