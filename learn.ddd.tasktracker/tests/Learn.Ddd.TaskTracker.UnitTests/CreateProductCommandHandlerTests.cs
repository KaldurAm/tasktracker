using Bogus;
using Learn.Ddd.TaskTracker.Application.Interfaces.Persistence.Repositories;
using Learn.Ddd.TaskTracker.Application.Persistence;
using Learn.Ddd.TaskTracker.Application.Products.Commands;
using Learn.Ddd.TaskTracker.Domain.Entities.Products;
using Microsoft.Extensions.Logging;
using Moq;

namespace Learn.Ddd.TaskTracker.UnitTests;

public class CreateProductCommandHandlerTests
{
    private readonly ILogger<CreateProductCommandHandler> _logger;
    private readonly IProductRepository _productRepository;
    private readonly IUnitOfWork _unitOfWork;

    private readonly CreateProductCommandHandler _handler;

    public CreateProductCommandHandlerTests()
    {
        _logger = Mock.Of<ILogger<CreateProductCommandHandler>>();
        _productRepository = Mock.Of<IProductRepository>();
        _unitOfWork = Mock.Of<IUnitOfWork>();
        _handler = new CreateProductCommandHandler(_logger, _productRepository, _unitOfWork);
    }

    [Fact]
    public async Task Handle_Should_Create_Product_And_Return_Successful_Result()
    {
        var faker = new Faker();
        
        // Arrange
        var productId = Guid.NewGuid();
        var productName = faker.Commerce.ProductName();
        var productDescription = faker.Commerce.ProductDescription();
        
        var command = new CreateProductCommand(productName, productDescription);
        
        var cancellationToken = CancellationToken.None;

        Mock.Get(_productRepository)
            .Setup(x => x.AddAsync(It.IsAny<Product>(), cancellationToken))
            .Callback<Product, CancellationToken>((product, _) => product.Id = productId)
            .Returns(Task.CompletedTask);

        // Act
        var result = await _handler.Handle(command, cancellationToken);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Equal(productId, result.Value.Id);
        Assert.Equal(2, result.Value.GetDomainEvents().Count);
        Mock.Get(_unitOfWork).Verify(x => x.SaveChangesAsync(cancellationToken), Times.Once);
        Mock.Get(_logger).Verify(
            x => x.Log(
                LogLevel.Information,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((o, t) => string.Equals("Start creating new product", o.ToString(), StringComparison.OrdinalIgnoreCase)),
                It.IsAny<Exception>(),
                It.Is<Func<It.IsAnyType, Exception, string>>((o, t) => true)!), 
            Times.Once);
        Mock.Get(_logger).Verify(
            x => x.Log(
                LogLevel.Information,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((o, t) => string.Equals("Product added", o.ToString(), StringComparison.OrdinalIgnoreCase)),
                It.IsAny<Exception>(),
                It.Is<Func<It.IsAnyType, Exception, string>>((o, t) => true)!), 
            Times.Once);
        Mock.Get(_logger).Verify(
            x => x.Log(
                LogLevel.Information,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((o, t) => string.Equals("Team added", o.ToString(), StringComparison.OrdinalIgnoreCase)),
                It.IsAny<Exception>(),
                It.Is<Func<It.IsAnyType, Exception, string>>((o, t) => true)!), 
            Times.Once);
        Mock.Get(_logger).Verify(
            x => x.Log(
                LogLevel.Information,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((o, t) => string.Equals("Backlog added", o.ToString(), StringComparison.OrdinalIgnoreCase)),
                It.IsAny<Exception>(),
                It.Is<Func<It.IsAnyType, Exception, string>>((o, t) => true)!), 
            Times.Once);
        Mock.Get(_logger).Verify(
            x => x.Log(
                LogLevel.Information,
                It.IsAny<EventId>(),
                It.Is<It.IsAnyType>((o, t) => string.Equals("Changes commited", o.ToString(), StringComparison.OrdinalIgnoreCase)),
                It.IsAny<Exception>(),
                It.Is<Func<It.IsAnyType, Exception, string>>((o, t) => true)!), 
            Times.Once);
    }
}