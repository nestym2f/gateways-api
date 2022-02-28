using System.ComponentModel.DataAnnotations;
namespace Gateways.Entities{    
 public class Peripheral{
    [Required]    
    public int Id { get; set; }    
    [Required]
    public string vendor {get; set; }
    public DateTime dateCreated {get; set; }
    [Required]
    public Status status {get; set;}
    [Required]
    public int gatewayId {get; set; }
    public Gateway? associatedToGateway {get; set; }
 }   
}