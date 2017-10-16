using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WorldWebServer.Models;

namespace WorldWebServer.Controllers
{
    [Route("api/[controller]")]
    public class CitiesController : Controller
    {
        private WorldDbContext cityDbContext;

        public CitiesController() {
            string connectionString = "server=localhost;port=3306;database=world;userid=root;pwd=68windingbrook;sslmode=none";
            this.cityDbContext = WorldDbContextFactory.Create(connectionString);
        }
        // api/cities
        [HttpGet]
        public ActionResult Get()
        {
            return Ok(cityDbContext.City.ToArray());
        }
        // api/cities/8
        [HttpGet("{id}")]
        public ActionResult Get(int id) {
            var city = cityDbContext.City.SingleOrDefault(c => c.ID == id);
            if (city != null){
                return Ok(city);
            }
            else{
                return NotFound();
            }
        }
        [HttpGet("cc/{cc}")]
        public ActionResult Get(string cc)
        {
            var cities = this.cityDbContext.City
            .Where(ct => string.Equals(ct.CountryCode, cc, StringComparison.CurrentCultureIgnoreCase))
            .ToArray();
            return Ok(cities);
        }

        // POST api/cities
        [HttpPost]
        public ActionResult Post([FromBody]City city) {
            if (!ModelState.IsValid)
                return BadRequest();
            cityDbContext.City.Add(city);
            cityDbContext.SaveChanges();
            return Created("api/cities/{city.ID}", city);               
            
        }
        // PUT api/cities/8
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody]City city) {
            if (!ModelState.IsValid) {
                return BadRequest();
            }
            var target = cityDbContext.City.SingleOrDefault(ct=>ct.ID == id);
            if (target != null && ModelState.IsValid)
            {
                cityDbContext.Entry(target).CurrentValues.SetValues(city);
                cityDbContext.SaveChanges();
                return Ok();
            }
            else {
                return NotFound();
            }
            
        }
        // delete api/cities/8
        [HttpDelete("{id}")]
        public ActionResult Delete(int id) {
            var target = cityDbContext.City.SingleOrDefault(c => c.ID == id);
            if (target != null) {
                cityDbContext.City.Remove(target);
                cityDbContext.SaveChanges();
                return Ok();
            }
            else{
                return NotFound();
            }
        }

    }
}
