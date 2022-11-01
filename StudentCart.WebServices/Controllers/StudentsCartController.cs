using Microsoft.AspNetCore.Mvc;
using StudentCart.Repository.Business.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StudentCart.WebServices.Controllers
{
    //[Route("api/[controller]")]
    [ApiController]
    [ApiVersion("1.0")]
    public class StudentsCartController : ControllerBase
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
                    return Ok(categoryList);
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {

            }
            return httpResponse;
        }
    }
}
