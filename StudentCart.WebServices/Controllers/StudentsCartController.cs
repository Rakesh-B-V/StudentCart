﻿using Microsoft.AspNetCore.Mvc;
using StudentCart.Repository.Business.Contracts;
using StudentCart.Repository.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StudentCart.WebServices.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class StudentsCartController : StudentsCartBaseController
    {
        private readonly IStudentsCartManager _studentsCart;

        public StudentsCartController(IStudentsCartManager studentsCart)
        {
            this._studentsCart = studentsCart;
        }


        [HttpGet]
        [Route("v{version:apiVersion}/CategoryDetails")]
        public async Task<IActionResult> CategoryDetails()
        {
            IActionResult httpResponse = null;
            try
            {
                var categoryList = await _studentsCart.GetCategoriesList();

                if(categoryList != null && categoryList.Any())
                {
                    httpResponse = GetSuccessResponse(categoryList);
                }
                else
                {
                    httpResponse = GetSuccessResponse(AppConstatnts.RECORDNOTFOUND, HttpStatusCode.NotFound);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {

            }
            return httpResponse;
        }

        [HttpPost]
        [Route("v{version:apiVersion}/SignUp")]
        public async Task<IActionResult> SignUp([FromBody] SignUp signUp)
        {
            IActionResult httpResponse = null;
            List<String> errorList = new List<String>();
            String finalErrors = String.Empty;
            try
            {
                if(ModelState.ErrorCount > 0)
                {
                    errorList = ModelState.ToList().Where(s=> s.Value.ValidationState.ToString() == "InValid").ToList().SelectMany(x => x.Value.Errors).ToList().Select(s => s.ErrorMessage).ToList();
                    finalErrors = String.Join(",\n", errorList);
                    throw new CustomException(finalErrors);
                }

                var result = await _studentsCart.SignUpProcess(signUp.UserName, signUp.Password);

                if(result != null && result.Contains(AppConstatnts.ACCOUNTCREATED))
                {
                    httpResponse = GetSuccessResponse(result);
                }
                else
                {
                    httpResponse = GetSuccessResponse(AppConstatnts.DUPLICATEUSER, HttpStatusCode.Forbidden);
                }

            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {

            }
            return httpResponse;
        }


        [HttpPost]
        [Route("v{version:apiVersion}/LogIn")]
        public async Task<IActionResult> LogIn([FromBody] LogIn logIn)
        {
            IActionResult httpResponse = null;
            try
            {
                var result = await _studentsCart.LogInProcess(logIn.UserName, logIn.Password);
                if(result != null && result.Contains(AppConstatnts.AUTHENTICATIONSUCCESSFUL))
                {
                    httpResponse = GetSuccessResponse(result);
                }
                else
                {
                    httpResponse = GetSuccessResponse(result, HttpStatusCode.Unauthorized);
                }
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return httpResponse;
        }

        [HttpGet]
        [Route("v{version:apiVersion}/ProductDetails")]
        public async Task<IActionResult> ProductDetails(String product)
        {
            IActionResult httpResponse = null;
            Object finalResult = null;
            try
            {
                if (!String.IsNullOrEmpty(product))
                {
                    switch (product)
                    {
                        case AppConstatnts.BICYCLES:
                            {
                                var result = await _studentsCart.BicycleDetails(product);
                                finalResult = result;
                                break;
                            }
                        case AppConstatnts.HOUSEHOLDITEMS:
                            {
                                var result = await _studentsCart.HouseItemDetails(product);
                                finalResult = result;
                                break;
                            }
                        case AppConstatnts.ACCOMODATIONSERVICES:
                            {
                                var result = await _studentsCart.AccomodationDetails(product);
                                finalResult = result;
                                break;
                            }
                        case AppConstatnts.BOOKS:
                            {
                                var result = await _studentsCart.BooksDetails(product);
                                finalResult = result;
                                break;
                            }
                        default:
                            { 
                                finalResult = "Please Select the item from available Category only";
                                break;
                            }
                    }
                    if (finalResult != null)
                    {
                        httpResponse = GetSuccessResponse(finalResult);
                    }
                    else
                    {
                        httpResponse = GetSuccessResponse(finalResult, HttpStatusCode.NotFound);
                    }
                }
                else
                {
                    finalResult = "Please Select the item from available Category only";
                    httpResponse = GetSuccessResponse(finalResult, HttpStatusCode.NotFound);
                }
                
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {

            }
            return httpResponse;
        }

        [HttpPost]
        [Route("v{version:apiVersion}/AddProductAsync")]
        public async Task<IActionResult> AddProductAsync(String productName, [FromBody] Dictionary<String, String> details)
        {
            IActionResult httpResponse = null;
            String result = String.Empty;
            try
            {
                if (!String.IsNullOrEmpty(productName))
                {
                    switch (productName.ToLower())
                    {
                        case AppConstatnts.ACCOMODATIONSERVICES:
                            {
                                result = await _studentsCart.AddAccomodationService(details);
                                break;
                            }
                        case AppConstatnts.BICYCLES:
                            {
                                result = await _studentsCart.AddBicycle(details);
                                break;
                            }
                        case AppConstatnts.HOUSEHOLDITEMS:
                            {
                                result = await _studentsCart.AddHouseHoldItems(details);
                                break;
                            }
                        case AppConstatnts.BOOKS:
                            {
                                result = await _studentsCart.AddBook(details);
                                break;
                            }
                        default:
                            {
                                result = "Please Select only available Products";
                                httpResponse = GetSuccessResponse(result, HttpStatusCode.NotFound);
                                break;
                            }
                    }
                }
                else
                {
                    result = "Product Name should not be empty";
                    httpResponse = GetSuccessResponse(result, HttpStatusCode.NotFound);
                }
                if (!String.IsNullOrEmpty(result) && result.Contains("Added Successfully"))
                {
                    httpResponse = GetSuccessResponse(result);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {

            }
            return httpResponse;
        }
    }
}
