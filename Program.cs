using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Blazor.Example;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

var baseAddress = builder.HostEnvironment.BaseAddress;

builder.Services.AddOidcAuthentication(opts => {
    var po = opts.ProviderOptions;
    var c = builder.Configuration;
    po.Authority = c["Auth0:Authority"];
    po.ClientId = c["Auth0:ClientId"];
    po.ResponseType = "code";
    po.AdditionalProviderParameters["audience"] = c["Auth0:Audience"];
});

builder.Services.AddScoped(_ => new HttpClient{ BaseAddress = new(baseAddress) });

await builder.Build().RunAsync();