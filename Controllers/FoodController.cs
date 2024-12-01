using ApiCatering.Models;
using Microsoft.AspNetCore.Mvc;
using MySql.Data.MySqlClient;
using System.Collections.Generic;

namespace ApiCatering.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodController : ControllerBase
    {
        private readonly string? _connectionString;

        public FoodController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("FoodMenuConnection");
        }

        // GET: api/food
        [HttpGet]
        public ActionResult<IEnumerable<FoodItem>> GetFoodItems()
        {
            var foodList = new List<FoodItem>();

            using (var connection = new MySqlConnection(_connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Foods";

                using (var command = new MySqlCommand(query, connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var food = new FoodItem
                        {
                            Id = reader.GetInt32("id"),
                            Title = reader.GetString("title"),
                            Price = reader.GetString("price"),
                            Description = reader.GetString("description"),
                            Image = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes(reader.GetString("image")))
                        };
                        foodList.Add(food);
                    }
                }
            }

            return Ok(foodList);
        }
    }
}
