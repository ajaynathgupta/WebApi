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

			return new List<EMP>() {
				new EMP{ID=101,NAME="Amar",CITY="Delhi",SALARY=5000000 },
				new EMP{ID=102,NAME="Ankit",CITY="Mumbai",SALARY=30000 },
				new EMP{ID=103,NAME="Ajeet",CITY="Delhi",SALARY=40000 },
				new EMP{ID=104,NAME="Arun",CITY="New York",SALARY=50000}
			};
        }


		public partial class EMPff
		{
			public int ID { get; set; }
			public string NAME { get; set; }
			public string CITY { get; set; }
			public Nullable<decimal> SALARY { get; set; }
		}

	}
}
