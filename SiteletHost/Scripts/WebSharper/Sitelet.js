(function()
{
 var Global=this,Runtime=this.IntelliFactory.Runtime,WebSharper,Html,Default,List,Operators,jQuery,EventsPervasives,Concurrency,Remoting,alert,Sitelet,Highlight,Client;
 Runtime.Define(Global,{
  Sitelet:{
   Highlight:{
    Client:{
     clearBtn:function()
     {
      var x,el,_this,inner,f,arg00;
      x=(el=Default.Button(List.ofArray([Default.Attr().Class("btn btn-default btn-lg"),(_this=Default.Attr(),_this.NewAttr("id","clear-btn"))])),(inner=Default.Text("Clear"),Operators.add(el,List.ofArray([inner]))));
      f=(arg00=function()
      {
       return function()
       {
        return jQuery("#code").val("").focus();
       };
      },function(arg10)
      {
       return EventsPervasives.Events().OnClick(arg00,arg10);
      });
      f(x);
      return x;
     },
     handleHighlight:function(elt)
     {
      var arg00,clo1,t;
      arg00=Concurrency.Delay((clo1=function()
      {
       var objectArg,arg001,progressJq,htmlJq,previewJq,code,x,f,x1,f1;
       objectArg=elt["HtmlProvider@32"];
       arg001=elt.Body;
       (function(arg20)
       {
        return objectArg.SetAttribute(arg001,"disabled",arg20);
       }("disabled"));
       progressJq=jQuery("#progress-bar");
       progressJq.show();
       htmlJq=jQuery("#html");
       previewJq=jQuery("#html-preview-tab");
       htmlJq.val("");
       previewJq.html("");
       code=(x=jQuery("#code").val(),(f=function(value)
       {
        return Global.String(value);
       },f(x)));
       x1=Remoting.Async("Sitelet:0",[code]);
       f1=function(_arg2)
       {
        var a,html,b,f2,f3;
        progressJq.hide();
        a=_arg2.$==1?(html=_arg2.$0,(htmlJq.val(html).click(function()
        {
         return htmlJq.select();
        }),(previewJq.html(html),Concurrency.Return(null)))):(alert("An error occured."),Concurrency.Return(null));
        b=(f2=function()
        {
         var objectArg1,arg002;
         objectArg1=elt["HtmlProvider@32"];
         arg002=elt.Body;
         objectArg1.RemoveAttribute(arg002,"disabled");
         return Concurrency.Return(null);
        },Concurrency.Delay(f2));
        f3=function()
        {
         return b;
        };
        return Concurrency.Bind(a,f3);
       };
       return Concurrency.Bind(x1,f1);
      },clo1));
      t={
       $:0
      };
      return Concurrency.Start(arg00);
     },
     highlightBtn:function()
     {
      var x,el,inner,f,arg00;
      x=(el=Default.Button(List.ofArray([Default.Attr().Class("btn btn-primary btn-lg")])),(inner=Default.Text("Highlight"),Operators.add(el,List.ofArray([inner]))));
      f=(arg00=function(elt)
      {
       return function(_arg10_)
       {
        return Client.handleHighlight(elt,_arg10_);
       };
      },function(arg10)
      {
       return EventsPervasives.Events().OnClick(arg00,arg10);
      });
      f(x);
      return x;
     },
     main:function()
     {
      return Default.Div(List.ofArray([Client.highlightBtn(),Client.clearBtn()]));
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
  WebSharper=Runtime.Safe(Global.IntelliFactory.WebSharper);
  Html=Runtime.Safe(WebSharper.Html);
  Default=Runtime.Safe(Html.Default);
  List=Runtime.Safe(WebSharper.List);
  Operators=Runtime.Safe(Html.Operators);
  jQuery=Runtime.Safe(Global.jQuery);
  EventsPervasives=Runtime.Safe(Html.EventsPervasives);
  Concurrency=Runtime.Safe(WebSharper.Concurrency);
  Remoting=Runtime.Safe(WebSharper.Remoting);
  alert=Runtime.Safe(Global.alert);
  Sitelet=Runtime.Safe(Global.Sitelet);
  Highlight=Runtime.Safe(Sitelet.Highlight);
  return Client=Runtime.Safe(Highlight.Client);
 });
 Runtime.OnLoad(function()
 {
 });
}());
