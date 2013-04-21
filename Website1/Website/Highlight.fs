namespace Website

open IntelliFactory.WebSharper

module Highlight =
   
    type Result = Failure | Success of string

    module Server =

        open System
        open System.Text.RegularExpressions

        type Token =
            | String    of string
            | Else      of string
            | Keyword   of string
            | Comment   of string
            | MLComment of string
        
        let commentRegex = Regex("//(.+)?", RegexOptions.Compiled)
        let mlCommentRegex = Regex("(?s)\(\*.+?\*\)", RegexOptions.Compiled)
        let keywordRegex = Regex("(\ ?( abstract )\ |\ ?( and )\ |\ ?( as )\ |\ ?( assert )\ |\ ?( base )\ |\ ?( begin )\ |\ ?( class )\ |\ ?( default )\ |\ ?( delegate )\ |\ ?( do )\ |\ ?( done )\ |\ ?( downcast )\ |\ ?( downto )\ |\ ?( elif )\ |\ ?( else )\ |\ ?( end )\ |\ ?( exception )\ |\ ?( extern )\ |\ ?( false )\ |\ ?( finally )\ |\ ?( for )\ |\ ?( fun )\ |\ ?( function )\ |\ ?( global)\ |\ ?(if)\ |\ ?(in)\ |\ ?(inherit)\ |\ ?(inline)\ |\ ?(interface)\ |\ ?(internal)\ |\ ?(lazy)\ |\ ?(let)\ |\ ?(let!)\ |\ ?(match)\ |\ ?(member)\ |\ ?(module)\ |\ ?(mutable)\ |\ ?(namespace)\ |\ ?(new)\ |\ ?(not)\ |\ ?(null)\ |\ ?(of)\ |\ ?(open)\ |\ ?(or)\ |\ ?(override)\ |\ ?(private)\ |\ ?(public)\ |\ ?(rec)\ |\ ?(return)\ |\ ?(return!)\ |\ ?(select)\ |\ ?(static)\ |\ ?(struct)\ |\ ?(then)\ |\ ?(to)\ |\ ?(true)\ |\ ?(try)\ |\ ?(type)\ |\ ?(upcast)\ |\ ?(use)\ |\ ?(use!)\ |\ ?(val)\ |\ ?(void)\ |\ ?(when)\ |\ ?(while)\ |\ ?(with)\ |\ ?(yield)\ |\ ?(yield!)\ )",RegexOptions.Compiled)
        let stringRegex = Regex("(?s)(\"[^\"]+?\"|\"{3}.+?\"{3})", RegexOptions.Compiled)
    
        let (|ParseRegex|_|) (regex : Regex) str =
            let m = regex.Match str
            match m.Success && m.Index = 0 with
                | false -> None
                | true  ->
                    let idx = m.Length
                    let str' = m.Value
                    Some (str', str.[idx ..])

        let (|ParseString|_|) str =
            match str with
                | ParseRegex stringRegex (str, str') -> Some (String str, str')
                | _                                  -> None

        let (|ParseKeyword|_|) str =
            match str with
                | ParseRegex keywordRegex (str, str') -> Some (Keyword str, str')
                | _                                   -> None

        let (|ParseComment|_|) str =
            match str with
                | ParseRegex commentRegex (str, str') -> Some (Comment str, str')
                | _                                   -> None

        let (|ParseMLComment|_|) str =
            match str with
                | ParseRegex mlCommentRegex (str, str') -> Some (MLComment str, str')
                | _                                     -> None

        let rec tokenize str acc =
            match str with
            | ""                           -> acc
            | ParseString    (token, str') -> tokenize str' <| token :: acc
            | ParseKeyword   (token, str') -> tokenize str' <| token :: acc
            | ParseComment   (token, str') -> tokenize str' <| token :: acc
            | ParseMLComment (token, str') -> tokenize str' <| token :: acc
            | _                            -> tokenize str.[1 ..] <| (Else <| str.Substring(0, 1)) :: acc

        let lineNums (str : String) =
            let str' = Regex("\n$").Replace(str, "")
            let count = str'.Split '\n' |> fun x -> x.Length
            let spans = [for x in 1 .. count -> "<span>" + string x + "</span>"] |> String.concat "<br />"
            "<div style='margin: 0px; padding: 0px; border: 1px solid lightgray; font-family: Consolas; background-color: #f8f8ff;'><table><tr><td style='padding: 5px; background-color: lightgray;'>" + spans + "</td><td style='vertical-align: top;'>" + str' + "</td></tr></table></div>"

        let serialize (tokens : Token list) =
            List.foldBack (fun token str ->
                let token' =
                    match token with
                        | String x                -> "<span style='color: #d14;'>" + x + "</span>"
                        | Keyword x               -> "<span style='color: blue;'>" + x + "</span>"
                        | Comment x | MLComment x -> "<span style='color: green;'>" + x + "</span>"
                        | Else x                  -> x
                str + token') tokens ""
            |> fun x -> "<pre style='padding: 5px; margin: 0px;'>" + x + "</pre>"
            |> lineNums

        let highlight str = tokenize str [] |> serialize

        [<Rpc>]
        let format src =
            async {
                try
                    let html = highlight src
                    return Success html
                with _ -> return Failure
            }

    [<JavaScript>]
    module Client =
        
        open IntelliFactory.WebSharper.Html
        open IntelliFactory.WebSharper.JQuery

        let main() =
            let textArea  = TextArea [Attr.Style "overflow: scroll; word-wrap: normal; height: 300px;"; Attr.Class "span12"]
            let textArea' = TextArea [Attr.Style "overflow: scroll; word-wrap: normal; height: 300px;"; Attr.Class "span12"]
            let formatBtn =
                Button [Attr.Class "btn btn-primary btn-large"; Attr.Style "float: left;"] -- Text "Highlight"
                |>! OnClick (fun elt _ ->
                    async {
                        elt.SetAttribute("disabled", "disabled")
                        let loaderJq = JQuery.Of("#loader")
                        loaderJq.Css("visibility", "visible").Ignore
                        textArea'.Value <- ""
                        let! result = Server.format textArea.Value
                        match result with
                            | Failure      -> JavaScript.Alert "An error occured."
                            | Success html ->
                                JQuery.Of("#html-textarea").Text(html).Ignore
                                JQuery.Of("#html-preview").Html(html).Ignore
                        loaderJq.Css("visibility", "hidden").Ignore
                        elt.RemoveAttribute("disabled")
                    } |> Async.Start)
            Div [
                H3 [Text "F# Code"]
                textArea
                Div [Attr.Style "padding: 10px;"] -< [
                    formatBtn
                    Div [Img [Attr.Style "padding: 15px; padding-left: 0px; visibility: hidden;"; Src "Images/Loader.gif"; Id "loader"]]
                ]
                Div [Attr.Style "height: 500px;"] -< [
                    Div [Attr.Class "tabbable"] -< [
                        UL [Attr.Class "nav nav-tabs"] -< [
                            LI [Attr.Class "active"] -< [A [HRef "#html"; HTML5.Attr.Data "toggle" "tab"] -< [Text "HTML"]]
                            LI [A [HRef "#html-preview"; HTML5.Attr.Data "toggle" "tab"] -< [Text "HTML Preview"]]
                        ]
                        Div [Attr.Class "tab-content"] -< [
                            Div [Attr.Class "tab-pane active"; Id "html"] -- TextArea [Id "html-textarea"; Attr.Style "overflow: scroll; word-wrap: normal; height: 300px;"; Attr.Class "span12"]
                            Div [Attr.Class "tab-pane"; Id "html-preview"]
                        ]
                    ]
                ]
            ]

    type Control() =
        
        inherit Web.Control()

        [<JavaScript>]
        override __.Body = Client.main() :> _