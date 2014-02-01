module Sitelet.Controller

open IntelliFactory.WebSharper.Sitelets
open Sitelet.Model

let controller =
    {Handle =
        function 
        | About -> Views.about
        | Error -> Views.error
        | Home -> Views.home}