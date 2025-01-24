using Microsoft.AspNetCore.Mvc;
using static System.Net.Mime.MediaTypeNames;
using System.Net;
using System.Text.Json;
using System;
using System.Web;

namespace ImageMicroservice.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ImageLoaderController : ControllerBase
    {
        [HttpGet("FromURL/{url}")]
        public IActionResult FromURL(string url)
        {
            using (WebClient webClient = new())
            {
                try
                {
                    var decodeUrl = HttpUtility.UrlDecode(url);
                    byte[] imageData = webClient.DownloadData(decodeUrl);
                    using (var ms = new MemoryStream(imageData))
                    {


                        var imageBase64String = Convert.ToBase64String(ms.ToArray());

                        var json = JsonSerializer.Serialize(imageBase64String);

                        return Ok(json);
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }
    }
}