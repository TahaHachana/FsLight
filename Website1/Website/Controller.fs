namespace Website

module Controller =

    open IntelliFactory.WebSharper.Sitelets
    open Model
    
    let controller =

        let handle = function
            | About -> Views.about
            | Error -> Views.error
            | Home  -> Views.home

        { Handle = handle }