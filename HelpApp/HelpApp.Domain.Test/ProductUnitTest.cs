using HelpApp.Domain.Entities;
using HelpApp.Infra.Data.Context;
using HelpApp.Infra.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using FluentAssertions;
using Xunit;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace HelpApp.Domain.Test
{
    public class ProductUnitTest
    {

        private readonly ApplicationDbContext _context;
        private readonly ProductRepository _repository;
        public ProductUnitTest()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseSqlServer()
                .Options;

            _context = new ApplicationDbContext(options);
            _repository = new ProductRepository(_context);

        }
        #region Testes Positivos
        [Fact(DisplayName ="Create Product With Parameters Full")]
        public void CreateProduct_WithValidParameters_ResultObjectValisState()
        {
            Action action= () => new Product(1, 
                "Product Name", 
                "Product Description", 
                9.99m, 99, 
                "https://img/product.jpg");
            action.Should()
                .NotThrow<HelpApp.Domain.Validation.DomainExceptionValidation>();
        }
        #endregion
        #region Testes Negativos
        [Fact(DisplayName ="Create Product With ID Negative")]
        public void CreateProduct_NegativeIdValue_DomainExceptionInvalidId()
        {
            Action action = () => new Product(-1, "Product Name", "Product Description", 9.99m,
                99, "product image");

            action.Should().Throw<HelpApp.Domain.Validation.DomainExceptionValidation>()
                .WithMessage("Update Invalid Id value");
        }

        [Fact(DisplayName = "Create Product With Short Name")]
        public void CreateProduct_ShortNameValue_DomainExceptionShortName()
        {
            Action action = () => new Product(1, "Pr", "Product Description", 9.99m, 99,
                "product image");
            action.Should().Throw<HelpApp.Domain.Validation.DomainExceptionValidation>()
                 .WithMessage("Invalid name, too short, minimum 3 characters.");
        }

        [Fact(DisplayName = "Create Product With Null URL Image")]
        public void CreateProduct_WithNullImageName_NoDomainException()
        {
            Action action = () => new Product(1, "Product Name", "Product Description", 9.99m, 99, null);
            action.Should().NotThrow<HelpApp.Domain.Validation.DomainExceptionValidation>();
        }


        [Fact(DisplayName = "Create Product With URL Image Empty")]
        public void CreateProduct_WithEmptyImageName_NoDomainException()
        {
            Action action = () => new Product(1, "Product Name", "Product Description", 9.99m, 99, "");
            action.Should().NotThrow<HelpApp.Domain.Validation.DomainExceptionValidation>();
        }

        [Theory(DisplayName = "Create Product With Invalid Price")]
        [InlineData(-25)]
        public void CreateProduct_InvalidPriceValue_DomainException(int value)
        {
            Action action = () => new Product(1, "Product Name", "Product Description", value,
                99, "");
            action.Should().Throw<HelpApp.Domain.Validation.DomainExceptionValidation>()
                 .WithMessage("Invalid price negative value.");
        }

        [Theory(DisplayName = "Create Product With Inavlid Stock")]
        [InlineData(-5)]
        public void CreateProduct_InvalidStockValue_ExceptionDomainNegativeValue(int value)
        {
            Action action = () => new Product(1, "Pro", "Product Description", 9.99m, value,
                "product image");
            action.Should().Throw<HelpApp.Domain.Validation.DomainExceptionValidation>()
                   .WithMessage("Invalid stock negative value.");
        }
        [Theory(DisplayName = "Create Product With Long URL Image")]
        [InlineData("https://avatars.githubusercontent.com/u/654654651398798798798798798798798798654654321321321321321321321321321321321321321321321658479879879846465465465465465132198498498465465246549879879846213219849849846521321684684987465132165419687498746541631658468465840123321005408?v=4&size=64")]
        public void CreateProduct_LongImageName_DomainExceptionLongImageName(string url)
        {
            Action action = () => new Product(1, "Product Name", "Product Description", 9.99m,
                99, url);
            action.Should()
                .Throw<HelpApp.Domain.Validation.DomainExceptionValidation>()
                 .WithMessage("Invalid image name, too long, maximum 250 characters.");
        }
        #endregion


        #region Testes Create
        [Fact(DisplayName = "Create Product in Repository")]
        public async Task CreateProduct_Repository_ShouldReturnProduct()
        {
            var product = new Product(1, "Notebook", "Descrição do notebook", 3500.00m, 5, "https://img/notebook.jpg");
            var result = await _repository.Create(product);
            result.Should().NotBeNull();
            result.Name.Should().Be("Notebook");
        }
        #endregion



        #region Testes Read
        [Fact(DisplayName = "Get Category By Id")]
        public async Task GetById_ExistingId_ShouldReturnProduct()
        {
            var product = new Product(2, "Mouse", "Mouse ópticos", 50.00m, 15, "https://img/notebook.jpg");
            await _repository.Create(product);
            var result = await _repository.GetById(2);

            result.Should().NotBeNull();
            result.Id.Should().Be(2);
            result.Name.Should().Be("Mouse");
        }

        [Fact(DisplayName = "Get All Products")]
        public async Task GetProducts_ShouldReturnAll()
        {
            await _repository.Create(new Product(3, "Teclado", "Teclado mecânico", 120.00m, 8, "https://img/teclado.jpg"));
            await _repository.Create(new Product(4, "Monitor", "Monitor Full Hd", 800.00m, 3, "https://img/monitor.jpg"));
            var products = await _repository.GetProducts();
            products.Should().NotBeEmpty();
            products.Count().Should().BeGreaterOrEqualTo(2);
        }
        #endregion
        #region Testes Update
        [Fact(DisplayName = "Update Product")]
        public async Task UpdateProduct_ShouldUpdateSuccessfully()
        {
            var product = new Product(5, "Impressora", "Impressora antifa", 250.00m, 2, "https://img/imp.jpg");
            await _repository.Create(product);

            product.Update("Impressora Nova", "Nova descrição", 300.00m, 5, "https://img/ipm-nova.jpg");
            var updated = await _repository.Update(product);
            updated.Name.Should().Be("Impressora Nova");
            updated.Price.Should().Be(300.00m);
        }
        #endregion
        #region Testes Delete
        [Fact(DisplayName = "Remove Product")]
        public async Task RemoveProduct_ShouldDeleteSuccessfully()
        {
            var product = new Product(6, "Cadeira", "Cadeira gamer", 600.00m, 1, "https://img/cadeira.jpg");
            await _repository.Create(product);

            
            var removed = await _repository.Remove(product);
            removed.Should().NotBeNull();


            var result = await _repository.GetById(6);
            result.Should().BeNull();
        }
        #endregion

    }
}
