#pragma checksum "C:\Users\mcuri\OneDrive\Desktop\NewWebApp\BarWebApplication\Views\Cart\EmptyCart.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "0995448c36b2d5b13e50916246e05a17c3e9e987"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Cart_EmptyCart), @"mvc.1.0.view", @"/Views/Cart/EmptyCart.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\mcuri\OneDrive\Desktop\NewWebApp\BarWebApplication\Views\_ViewImports.cshtml"
using BarWebApplication;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\mcuri\OneDrive\Desktop\NewWebApp\BarWebApplication\Views\_ViewImports.cshtml"
using BarWebApplication.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"0995448c36b2d5b13e50916246e05a17c3e9e987", @"/Views/Cart/EmptyCart.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"df04de26ae123ef8cfb615f9da1097784b34cfd9", @"/Views/_ViewImports.cshtml")]
    public class Views_Cart_EmptyCart : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 2 "C:\Users\mcuri\OneDrive\Desktop\NewWebApp\BarWebApplication\Views\Cart\EmptyCart.cshtml"
  
    ViewData["Title"] = "EmptyCart";
    Layout = "~/Views/Shared/_Layout.cshtml";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<h1>Cart is empty!</h1>\r\n<h3>Please add something to cart and come back later</h3>\r\n\r\n");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591