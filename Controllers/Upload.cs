

using chaos.AuthRepository;
using chaos.Models;
using chaos.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Upload{
    [ApiController]
    [Route("/api/uploads")]
    [Authorize]
    public class Upload: ControllerBase {

        IUpload client;

        public Upload(IUpload client){
            this.client = client;
        }

        /// <summary>
        /// Upload a user file
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        [HttpPost("{userId}")] // Todo: temporary should replace with something else that will work
        public async Task<ActionResult<MediaUploads?>> uploadUserImage(string userId, IFormFile file){
            var media = await this.client.Upload(file, userId);

            return media;
        }   

    }

}