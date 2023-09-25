
using chaos.Models;
using chaos.utils;
using Supabase;

namespace chaos.Services {

    public class SupabaseUpload : IUpload
    {
        ChatContext context;
        Supabase.Client supabase;

        public SupabaseUpload(ChatContext context, Supabase.Client supabase) {
            this.context = context;
            this.supabase = supabase;
        }
        
        public async Task<MediaUploads> Upload(IFormFile file, string user_id)
        {
            var imagePath = Path.Combine("ChatAssets", ((DateTimeOffset)DateTime.UtcNow).ToUnixTimeMilliseconds().ToString(), file.FileName);
            var byteArray = await Utils.ConvertFormFileToByteArray(file);
            
            var result = await this.supabase.Storage.From("ChatMedia").Upload(byteArray, imagePath);

            Console.WriteLine("The upload result::", result);

            var file_url = this.supabase.Storage.From("ChatMedia").GetPublicUrl(result.Replace("ChatMedia/", ""));

            Console.WriteLine("FILE URL", file_url);

            var media_upload_id = Utils.GenerateUniqueID("upload");

            var new_upload = new MediaUploads(){
                size =file.Length,
                ID = media_upload_id,
                Name = file.Name,
                Type = file.ContentType,
                OwnerId = user_id,
                FileUrl = file_url
            };

            this.context.MEDIA_UPLOADS.Add(new_upload);

            this.context.SaveChanges();

            return new_upload;
        }

        public MessageMedia addMessageMedia(string media_id, string message_id) {

            var messageMedia = new MessageMedia(){
                ID = Utils.GenerateUniqueID("msg_upload"),
                MediaID = media_id,
                MessageID = message_id
            };

            this.context.MESSAGE_MEDIA.Add(messageMedia);

            this.context.SaveChanges();

            return messageMedia;

        }
    }

}