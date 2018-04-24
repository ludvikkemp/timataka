using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System;
using System.Data.SqlClient;
using Timataka.Core.Models.Entities;

namespace Timataka.Core.Data
{
    public class TimingDbContext : DbContext
    {
        public MySqlConnection Connection;

        public TimingDbContext(string connectionString)
        {
            Connection = new MySqlConnection(connectionString);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("user id=sql2234241;password=xP2%jX2!;host=sql2.freemysqlhosting.net;database=sql2234241;persist security info=True; SslMode = none");
        }

        public DbSet<_Chip> Chips { get; set; }
        public DbSet<_Marker> Markers { get; set; }
        public DbSet<_Result> Results { get; set; }
        //public DbSet<_Time> Times { get; set; }

    }

}
