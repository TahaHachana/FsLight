module Sitelet.Skin

open IntelliFactory.WebSharper.Sitelets
open System.Web

type Page = 
    {Title : string
     MetaDesc : string
     Body : Content.HtmlElement}

    static member Make title metaDesc makeBody context = 
        {Title = title
         MetaDesc = metaDesc
         Body = makeBody context}

let loadFrequency =
#if DEBUG 
    Content.Template.PerRequest
#else
    Content.Template.Once
#endif

let makeTemplate<'T> path = 
    let path' = HttpContext.Current.Server.MapPath path
    Content.Template<'T>(path', loadFrequency)

let makePageTemplate path = 
    makeTemplate<Page> path 
    |> fun x -> 
        x.With("title", fun x -> x.Title)
         .With("meta-desc", fun x -> x.MetaDesc)
         .With("body", fun x -> x.Body)

let withTemplate<'T> template title metaDesc makeBody : Content<'T> = 
    Content.WithTemplate template 
    <| fun context -> Page.Make title metaDesc makeBody context