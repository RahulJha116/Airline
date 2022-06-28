using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Project_two.IJwtAuthentication;
using Project_two.Model;
using Project_two.Repository;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Project_two.Controller
{


    [Route("api/[controller]")]
    [ApiController]
    public class AirlineController : ControllerBase
    {
       
        private readonly IAirlineReository _airlineRepository;
        private readonly IJwtAuthenticationManager JwtAuthenticationManager;

        public AirlineController(IAirlineReository airlineReository, IJwtAuthenticationManager jwtAuthenticationManager,
            IHostingEnvironment hostingEnvironment)
        {
            _airlineRepository = airlineReository;
            this.JwtAuthenticationManager = jwtAuthenticationManager;
        }


        [HttpGet]
        public IActionResult Get()
        {
            var airlines = _airlineRepository.GetAirlines();
            return new OkObjectResult(airlines);
        }

        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(int id)
        {
            var airlines = _airlineRepository.GetAirlineByID(id);
            return new OkObjectResult(airlines);
        }

        // POST api/<AirlineController>
        [Authorize]
        [HttpPost("register")]
        //public IActionResult RegisterAirline([FromBody] Airline airline)
        //{
        //    using (var scope = new TransactionScope())
        //    {
        //        _airlineRepository.RegisterAirline(airline);
        //        scope.Complete();
        //        return CreatedAtAction(nameof(Get), new { id = airline.airlineId }, airline);
        //    }
        //}

        public IActionResult RegisterAirline([FromBody] Airline airline)
        {
            
            var factory = new ConnectionFactory
            {
                Uri = new Uri("amqp://guest:guest@localhost:5672")
            };
            using (var connection = factory.CreateConnection())
            using(var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "AddAirlineQueue",
                    durable: false,
                    exclusive: false,
                    autoDelete: false,
                    arguments: null);
                string message = airline.airlineName + "|"+ airline.airlineAddress + "|" + airline.airlineContactNumber + "|"
                    + airline.airlineLogo;
                var body = Encoding.UTF8.GetBytes((message));

                channel.BasicPublish(exchange: "",
                    routingKey: "AddAirlineQueue",
                    basicProperties: null,
                    body: body);
            }

            using (var scope = new TransactionScope())
            {
                _airlineRepository.RegisterAirline(airline);
                scope.Complete();
                return Ok();
                //CreatedAtAction(nameof(Get), new { id = airline.airlineId }, airline);
            }
        }

        // PUT 
        [Authorize]
        [HttpPut("updateAirline")]
        public IActionResult UpdateAirline([FromBody] Airline airline)
        {
            if (airline != null)
            {
                using (var scope = new TransactionScope())
                {
                    _airlineRepository.UpdateAirline(airline);
                    scope.Complete();
                    return new OkResult();
                }
            }
            return new NoContentResult();
        }

        // DELETE api/<airlineController>/5
        [Authorize]
        [HttpDelete("DeleteAirline")]
        public IActionResult DeleteAirline(int id)
        {
            _airlineRepository.DeleteAirline(id);
            return new OkResult();
        }

        [AllowAnonymous]
        [HttpPost("Adminlogin")]
        public IActionResult AdminLogin(string adminEmailId, string adminPasskey)
        {

            var token = JwtAuthenticationManager.Authenticate(adminEmailId, adminPasskey);
            var user = JsonConvert.SerializeObject(token);
            if (user == null)
            {
                return Unauthorized();
            }
            return Ok(user);
        }

       
        

       
    }
}
