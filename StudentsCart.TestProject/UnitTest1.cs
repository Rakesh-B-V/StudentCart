using Microsoft.AspNetCore.Mvc;
using Moq;
using StudentCart.Repository.Business.Contracts;
using StudentCart.Repository.Business.Models;
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
        private Mock<IStudentsCartManager> _mockIStudentsCartManager;
        public UnitTest1()
        {
            _mockIStudentsCartManager = new Mock<IStudentsCartManager>();
        }

        [Fact]
        public async Task SignUpProcess_Returns_TrueResult()
        {
            var expectedOutput = AppConstatnts.ACCOUNTCREATED;
            var signUp = new SignUp()
            {
                Password = "rAgjuhu67",
                UserName = "marietgrg"
            };

            _mockIStudentsCartManager.Setup(repo => repo.SignUpProcess(signUp.UserName, signUp.Password)).ReturnsAsync(expectedOutput);

            var studentsCartController = new StudentsCartController(_mockIStudentsCartManager.Object);

            var result = await studentsCartController.SignUp(signUp);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<String>(okResult.Value);
            Assert.Equal(AppConstatnts.ACCOUNTCREATED, returnValue);
        }

        [Fact]
        public async Task SignUpProcess_Returns_FalseResult()
        {
            var expectedOutput = AppConstatnts.DUPLICATEUSER;

            var signup = new SignUp()
            {
                Password = "rAgjuhu67",
                UserName = "marietgrg"
            };
            _mockIStudentsCartManager.Setup(repo => repo.SignUpProcess(signup.UserName, signup.Password)).ReturnsAsync(expectedOutput);

            var studentsCartController = new StudentsCartController(_mockIStudentsCartManager.Object);
            studentsCartController.ModelState.AddModelError(AppConstatnts.DUPLICATEUSER, AppConstatnts.DUPLICATEUSER);
            var result = await studentsCartController.SignUp(signup);

            var okResult = Assert.IsType<ObjectResult>(result);
            var returnValue = Assert.IsType<String>(okResult.Value);
            Assert.Equal("User name already exists! Please modify the User name", returnValue);
        }

        [Fact]
        public async Task Login_Returns_TrueResult()
        {
            var expectedOutput = new String("Authentication Successful");

            var logIn = new LogIn()
            {
                Password = "rAgjuhu67",
                UserName = "marietgrg"
            };
            _mockIStudentsCartManager.Setup(repo => repo.LogInProcess(logIn.UserName, logIn.Password)).ReturnsAsync(expectedOutput);

            var studentsCartController = new StudentsCartController(_mockIStudentsCartManager.Object);


            var result = await studentsCartController.LogIn(logIn);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<String>(okResult.Value);
            Assert.Equal("Authentication Successful", returnValue);
        }


        [Fact]
        public async Task Login_Returns_FalseResult()
        {
            var expectedOutput = new String("Incorrect UserName or Password");

            var logIn = new LogIn()
            {
                Password = null,
                UserName = null
            };

            _mockIStudentsCartManager.Setup(repo => repo.LogInProcess(null, null)).ReturnsAsync(expectedOutput);

            var studentsCartController = new StudentsCartController(_mockIStudentsCartManager.Object);


            var result = await studentsCartController.LogIn(logIn);

            var Result = Assert.IsType<ObjectResult>(result);
            var returnValue = Assert.IsType<String>(Result.Value);
            Assert.Equal("Incorrect UserName or Password", returnValue);
        }
        [Fact]
        public async Task CategoryDetails_Returns_FalseResult()
        {
            //Arrange
            _mockIStudentsCartManager.Setup(repo => repo.GetCategoriesList())
                .ReturnsAsync((List<String>)null);
            var studentsCartController = new StudentsCartController(_mockIStudentsCartManager.Object);

            //Act
            var result = await studentsCartController.CategoryDetails();

            //Assert
            Assert.IsType<ObjectResult>(result);
        }

        
        [Fact]
        public async Task CategoryDetails_Returns_TrueResult()
        {
            //Arrange
            var expectedOutput = new List<String>() { "Bicycles", "AccomodationServices", "HouseHoldItems", "Books" };
            _mockIStudentsCartManager.Setup(repo => repo.GetCategoriesList()).ReturnsAsync(expectedOutput);
            var studentsCartController = new StudentsCartController(_mockIStudentsCartManager.Object);

            //Act
            var result = await studentsCartController.CategoryDetails();

            //Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<String>>(okResult.Value);
            var category = returnValue.FirstOrDefault();
            Assert.Equal("Bicycles", category);
        }

        

        [Fact]
        public async Task ProductDetails_Returns_TrueResult()
        {

            
            var bicyclesList = new List<Bicycles>();
            var bicycle1 = new Bicycles()
            {
                OwnerName = "Daniel",
                Category = "Hybrid",
                OwnerNumber = "085230897",
                Price = "$80"
            };
            var bicycle2 = new Bicycles()
            {
                OwnerName = "Angel",
                Category = "Hybrid",
                OwnerNumber = "085230898",
                Price = "$125"
            };
            var bicycle3 = new Bicycles()
            {
                OwnerName = "mike",
                Category = "Hybrid",
                OwnerNumber = "085678901",
                Price = "$200"
            };
            bicyclesList.Add(bicycle1);
            bicyclesList.Add(bicycle2);
            bicyclesList.Add(bicycle3);


            var expectedOutput = bicyclesList;

            _mockIStudentsCartManager.Setup(repo => repo.BicycleDetails("Bicycles")).ReturnsAsync(expectedOutput);

            var studentsCartController = new StudentsCartController(_mockIStudentsCartManager.Object);

            var result = await studentsCartController.ProductDetails("Bicycles");

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<List<Bicycles>>(okResult.Value);
            Assert.Equal(bicyclesList, returnValue);

        }

        [Fact]
        public async Task ProductDetails_Returns_FalseResult()
        {

            
            Object expectedOutput = AppConstatnts.SELECTAVAILABLECATEGORY;
            _mockIStudentsCartManager.Setup(repo => repo.BicycleDetails("")).ReturnsAsync(new List<Bicycles>());

            var studentsCartController = new StudentsCartController(_mockIStudentsCartManager.Object);

            var result = await studentsCartController.ProductDetails(null);

            var okResult = Assert.IsType<ObjectResult>(result);
            var returnValue = Assert.IsType<String>(okResult.Value);
            Assert.Equal(AppConstatnts.SELECTAVAILABLECATEGORY, returnValue);

        }
        [Fact]
        public async Task AddProductAsync_Returns_TrueResult()
        {
            var expectedOutput = AppConstatnts.ADDACCOMODATIONSUCCESSFUL;

            var productName = "AccomodationServices";
            Dictionary<String, String> details = new Dictionary<string, string>();
            details.Add("OwnerName", "Boss");
            details.Add("OwnerNumber", "0987215643");
            details.Add("Address", "Kilkenny");
            details.Add("ApartmentType", "Three Room");
            details.Add("Price", "1200 Euro/Month");

            _mockIStudentsCartManager.Setup(repo => repo.AddAccomodationService(details)).ReturnsAsync(expectedOutput);

            var studentsCartController = new StudentsCartController(_mockIStudentsCartManager.Object);

            var result = await studentsCartController.AddProductAsync(productName, details);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<String>(okResult.Value);
            Assert.Equal(AppConstatnts.ADDACCOMODATIONSUCCESSFUL, returnValue);
        }
        [Fact]
        public async Task AddProductAsync_Returns_FalseResult()
        {
            var expectedOutput = AppConstatnts.SELECTAVAILABLEPRODUCTS;

            var productName = "Buses";
            Dictionary<String, String> details = new Dictionary<string, string>();

            _mockIStudentsCartManager.Setup(repo => repo.AddAccomodationService(details)).ReturnsAsync(expectedOutput);

            var studentsCartController = new StudentsCartController(_mockIStudentsCartManager.Object);

            var result = await studentsCartController.AddProductAsync(productName, details);

            var okResult = Assert.IsType<ObjectResult>(result);
            var returnValue = Assert.IsType<String>(okResult.Value);
            Assert.Equal(AppConstatnts.SELECTAVAILABLEPRODUCTS, returnValue);
        }

        [Fact]
        public async Task EditProductDetailsAsync_Returns_TrueResult()
        {
            var expectedOutput = AppConstatnts.EDITACCOMODATIONSUCCESSFUL;

            UpdateItem updateDetails = new UpdateItem()
            {
                ContactNumber = "0764957567",
                NewContactNumber = "0879915157",
                Price = "130 Euro"
            };

            _mockIStudentsCartManager.Setup(repo => repo.EditAccomodationService(updateDetails.ContactNumber, "camera", updateDetails.Price,
                                                    AppConstatnts.ACCOMODATIONSERVICES, updateDetails.NewContactNumber)).ReturnsAsync(expectedOutput);

            var studentsCartController = new StudentsCartController(_mockIStudentsCartManager.Object);

            var result = await studentsCartController.EditProductDetailsAsync(AppConstatnts.ACCOMODATIONSERVICES, "camera", updateDetails);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<String>(okResult.Value);
            Assert.Equal(AppConstatnts.EDITACCOMODATIONSUCCESSFUL, returnValue);
        }
        [Fact]
        public async Task EditProductDetailsAsync_Returns_FalseResult()
        {
            var expectedOutput = "Please select the right Category";

            var productName = "KSRTC";
            UpdateItem updateDetails = new UpdateItem();

            _mockIStudentsCartManager.Setup(repo => repo.EditAccomodationService(updateDetails.ContactNumber, productName, updateDetails.Price, "Bus", updateDetails.NewContactNumber)).ReturnsAsync(expectedOutput);

            var studentsCartController = new StudentsCartController(_mockIStudentsCartManager.Object);

            var result = await studentsCartController.EditProductDetailsAsync("Bus", productName, updateDetails);

            var okResult = Assert.IsType<ObjectResult>(result);
            var returnValue = Assert.IsType<String>(okResult.Value);
            Assert.Equal("Please select the right Category", returnValue);
        }
        [Fact]
        public async Task DeleteProductDetails_Returns_TrueResult()
        {
            var expectedOutput = AppConstatnts.DELETEBICYCLESUCCESSFUL;
            

            var bicycles = new Bicycles()
            {
                OwnerName = "mike",
                Category = "bicycles",
            };

            _mockIStudentsCartManager.Setup(repo => repo.DeleteBicycle(bicycles.OwnerName, bicycles.Category)).ReturnsAsync(expectedOutput);

            var studentsCartController = new StudentsCartController(_mockIStudentsCartManager.Object);

            var result = await studentsCartController.DeleteProductDetailsAsync(AppConstatnts.BICYCLES, bicycles.OwnerName, bicycles.Category);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<String>(okResult.Value);
            Assert.Equal("Bicycle Deleted Successfully", returnValue);
        }

        [Fact]
        public async Task DeleteProductDetails_Returns_FalseResult()
        {
            var expectedOutput = AppConstatnts.DELETEDETAILSREQUIRED;
            

            var bicycles = new Bicycles()
            {
                OwnerName = "mike",
                Category = "bicycles",
                OwnerNumber = "09876543"
            };

            _mockIStudentsCartManager.Setup(repo => repo.DeleteBicycle(bicycles.OwnerName, bicycles.Category)).ReturnsAsync(expectedOutput);

            var studentsCartController = new StudentsCartController(_mockIStudentsCartManager.Object);

            var result = await studentsCartController.DeleteProductDetailsAsync(null, bicycles.OwnerName, bicycles.Category);

            var okResult = Assert.IsType<ObjectResult>(result);
            var returnValue = Assert.IsType<String>(okResult.Value);
            Assert.Equal(AppConstatnts.DELETEDETAILSREQUIRED, returnValue);
        }

        [Fact]
        public async Task LogOut_Returns_TrueResult()
        {
            var expectedOutput = new String("Added Successfully");
            

            var signup = new SignUp()
            {
                Password = "rAgjuhu67",
                UserName = "marietgrg"
            };

            _mockIStudentsCartManager.Setup(repo => repo.LogOutProcess(signup.UserName, signup.Password)).ReturnsAsync(expectedOutput);

            var studentsCartController = new StudentsCartController(_mockIStudentsCartManager.Object);

            var result = await studentsCartController.LogOut(signup);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var returnValue = Assert.IsType<String>(okResult.Value);
            Assert.Equal("Added Successfully", returnValue);
        }

        [Fact]
        public async Task LogOut_Returns_FailResult()
        {
            var expectedOutput = AppConstatnts.USERNAMEMANDATORY;
            

            var signup = new SignUp()
            {
                Password = null,
                UserName = null
            };
            _mockIStudentsCartManager.Setup(repo => repo.LogOutProcess(null, null)).ReturnsAsync(expectedOutput);

            var studentsCartController = new StudentsCartController(_mockIStudentsCartManager.Object);

            var result = await studentsCartController.LogOut(signup);

            var okResult = Assert.IsType<ObjectResult>(result);
            var returnValue = Assert.IsType<String>(okResult.Value);
            Assert.Equal(AppConstatnts.USERNAMEMANDATORY, returnValue);
        }
        
    }
}
