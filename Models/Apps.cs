
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using chaos.utils;

namespace chaos.Models;


public class Apps {

    [Key]
    public string? ID {get; set;} = Utils.GenerateUniqueID("app");

    public string? Name {get; set;}

    [Column(TypeName ="timestamptz")]
    public DateTime CreatedAt {get;set;} = DateTime.UtcNow;


    public string? OrgID {get;set;}

    [ForeignKey("OrgID")]
    public Organization? Org{get;set;} 

    public List<User> Users = new List<User>();

}