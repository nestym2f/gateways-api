
using System;
using Gateways.Entities;
using Gateways.Persistence;
using Gateways.Controllers;
using Gateways.Dtos;
using Moq;
using Xunit;
using System.Text.Json;
using Newtonsoft.Json.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;


namespace Gateways_API_UnitTests;

public class PeripheralControllerTests
{
    public IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();
    [Fact]
    public async Task GetPeripheral_NotPeripheralExists_ReturnsNotFound(){
    
        var options = new DbContextOptionsBuilder<DataContext>()
            .UseSqlServer(configuration.GetConnectionString("localConnection"))
            .Options;
        
        var myContext = new DataContext(options);
        myContext.Database.EnsureCreated();
        //Arrange
        var controller = new PeripheralController(myContext);

        // Act
        var result = await controller.GetPeripheral(It.IsAny<int>());

        // Assert
        Assert.Null(result.Value);
    }
   
    [Fact]
    public async Task GetPeripheral_PeripheralExists_ReturnsFound(){
    
    var options = new DbContextOptionsBuilder<DataContext>()
        .UseSqlServer(configuration.GetConnectionString("localConnection"))
        .Options;
    
    var myContext = new DataContext(options);
    myContext.Database.EnsureCreated();    
    //Arrange
    var controller = new PeripheralController(myContext);

    // Act
    var result = await controller.GetPeripheral(27);

    // Assert    
    Assert.NotNull(result.Value);
    }
    
    [Fact]
    public async Task GetPeripherals_PeripheralExists_ReturnsAll(){
    
    var options = new DbContextOptionsBuilder<DataContext>()
        .UseSqlServer(configuration.GetConnectionString("localConnection"))
        .Options;
    
    var myContext = new DataContext(options);
    myContext.Database.EnsureCreated();
    var expectedGTW = CreateRandomPeripheral();
    //Arrange
    var controller = new PeripheralController(myContext);

    // Act
    var result = await controller.GetPeripherals();

    // Assert    
    Assert.NotNull(result.Value);
    }
   
    [Fact]
    public async Task PostPeripheral_WithPeripheral_ReturnsCreatedPeripheral(){
    
    var options = new DbContextOptionsBuilder<DataContext>()
        .UseSqlServer(configuration.GetConnectionString("localConnection"))
        .Options;
    
    var myContext = new DataContext(options);    
    //Arrange
    var controller = new PeripheralController(myContext);    
    
    // Act
    var createdPeripheral = CreateRandomPeripheral();
    PeripheralDto peripheral = new PeripheralDto{
        vendor = createdPeripheral.vendor,                
        status = createdPeripheral.status,
        gatewayId = createdPeripheral.gatewayId
    };
    
    var result = await controller.PostPeripheral(peripheral);
    var createdItem = (result.Result as CreatedAtActionResult).Value;
    // Assert    
    Assert.NotNull(createdItem);
    }
    
    [Fact]
    public async Task DeletePeripheral_WithPeripheral_ReturnsNoContent(){
    
    var options = new DbContextOptionsBuilder<DataContext>()
        .UseSqlServer(configuration.GetConnectionString("localConnection"))
        .Options;
    
    var myContext = new DataContext(options);        
    var expectedGTW = CreateRandomPeripheral();
    //Arrange
    var controller = new PeripheralController(myContext);    
    var toDelete = CreateAndDestroyPeripheral();
    toDelete.Wait();    
    var toDeleteResult = JsonSerializer.Serialize((Object)toDelete.Result.Result);        
    JObject json = JObject.Parse(toDeleteResult);
    int idToDelete = 0;
    foreach (var e in json){
        if(e.Key == "Value"){                    
            idToDelete = (int)e.Value.SelectToken("Id", true);
            break; 
        }           
    }
    // Act
    var result = await controller.DeletePeripheral(idToDelete);    
    
    // Assert    
    Assert.IsType<NoContentResult>(result);
    }

    private Peripheral CreateRandomPeripheral(){
        var rdn = new Random();
        var newItem = new Peripheral(){            
            vendor = rdn.Next().ToString(),
            status = Status.Online,
            gatewayId = 2
        };
        return newItem;
    }
     
    private async Task <ActionResult<Peripheral>> CreateAndDestroyPeripheral(){
        var rdn = new Random();
        var newItem = new Peripheral(){            
            vendor = rdn.Next().ToString(),
            status = Status.Online,
            gatewayId = 2
        };        

        var options = new DbContextOptionsBuilder<DataContext>()
        .UseSqlServer(configuration.GetConnectionString("localConnection"))
        .Options;        
        var myContext = new DataContext(options);                
        myContext.Database.EnsureCreated();                
        var controller = new PeripheralController(myContext);
        
        PeripheralDto peripheral = new PeripheralDto{
            vendor = newItem.vendor,                
            status = newItem.status,
            gatewayId = newItem.gatewayId
        };      
        var result = await controller.PostPeripheral(peripheral);
        var createdItem = (result.Result as CreatedAtActionResult);
        return createdItem;      
    }
    
}