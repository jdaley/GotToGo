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
        public IEnumerable<Toilet> Get()
        {
            using (GotToGoContext context = new GotToGoContext())
            {
                return context.Toilets.ToList();
            }
        }
    }
}
