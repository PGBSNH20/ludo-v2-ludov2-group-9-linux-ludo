#pragma checksum "C:\Users\adam-\Documents\Workspace\ludo-v2-ludov2-group-9-linux-ludo\src\linuxludo.web\Shared\MainLayout.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "d9b670796cb4ce8a50040ab0e821e50559f9a1f5"
// <auto-generated/>
#pragma warning disable 1591
namespace LinuxLudo.Web.Shared
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
using LinuxLudo.Web.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 14 "C:\Users\adam-\Documents\Workspace\ludo-v2-ludov2-group-9-linux-ludo\src\linuxludo.web\_Imports.razor"
using LinuxLudo.Web.Authentication;

#line default
#line hidden
#nullable disable
    public partial class MainLayout : LayoutComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
            __builder.OpenElement(0, "div");
            __builder.AddAttribute(1, "class", "flex flex-col min-h-screen");
            __builder.AddAttribute(2, "b-8j0glk41zk");
            __builder.OpenComponent<LinuxLudo.Web.Shared.NavMenu>(3);
            __builder.CloseComponent();
            __builder.AddMarkupContent(4, "\r\n\r\n    \r\n    ");
            __builder.OpenElement(5, "div");
            __builder.AddAttribute(6, "class", "p-4");
            __builder.AddAttribute(7, "b-8j0glk41zk");
            __builder.AddContent(8, 
#nullable restore
#line 11 "C:\Users\adam-\Documents\Workspace\ludo-v2-ludov2-group-9-linux-ludo\src\linuxludo.web\Shared\MainLayout.razor"
         Body

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.CloseElement();
        }
        #pragma warning restore 1998
    }
}
#pragma warning restore 1591
