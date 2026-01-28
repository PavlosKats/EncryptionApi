using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace EncryptionApi.Tests
{
    public class EncryptionControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;

        public EncryptionControllerTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task EncryptEndpoint_ReturnsEncryptedWord()
        {
            // Arrange
            var requestBody = new { word = "HELLO" };
            var content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/api/encryption/encrypt", content);
            var responseString = await response.Content.ReadAsStringAsync();
            var responseData = JsonSerializer.Deserialize<EncryptResponse>(responseString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            // Assert
            Assert.True(response.IsSuccessStatusCode);
            Assert.NotNull(responseData);
            Assert.Equal("HELLO", responseData.Original);
            Assert.Equal("OLLEH", responseData.Encrypted); // Assuming encryption reverses the word
        }

        [Fact]
        public async Task DecryptEndpoint_ReturnsDecryptedWord()
        {
            // Arrange
            var requestBody = new { word = "OLLEH" };
            var content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/api/encryption/decrypt", content);
            var responseString = await response.Content.ReadAsStringAsync();
            var responseData = JsonSerializer.Deserialize<DecryptResponse>(responseString, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            // Assert
            Assert.True(response.IsSuccessStatusCode);
            Assert.NotNull(responseData);
            Assert.Equal("OLLEH", responseData.Encrypted);
            Assert.Equal("HELLO", responseData.Decrypted); // Assuming decryption reverses the word back
        }

        private class EncryptResponse
        {
            public string Original { get; set; }
            public string Encrypted { get; set; }
        }

        private class DecryptResponse
        {
            public string Encrypted { get; set; }
            public string Decrypted { get; set; }
        }
    }
}