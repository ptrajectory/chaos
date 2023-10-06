using chaos.Models;

namespace chaos.Dtos.Media;


public class GetMessageMedia {
        public string? ID {get;set;}

        public string? MessageID {get;set;}

        public string? MediaID {get;set;}

        public GetMedia? MediaUpload {get;set;}


}