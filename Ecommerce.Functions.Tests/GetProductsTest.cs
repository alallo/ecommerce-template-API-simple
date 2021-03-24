using Microsoft.Azure.WebJobs;
using Moq;
using Xunit;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.Functions.Tests
{
    public class GetProductsTest
    {
        [Fact]
        public async void Get_ProductsList()
        {
            //Arrange
            var loggerMock = new Mock<ILogger<GetProducts>>();
            var httpReqMock = new Mock<HttpRequest>();
            var executionContextMock = new Mock<ExecutionContext>();
            var function = new GetProducts();

            //Act
            var result = await function.Run(httpReqMock.Object, loggerMock.Object, executionContextMock.Object, null);
            var okResult = result as OkObjectResult;

            //Assert    
            Assert.NotNull(okResult);
            Assert.Equal(StatusCodes.Status200OK, okResult.StatusCode);
        }
    }
}