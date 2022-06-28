using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Project_two.Model
{
    public class Airline
    {
        public int airlineId { get; set; }
        public string airlineName { get; set; }

        public long airlineContactNumber { get; set; }

        public string airlineAddress { get; set; }

       
        public string airlineLogo { get; set; }
    }
}
