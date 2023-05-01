using Blazored.LocalStorage;
using System.Net.Http.Headers;

namespace HR.LeaveManagement.BlazorUI.Services.Base
{
    public class BaseHttpService
    {
        protected IClient _client;
        protected readonly ILocalStorageService _localStorage;

        public BaseHttpService(IClient client, ILocalStorageService localStorage)
        {
            _client = client;
            _localStorage = localStorage;

            Console.WriteLine("BaseHttpService initialized"); // add this line
        }

        protected Response<Guid> ConvertApiExceptions<Guid>(ApiException ex)
        {
            Console.WriteLine("ConvertApiExceptions method called"); // add this line

            if (ex.StatusCode == 400)
            {
                Console.WriteLine("400 error: Invalid data was submitted"); // add this line
                return new Response<Guid>() { Message = "Invalid data was submitted", ValidationErrors = ex.Response, Success = false };
            }
            else if (ex.StatusCode == 404)
            {
                Console.WriteLine("404 error: The record was not found."); // add this line
                return new Response<Guid>() { Message = "The record was not found.", Success = false };
            }
            else
            {
                Console.WriteLine("Unknown error occurred"); // add this line
                return new Response<Guid>() { Message = "Something went wrong, please try again later.", Success = false };
            }
        }

        protected async Task AddBearerToken()
        {
            Console.WriteLine("AddBearerToken method called"); // add this line

            if (await _localStorage.ContainKeyAsync("token"))
            {
                Console.WriteLine("Token found in local storage"); // add this line
                _client.HttpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", await _localStorage.GetItemAsync<string>("token"));
            }
            else
            {
                Console.WriteLine("Token not found in local storage"); // add this line
            }
        }

    }
}