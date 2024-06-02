using Bissell.Services.DataTransferObjects;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Text;
using System.Text.Json;

namespace Testing.IntegrationTests
{
    public class BugTests : IClassFixture<WebApplicationFactory<Program>>
    {
        protected WebApplicationFactory<Program> ProgramFactory { get; set; }

        protected HttpClient HttpClient { get; set; }

        protected BugDto BugDto { get; set; }

        public BugTests(WebApplicationFactory<Program> programFactory)
        {
            ProgramFactory = programFactory;
            HttpClient = ProgramFactory.CreateClient();

            BugDto = new BugDto()
            {
                Title = "Node.js doesn't load",
                Description = "Can't get Node.js to run",
                Priority = Bissell.Core.Models.BugPriority.High,
                Status = Bissell.Core.Models.BugStatus.NotStarted
            };
        }

        /// <summary>Sample Integration Test that goes end to end from the API to the Database.</summary>
        [Fact]
        public async Task SampleIntegrationTest()
        {
            string serialize = JsonSerializer.Serialize(BugDto);
 
            var response = await HttpClient.PostAsync("/bug", new StringContent(serialize, Encoding.UTF8,"application/json"));
            var result = response.Content.ReadAsStringAsync().Result;

            Assert.True(response.IsSuccessStatusCode);

            BugDto? resultBugDto = JsonSerializer.Deserialize<BugDto>(result, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            Assert.True(resultBugDto?.BugId > 0);

            BugDto = resultBugDto;

            response = await HttpClient.GetAsync($"/bug/{BugDto.BugId}");
            result = response.Content.ReadAsStringAsync().Result;

            Assert.True(response.IsSuccessStatusCode);

            resultBugDto = JsonSerializer.Deserialize<BugDto>(result, new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });

            Assert.Equal(resultBugDto?.BugId, BugDto.BugId);

            HttpRequestMessage httpRequestMessage = new(HttpMethod.Delete, $"/bug/{BugDto.BugId}");

            response = await HttpClient.SendAsync(httpRequestMessage);

            Assert.True(response.IsSuccessStatusCode);
        }
    }
}