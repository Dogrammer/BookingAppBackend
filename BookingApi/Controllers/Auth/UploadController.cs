using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using BookingCore.RequestModels;
using BookingCore.Services;
using BookingDomain.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookingApi.Controllers.Auth
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        private readonly IImageService _imageService;

        public UploadController(IImageService imageService)
        {
            _imageService = imageService;
        }


        [Route("uploadApartmentImage")]
        [HttpPost, DisableRequestSizeLimit]
        public async Task<IActionResult> Upload([FromForm]ImportApartmentImage request)
        {
            //tu spremi file u file storage 
            var folderName = Path.Combine("Resources", "Images");
            var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

            if (request.File.Length > 0)
            {
                var fileName = ContentDispositionHeaderValue.Parse(request.File.ContentDisposition).FileName.Trim('"');
                var fullPath = Path.Combine(pathToSave, fileName);
                var dbPath = Path.Combine(folderName, fileName);

                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    request.File.CopyTo(stream);
                }

                var newApartmentImage = new Image
                {
                    ApartmentId = request.ApartmentId,
                    Name = request.File.FileName,
                    FileType = Path.GetExtension(request.File.FileName),
                    FilePath = dbPath
                };

                _imageService.Attach(newApartmentImage);
                await _imageService.Save();

                return Ok();
            }

            else
            {
                return BadRequest("File size is 0");
            }

            
                //

                // tu spremi podatke i filepath u bazu 

                
            //try
            //{
            //    var file = Request.Form.Files[0];
            //    var folderName = Path.Combine("Resources", "Images");
            //    var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

            //    if (file.Length > 0)
            //    {
            //        var fileName = ContentDispositionHeaderValue.Parse(file.ContentDisposition).FileName.Trim('"');
            //        var fullPath = Path.Combine(pathToSave, fileName);
            //        var dbPath = Path.Combine(folderName, fileName);

            //        using (var stream = new FileStream(fullPath, FileMode.Create))
            //        {
            //            file.CopyTo(stream);
            //        }

            //        return Ok(new { dbPath });
            //    }
            //    else
            //    {
            //        return BadRequest();
            //    }
            //}
            //catch (Exception ex)
            //{
            //    return StatusCode(500, $"Internal server error: {ex}");
            //}
        }
    }
}