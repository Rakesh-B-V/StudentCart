using Microsoft.AspNetCore.Mvc;
using Moq;
using StudentCart.Repository.Business.Contracts;
using StudentCart.WebServices.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace StudentsCart.TestProject
{
    public class UnitTest1
    {
        [Fact]
        public async Task CategoryDetails_Returns_TrueResult()
        {
            var expectedOutput = new List<String>() { "Bicycles", "AccomodationServices", "HouseHoldItems", "Books" };
            var mockIStudentsCartManager = new Mock<IStudentsCartManager>();
            mockIStudentsCartManager.Setup(repo => repo.GetCategoriesList()).ReturnsAsync(expectedOutput);

            var studentsCartController = new StudentsCartController(mockIStudentsCartManager.Object);

            var result = await studentsCartController.CategoryDetails();

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<String>>(okResult.Value);
            var category = returnValue.FirstOrDefault();
            Assert.Equal("Bicycles", category);
        }

        [Fact]
        public async Task CategoryDetails_Returns_FalseResult()
        {
            var mockIStudentsCartManager = new Mock<IStudentsCartManager>();
            mockIStudentsCartManager.Setup(repo => repo.GetCategoriesList())
                .ReturnsAsync((List<String>)null);

            var studentsCartController = new StudentsCartController(mockIStudentsCartManager.Object);

            //var result = await studentsCartController.CategoryDetails();

            var result = await studentsCartController.CategoryDetails();

            Assert.IsType<ObjectResult>(result);
        }

        //[Fact]
        //public async Task SignUpProcess_Returns_TrueResult()
        //{
        //    var expectedOutput = new String { "" };
        //    var mockIStudentsCartManager = new Mock<IStudentsCartManager>();
        //    mockIStudentsCartManager.Setup(repo => repo.SignUpProcess()).ReturnsAsync(expectedOutput);

        //    var studentsCartController = new StudentsCartController(mockIStudentsCartManager.Object);

        //    var result = await studentsCartController.CategoryDetails();

        //    var okResult = Assert.IsType<OkObjectResult>(result);
        //    var returnValue = Assert.IsType<List<String>>(okResult.Value);
        //    var category = returnValue.FirstOrDefault();
        //    Assert.Equal("Bicycles", category);
        //}
    }
}
