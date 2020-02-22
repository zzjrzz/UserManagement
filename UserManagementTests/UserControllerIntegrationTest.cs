using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using UserManagement;
using Xunit;

namespace UserManagementTests
{
    public class UserControllerIntegrationTest: IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _webApplicationFactory;

        public UserControllerIntegrationTest(WebApplicationFactory<Startup> webApplicationFactory)
        {
            _webApplicationFactory = webApplicationFactory;
        }

        [Fact]
        public async Task Get_User_Is_Successful()
        {
            var client = _webApplicationFactory.CreateClient();
            
            var response = await client.GetAsync("/api/user");
            
            response.EnsureSuccessStatusCode();
            Assert.Equal("text/html; charset=utf-8", 
                response.Content.Headers.ContentType.ToString());
        }
    }
}