using Blazored.LocalStorage;
using HR.LeaveManagement.BlazorUI.Contracts;
using HR.LeaveManagement.BlazorUI.Providers;
using HR.LeaveManagement.BlazorUI.Services.Base;
using Microsoft.AspNetCore.Components.Authorization;
using System;

/*
This code defines a service called AuthenticationService that is responsible for handling user authentication 
and registration in a Blazor application.

The service has a dependency on an AuthenticationStateProvider, which is used to manage the 
authentication state of the application.

The AuthenticateAsync method is used to authenticate a user by sending a login request to the server 
with the provided email and password. If the authentication is successful, the received authentication 
token is saved in local storage, and the user's login state is set in the Blazor application.

The Logout method is used to log out the user by removing the user's claims in the Blazor application 
and invalidating the login state.

The RegisterAsync method is used to register a new user by sending a registration request to the server 
with the provided user details. If the registration is successful, it returns true; otherwise, it returns false.

- IAuthenticationService is an interface that defines a contract for an authentication service in a Blazor application. 
It specifies the methods and operations that the authentication service should implement.
 */

namespace HR.LeaveManagement.BlazorUI.Services
{
    public class AuthenticationService : BaseHttpService, IAuthenticationService
    {
        private readonly AuthenticationStateProvider _authenticationStateProvider;

        public AuthenticationService(IClient client,
            ILocalStorageService localStorage,
            AuthenticationStateProvider authenticationStateProvider) : base(client, localStorage)
        {
            _authenticationStateProvider = authenticationStateProvider;
        }

        public async Task<bool> AuthenticateAsync(string email, string password)
        {
            try
            {
                Console.WriteLine("Authenticating user with email: " + email);

                AuthRequest authenticationRequest = new AuthRequest() { Email = email, Password = password };
                var authenticationResponse = await _client.LoginAsync(authenticationRequest);
                if (authenticationResponse.Token != string.Empty)
                {
                    await _localStorage.SetItemAsync("token", authenticationResponse.Token);

                    // Set claims in Blazor and login state
                    await ((ApiAuthenticationStateProvider)_authenticationStateProvider).LoggedIn();

                    var token = await _localStorage.GetItemAsync<string>("token");
                    Console.WriteLine("Token saved in local storage: " + token);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception occurred in AuthenticateAsync: {0}", ex);
                return false;
            }
        }

        public async Task Logout()
        {
            Console.WriteLine("Logging out user");

            // remove claims in Blazor and invalidate login state
            await ((ApiAuthenticationStateProvider)_authenticationStateProvider).LoggedOut();
        }

        public async Task<bool> RegisterAsync(string firstName, string lastName, string userName, string email, string password)
        {
            Console.WriteLine("Registering user with email: " + email);

            RegistrationRequest registrationRequest = new RegistrationRequest() { FirstName = firstName, LastName = lastName, Email = email, UserName = userName, Password = password };
            var response = await _client.RegisterAsync(registrationRequest);

            if (!string.IsNullOrEmpty(response.UserId))
            {
                return true;
            }
            return false;
        }
    }
}