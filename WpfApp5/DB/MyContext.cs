using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfApp5.DB
{
    public class MyContext : DbContext
    {
        private string cs =
            "Server=192.168.10.160; database=AhtyamovInOutDB;" +
            " user id = stud; password=stud;";

        private string cs1 =
            "Server=85.236.170.148,444; database=AhtyamovInOutDB;" +
            " user id = stud; password=stud;";



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           optionsBuilder.UseSqlServer(cs1  );
        }

        public DbSet<Acaunt> Acaunts { get; set; }
        public DbSet<EntryControl> EntryControls { get; set; }
          

    }
}
