

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace chaos.Models {

    public class MessageMedia {

        [Key]
        public string? ID {get;set;}

        public string? MessageID {get;set;}

        public string? MediaID {get;set;}

        [ForeignKey("MediaID")]
        public MediaUploads? MediaUpload {get;set;}

        [ForeignKey("MessageID")]
        public Message? Message{get;set;}

    }

}