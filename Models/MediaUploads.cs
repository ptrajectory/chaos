

using System.ComponentModel.DataAnnotations.Schema;

namespace chaos.Models{
    public class MediaUploads {

        public string? ID {get;set;}

        public string Type {get;set;} = "image";

        [Column(TypeName ="timestamptz")]
        public DateTime CreatedAt = DateTime.UtcNow;

        public double size = 0; // size in bytes

        public string? OwnerId {get;set;}

        public string? Name {get; set;}

        public string? FileUrl {get; set;}

    }
}