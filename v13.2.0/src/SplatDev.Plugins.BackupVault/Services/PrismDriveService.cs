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
        private const string LOGIN_PATH = "auth/login";
        private const string UPLOAD_PATH = "uploads";
        private const string BEARER = "Bearer";
        private const string MISSING_FILE = "missing file";
        private const string MISSING_TOKEN = "missing token";
        private const string DEFAULT_CONTENT_TYPE = "application/octet-stream";
        private const string CLIENT_NAME = "clientName";
        private const string CLIENT_EXTENSION = "clientExtension";
        private const string CLIENT_MIME = "clientMime";
        private const string SIZE = "size";

        public static async Task<PrismDriveLoginViewModel> LoginAsync(string username, string password, string tokenName = "BackupVault", string deviceName = "iphone 12")
        {
            var client = new RestClient(BASE_URL);
            var request = new RestRequest(LOGIN_PATH, Method.Post);
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
            if (file.Length == 0) return new RestResponse { ErrorMessage = MISSING_FILE };
            if (string.IsNullOrEmpty(token)) return new RestResponse { ErrorMessage = MISSING_TOKEN };

            var fileStream = new MemoryStream();
            file.CopyTo(fileStream);
            var bytes = fileStream.ToArray();
            new FileExtensionContentTypeProvider().TryGetContentType(file.FileName, out string contentType);
            contentType ??= DEFAULT_CONTENT_TYPE;
            string extension = new FileInfo(file.FileName).Extension[..1];
            var options = new RestClientOptions(BASE_URL)
            {
                MaxTimeout = -1,
            };
            var client = new RestClient(options);
            var request = new RestRequest(UPLOAD_PATH, Method.Post)
            {
                Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(token, BEARER),
                AlwaysMultipartFormData = true
            };
            request.AddFile("file", bytes, file.FileName);
            request.AddParameter(CLIENT_NAME, $"[\"{clientName}\"]");
            request.AddParameter(CLIENT_MIME, $"[\"{contentType}\"]");
            request.AddParameter(CLIENT_EXTENSION, $"[\"{extension}\"]");
            request.AddParameter(SIZE, $"[{fileStream.Length}]");
            RestResponse response = await client.ExecuteAsync(request);
            return response;
        }

        public static string Upload(IFormFile file, string token, string clientName = "browser")
        {
            if (file.Length == 0) return MISSING_FILE;
            if (string.IsNullOrEmpty(token)) return MISSING_TOKEN;

            new FileExtensionContentTypeProvider().TryGetContentType(file.FileName, out string contentType);
            contentType ??= DEFAULT_CONTENT_TYPE;
            string extension = new FileInfo(file.FileName).Extension[..1];

            var fileStream = new MemoryStream();
            file.CopyTo(fileStream);
            var client = new HttpClient();
            var request = new HttpRequestMessage(HttpMethod.Post, BASE_URL + UPLOAD_PATH);
            request.Headers.Authorization = new AuthenticationHeaderValue(BEARER, token);
            var content = new MultipartFormDataContent
            {
                { new StreamContent(fileStream), "file", file.FileName },
                { new StringContent($"[\"{clientName}\"]"), CLIENT_NAME },
                { new StringContent($"[\"{contentType}\"]"), CLIENT_MIME },
                { new StringContent($"[\"{extension}\"]"), CLIENT_EXTENSION },
                { new StringContent($"[{file.Length}]"), SIZE }
            };
            request.Content = content;
            var response = client.Send(request);
            response.EnsureSuccessStatusCode();
            return response.Content.ReadAsStringAsync().Result;
        }
    }
}