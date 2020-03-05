using Golobal_IMC_Task.Application.Common.Interfaces;
using Golobal_IMC_Task.Domain.Entities;
using Golobal_IMC_Task.Infrastructure.Persistence;
using IdentityServer4.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Moq;
using System;

namespace Golobal_IMC_Task.Application.UnitTests.Common
{
    public static class ApplicationDbContextFactory
    {
        public static ApplicationDbContext Create()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var operationalStoreOptions = Options.Create(
                new OperationalStoreOptions
                {
                    DeviceFlowCodes = new TableConfiguration("DeviceCodes"),
                    PersistedGrants = new TableConfiguration("PersistedGrants")
                });

            var dateTimeMock = new Mock<IDateTime>();
            dateTimeMock.Setup(m => m.Now)
                .Returns(new DateTime(3001, 1, 1));

            var currentUserServiceMock = new Mock<ICurrentUserService>();
            currentUserServiceMock.Setup(m => m.UserId)
                .Returns("00000000-0000-0000-0000-000000000000");

            var context = new ApplicationDbContext(
                options, operationalStoreOptions,
                currentUserServiceMock.Object, dateTimeMock.Object);

            context.Database.EnsureCreated();

            SeedSampleData(context);

            return context;
        }

        public static void SeedSampleData(ApplicationDbContext context)
        {
            context.Categorys.AddRange(
                new Category { Id = 1, CategoryName = "Food & Drink" }
            );

            context.Products.AddRange(
                new Product { Id = 1, CategoryId = 1, Title = "Bread"},
                new Product { Id = 2, CategoryId = 1, Title = "Butter" },
                new Product { Id = 3, CategoryId = 1, Title = "Milk" },
                new Product { Id = 4, CategoryId = 1, Title = "Sugar" },
                new Product { Id = 5, CategoryId = 1, Title = "Coffee" }
            );

            context.SaveChanges();
        }

        public static void Destroy(ApplicationDbContext context)
        {
            context.Database.EnsureDeleted();

            context.Dispose();
        }
    }
}