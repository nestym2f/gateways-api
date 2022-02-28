using System.ComponentModel.DataAnnotations;
//using Gateways.Dtos;
namespace Gateways.Entities{    
 public class Gateway{
    public int Id { get; set; } 
    [Required]
    public string serialNumber { get; set; }
    [Required]
    public string name {get; set; } = string.Empty;

    [RegularExpression(@"^(?:(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.){3}(?:25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)$", ErrorMessage = "Invalid IP address")]
    [Required]
    public string IPv4Address {get; set; } = string.Empty;    
 }   
}