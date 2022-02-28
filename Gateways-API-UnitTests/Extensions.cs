using Gateways.Dtos;
using Gateways.Entities;
namespace Gateways_API_UnitTests
{
    public static class ExtensionsTest
    {
        private static PeripheralDto ToDto(this Peripheral peripheral){
            return new PeripheralDto{                
            vendor = peripheral.vendor,                
            status = peripheral.status,
            gatewayId = peripheral.gatewayId
            };
        }
    }
}