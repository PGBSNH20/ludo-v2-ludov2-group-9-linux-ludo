#pragma checksum "C:\Users\adam-\Documents\Workspace\ludo-v2-ludov2-group-9-linux-ludo\src\linuxludo.web\Pages\Index.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "82b810eabdebbb364037e3cad8f9e42446730822"
// <auto-generated/>
#pragma warning disable 1591
namespace LinuxLudo.Web.Pages
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Components;
#nullable restore
#line 1 "C:\Users\adam-\Documents\Workspace\ludo-v2-ludov2-group-9-linux-ludo\src\linuxludo.web\_Imports.razor"
using System.Net.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\adam-\Documents\Workspace\ludo-v2-ludov2-group-9-linux-ludo\src\linuxludo.web\_Imports.razor"
using System.Net.Http.Json;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "C:\Users\adam-\Documents\Workspace\ludo-v2-ludov2-group-9-linux-ludo\src\linuxludo.web\_Imports.razor"
using Microsoft.AspNetCore.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "C:\Users\adam-\Documents\Workspace\ludo-v2-ludov2-group-9-linux-ludo\src\linuxludo.web\_Imports.razor"
using Microsoft.AspNetCore.Components.Authorization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "C:\Users\adam-\Documents\Workspace\ludo-v2-ludov2-group-9-linux-ludo\src\linuxludo.web\_Imports.razor"
using Microsoft.AspNetCore.Components.Forms;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "C:\Users\adam-\Documents\Workspace\ludo-v2-ludov2-group-9-linux-ludo\src\linuxludo.web\_Imports.razor"
using Microsoft.AspNetCore.Components.Routing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "C:\Users\adam-\Documents\Workspace\ludo-v2-ludov2-group-9-linux-ludo\src\linuxludo.web\_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "C:\Users\adam-\Documents\Workspace\ludo-v2-ludov2-group-9-linux-ludo\src\linuxludo.web\_Imports.razor"
using Microsoft.AspNetCore.Components.Web.Virtualization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "C:\Users\adam-\Documents\Workspace\ludo-v2-ludov2-group-9-linux-ludo\src\linuxludo.web\_Imports.razor"
using Microsoft.AspNetCore.Components.WebAssembly.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 10 "C:\Users\adam-\Documents\Workspace\ludo-v2-ludov2-group-9-linux-ludo\src\linuxludo.web\_Imports.razor"
using Microsoft.JSInterop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 11 "C:\Users\adam-\Documents\Workspace\ludo-v2-ludov2-group-9-linux-ludo\src\linuxludo.web\_Imports.razor"
using LinuxLudo.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 12 "C:\Users\adam-\Documents\Workspace\ludo-v2-ludov2-group-9-linux-ludo\src\linuxludo.web\_Imports.razor"
using LinuxLudo.Web.Shared;

#line default
#line hidden
#nullable disable
#nullable restore
#line 13 "C:\Users\adam-\Documents\Workspace\ludo-v2-ludov2-group-9-linux-ludo\src\linuxludo.web\_Imports.razor"
using LinuxLudo.Web.Authentication;

#line default
#line hidden
#nullable disable
#nullable restore
#line 14 "C:\Users\adam-\Documents\Workspace\ludo-v2-ludov2-group-9-linux-ludo\src\linuxludo.web\_Imports.razor"
using LinuxLudo.Web.Domain;

#line default
#line hidden
#nullable disable
#nullable restore
#line 15 "C:\Users\adam-\Documents\Workspace\ludo-v2-ludov2-group-9-linux-ludo\src\linuxludo.web\_Imports.razor"
using LinuxLudo.Web.Game;

#line default
#line hidden
#nullable disable
#nullable restore
#line 16 "C:\Users\adam-\Documents\Workspace\ludo-v2-ludov2-group-9-linux-ludo\src\linuxludo.web\_Imports.razor"
using LinuxLudo.Web.Game.Services;

#line default
#line hidden
#nullable disable
#nullable restore
#line 17 "C:\Users\adam-\Documents\Workspace\ludo-v2-ludov2-group-9-linux-ludo\src\linuxludo.web\_Imports.razor"
using Blazor.Extensions;

#line default
#line hidden
#nullable disable
#nullable restore
#line 18 "C:\Users\adam-\Documents\Workspace\ludo-v2-ludov2-group-9-linux-ludo\src\linuxludo.web\_Imports.razor"
using Blazor.Extensions.Canvas.Canvas2D;

#line default
#line hidden
#nullable disable
#nullable restore
#line 19 "C:\Users\adam-\Documents\Workspace\ludo-v2-ludov2-group-9-linux-ludo\src\linuxludo.web\_Imports.razor"
using Blazor.Extensions.Canvas;

#line default
#line hidden
#nullable disable
    [Microsoft.AspNetCore.Components.RouteAttribute("/")]
    [Microsoft.AspNetCore.Components.RouteAttribute("/Home")]
    public partial class Index : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
            __builder.OpenElement(0, "div");
            __builder.AddAttribute(1, "class", "index-body");
            __builder.AddAttribute(2, "b-m53w0cp7ne");
            __builder.AddMarkupContent(3, "<h1 class=\"title-text\" b-m53w0cp7ne>A multiplayer ludo game</h1>\r\n    ");
            __builder.AddMarkupContent(4, "<p b-m53w0cp7ne>Developed by Adam Brodin & Leo Rönnebro</p>\r\n\r\n    ");
            __builder.AddMarkupContent(5, @"<div class=""about-page"" b-m53w0cp7ne><h2 b-m53w0cp7ne>This is the result of a school-project that was built using <b b-m53w0cp7ne>Blazor</b> & the <b b-m53w0cp7ne>ASP.NET Core</b>
            framework.</h2>
        <h2 class=""repository-link"" b-m53w0cp7ne><a href=""https://github.com/PGBSNH20/ludo-v2-ludov2-group-9-linux-ludo"" b-m53w0cp7ne>Link to
                repository</a></h2></div>

    ");
            __builder.OpenElement(6, "div");
            __builder.AddAttribute(7, "class", "navigation");
            __builder.AddAttribute(8, "b-m53w0cp7ne");
            __builder.OpenElement(9, "button");
            __builder.AddAttribute(10, "class", "play-btn");
            __builder.AddAttribute(11, "onclick", Microsoft.AspNetCore.Components.EventCallback.Factory.Create<Microsoft.AspNetCore.Components.Web.MouseEventArgs>(this, 
#nullable restore
#line 18 "C:\Users\adam-\Documents\Workspace\ludo-v2-ludov2-group-9-linux-ludo\src\linuxludo.web\Pages\Index.razor"
                                            () => NavManager.NavigateTo("/Play")

#line default
#line hidden
#nullable disable
            ));
            __builder.AddAttribute(12, "b-m53w0cp7ne");
            __builder.AddContent(13, "Play");
            __builder.CloseElement();
            __builder.CloseElement();
            __builder.CloseElement();
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private NavigationManager NavManager { get; set; }
    }
}
#pragma warning restore 1591
