using HelpApp.Domain.Entities;
using HelpApp.Infra.Data.Context;
using HelpApp.Infra.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using FluentAssertions;
using Xunit;

namespace HelpApp.Domain.Test
{
    public class CategoryUnitTest
    {

        private readonly ApplicationDbContext _context;
        private readonly CategoryRepository _repository;
        public CategoryUnitTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlServer()
                .Options;

            _context = new ApplicationDbContext(options);
            _repository = new CategoryRepository(_context);

        }

        #region Testes Positivos
        [Fact(DisplayName = "Create Category With Valid State")]
        public void CreateCategory_WithValidParameters_ResultObjectValidState()
        {
            Action action = () => new Category(1, "Category Name");
            action.Should().NotThrow<HelpApp.Domain.Validation.DomainExceptionValidation>();
        }
        #endregion
        #region Testes Negativos
        [Fact(DisplayName ="Create Category With Name Empty")]
        public void CreateCategory_WithNameEmpty_ResultObjetcException()
        {
            Action action = () => new Category(1, "");
            action.Should().Throw<HelpApp.Domain.Validation.DomainExceptionValidation>()
                .WithMessage("Invalid name, name is required.");
        }
        #endregion



        #region Testes Create
        [Fact(DisplayName = "Create Valid Category")]
        public async Task Create_Category_ValidData_ShouldReturnCreatedCategory()
        {
            var category = new Category(1 ,"Eletrônicos");
            var result = await _repository.Create(category);
            result.Should().NotBeNull();
            result.Name.Should().Be("Eletrônicos");
        }
        #endregion



        #region Testes Read
        [Fact(DisplayName = "Get Category By Id")]
        public async Task GetById_ExistingId_ShouldReturnCategory()
        {
            var category = new Category(2, "Vestuário");
            await _repository.Create(category);
            var result = await _repository.GetById(2);

            result.Should().NotBeNull();
            result.Id.Should().Be(2);
            result.Name.Should().Be("Vestuário");
        }

        [Fact(DisplayName = "Get All Categories")]
        public async Task GetCategories_ShouldReturnAllCategories()
        {
            await _repository.Create(new Category(3, "Alimentos"));
            await _repository.Create(new Category(4, "Móveis"));
            var categories = await _repository.GetCategories();
            categories.Should().NotBeEmpty();
            categories.Count().Should().BeGreaterOrEqualTo(2);
        }
        #endregion



    }
}
