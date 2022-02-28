using System.ComponentModel.DataAnnotations;
using Gateways.Entities;
namespace Gateways.Dtos{    
 public class PeripheralDto{ 
    [Required]
    public string vendor {get; set; }    
    [Required]
    public Status status {get; set;}
    [Required]
    public int gatewayId {get; set; }
 }   
}