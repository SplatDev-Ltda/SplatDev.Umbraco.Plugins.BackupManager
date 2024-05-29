using System.Net.Http.Headers;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.StaticFiles;

using Newtonsoft.Json;

using RestSharp;
using RestSharp.Authenticators.OAuth2;

using SplatDev.Plugins.BackupVault.Models;

namespace SplatDev.Plugins.BackupVault.Services
{
    public class PrismDriveService
    {
        private const string BASE_URL = "https://app.prismdrive.com/api/v1/";
        public static async Task<PrismDriveLoginViewModel> LoginAsync(string username, string password, string tokenName = "BackupVault", string deviceName = "iphone 12")
        {
            var client = new RestClient(BASE_URL);
            var request = new RestRequest("auth/login", Method.Post);
            request.AddBody(JsonConvert.SerializeObject(new PrismDriveLoginRequest
            {
                DeviceName = deviceName,
                Email = username,
                Password = password,
                TokenName = tokenName
            }), ContentType.Json);
            var response = await client.PostAsync(request);
            var responseContent = JsonConvert.DeserializeObject<PrismDriveLoginResponse>(response.Content!);
            return new PrismDriveLoginViewModel
            {
                AccessToken = responseContent!.User.AccessToken,
                Status = responseContent.Status
            };
        }

        public static async Task<RestResponse?> UploadAsync(IFormFile file, string token, string clientName = "browser")
        {
            if (file.Length == 0) return new RestResponse { ErrorMessage = "missing file" };
            if (string.IsNullOrEmpty(token)) return new RestResponse { ErrorMessage = "missing token" };

            var fileStream = new MemoryStream();
            file.CopyTo(fileStream);
            var bytes = fileStream.ToArray();
            new FileExtensionContentTypeProvider().TryGetContentType(file.FileName, out string contentType);
            contentType ??= "application/octet-stream";
            string extension = new FileInfo(file.FileName).Extension[..1];
            var options = new RestClientOptions(BASE_URL)
            {
                MaxTimeout = -1,
            };
            var client = new RestClient(options);
            var request = new RestRequest("uploads", Method.Post)
            {
                Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(token, "Bearer"),
                AlwaysMultipartFormData = true
            };
            request.AddFile("file", bytes, file.FileName);
            request.AddParameter("clientName", $"[\"{clientName}\"]");
            request.AddParameter("clientMime", $"[\"{contentType}\"]");
            request.AddParameter("clientExtension", $"[\"{extension}\"]");
            request.AddParameter("size", $"[{fileStream.Length}]");
            RestResponse response = await client.ExecuteAsync(request);
            return response;
        }

        public static string Upload(IFormFile file, string token, string clientName = "browser")
        {
            if (file.Length == 0) return "missing file";
            if (string.IsNullOrEmpty(token)) return "missing token";

            new FileExtensionContentTypeProvider().TryGetContentType(file.FileName, out string contentType);
            contentType ??= "application/octet-stream";
            string extension = new FileInfo(file.FileName).Extension[..1];

            var fileStream = new MemoryStream();
            file.CopyTo(fileStream);
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, BASE_URL + "uploads");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var content = new MultipartFormDataContent
            {
                { new StreamContent(fileStream), "file", file.FileName },
                { new StringContent($"[\"{clientName}\"]"), "clientName" },
                { new StringContent($"[\"{contentType}\"]"), "clientMime" },
                { new StringContent($"[\"{extension}\"]"), "clientExtension" },
                { new StringContent($"[{file.Length}]"), "size" }
            };
            request.Content = content;
            var response = client.Send(request);
            response.EnsureSuccessStatusCode();
            return response.Content.ReadAsStringAsync().Result;
        }
    }
}