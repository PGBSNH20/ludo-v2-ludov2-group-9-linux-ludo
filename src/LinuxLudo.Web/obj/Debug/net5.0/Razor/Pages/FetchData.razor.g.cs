#pragma checksum "/home/leo/Projects/school/ludo-v2-ludov2-group-9-linux-ludo/src/LinuxLudo.Web/Pages/FetchData.razor" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "c93c1b02661f928d82b8558c4ceae4df18718f4f"
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
#line 1 "/home/leo/Projects/school/ludo-v2-ludov2-group-9-linux-ludo/src/LinuxLudo.Web/_Imports.razor"
using System.Net.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "/home/leo/Projects/school/ludo-v2-ludov2-group-9-linux-ludo/src/LinuxLudo.Web/_Imports.razor"
using System.Net.Http.Json;

#line default
#line hidden
#nullable disable
#nullable restore
#line 3 "/home/leo/Projects/school/ludo-v2-ludov2-group-9-linux-ludo/src/LinuxLudo.Web/_Imports.razor"
using Microsoft.AspNetCore.Components.Forms;

#line default
#line hidden
#nullable disable
#nullable restore
#line 4 "/home/leo/Projects/school/ludo-v2-ludov2-group-9-linux-ludo/src/LinuxLudo.Web/_Imports.razor"
using Microsoft.AspNetCore.Components.Routing;

#line default
#line hidden
#nullable disable
#nullable restore
#line 5 "/home/leo/Projects/school/ludo-v2-ludov2-group-9-linux-ludo/src/LinuxLudo.Web/_Imports.razor"
using Microsoft.AspNetCore.Components.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "/home/leo/Projects/school/ludo-v2-ludov2-group-9-linux-ludo/src/LinuxLudo.Web/_Imports.razor"
using Microsoft.AspNetCore.Components.Web.Virtualization;

#line default
#line hidden
#nullable disable
#nullable restore
#line 7 "/home/leo/Projects/school/ludo-v2-ludov2-group-9-linux-ludo/src/LinuxLudo.Web/_Imports.razor"
using Microsoft.AspNetCore.Components.WebAssembly.Http;

#line default
#line hidden
#nullable disable
#nullable restore
#line 8 "/home/leo/Projects/school/ludo-v2-ludov2-group-9-linux-ludo/src/LinuxLudo.Web/_Imports.razor"
using Microsoft.JSInterop;

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "/home/leo/Projects/school/ludo-v2-ludov2-group-9-linux-ludo/src/LinuxLudo.Web/_Imports.razor"
using LinuxLudo.Web;

#line default
#line hidden
#nullable disable
#nullable restore
#line 10 "/home/leo/Projects/school/ludo-v2-ludov2-group-9-linux-ludo/src/LinuxLudo.Web/_Imports.razor"
using LinuxLudo.Web.Shared;

#line default
#line hidden
#nullable disable
    [Microsoft.AspNetCore.Components.RouteAttribute("/fetchdata")]
    public partial class FetchData : Microsoft.AspNetCore.Components.ComponentBase
    {
        #pragma warning disable 1998
        protected override void BuildRenderTree(Microsoft.AspNetCore.Components.Rendering.RenderTreeBuilder __builder)
        {
            __builder.AddMarkupContent(0, "<h1>Weather forecast</h1>\n\n");
            __builder.AddMarkupContent(1, "<p>This component demonstrates fetching data from the server.</p>");
#nullable restore
#line 8 "/home/leo/Projects/school/ludo-v2-ludov2-group-9-linux-ludo/src/LinuxLudo.Web/Pages/FetchData.razor"
 if (forecasts == null)
{

#line default
#line hidden
#nullable disable
            __builder.AddMarkupContent(2, "<p><em>Loading...</em></p>");
#nullable restore
#line 13 "/home/leo/Projects/school/ludo-v2-ludov2-group-9-linux-ludo/src/LinuxLudo.Web/Pages/FetchData.razor"
}
else
{

#line default
#line hidden
#nullable disable
            __builder.OpenElement(3, "table");
            __builder.AddAttribute(4, "class", "table");
            __builder.AddMarkupContent(5, "<thead><tr><th>Date</th>\n            <th>Temp. (C)</th>\n            <th>Temp. (F)</th>\n            <th>Summary</th></tr></thead>\n        ");
            __builder.OpenElement(6, "tbody");
#nullable restore
#line 26 "/home/leo/Projects/school/ludo-v2-ludov2-group-9-linux-ludo/src/LinuxLudo.Web/Pages/FetchData.razor"
         foreach (var forecast in forecasts)
        {

#line default
#line hidden
#nullable disable
            __builder.OpenElement(7, "tr");
            __builder.OpenElement(8, "td");
            __builder.AddContent(9, 
#nullable restore
#line 29 "/home/leo/Projects/school/ludo-v2-ludov2-group-9-linux-ludo/src/LinuxLudo.Web/Pages/FetchData.razor"
                     forecast.Date.ToShortDateString()

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.AddMarkupContent(10, "\n                ");
            __builder.OpenElement(11, "td");
            __builder.AddContent(12, 
#nullable restore
#line 30 "/home/leo/Projects/school/ludo-v2-ludov2-group-9-linux-ludo/src/LinuxLudo.Web/Pages/FetchData.razor"
                     forecast.TemperatureC

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.AddMarkupContent(13, "\n                ");
            __builder.OpenElement(14, "td");
            __builder.AddContent(15, 
#nullable restore
#line 31 "/home/leo/Projects/school/ludo-v2-ludov2-group-9-linux-ludo/src/LinuxLudo.Web/Pages/FetchData.razor"
                     forecast.TemperatureF

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.AddMarkupContent(16, "\n                ");
            __builder.OpenElement(17, "td");
            __builder.AddContent(18, 
#nullable restore
#line 32 "/home/leo/Projects/school/ludo-v2-ludov2-group-9-linux-ludo/src/LinuxLudo.Web/Pages/FetchData.razor"
                     forecast.Summary

#line default
#line hidden
#nullable disable
            );
            __builder.CloseElement();
            __builder.CloseElement();
#nullable restore
#line 34 "/home/leo/Projects/school/ludo-v2-ludov2-group-9-linux-ludo/src/LinuxLudo.Web/Pages/FetchData.razor"
        }

#line default
#line hidden
#nullable disable
            __builder.CloseElement();
            __builder.CloseElement();
#nullable restore
#line 37 "/home/leo/Projects/school/ludo-v2-ludov2-group-9-linux-ludo/src/LinuxLudo.Web/Pages/FetchData.razor"
}

#line default
#line hidden
#nullable disable
        }
        #pragma warning restore 1998
#nullable restore
#line 39 "/home/leo/Projects/school/ludo-v2-ludov2-group-9-linux-ludo/src/LinuxLudo.Web/Pages/FetchData.razor"
       
    private WeatherForecast[] forecasts;

    protected override async Task OnInitializedAsync()
    {
        forecasts = await Http.GetFromJsonAsync<WeatherForecast[]>("sample-data/weather.json");
    }

    public class WeatherForecast
    {
        public DateTime Date { get; set; }

        public int TemperatureC { get; set; }

        public string Summary { get; set; }

        public int TemperatureF => 32 + (int) (TemperatureC / 0.5556);
    }


#line default
#line hidden
#nullable disable
        [global::Microsoft.AspNetCore.Components.InjectAttribute] private HttpClient Http { get; set; }
    }
}
#pragma warning restore 1591
