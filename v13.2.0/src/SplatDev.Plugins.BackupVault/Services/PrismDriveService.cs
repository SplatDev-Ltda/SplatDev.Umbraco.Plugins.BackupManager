using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Http;

using Newtonsoft.Json;

using RestSharp;
using RestSharp.Authenticators.OAuth2;

using SplatDev.Plugins.BackupVault.Models;

namespace SplatDev.Plugins.BackupVault.Services
{
    public class PrismDriveService
    {
        private const string BASE_URL = "https://app.prismdrive.com/api/v1/";
        public async Task<PrismDriveLoginViewModel> LoginAsync()
        {
            var client = new RestClient(BASE_URL);
            var request = new RestRequest("auth/login", Method.Post);
            request.AddBody(JsonConvert.SerializeObject(new PrismDriveLoginRequest
            {
                DeviceName = "iphone 12",
                Email = "carlos.casalicchio@gmail.com",
                Password = "xxxxx",
                TokenName = "BackupVault"
            }), ContentType.Json);
            var response = await client.PostAsync(request);
            var responseContent = JsonConvert.DeserializeObject<PrismDriveLoginResponse>(response.Content);
            return new PrismDriveLoginViewModel
            {
                AccessToken = responseContent.User.AccessToken,
                Status = responseContent.Status
            };
        }

        public async Task<RestResponse?> UploadAsync(IFormFile file, string token)
        {
            if (file.Length == 0) return new RestResponse { ErrorMessage = "missing file" };
            if (string.IsNullOrEmpty(token)) return new RestResponse { ErrorMessage = "missing token" };

            using var ms = new MemoryStream();
            file.CopyTo(ms);
            var fileBytes = ms.ToArray();

            var options = new RestClientOptions(BASE_URL)
            {
                Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(token, "Bearer"),
            };
            var client = new RestClient(options);
            client.AddDefaultHeader("Accept", "application/json");
            client.AddDefaultHeader("Content-Type", "multipart/form-data");
            var restSharpRequest = new RestRequest("uploads");
            restSharpRequest.AddBody(new PrismDriveFileUploadRequest
            {
                File = Convert.ToBase64String(fileBytes),
                ParentId = null,
                RelativePath = file.FileName
            });
            RestResponse? response = null;
            try
            {
                response = await client.PostAsync(restSharpRequest);
            }
            catch (Exception ex)
            {
                _ = ex;
                throw;
            }
            return response;
        }
    }
}
