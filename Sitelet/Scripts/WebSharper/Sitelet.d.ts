declare module Sitelet {
    module Site {
        interface Website {
        }
    }
    module Model {
        interface Action {
        }
    }
    module Highlight {
        module Client {
            var handleHighlight : {
                <_M1>(elt: _Html.Element, _arg1: _M1): void;
            };
            var highlightBtn : {
                (): _Html.Element;
            };
            var clearBtn : {
                (): _Html.Element;
            };
            var main : {
                (): _Html.Element;
            };
        }
        module Server {
            interface Token {
            }
        }
        interface Result {
        }
        interface Control {
            get_Body(): _Html.IPagelet;
        }
    }
    module Skin {
        interface Page {
            Title: string;
            MetaDesc: string;
            Body: _Html1.Element<_Web.Control>;
        }
    }
    
    import _Html = IntelliFactory.WebSharper.Html;
    import _Html1 = IntelliFactory.Html.Html;
    import _Web = IntelliFactory.WebSharper.Web;
}
