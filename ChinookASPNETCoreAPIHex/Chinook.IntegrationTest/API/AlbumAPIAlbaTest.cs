using System.Collections.Generic;
using System.Threading.Tasks;
using Alba;
using Chinook.API;
using Chinook.Domain.ViewModels;
using Xunit;

namespace Chinook.IntegrationTest.API
{
    public class AlbumAPIAlbaTest
    {
        [Fact]
        public Task should_get_list_of_albums()
        {
            using (var system = SystemUnderTest.ForStartup<Startup>())
            {
                // This runs an HTTP request and makes an assertion
                // about the expected content of the response
                return system.Scenario(_ =>
                {
                    _.Get.Url("/api/Album");
                    _.StatusCodeShouldBeOk();
                });
            }
        }
        
        [Fact]
        public Task should_get_single_album()
        {
            using (var system = SystemUnderTest.ForStartup<Startup>())
            {
                // This runs an HTTP request and makes an assertion
                // about the expected content of the response
                return system.Scenario(_ =>
                {
                    _.Get.Url("/api/Album/4");
                    _.StatusCodeShouldBeOk();
                });
            }
        }
    }
}