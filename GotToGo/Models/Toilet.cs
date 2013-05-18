using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GotToGo.Models
{
    public class Toilet
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Lat { get; set; }
        public decimal Lng { get; set; }
    }
}