

using chaos.Models;

namespace chaos.Services {

    public interface IUpload {

        public Task<MediaUploads> Upload(IFormFile file,  string creator_id);//TODO: add in the user id as a parameter  later 

        public MessageMedia addMessageMedia(string media_upload_id, string message_id);

    }

}