(function()
{
 var Global=this,Runtime=this.IntelliFactory.Runtime,Website,ForkMe,WebSharper,Html,Operators,Default,List,jQuery,Remoting,Concurrency,alert,EventsPervasives,HTML5,Highlight,Client;
 Runtime.Define(Global,{
  Website:{
   ForkMe:{
    Control:Runtime.Class({
     get_Body:function()
     {
      return ForkMe.main();
     }
    }),
    main:function()
    {
     return Operators.add(Default.A(List.ofArray([Default.HRef("https://github.com/TahaHachana/FSLight")])),List.ofArray([Default.Img(List.ofArray([Default.Src("https://s3.amazonaws.com/github/ribbons/forkme_right_green_007200.png"),Default.Alt("Fork me on GitHub"),Default.Id("forkme")]))]));
    }
   },
   Highlight:{
    Client:{
     main:function()
     {
      var textArea,_this,_textArea_,_this1,formatBtn,x,el,_this2,inner,f,x1,_this3,_this4,_this5,_this6,arg002,_this7,arg003,el1,inner1,_this8;
      textArea=Default.TextArea(List.ofArray([(_this=Default.Attr(),_this.NewAttr("style","overflow: scroll; word-wrap: normal; height: 300px;")),Default.Attr().Class("span12")]));
      _textArea_=Default.TextArea(List.ofArray([(_this1=Default.Attr(),_this1.NewAttr("style","overflow: scroll; word-wrap: normal; height: 300px;")),Default.Attr().Class("span12")]));
      formatBtn=(x=(el=Default.Button(List.ofArray([Default.Attr().Class("btn btn-primary btn-large"),(_this2=Default.Attr(),_this2.NewAttr("style","float: left;"))])),(inner=Default.Text("Highlight"),Operators.add(el,List.ofArray([inner])))),(f=(x1=function(elt)
      {
       return function()
       {
        var x2,f1,f5;
        x2=(f1=function()
        {
         var objectArg,arg00,loaderJq,x3,f2;
         objectArg=elt["HtmlProvider@32"];
         ((arg00=elt.Body,function(arg10)
         {
          return function(arg20)
          {
           return objectArg.SetAttribute(arg00,arg10,arg20);
          };
         })("disabled"))("disabled");
         loaderJq=jQuery("#loader");
         loaderJq.css("visibility","visible");
         _textArea_.set_Value("");
         x3=Remoting.Async("Website:0",[textArea.get_Value()]);
         f2=function(_arg11)
         {
          var a,html,b,f3,f4;
          a=_arg11.$==1?(html=_arg11.$0,(jQuery("#html-textarea").text(html),(jQuery("#html-preview").html(html),Concurrency.Return(null)))):(alert("An error occured."),Concurrency.Return(null));
          b=(f3=function()
          {
           var objectArg1,arg001;
           loaderJq.css("visibility","hidden");
           objectArg1=elt["HtmlProvider@32"];
           (arg001=elt.Body,function(arg10)
           {
            return objectArg1.RemoveAttribute(arg001,arg10);
           })("disabled");
           return Concurrency.Return(null);
          },Concurrency.Delay(f3));
          f4=function()
          {
           return b;
          };
          return Concurrency.Bind(a,f4);
         };
         return Concurrency.Bind(x3,f2);
        },Concurrency.Delay(f1));
        f5=function(arg00)
        {
         var t;
         t={
          $:0
         };
         return Concurrency.Start(arg00);
        };
        return f5(x2);
       };
      },function(arg10)
      {
       return EventsPervasives.Events().OnClick(x1,arg10);
      }),(f(x),x)));
      return Default.Div(List.ofArray([Default.H3(List.ofArray([Default.Text("F# Code")])),textArea,Operators.add(Default.Div(List.ofArray([(_this3=Default.Attr(),_this3.NewAttr("style","padding: 10px;"))])),List.ofArray([formatBtn,Default.Div(List.ofArray([Default.Img(List.ofArray([(_this4=Default.Attr(),_this4.NewAttr("style","padding: 15px; padding-left: 0px; visibility: hidden;")),Default.Src("Images/Loader.gif"),Default.Id("loader")]))]))])),Operators.add(Default.Div(List.ofArray([(_this5=Default.Attr(),_this5.NewAttr("style","height: 500px;"))])),List.ofArray([Operators.add(Default.Div(List.ofArray([Default.Attr().Class("tabbable")])),List.ofArray([Operators.add(Default.UL(List.ofArray([Default.Attr().Class("nav nav-tabs")])),List.ofArray([Operators.add(Default.LI(List.ofArray([Default.Attr().Class("active")])),List.ofArray([Operators.add(Default.A(List.ofArray([Default.HRef("#html"),(_this6=HTML5.Attr(),(arg002="data-"+"toggle",_this6.NewAttr(arg002,"tab")))])),List.ofArray([Default.Text("HTML")]))])),Default.LI(List.ofArray([Operators.add(Default.A(List.ofArray([Default.HRef("#html-preview"),(_this7=HTML5.Attr(),(arg003="data-"+"toggle",_this7.NewAttr(arg003,"tab")))])),List.ofArray([Default.Text("HTML Preview")]))]))])),Operators.add(Default.Div(List.ofArray([Default.Attr().Class("tab-content")])),List.ofArray([(el1=Default.Div(List.ofArray([Default.Attr().Class("tab-pane active"),Default.Id("html")])),(inner1=Default.TextArea(List.ofArray([Default.Id("html-textarea"),(_this8=Default.Attr(),_this8.NewAttr("style","overflow: scroll; word-wrap: normal; height: 300px;")),Default.Attr().Class("span12")])),Operators.add(el1,List.ofArray([inner1])))),Default.Div(List.ofArray([Default.Attr().Class("tab-pane"),Default.Id("html-preview")]))]))]))]))]));
     }
    },
    Control:Runtime.Class({
     get_Body:function()
     {
      return Client.main();
     }
    })
   }
  }
 });
 Runtime.OnInit(function()
 {
  Website=Runtime.Safe(Global.Website);
  ForkMe=Runtime.Safe(Website.ForkMe);
  WebSharper=Runtime.Safe(Global.IntelliFactory.WebSharper);
  Html=Runtime.Safe(WebSharper.Html);
  Operators=Runtime.Safe(Html.Operators);
  Default=Runtime.Safe(Html.Default);
  List=Runtime.Safe(WebSharper.List);
  jQuery=Runtime.Safe(Global.jQuery);
  Remoting=Runtime.Safe(WebSharper.Remoting);
  Concurrency=Runtime.Safe(WebSharper.Concurrency);
  alert=Runtime.Safe(Global.alert);
  EventsPervasives=Runtime.Safe(Html.EventsPervasives);
  HTML5=Runtime.Safe(Default.HTML5);
  Highlight=Runtime.Safe(Website.Highlight);
  return Client=Runtime.Safe(Highlight.Client);
 });
 Runtime.OnLoad(function()
 {
 });
}());
