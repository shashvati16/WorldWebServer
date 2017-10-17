using Microsoft.AspNetCore.Mvc;
using System;
using WorldWebServer.Controllers;
using WorldWebServer.Models;
using Xunit;

namespace WorldWebServer.Test
{
    public class FunctionTest
    {
        [Fact]
        public void GetCountriesSmokeTest()
        {
            var controller = new CountriesController();
            Assert.IsType<OkObjectResult>(controller.Get());
        }
        [Fact]
        public void GetCitiesSmokeTest()
        {
            var controller = new CitiesController();
            Assert.IsType<OkObjectResult>(controller.Get());
        }
        [Fact]
        public void GetCityByIDTest() {
            var controller = new CitiesController();
            var result = controller.Get(3816) as OkObjectResult;
            var city = result.Value as City;
            Assert.Equal(3816, city.ID);
            Assert.Equal("Seattle", city.Name);
            Assert.Equal("Washington", city.District);
            Assert.Equal("USA", city.CountryCode);
        }
        [Fact]
        public void GetCityByCCTest()
        {
            var controller = new CitiesController();
            Assert.IsType<OkObjectResult>(controller.Get("USA"));
        }
        [Fact]
        public void PostCityTest()
        {
            var controller = new CitiesController();
            var city1 = new City { ID = 0, Name = "Kitland", District = "Washington", CountryCode = "USA", Population = 88888 };
            Assert.IsType<CreatedResult>(controller.Post(city1));
        }
        [Fact]
        public void PutCityTest()
        {
            var controller = new CitiesController();
            var city1 = new City { ID = 4088, Name = "Kittland", District = "Washington", CountryCode = "USA", Population = 888908 };
            Assert.IsType<OkResult>(controller.Put(4088, city1));
        }
        [Fact]
        public void DeleteCityTest()
        {
            var controller = new CitiesController();
            Assert.IsType<OkResult>(controller.Delete(4088));
        }

    }
}
