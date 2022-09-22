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



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           optionsBuilder.UseSqlServer(cs  );
        }

        public DbSet<Acaunt> Acaunts { get; set; }
        public DbSet<EntryControl> EntryControls { get; set; }
          

    }
}
