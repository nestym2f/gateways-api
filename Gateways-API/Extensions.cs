using Gateways.Dtos;
using Gateways.Entities;
namespace Gateways
{
    public static class Extensions{
        /*public static GatewayDto asDto(this Gateway gateway){
            return new GatewayDto{
                serialNumber = gateway.serialNumber,
                name = gateway.name,
                IPv4Address = gateway.IPv4Address,
                associatedPeripheralDevices = gateway.associatedPeripheralDevices
            };
        }   */     
        public static PeripheralDto pAsDto(this Peripheral peripheral){
            return new PeripheralDto{                
                vendor = peripheral.vendor,                
                status = peripheral.status,
                gatewayId = peripheral.gatewayId
            };
        }
    }
}