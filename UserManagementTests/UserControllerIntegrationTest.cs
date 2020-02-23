using System.Threading.Tasks;
using UserManagement;
using Xunit;

namespace UserManagementTests
{
    public class UserControllerIntegrationTest: IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _webApplicationFactory;

        public UserControllerIntegrationTest(CustomWebApplicationFactory<Startup> webApplicationFactory)
        {
            _webApplicationFactory = webApplicationFactory;
        }

        [Theory]
        [InlineData("/api/user")]
        [InlineData("/api/user/jeremy@example.com")]
        public async Task Get_User_Endpoints_Are_Successful(string requestUri)
        {
            var client = _webApplicationFactory.CreateClient();
            
            var response = await client.GetAsync(requestUri);
            
            response.EnsureSuccessStatusCode();
            Assert.Equal("application/json; charset=utf-8", 
                response.Content.Headers.ContentType.ToString());
        }

    }
}