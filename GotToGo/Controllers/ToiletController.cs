using GotToGo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace GotToGo.Controllers
{
    public class ToiletController : ApiController
    {
        private static List<Toilet> toilets = new List<Toilet>
        {
            new Toilet { Name = "Poulton Park", Lat = -33.97942512, Lng = 151.0982179 },
            new Toilet { Name = "Helipad - Dr Reid Park", Lat = -28.62834519, Lng = 153.0017754 },
            new Toilet { Name = "Federal Park", Lat = -32.89961804, Lng = 151.6701746 }
        };

        public IEnumerable<Toilet> Get()
        {
            return toilets;
        }
    }
}
