namespace Website

module Content =

    open IntelliFactory.WebSharper
    open IntelliFactory.Html
    open IntelliFactory.WebSharper.Sitelets.Content
    open Utils.Server

    module Shared =
        
        let navigation : HtmlElement = nav None

        let footer : HtmlElement =
            HTML5.Footer [Id "footer"] -< [
                Div [Class "container"; Style "padding-top: 20px;"] -< [
                    P [Text "Powered by "] -< [
                        A ["WebSharper" => "http://www.websharper.com/"]
                    ]
                ]            
            ]

    module Home =

        let title = "Online F# Syntax Highlighting Tool"

        let metaDescription = "A Web-based FSharp syntax highlighting tool."

        let navigation : HtmlElement = nav <| Some "Home"

        let header : HtmlElement =
            header
                "F# Syntax Highlighting"
                "Paste your FSharp code and press 'Highlight' to generate a properly formatted output."

    module About =
    
        let title = "FSharp Syntax Highlighting Tool"

        let metaDescription = "Online tool for F# code syntax highlightinh built with WebSharper."

        let navigation : HtmlElement = nav <| Some "About"