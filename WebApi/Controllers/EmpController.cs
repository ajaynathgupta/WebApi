using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebApi.Models;

namespace WebApi.Controllers
{
    [Authorize]
    public class EmpController : ApiController
    {
        TESTEntitiesContext TestDB = new TESTEntitiesContext();
        //GET api/values
        public IEnumerable<EMP> Get()
        {

            return TestDB.EMPs.AsEnumerable().ToList();
        }

    }
}
