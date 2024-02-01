using api.Data;
using api.Models;
using api.Repositories;
using api.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace api.tests
{
    public class ClientApiTests
    {
        [Fact]
        public async void Test_CreateNewClients_Success()
        {
            // Arrange 
            var builder = new DbContextOptionsBuilder<DataContext>();
            builder.UseInMemoryDatabase(databaseName: "ClientInMemory");
            var dbContextOptions = builder.Options;

            var dbContext = new DataContext(dbContextOptions);
            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();

            var repository = new ClientRepository(dbContext);
            var emailService = new EmailService();
            var service = new ClientService(repository, emailService);

            var clientRequest = new Client
            {
                Id = "1",
                FirstName = "Test User",
                LastName = "Test",
                Email = "Test@mailnator.com",
                PhoneNumber = "1111111"
            };

            // Act 
            var result =  await ClientEndpointsV1.CreateClient(clientRequest, service);

            // Assert
            Assert.Equal("CreatedResult", result.GetType().Name);
        }

        [Fact]
        public async void Test_CreateNewClients_BadRequest()
        {
            // Arrange 
            var builder = new DbContextOptionsBuilder<DataContext>();
            builder.UseInMemoryDatabase(databaseName: "ClientInMemory");
            var dbContextOptions = builder.Options;

            var dbContext = new DataContext(dbContextOptions);
            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();

            var repository = new ClientRepository(dbContext);
            var emailService = new EmailService();
            var service = new ClientService(repository, emailService);

            var clientRequest = new Client
            {
                Id = "1",
                FirstName = "Test User",
                LastName = "Test",
                Email = "Test@mailnator.com",
                PhoneNumber = "1111111"
            };

            await ClientEndpointsV1.CreateClient(clientRequest, service);

            // Act 
            var result = await ClientEndpointsV1.CreateClient(clientRequest, service);

            // Assert
            Assert.Equal("BadRequestObjectResult", result.GetType().Name);
        }

        [Fact]
        public async void Test_UpdateClient_Success()
        {
            // Arrange 
            var builder = new DbContextOptionsBuilder<DataContext>();
            builder.UseInMemoryDatabase(databaseName: "ClientInMemory");
            var dbContextOptions = builder.Options;

            var dbContext = new DataContext(dbContextOptions);
            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();

            var repository = new ClientRepository(dbContext);
            var emailService = new EmailService();
            var service = new ClientService(repository, emailService);

            var createRequest = new Client
            {
                Id = "1",
                FirstName = "Test User",
                LastName = "Test",
                Email = "Test@mailnator.com",
                PhoneNumber = "1111111"
            };

            await ClientEndpointsV1.CreateClient(createRequest, service);

            var updateRequest = new Client
            {
                Id = "2",
                FirstName = "Test User2",
                LastName = "Test2",
                Email = "Test@mailnator.com",
                PhoneNumber = "1111111"
            };

            // Act 
            var result = await ClientEndpointsV1.UpdateClient(createRequest.Id,
                updateRequest, service);

            // Assert
            Assert.Equal("OkObjectResult", result.GetType().Name);
        }

        [Fact]
        public async void Test_UpdateClient_BadRequest()
        {
            // Arrange 
            var builder = new DbContextOptionsBuilder<DataContext>();
            builder.UseInMemoryDatabase(databaseName: "ClientInMemory");
            var dbContextOptions = builder.Options;

            var dbContext = new DataContext(dbContextOptions);
            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();

            var repository = new ClientRepository(dbContext);
            var emailService = new EmailService();
            var service = new ClientService(repository, emailService);

            var createRequest = new Client
            {
                Id = "1",
                FirstName = "Test User",
                LastName = "Test",
                Email = "Test@mailnator.com",
                PhoneNumber = "1111111"
            };

            await ClientEndpointsV1.CreateClient(createRequest, service);

            var updateRequest = new Client
            {
                Id = "2",
                FirstName = "Test User2",
                LastName = "Test2",
                Email = "Test@mailnator.com",
                PhoneNumber = "1111111"
            };

            // Act 
            var result = await ClientEndpointsV1.UpdateClient("3",
                updateRequest, service);

            // Assert
            Assert.Equal("BadRequestObjectResult", result.GetType().Name);
        }

        [Fact]
        public async void Test_GetClients()
        {
            // Arrange 
            var builder = new DbContextOptionsBuilder<DataContext>();
            builder.UseInMemoryDatabase(databaseName: "ClientInMemory");
            var dbContextOptions = builder.Options;

            var dbContext = new DataContext(dbContextOptions);
            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();

            var repository = new ClientRepository(dbContext);
            var emailService = new EmailService();
            var service = new ClientService(repository, emailService);

            var clientRequest = new Client
            {
                Id = "1",
                FirstName = "Test User",
                LastName = "Test",
                Email = "Test@mailnator.com",
                PhoneNumber = "1111111"
            };

            await ClientEndpointsV1.CreateClient(clientRequest, service);

            // Act 
            var result = await ClientEndpointsV1.GetClients(service);

            // Assert
            Assert.Equal("OkObjectResult", result.GetType().Name);
        }

        [Theory]
        [InlineData("steven")]
        [InlineData("Steven")]
        [InlineData("STEVEN")]

        public async void Test_SearchClientName_Found(string expectedName)
        {
            // Arrange 
            var builder = new DbContextOptionsBuilder<DataContext>();
            builder.UseInMemoryDatabase(databaseName: "ClientInMemory");
            var dbContextOptions = builder.Options;

            var dbContext = new DataContext(dbContextOptions);
            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();

            var repository = new ClientRepository(dbContext);
            var emailService = new EmailService();
            var service = new ClientService(repository, emailService);

            var clientOneRequest = new Client
            {
                Id = "1",
                FirstName = "John",
                LastName = "Stevens",
                Email = "Test@mailnator.com",
                PhoneNumber = "1111111"
            };

            var clientTwoRequest = new Client
            {
                Id = "2",
                FirstName = "Steven",
                LastName = "Smith",
                Email = "Test2@mailnator.com",
                PhoneNumber = "2222222"
            };

            await ClientEndpointsV1.CreateClient(clientOneRequest, service);
            await ClientEndpointsV1.CreateClient(clientTwoRequest, service);

            // Act 
            var result = await ClientEndpointsV1.SearchClients(expectedName, service);
            
            // Assert
            Assert.Equal("OkObjectResult", result.GetType().Name);
        }

        [Theory]
        [InlineData("steven")]
        [InlineData("Steven")]
        [InlineData("STEVEN")]

        public async void Test_SearchClientName_NotFound(string expectedName)
        {
            // Arrange 
            var builder = new DbContextOptionsBuilder<DataContext>();
            builder.UseInMemoryDatabase(databaseName: "ClientInMemory");
            var dbContextOptions = builder.Options;

            var dbContext = new DataContext(dbContextOptions);
            dbContext.Database.EnsureDeleted();
            dbContext.Database.EnsureCreated();

            var repository = new ClientRepository(dbContext);
            var emailService = new EmailService();
            var service = new ClientService(repository, emailService);

            var clientOneRequest = new Client
            {
                Id = "1",
                FirstName = "John",
                LastName = "Smith",
                Email = "Test@mailnator.com",
                PhoneNumber = "1111111"
            };

            var clientTwoRequest = new Client
            {
                Id = "2",
                FirstName = "Mary",
                LastName = "Smith",
                Email = "Test2@mailnator.com",
                PhoneNumber = "2222222"
            };

            await ClientEndpointsV1.CreateClient(clientOneRequest, service);
            await ClientEndpointsV1.CreateClient(clientTwoRequest, service);

            // Act 
            var result = await ClientEndpointsV1.SearchClients(expectedName, service);

            // Assert
            Assert.Equal("NotFoundObjectResult", result.GetType().Name);
        }
    }
}