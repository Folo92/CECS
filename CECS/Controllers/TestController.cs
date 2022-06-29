using BLL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CECS.Controllers
{
    public class TestController : ApiController
    {
        IBuildingService BuildingService { get; set; }  //通过Spring.Net容器实例化
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            var buildingList = BuildingService.LoadEntities(u => u.BuildingID == u.BuildingID).ToList();
            foreach(var building in buildingList)
            {
                yield return Common.SerializeHelper.SerializeToString(building);
            }
            yield break;
            //return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            var building = BuildingService.LoadEntities(u => u.BuildingID == id).FirstOrDefault();
            return Common.SerializeHelper.SerializeToString(building);
            //return Json(building);
            //return building.StructureSystem.ToString();
        }

        // POST api/<controller>
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}
