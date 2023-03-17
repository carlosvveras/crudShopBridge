using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Shopbridge_base.Controllers;
using Shopbridge_base.Data;
using Shopbridge_base.Data.Repository;
using Shopbridge_base.Domain.Models;
using Shopbridge_base.Domain.Services;

namespace Shopbridge_Test
{
    public class UnitTest1
    {
        private Repository<Product> repository;
        private ProductService productService;
        public static DbContextOptions<Shopbridge_Context> dbContextOptions { get; }
        public static string connectionString = "Server=(localdb)\\MSSQLLocalDB;Database=Shopbridge_Context-bf53e110-7950-4cb0-a45d-226517a8f7dd;Trusted_Connection=True;MultipleActiveResultSets=true";


        static UnitTest1()
        {
            dbContextOptions = new DbContextOptionsBuilder<Shopbridge_Context>()
                .UseSqlServer(connectionString)
                .Options;
        }

        public UnitTest1()
        {
            var context = new Shopbridge_Context(dbContextOptions);
            DummyData db = new DummyData();
            db.Seed(context);

            repository = new Repository<Product>(context);
            productService = new ProductService(repository, null);
        }

        [Fact]
        public async void UnitTestPostOk()
        {
            var context = new Shopbridge_Context(dbContextOptions);
            DummyData db = new DummyData();
            db.Seed(context);

            repository = new Repository<Product>(context);
            productService = new ProductService(repository, null);

            //Arrange  
            var controller = new ProductsController(productService, null);

            Product product = new Product();
            product.CreationDate = DateTime.Now;
            product.Price = 1;
            product.Name = "xUnit Test";
            product.Description = "xUnitTest";

            //Act  
            var data = await controller.PostProduct(product);

            //Assert  
            Assert.IsType<OkObjectResult>(data);
        }


        [Fact]
        public async void UnitTestPostAlreadyExists()
        {

            var context = new Shopbridge_Context(dbContextOptions);
            DummyData db = new DummyData();
            db.Seed(context);

            repository = new Repository<Product>(context);
            productService = new ProductService(repository, null);

            //Arrange  
            var controller = new ProductsController(productService, null);

            Product product = new Product();
            product.Product_Id = 1;
            product.CreationDate = DateTime.Now;
            product.Price = 1;
            product.Name = "xUnit Test";
            product.Description = "xUnitTest";

            //Act  
            var data = await controller.PostProduct(product);

            //Assert  
            Assert.IsType<BadRequestObjectResult>(data);
        }

        [Fact]
        public async void UnitTestGetByIdSuccess()
        {

            var context = new Shopbridge_Context(dbContextOptions);
            DummyData db = new DummyData();
            db.Seed(context);
            //context.Product.AsNoTracking();

            repository = new Repository<Product>(context);
            productService = new ProductService(repository, null);

            //Arrange  
            var controller = new ProductsController(productService, null);

            int id = 1;

            //Act  
            var data = await controller.GetProduct(id);

            //Assert  
            Assert.IsType<OkObjectResult>(data);
        }

        [Fact]
        public async void UnitTestGetByIdNotFound()
        {

            var context = new Shopbridge_Context(dbContextOptions);
            DummyData db = new DummyData();
            db.Seed(context);

            repository = new Repository<Product>(context);
            productService = new ProductService(repository, null);

            //Arrange  
            var controller = new ProductsController(productService, null);

            int id = 253;

            //Act  
            var data = await controller.GetProduct(id);

            //Assert  
            Assert.IsType<NotFoundResult>(data);
        }

        [Fact]
        public async void UnitTestUpdateNotFound()
        {
            var context = new Shopbridge_Context(dbContextOptions);
            DummyData db = new DummyData();
            db.Seed(context);

            repository = new Repository<Product>(context);
            productService = new ProductService(repository, null);

            //Arrange  
            var controller = new ProductsController(productService, null);

            int id = 253;
            Product product = new Product();
            product.Product_Id = 253;
            product.CreationDate = DateTime.Now;
            product.Price = 1;
            product.Name = "xUnit Test";
            product.Description = "xUnitTest";

            //Act  
            var data = await controller.PutProduct(id, product);

            //Assert  
            Assert.IsType<NotFoundResult>(data);
        }

        [Fact]
        public async void UnitTestUpdateSuccess()
        {

            var context = new Shopbridge_Context(dbContextOptions);
            DummyData db = new DummyData();
            db.Seed(context);

            repository = new Repository<Product>(context);
            productService = new ProductService(repository, null);

            //Arrange  
            var controller = new ProductsController(productService, null);

            int id = 1;
            Product product = new Product();
            product.Product_Id = 1;
            product.CreationDate = DateTime.Now;
            product.Price = 1;
            product.Name = "xUnit Test";
            product.Description = "xUnitTest";

            //Act  
            var data = await controller.PutProduct(id, product);

            //Assert  
            Assert.IsType<OkResult>(data);
        }

        [Fact]
        public async void UnitTestDeleteNotFound()
        {
            var context = new Shopbridge_Context(dbContextOptions);
            DummyData db = new DummyData();
            db.Seed(context);

            repository = new Repository<Product>(context);
            productService = new ProductService(repository, null);

            //Arrange  
            var controller = new ProductsController(productService, null);

            int id = 253;

            //Act  
            var data = await controller.DeleteProduct(id);

            //Assert  
            Assert.IsType<NotFoundResult>(data);
        }

        [Fact]
        public async void UnitTestDeleteSuccess()
        {

            var context = new Shopbridge_Context(dbContextOptions);
            DummyData db = new DummyData();
            db.Seed(context);

            repository = new Repository<Product>(context);
            productService = new ProductService(repository, null);

            //Arrange  
            var controller = new ProductsController(productService, null);

            int id = 1;

            //Act  
            var data = await controller.DeleteProduct(id);

            //Assert  
            Assert.IsType<OkResult>(data);
        }

    }
}