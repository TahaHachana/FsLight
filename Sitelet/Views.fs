module Sitelet.Views

open Sitelet.Content
open Sitelet.Model
open Sitelet.Skin

let home = 
    withTemplate<Action>
        Templates.home
        "Online F# Syntax Highlighting Tool" 
        "A web-based FSharp syntax highlighting tool."
        Home.body

let about =
    withTemplate<Action>
        Templates.about
        "FSharp Syntax Highlighting Tool" 
        "Online tool for F# syntax highlighting built with WebSharper."
        About.body

let error = 
    withTemplate<Action>
        Templates.error
        "Error - Page Not Found"
        ""
        Error.body