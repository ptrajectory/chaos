

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace chaos.Models;

public class Organization {

    [Key]
    public string? ID{get;set;} = utils.Utils.GenerateUniqueID("org");

    public string? Name{get;set;}

    public string? Email{get;set;}

    public string? Banner{get;set;}

    [Column(TypeName = "timestamptz")]
    public DateTime CreatedAt{get;set;} = DateTime.UtcNow; 

    public List<Apps> Apps = new List<Apps>();

}