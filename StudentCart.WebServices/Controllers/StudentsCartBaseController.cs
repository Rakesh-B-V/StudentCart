using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace StudentCart.WebServices.Controllers
{
    [ApiController]
    public class StudentsCartBaseController : ControllerBase
    {
        #region Constructors
        public StudentsCartBaseController()
        {
        }
        #endregion
        protected virtual IActionResult GetSuccessResponse<T>(T value)
        {
            return Ok(value);
        }
        protected virtual IActionResult GetSuccessResponse<T>(T value, HttpStatusCode httpStatusCode)
        {
            return StatusCode((int)httpStatusCode, value);
        }
    }
}
