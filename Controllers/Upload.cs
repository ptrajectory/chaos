

using chaos.Models;
using chaos.Services;
using Microsoft.AspNetCore.Mvc;

namespace Upload{

    [ApiController]
    [Route("/api/uploads")]
    public class Upload: ControllerBase {

        IUpload client;

        public Upload(IUpload client){
            this.client = client;
        }

        [HttpPost("{userId}")] // Todo: temporary should replace with something else that will work
        public async Task<ActionResult<MediaUploads?>> uploadUserImage(string userId, IFormFile file){
            var media = await this.client.Upload(file, userId);

            return media;
        }   

    }

}