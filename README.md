# Auth0 (or any OIDC) with Blazor WASM standalone

## Setup
Blazor WASM is different from the server project. There's no Auth0 library for WASM,
so the generic OIDC library (`Microsoft.AspNetCore.Components.WebAssembly.Authentication`)
is used instead.

**Steps**:
1. Add a Nuget package `Microsoft.AspNetCore.Components.WebAssembly.Authentication` into the project.
   * The package is actually for OIDC/OAuth2 authentication, this case we use Auth0
     as an OIDC provider.
2. Set up OIDC authentication with `AddOidcAuthentication` and set `ProviderOptions`.
   See `Program.cs` for example.
   * Two important properties are `Authority` and `ClientId`. See `appsettings.json`.
   * Note that `Authority` is an absolute URL, not just a domain name.
3. Add an authentication helper page `/authentication/{action}`, see `Authentication.razor`.
   * This is the single point to handle all authentication activities. To login, pass `login`
     as the action. To logout, pass `logout` as the action. It's also the endpoint for callback too
     (`/authentication/login-callback`).
4. Add namespace `Microsoft.AspNetCore.Authorization` and `Microsoft.AspNetCore.Components.Authorization` in
   `_Imports.razor`
5. Wrap `Route` component in `App.razor` with `CascadingAuthenticaitonState` component
   (from `Microsoft.AspNetCore.Authorization`) to allow underlying compoonents to retrieve
   authentication state.
   * Also change `RouteView` component to `AuthorizeRouteView` which allows handling
     authorizing state and not-authorized state.

## Protect Blazor component 
Blazor framework has `AuthorizeView` component to define views for authorized user and 
unauthorized user. See the usage in `Index.razor`.

## To Login/Logout with Auth0
Since this example is configured for Authorization Code Flow. Some pre-configuration needs
to be done on Auth0 side (setting Redirect URIs and Logout URIs -- see the original document
for guidance). However, since WASM is client side, so the authentication is PKCE flow, which
is handled by the library. To login/logout, use extension methods `NavigateToLogin` and
`NavigateToLogout`, see `Index.razor` about how to login/logout using such methods.

# See also
* [Blazor Server with Auth0 abridge version](https://github.com/ruxo/blazor-server-auth0)