using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using FluentValidationRemoteClientSideValidation.Models;

namespace FluentValidationRemoteClientSideValidation.Controllers
{
    [RoutePrefix("api/validation")]
    public class ValidationController : ApiController
    {
        private readonly IApplicationDbContext dbContext;

        public ValidationController(IApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [Route("uniqueemail")]
        [HttpGet]
        public async Task<IHttpActionResult> ValidateUniqueEmail(string email)
        {
            bool validationResult = true;

            if (!String.IsNullOrEmpty(email))
            {
                var user = await dbContext.Users.Where(u => u.Email == email)
                                            .FirstOrDefaultAsync();

                validationResult = (user == null);
            }

            return Ok(validationResult);
        }

    }
}
