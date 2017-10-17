using System;
using WorldWebServer.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Xunit;
using Newtonsoft.Json;

namespace WorldWebServer.Test
{
    public class EndToEndTest
    {
        private HttpClient GetHttpClient()
        {
            var httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("http://localhost:51306");
            var acceptType = new MediaTypeWithQualityHeaderValue("application/json");
            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.Accept.Add(acceptType);
            return httpClient;

        }
        
        private bool SameCity(City c1, City c2) {
            return c1.ID == c2.ID
                && c1.CountryCode == c2.CountryCode
                && c1.Name == c2.Name
                && c1.District == c2.District
                && c1.Population == c2.Population;
        }
        [Fact]
        public async void GetAllCountriesTest()
        {
            var httpClient = GetHttpClient();
            var response = await httpClient.GetAsync("api/countries");
            Assert.True(response.IsSuccessStatusCode);
        }
        [Fact]
        public async void GetAllCitiesTest()
        {
            var httpClient = GetHttpClient();
            var response = await httpClient.GetAsync("api/cities");
            Assert.True(response.IsSuccessStatusCode);
        }
        [Fact]
        public async void GetCityByIdTest() {
            var httpClient = GetHttpClient();
            var response = await httpClient.GetAsync("api/cities/3816");
            Assert.True(response.IsSuccessStatusCode);
            var json = await response.Content.ReadAsStringAsync();
            var city = JsonConvert.DeserializeObject<City>(json);
            Assert.Equal(3816, city.ID);
            Assert.Equal("Seattle", city.Name);
            Assert.Equal("Washington",city.District);
            Assert.Equal("USA",city.CountryCode);
        }
        [Fact]
        public async void GetCityByCountryCodeTest()
        {
            var httpClient = GetHttpClient();
            var response = await httpClient.GetAsync("api/cities/cc/USA");
            Assert.True(response.IsSuccessStatusCode);
        }
        [Fact]
        public async void PostCityTest()
        {
            var httpClient = GetHttpClient();
            var city1 = new City { ID = 0, Name = "Kirkland", District = "Washington", CountryCode = "USA", Population = 88888 };
            var json = JsonConvert.SerializeObject(city1);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync("api/cities",content);
            Assert.True(response.IsSuccessStatusCode);
            var json2 = await response.Content.ReadAsStringAsync();
            var city2 = JsonConvert.DeserializeObject<City>(json2);
            city1.ID = city2.ID;
            Assert.True(SameCity(city1, city2));

        }
        [Fact] 
        public async void PutCityTest()
        {
            var httpClient = GetHttpClient();
            var city1 = new City { ID = 0, Name = "Kirkland", District = "Washington", CountryCode = "USA", Population = 88888 };
            var json = JsonConvert.SerializeObject(city1);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync("api/cities", content);
            Assert.True(response.IsSuccessStatusCode);
            var json2 = await response.Content.ReadAsStringAsync();
            var city2 = JsonConvert.DeserializeObject<City>(json2);
            city2.Population = 99999;
            json2 = JsonConvert.SerializeObject(city2);
            var content2 = new StringContent(json2, Encoding.UTF8, "application/json");
            var response2 = await httpClient.PutAsync($"api/cities/{city2.ID}", content2);
            Assert.True(response2.IsSuccessStatusCode);
            var response3 = await httpClient.GetAsync($"api/cities/{city2.ID}");
            var json3 = await response3.Content.ReadAsStringAsync();
            var city3 = JsonConvert.DeserializeObject<City>(json3);
            Assert.Equal(city3.Population, city2.Population);
        }

        [Fact]
        public async void DeleteTest()
        {
            var httpClient = GetHttpClient();
            var city1 = new City { ID = 0, Name = "Kirkland", District = "Washington", CountryCode = "USA", Population = 88888 };
            var json = JsonConvert.SerializeObject(city1);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync("api/cities", content);
            Assert.True(response.IsSuccessStatusCode);
            var json2 = await response.Content.ReadAsStringAsync();
            var city2 = JsonConvert.DeserializeObject<City>(json2);
            var response2 = await httpClient.DeleteAsync($"api/cities/{city2.ID}");
            Assert.True(response2.IsSuccessStatusCode);
            var response3 = await httpClient.DeleteAsync($"api/cities/{city2.ID}");
            Assert.False(response3.IsSuccessStatusCode);
        }
    }

}

