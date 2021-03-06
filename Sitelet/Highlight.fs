﻿module Sitelet.Highlight

open IntelliFactory.WebSharper

type Result = Error | Success of string

module Server =
    open System
    open System.Text.RegularExpressions

    type Token = 
        | Comment of string
        | Else of string
        | Keyword of string
        | MLComment of string
        | String of string

    let compileRegex pattern = Regex(pattern, RegexOptions.Compiled)
    let commentRegex = compileRegex "^///?(.+)?"
    let mlCommentRegex = compileRegex "(?s)^\(\*.+?\*\)"
    let keywordRegex = compileRegex "^(#else|#endif|#help|#I|#if|#light|#load|#quit|#r|#time|abstract|and|as|assert|base|begin|class|default|delegate|do|done|downcast|downto|elif|else|end|exception|extern|false|finally|for|fun|function|global|if|in|inherit|inline|interface|internal|lazy|let|let!|match|member|module|mutable|namespace|new|not|null|of|open|or|override|private|public|rec|return|return!|select|static|struct|then|to|true|try|type|upcast|use|use!|val|void|when|while|with|yield|yield!)(\ |\n|$)"
    let stringRegex = compileRegex "(?s)^(\"[^\"\\\]*(?:\\\.[^\"\\\]*)*\"|\"{3}[^\"\\\]\*(?:\\\.[^\"\\\]*)*\"{3})"
    let elseRegex = compileRegex "^\w+(\ |\n|$)"

    let (|ParseRegex|_|) (regex : Regex) str = 
        let m = regex.Match str
        match m.Success with
        | false -> None
        | true -> Some(m.Value, str.[m.Length..])

    let (|ParseString|_|) str = 
        match str with
        | ParseRegex stringRegex (str, str') -> Some(String str, str')
        | _ -> None

    let (|ParseKeyword|_|) str = 
        match str with
        | ParseRegex keywordRegex (str, str') -> Some(Keyword str, str')
        | _ -> None

    let (|ParseComment|_|) str = 
        match str with
        | ParseRegex commentRegex (str, str') -> Some(Comment str, str')
        | _ -> None

    let (|ParseMLComment|_|) str = 
        match str with
        | ParseRegex mlCommentRegex (str, str') -> Some(MLComment str, str')
        | _ -> None

    let (|ParseElse|_|) str = 
        match str with
        | ParseRegex elseRegex (str, str') -> Some(Else str, str')
        | _ -> None

    let rec tokenize str acc = 
        match str with
        | "" -> acc
        | ParseString(token, str') -> tokenize str' <| token :: acc
        | ParseKeyword(token, str') -> tokenize str' <| token :: acc
        | ParseComment(token, str') -> tokenize str' <| token :: acc
        | ParseMLComment(token, str') -> tokenize str' <| token :: acc
        | ParseElse(token, str') -> tokenize str' <| token :: acc
        | _ -> tokenize str.[1..] <| (Else <| str.Substring(0, 1)) :: acc

    let lineNums(str : String) = 
        let count = str.Split '\n' |> fun x -> x.Length
        let spans = 
            [for x in 1..count -> "<span>" + string x + "</span>"]
            |> String.concat "<br />"
        "<div style='margin: 0px; padding: 0px; border: 1px solid #ececec; font-family: Monaco, Menlo, Consolas, monospace;'><style type='text/css'>.fs-str {color: #d14;} .fs-key {color: blue;} .fs-com {color: green; font-style: italic;}</style><table><tr><td style='padding: 5px; vertical-align: top; background-color: #ececec; color: rgb(160, 160, 160); font-size: 15px;'>" 
        + spans + "</td><td style='font-size: 15px; vertical-align: top; padding: 5px;'>" + str 
        + "</td></tr></table><div style='font-weight: bold; padding: 10px;'>Created with <a href='http://fslight.apphb.com/' target='_blank'>FsLight</a></div></div>"

    let serialize(tokens : Token list) = 
        List.foldBack (fun token str -> 
            let token' = 
                match token with
                | String x -> "<span class='fs-str'>" + x + "</span>"
                | Keyword x -> "<span class='fs-key'>" + x + "</span>"
                | Comment x | MLComment x -> "<span class='fs-com'>" + x + "</span>"
                | Else x -> x
            str + token') tokens ""
        |> fun x -> 
            "<pre style='margin: 0px; border: none; padding: 0; white-space: pre; font-size: 15px; background-color: white; font-family: Monaco, Menlo, Consolas, monospace;'>" 
            + x + "</pre>"
        |> lineNums

    let replaceAmpLtGt str = 
        Regex("&").Replace(str, "&amp;")
        |> fun x -> Regex("<").Replace(x, "&lt;")
        |> fun x -> Regex(">").Replace(x, "&gt;")

    let highlight str = 
        replaceAmpLtGt str
        |> fun x -> tokenize x []
        |> serialize

    [<Remote>]
    let format src =
        async {
            try
                let html = highlight src
                return Success html
            with _ -> return Error
        }

[<JavaScript>]
module Client =

    open IntelliFactory.WebSharper.Html
    open IntelliFactory.WebSharper.JQuery

    let handleHighlight (elt:Element) _ =
        async {
            elt.SetAttribute("disabled", "disabled")
            let progressJq = JQuery.Of("#progress-bar")
            progressJq.Show().Ignore
            let htmlJq = JQuery.Of("#html")
            let previewJq = JQuery.Of("#html-preview-tab")
            htmlJq.Val("").Ignore
            previewJq.Html("").Ignore
            let code = JQuery.Of("#code").Val() |> string
            let! result = Server.format code
            progressJq.Hide().Ignore
            match result with
                | Error        -> JavaScript.Alert "An error occured."
                | Success html ->
                    htmlJq.Val(html).Click(fun _ _ -> htmlJq.Select().Ignore).Ignore
                    previewJq.Html(html).Ignore
            elt.RemoveAttribute("disabled")
        } |> Async.Start

    let highlightBtn() =
        Button [Attr.Class "btn btn-primary btn-lg"]
        -- Text "Highlight"
        |>! OnClick handleHighlight

    let clearBtn() =
        Button [
            Attr.Class "btn btn-default btn-lg"
            Attr.Id "clear-btn"
        ] -- Text "Clear"
        |>! OnClick (fun _ _ -> JQuery.Of("#code").Val("").Focus().Ignore)

    let main() =
        Div [
            highlightBtn()
            clearBtn()
        ]

type Control() =
    inherit Web.Control()

    [<JavaScript>]
    override __.Body = Client.main() :> _