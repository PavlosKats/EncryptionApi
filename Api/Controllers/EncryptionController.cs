using Microsoft.AspNetCore.Mvc;

namespace EncryptionApi.Controllers
{
    [ApiController]
    [Route("api/encryption")]
    public class EncryptionController : ControllerBase
    {
        private const int CaesarShift = 3; // You can change the shift value if desired

        // POST: /api/encryption/encrypt
        [HttpPost("encrypt")]
        public IActionResult Encrypt([FromBody] WordRequest request)
        {
            if (string.IsNullOrWhiteSpace(request.Word))
            {
                return BadRequest(new { error = "Word cannot be empty" });
            }

            var encryptedWord = CaesarEncrypt(request.Word, CaesarShift);
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

            var decryptedWord = CaesarEncrypt(request.Word, 26 - CaesarShift);
            return Ok(new { encrypted = request.Word, decrypted = decryptedWord });
        }

        // Caesar cipher encryption/decryption helper
        private static string CaesarEncrypt(string input, int shift)
        {
            char ShiftChar(char c)
            {
                if (char.IsLetter(c))
                {
                    char offset = char.IsUpper(c) ? 'A' : 'a';
                    return (char)(((c - offset + shift) % 26) + offset);
                }
                return c;
            }

            return new string(input.Select(ShiftChar).ToArray());
        }
    }

    // Request model for the API
    public class WordRequest
    {
        public string? Word { get; set; }
    }
}