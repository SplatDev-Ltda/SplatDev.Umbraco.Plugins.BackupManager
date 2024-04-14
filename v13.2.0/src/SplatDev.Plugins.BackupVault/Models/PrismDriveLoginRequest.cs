using Newtonsoft.Json;

namespace SplatDev.Plugins.BackupVault.Models
{
    public class PrismDriveLoginRequest
    {
        [JsonProperty("email")]
        public string Email { get; set; } = string.Empty;
        [JsonProperty("password")]
        public string Password { get; set; } = string.Empty;
        [JsonProperty("device_name")]
        public string DeviceName { get; set; } = string.Empty;
        [JsonProperty("token_name")]
        public string TokenName { get; set; } = string.Empty;

    }
}
