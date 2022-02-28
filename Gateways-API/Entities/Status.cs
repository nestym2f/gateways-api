using System;
namespace Gateways.Entities{
using System.ComponentModel;
    public enum Status : byte
    {
        [Description("Offline")]
        Offline = 0,        

        [Description("Online")]
        Online = 1,        
    }
}


