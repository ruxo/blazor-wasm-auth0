# Auth0 (or any OIDC) with Blazor WASM standalone

Blazor WASM is different from the server project. There's no Auth0 library for WASM,
so the generic OIDC library (`Microsoft.AspNetCore.Components.WebAssembly.Authentication`)
is used instead.

**Steps**:
1. 