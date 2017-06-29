using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MySQL.Data.EntityFrameworkCore.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Core1.ViewModels;
using MySql.Data.MySqlClient;

namespace Core1.Models
{
    /// <summary>
    /// The entity framework context with a Trips DbSet
    /// </summary>
    public class Core1Context : IdentityDbContext<CoreUser>
    {
        private IConfigurationRoot _config;

        public Core1Context(IConfigurationRoot config, DbContextOptions<Core1Context> options)
        : base(options)
        {
            _config = config;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);

            string connectionString = _config["ConnectionStrings:SampleConnection"];

            optionsBuilder.UseMySQL(connectionString);
        }

        public DbSet<Trip> Trips { get; set; }
        public DbSet<Image> Images { get; set; }
        //public DbSet<Stop> Stops { get; set; }

        public IEnumerable<Trip> GetTrips()
        {
            return Trips.ToList();
        }

        public IEnumerable<Trip> GetMyTrips(string username)
        {
            return Trips.Where(t => t.UserName == username).ToList();
        }

        public IEnumerable<TripStop> GetMyTripStop(string username)
        {
            IEnumerable<Trip> trips = Trips.Include(t => t.Images).Where(t => t.UserName == username);
            List<TripStop> tss = new List<TripStop>();
            foreach (Trip tr in trips)
            {
                TripStop ts = new TripStop()
                {
                    Name = tr.Name,
                    Descripton = tr.Descripton,
                    DateCreated = tr.DateCreated,
                    UserName = tr.UserName,
                    Images = tr.Images
                };
                tss.Add(ts);
            }

            return tss;
        }

        public TripStop GetTripByName(string tripName, string id)
        {
            Trip tr = Trips.Include(t => t.Images).Where(t => t.Name == tripName && t.Id.ToString() == id).FirstOrDefault();
            TripStop ts = new TripStop()
            {
                Name = tr.Name,
                Descripton = tr.Descripton,
                DateCreated = tr.DateCreated,
                UserName = tr.UserName,
                Stops = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Stop>>(tr.Stops),
                //Stops = Newtonsoft.Json.JsonConvert.DeserializeObject<List<Stop>>(GetStopsJson(tripName))
                Images = tr.Images
            };
            
            return ts;
        }

        public string GetStopsJson(string tripName)
        {
            string result = "";
            string connStr = "server=localhost;userid=root;pwd=ferrari599;port=3306;database=Core1i;sslmode=none;";
            MySqlConnection conn = new MySqlConnection(connStr);
            try
            {
                //Console.WriteLine("Connecting to MySQL...");
                conn.Open();

                string sql = "SELECT Stops FROM trips";
                MySqlCommand cmd = new MySqlCommand(sql, conn);
                MySqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    //Console.WriteLine(rdr[0] + " -- " + rdr[1]);
                    result = rdr.ToString();
                }
                rdr.Close();
            }
            catch (Exception ex)
            {
                //Console.WriteLine(ex.ToString());
            }

            conn.Close();
            //Console.WriteLine("Done.");

            return result;
        }

    }
}
