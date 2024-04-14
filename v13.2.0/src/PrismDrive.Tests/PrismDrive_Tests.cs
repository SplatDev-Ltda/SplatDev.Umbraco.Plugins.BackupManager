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
            var response = service.LoginAsync().Result;
            Assert.IsNotNull(response.AccessToken);
        }

        [TestMethod]
        public void PrismDrive_Tests__Upload()
        {
            var stream = new MemoryStream(Encoding.UTF8.GetBytes("This is a test"));
            var file = new FormFile(stream, 0, stream.Length, "Test", "test.txt");

            var service = new PrismDriveService();
            var login = service.LoginAsync().Result;
            var response = service.UploadAsync(file, login.AccessToken).Result;
            Assert.IsNotNull(response);
        }
    }
}