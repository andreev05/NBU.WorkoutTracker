using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NBU.WorkoutTracker.Core.Contracts;
using NBU.WorkoutTracker.Core.ViewModels;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NBU.WorkoutTracker.Controllers
{
    /// <summary>
    /// Used only to create new users through an API
    /// </summary>
    [Route("api/[controller]")]
    public class UserApiController : Controller
    {

        IAdmin adminService;

        public UserApiController(IAdmin adminService)
        {
            this.adminService = adminService;
        }

        // POST api/<controller>
        [HttpPost]
        public async Task<HttpResponseMessage> Create([FromBody]CreateUserViewModel vm)
        {
            try
            {
                await adminService.CreateUser(vm);
                return new HttpResponseMessage(HttpStatusCode.Created);
            }
            catch
            {
                return new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }            
        }
    }
}
