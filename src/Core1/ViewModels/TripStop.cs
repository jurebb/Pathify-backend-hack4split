using Core1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core1.ViewModels
{
    public class TripStop
    {
        public string Name { get; set; }
        public string Descripton { get; set; }
        public DateTime DateCreated { get; set; }
        public string UserName { get; set; }
        public List<Stop> Stops { get; set; }
        public ICollection<Image> Images { get; set; }
    }
}
