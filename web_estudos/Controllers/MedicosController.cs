using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace web_estudos.Controllers
{
    public class MedicosController : ApiController
    {
        // GET: api/Medicos
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Medicos/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Medicos
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Medicos/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Medicos/5
        public void Delete(int id)
        {
        }
    }
}
