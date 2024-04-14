namespace SplatDev.Plugins.BackupVault.Models
{
    public class PrismDriveFileUploadRequest
    {
        public string File { get; set; } = string.Empty;
        public int? ParentId { get; set; }
        public string RelativePath { get; set; } = string.Empty;
    }
}
