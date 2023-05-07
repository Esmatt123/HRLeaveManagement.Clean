using Blazored.LocalStorage;
using Microsoft.IdentityModel.Tokens;
using System.Net.Http.Headers;

/*
//--- This code defines a custom message handler, JwtAuthorizationMessageHandler, that extends the DelegatingHandler class. 
The purpose of this handler is to add an authorization header to outgoing HTTP requests using a JWT (JSON Web Token) retrieved 
from local storage. In summary, this message handler adds the JWT token as an authorization header to outgoing HTTP requests, 
allowing the server to authenticate and authorize the requests based on the provided token. ---//

- The JwtAuthorizationMessageHandler class constructor takes an ILocalStorageService parameter, which is used to access the local storage where the JWT token is stored.

- The SendAsync method is overridden from the base DelegatingHandler class. It intercepts outgoing HTTP requests and modifies them before they are sent.

- Inside the SendAsync method:
It retrieves the JWT token from the local storage using the GetItemAsync method of the ILocalStorageService.
If a valid token is found (not null or empty), it sets the Authorization header of the HttpRequestMessage to 
include the token using the AuthenticationHeaderValue class with the scheme "Bearer".
The modified request is then passed to the base SendAsync method to continue the request processing.

- `Bearer` is an authentication scheme used in HTTP requests. It involves including an access token in the "Authorization" header. 
The token, usually a JWT, represents the user's authentication and authorization information. 
The "Bearer" keyword precedes the token in the header. The server validates the token to authenticate and authorize the request. 
Bearer tokens are commonly used in OAuth 2.0 and provide a scalable way to authenticate API requests.

- `JWT (JSON Web Token)` is a compact and self-contained method for securely transmitting information as a JSON object. 
It consists of three parts: a header, a payload, and a signature. JWTs are commonly used for authentication and authorization 
in web applications without the need for server-side session storage.


- `A delegating handler` is a class in ASP.NET Web API or ASP.NET Core that can intercept HTTP requests and responses. 
It sits in the request processing pipeline and can perform additional processing before or after the actual request 
is handled by the application. Delegating handlers can be used for various purposes such as authentication, authorization,
logging, caching, and modifying requests or responses. They allow developers to extend or customize the behavior of the HTTP
processing pipeline.

- `ILocalStorageService` is an interface that represents a service for accessing and manipulating data stored in the local 
storage of a web browser. It provides methods to store, retrieve, update, and delete data in the local storage, 
which is a client-side storage mechanism available in web browsers. The ILocalStorageService abstracts the underlying 
implementation details and provides a consistent API for working with local storage across different browsers. 
It allows web applications to persist data on the client-side and retrieve it later, even when the user navigates 
away from the application or closes the browser.


*/

namespace HR.LeaveManagement.BlazorUI.Handlers
{
    public class JwtAuthorizationMessageHandler : DelegatingHandler
    {
        private readonly ILocalStorageService _localStorageService;

        public JwtAuthorizationMessageHandler(ILocalStorageService localStorageService) 
        {
            _localStorageService = localStorageService;
        }

        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var token = await _localStorageService.GetItemAsync<string>("token");
            if (!string.IsNullOrEmpty(token)) 
            {
                request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            }
            return await base.SendAsync(request, cancellationToken);
        }
    }
}
