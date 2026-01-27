using Microsoft.AspNetCore.Mvc;

namespace EncryptionApi.Controllers
{
    [ApiController]
    [Route("api/encryption")]
    public class EncryptionController : ControllerBase
    {
        // POST: /api/encryption/encrypt
        [HttpPost("encrypt")]
        public IActionResult Encrypt([FromBody] WordRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Word))
            {
                return BadRequest(new { error = "Word cannot be empty" });
            }

            // Example encryption logic: Reverse the word
            var encryptedWord = new string(request.Word.Reverse().ToArray());
            return Ok(new { original = request.Word, encrypted = encryptedWord });
        }

        // POST: /api/encryption/decrypt
        [HttpPost("decrypt")]
        public IActionResult Decrypt([FromBody] WordRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Word))
            {
                return BadRequest(new { error = "Word cannot be empty" });
            }

            // Example decryption logic: Reverse the word back
            var decryptedWord = new string(request.Word.Reverse().ToArray());
            return Ok(new { encrypted = request.Word, decrypted = decryptedWord });
        }
    }

    // Request model for the API
    public class WordRequest
    {
        public string Word { get; set; }
    }
}