using Newtonsoft.Json;

namespace SplatDev.Plugins.BackupVault.Models
{
    public class PrismDriveLoginResponse
    {
        [JsonProperty("themes")]
        public Themes Themes { get; set; } = new();
        [JsonProperty("user")]
        public User User { get; set; } = new();
        [JsonProperty("menus")]
        public object[]? Menus { get; set; }
        [JsonProperty("settings")]
        public Settings? Settings { get; set; }
        [JsonProperty("locales")]
        public Locale[]? Locales { get; set; }
        [JsonProperty("status")]
        public string Status { get; set; } = string.Empty;

    }

    public class Themes
    {
        [JsonProperty("light")]
        public Theme? Light { get; set; }
        [JsonProperty("dark")]
        public Theme? Dark { get; set; }
    }

    public class Theme
    {
        [JsonProperty("Id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; } = string.Empty;
        [JsonProperty("is_dark")]
        public bool IsDark { get; set; }
        [JsonProperty("default_light")]
        public bool DefaultLight { get; set; }
        [JsonProperty("default_dark")]
        public bool DefaultDark { get; set; }
        [JsonProperty("user_id")]
        public int UserId { get; set; }
        [JsonProperty("colors")]
        public Colors? Colors { get; set; }
        [JsonProperty("created_ad")]
        public DateTime CreatedAt { get; set; }
        [JsonProperty("updated_at")]
        public DateTime? UpdatedAt { get; set; }
    }

    public class Colors
    {
        [JsonProperty("beforegroundbase")]
        public int[]? BeForegroundBase { get; set; }
        [JsonProperty("beprimarylight")]
        public int[]? BePrimaryLight { get; set; }
        [JsonProperty("beprimary")]
        public int[]? BePrimary { get; set; }
        [JsonProperty("beprimarydark")]
        public int[]? BePrimaryDark { get; set; }
        [JsonProperty("beonprimary")]
        public int[]? BeOnPrimary { get; set; }
        [JsonProperty("bebackground")]
        public int[]? BeBackground { get; set; }
        [JsonProperty("bebackgroundalt")]
        public int[]? BeBackgroundAlt { get; set; }
        [JsonProperty("bebackgroundchip")]
        public int[]? BeBackgroundChip { get; set; }
        [JsonProperty("bepaper")]
        public int[]? BePaper { get; set; }
        [JsonProperty("bedisabledbgopacity")]
        public int BeDisabledBgOpacity { get; set; }
        [JsonProperty("bedisabledfgopacity")]
        public int BeDisabledFgOpacity { get; set; }
        [JsonProperty("behoveropacity")]
        public int BeHoverOpacity { get; set; }
        [JsonProperty("befocusopacity")]
        public int BeFocusOpacity { get; set; }
        [JsonProperty("beselectedopacity")]
        public int BeSelectedOpacity { get; set; }
        [JsonProperty("betextmainopacity")]
        public int BeTextMainOpacity { get; set; }
        [JsonProperty("betextmutedopacity")]
        public int BeTextMutedOpacity { get; set; }
        [JsonProperty("bedivideropacity")]
        public int BeDividerOpacity { get; set; }
    }

    public class User
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("username")]
        public string Username { get; set; } = string.Empty;
        [JsonProperty("email")]
        public string Email { get; set; } = string.Empty;
        [JsonProperty("two_factor_secret")]
        public object? TwoFactorSecret { get; set; }
        [JsonProperty("two_factor_recovery_codes")]
        public object? TwoFactorRecoveryCodes { get; set; }
        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }
        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }
        [JsonProperty("space_available")]
        public object? SpaceAvailable { get; set; }
        [JsonProperty("language")]
        public string Language { get; set; } = string.Empty;
        [JsonProperty("country")]
        public string Country { get; set; } = string.Empty;
        [JsonProperty("timezone")]
        public string Timezone { get; set; } = string.Empty;
        [JsonProperty("avatar")]
        public string Avatar { get; set; } = string.Empty;
        [JsonProperty("stripe_id")]
        public object? StripeId { get; set; }
        [JsonProperty("available_space")]
        public object? AvailableSpace { get; set; }
        [JsonProperty("first_name")]
        public string FirstName { get; set; } = string.Empty;
        [JsonProperty("last_name")]
        public string LastName { get; set; } = string.Empty;
        [JsonProperty("card_brand")]
        public object? CardBrand { get; set; }
        [JsonProperty("card_last_four")]
        public object? CardLastFour { get; set; }
        [JsonProperty("email_verified_at")]
        public DateTime EmailVerifiedAt { get; set; }
        [JsonProperty("card_expires")]
        public object? CardExpires { get; set; }
        [JsonProperty("paypal_id")]
        public object? PaypalId { get; set; }
        [JsonProperty("access_token")]
        public string AccessToken { get; set; } = string.Empty;
        [JsonProperty("fcm_token")]
        public object? FcmToken { get; set; }
        [JsonProperty("display_name")]
        public string DisplayName { get; set; } = string.Empty;
        [JsonProperty("has_password")]
        public bool HasPassword { get; set; }
        [JsonProperty("model_type")]
        public string ModelType { get; set; } = string.Empty;
    }

    public class Settings
    {
        [JsonProperty("socialgoogleenable")]
        public bool SocialGoogleEnable { get; set; }
    }

    public class Locale
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; } = string.Empty;
        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }
        [JsonProperty("updated_at")]
        public DateTime? UpdatedAt { get; set; }
        [JsonProperty("language")]
        public string Language { get; set; } = string.Empty;
    }

}
