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

        [Fact]
        public async Task Get_User_Is_Successful()
        {
            var client = _webApplicationFactory.CreateClient();
            
            var response = await client.GetAsync("/api/user");
            
            response.EnsureSuccessStatusCode();
            Assert.Equal("application/json; charset=utf-8", 
                response.Content.Headers.ContentType.ToString());
        }

        [Fact]
        public async Task Get_User_By_Email_Is_Successful()
        {
            var client = _webApplicationFactory.CreateClient();

            var response = await client.GetAsync("/api/user/jeremy@example.com");

            response.EnsureSuccessStatusCode();

            Assert.Equal("application/json; charset=utf-8", 
                response.Content.Headers.ContentType.ToString());        }
    }
}