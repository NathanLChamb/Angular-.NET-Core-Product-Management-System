using eCommerce.Application.Features.Categories.Commands.CreateCategory;
using eCommerce.Application.Features.Categories.DTOs;
using eCommerce.Application.Features.Categories.Queries.GetAllCategories;
using eCommerce.Application.Features.Categories.Queries.GetCategoryById;
using eCommerce.Application.Shared;
using eCommerce.Domain.Metadata;
using eCommerce.Infrastructure.Persistence;
using eCommerce.Tests.Infrastructure;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace eCommerce.Tests.Application
{
    [Collection("Database Collection")]
    public class CategoryFeatureTests
    {
        private readonly PostgresContainerFixture _fixture;

        public CategoryFeatureTests(PostgresContainerFixture fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task GetAllCategories_WithValidPaginationParams_ShouldReturnCategories()
        {
            await _fixture.ResetDatabase();

            using var scope = _fixture.Services.CreateScope();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
            var context = scope.ServiceProvider.GetRequiredService<eCommerceContext>();

            var categories = new List<Category>
            {
                new Category
                {
                    Name = "Test Name1",
                    Description = "Test Description1"
                },
                new Category
                {
                    Name = "Test Name2",
                    Description = "Test Description2"
                }
            };
            context.Categories.AddRange(categories);
            await context.SaveChangesAsync();

            var pageParams = new PaginationParams
            {
                PageNumber = 1,
                PageSize = 1
            };

            var result = await mediator.Send(new GetAllCategoriesQuery(pageParams));

            Assert.NotNull(result);

            Assert.Equal(pageParams.PageNumber, result.PageNumber);
            Assert.Equal(pageParams.PageSize, result.PageSize);
            Assert.Equal(categories.Count, result.TotalCount);
            Assert.Single(result.Items);

            var category = result.Items[0];

            Assert.Equal(categories[0].Name, category.Name);
            Assert.Equal(categories[0].Description, category.Description);
        }

        [Fact]
        public async Task GetCategoryById_WithValidId_ShouldReturnCategory()
        {
            await _fixture.ResetDatabase();

            using var scope = _fixture.Services.CreateScope();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
            var context = scope.ServiceProvider.GetRequiredService<eCommerceContext>();

            var category = new Category
            {
                Name = "Test Name",
                Description = "Test Description"
            };
            context.Categories.Add(category);
            await context.SaveChangesAsync();

            var result = await mediator.Send(new GetCategoryByIdQuery(category.Id));

            Assert.NotNull(result);

            Assert.Equal(category.Id, result.Id);
            Assert.Equal(category.Name, result.Name);
            Assert.Equal(category.Description, result.Description);
        }

        [Fact]
        public async Task CreateCategory_WithValidData_ShouldPersist()
        {
            await _fixture.ResetDatabase();

            using var scope = _fixture.Services.CreateScope();
            var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
            var context = scope.ServiceProvider.GetRequiredService<eCommerceContext>();

            var dto = new CreateCategoryDto
            {
                Name = "Test Name",
                Description = "Test Description"
            };

            var result = await mediator.Send(new CreateCategoryCommand(dto.Name, dto.Description));

            Assert.NotNull(result);
            Assert.Equal(dto.Name, result.Name);
            Assert.Equal(dto.Description, result.Description);
            Assert.True(result.Id > 0);

        }
    }
}
