using System.IO;
using System.Text;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;

using SplatDev.Plugins.BackupVault.Services;

namespace PrismDrive.Tests
{
    [TestClass]
    public class PrismDrive_Tests
    {
        [TestMethod]
        public void PrismDrive_Tests__Login()
        {
            var service = new PrismDriveService();
            var response = PrismDriveService.LoginAsync().Result;
            Assert.IsNotNull(response.AccessToken);
        }

        [TestMethod]
        public void PrismDrive_Tests__Upload()
        {
            var stream = new MemoryStream(Encoding.UTF8.GetBytes("This is a test"));
            var file = new FormFile(stream, 0, stream.Length, "Test", "test.txt");

            var service = new PrismDriveService();
            var login = PrismDriveService.LoginAsync().Result;
            var response = PrismDriveService.Upload(file, login.AccessToken);
            Assert.IsNotNull(response);
        }

        [TestMethod]
        public void PrismDrive_Tests__UploadAsync()
        {
            var stream = new MemoryStream(Encoding.UTF8.GetBytes("This is a test"));
            var file = new FormFile(stream, 0, stream.Length, "Test", "test.txt");

            var service = new PrismDriveService();
            var login = PrismDriveService.LoginAsync().Result;
            var response = PrismDriveService.UploadAsync(file, login.AccessToken).Result;
            Assert.IsNotNull(response);
        }
    }
}