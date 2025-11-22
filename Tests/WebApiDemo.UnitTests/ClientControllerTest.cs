using Domain;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Moq;
using Storage;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebApiDemo.Controllers;
using Xunit;

namespace WebApiDemo.UnitTests
{
    
      public class ClientControllerTest
      {
            //define Mock
            private readonly Mock<IClientService> _mockClientService;
            private readonly ClientController _controller;


            public ClientControllerTest()
            {
                  _mockClientService = new Mock<IClientService>();
                  _controller = new ClientController(_mockClientService.Object);
            }

            [Fact]
            public async Task AddClientTest()
            {
                  //arrange
                  var client = new Client { 
                        Address="A Road SS Street",
                        ClientName ="Tom"
                  };

                  _mockClientService.Setup(service => service.CreateClient(It.IsAny<Client>()))
                        .ReturnsAsync((Client c) => 
                        {
                              c.Id = 1000; // Simulate database assigning an ID
                              return c;
                        });

                  //Act
                  var resClient = await _controller.Create(client);

                  //Assert
                  Assert.IsType<Client>(resClient);
                  Assert.NotNull(resClient);
                  Assert.Equal(1000, resClient.Id);
            }
      }
}
