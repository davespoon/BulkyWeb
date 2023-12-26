using System.Collections.Generic;
using System.Linq;
using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using BulkyWeb.Areas.Admin.Controllers;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace BulkyTests;

public class ProductTests
{
    private List<Product> _products;


    [SetUp]
    public void Setup()
    {
        _products = new()
        {
            new Product
            {
                Id = 1,
                Title = "Fortune of Time",
                Author = "Billy Spark",
                Description =
                    "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
                ISBN = "SWD9999001",
                ListPrice = 99,
                Price = 90,
                Price50 = 85,
                Price100 = 80,
                CategoryId = 1
            },
            new Product
            {
                Id = 2,
                Title = "Dark Skies",
                Author = "Nancy Hoover",
                Description =
                    "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
                ISBN = "CAW777777701",
                ListPrice = 40,
                Price = 30,
                Price50 = 25,
                Price100 = 20,
                CategoryId = 1
            },
            new Product
            {
                Id = 3,
                Title = "Vanish in the Sunset",
                Author = "Julian Button",
                Description =
                    "Praesent vitae sodales libero. Praesent molestie orci augue, vitae euismod velit sollicitudin ac. Praesent vestibulum facilisis nibh ut ultricies.\r\n\r\nNunc malesuada viverra ipsum sit amet tincidunt. ",
                ISBN = "RITO5555501",
                ListPrice = 55,
                Price = 50,
                Price50 = 40,
                Price100 = 35,
                CategoryId = 1
            }
        };
    }

    [Test]
    public void ProductControllerIndexTest()
    {
        var mock = new Mock<IUnitOfWork>();
        mock.Setup(p => p.Product.GetAll()).Returns(_products);
        var controller = new ProductController(mock.Object);

        var result = controller.Index();

        result.Should().BeOfType<ViewResult>();
       
        var viewResult = result as ViewResult;
        viewResult?.ViewData.Model.Should().BeAssignableTo<IEnumerable<Product>>();
        
        var model = viewResult?.ViewData.Model as IEnumerable<Product>;
        model?.Count().Should().Be(3);
    }
}