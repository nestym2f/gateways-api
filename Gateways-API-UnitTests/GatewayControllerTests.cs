
using System;
using Gateways.Entities;
using Gateways.Persistence;
using Gateways.Controllers;
using Moq;
using Xunit;
using System.Text.Json;
using Newtonsoft.Json.Linq;
using System.Web.Http.Results;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;

namespace Gateways_API_UnitTests;

public class GatewayControllerTests
{
    public IConfigurationRoot configuration = new ConfigurationBuilder()
            .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
            .AddJsonFile("appsettings.json")
            .Build();
    [Fact]
    public async Task GetGateway_NotGatewayExists_ReturnsNotFound(){
    
    var options = new DbContextOptionsBuilder<DataContext>()
        .UseSqlServer(configuration.GetConnectionString("localConnection"))
        .Options;
    
    var myContext = new DataContext(options);
    myContext.Database.EnsureCreated();
    //Arrange
    var controller = new GatewayController(myContext);

    // Act
    var result = await controller.GetGateway(It.IsAny<int>());

    // Assert
    Assert.Null(result.Value);
    }
   
    [Fact]
    public async Task GetGateway_GatewayExists_ReturnsFound(){
    
    var options = new DbContextOptionsBuilder<DataContext>()
        .UseSqlServer(configuration.GetConnectionString("localConnection"))
        .Options;
    
    var myContext = new DataContext(options);
    myContext.Database.EnsureCreated();
    var expectedGTW = CreateRandomGateway();
    //Arrange
    var controller = new GatewayController(myContext);

    // Act
    var result = await controller.GetGateway(1);

    // Assert    
    Assert.NotNull(result.Value);
    }
    
    [Fact]
    public async Task GetGateways_GatewayExists_ReturnsAll(){
    
    var options = new DbContextOptionsBuilder<DataContext>()
        .UseSqlServer(configuration.GetConnectionString("localConnection"))
        .Options;
    
    var myContext = new DataContext(options);
    myContext.Database.EnsureCreated();
    var expectedGTW = CreateRandomGateway();
    //Arrange
    var controller = new GatewayController(myContext);

    // Act
    var result = await controller.GetGateways();

    // Assert    
    Assert.NotNull(result.Value);
    }
   
    [Fact]
    public async Task PostGateway_WithGateway_ReturnsCreatedGateway(){
    
    var options = new DbContextOptionsBuilder<DataContext>()
        .UseSqlServer(configuration.GetConnectionString("localConnection"))
        .Options;
    
    var myContext = new DataContext(options);        
    var expectedGTW = CreateRandomGateway();
    //Arrange
    var controller = new GatewayController(myContext);    

    // Act
    var result = await controller.PostGateway(CreateRandomGateway());
    var createdItem = (result.Result as CreatedAtActionResult).Value;
    // Assert    
    Assert.NotNull(createdItem);
    }
    
    [Fact]
    public async Task DeleteGateway_WithGateway_ReturnsNoContent(){
    
    var options = new DbContextOptionsBuilder<DataContext>()
        .UseSqlServer(configuration.GetConnectionString("localConnection"))
        .Options;
    
    var myContext = new DataContext(options);        
    var expectedGTW = CreateRandomGateway();
    //Arrange
    var controller = new GatewayController(myContext);    
    var toDelete = CreateAndDestroyRandomGateway();
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
    var result = await controller.DeleteGateway(idToDelete);    
    
    // Assert    
    Assert.IsType<NoContentResult>(result);
    }

    private Gateway CreateRandomGateway(){
        var rdn = new Random();
        return new(){            
            serialNumber = rdn.Next().ToString(),
            name = "NewStringName",
            IPv4Address = "192.168.1.5"
        };
    }

    
    private async Task <ActionResult<Gateway>> CreateAndDestroyRandomGateway(){
        var rdn = new Random();
        var newItem = new Gateway(){            
            serialNumber = rdn.Next().ToString(),
            name = "RandomName",
            IPv4Address = "155.15.51.155"
        };        

        var options = new DbContextOptionsBuilder<DataContext>()
        .UseSqlServer(configuration.GetConnectionString("localConnection"))
        .Options;        
        var myContext = new DataContext(options);                
        myContext.Database.EnsureCreated();                
        var controller = new GatewayController(myContext);        
        var result = await controller.PostGateway(newItem);
        var createdItem = (result.Result as CreatedAtActionResult);
        return createdItem;      
    }
}