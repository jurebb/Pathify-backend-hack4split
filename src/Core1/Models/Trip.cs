using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Core1.Models
{
    public class Trip
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Descripton { get; set; }
        public DateTime DateCreated { get; set; }
        public string UserName { get; set; }

        //public ICollection<Stop> Stops { get; set; }
        public string Stops { get; set; }

        public ICollection<Image> Images { get; set; }
    }
}
