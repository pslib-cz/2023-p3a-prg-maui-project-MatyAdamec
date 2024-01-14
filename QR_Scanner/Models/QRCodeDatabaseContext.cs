using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace QR_Scanner.Models
{
    public class QRCodeDatabaseContext : DbContext
    {
        public DbSet<QRCodeScan> QRCodeScans { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string folderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string dbPath = Path.Combine(folderPath, "QRCodeDatabase.db");

            optionsBuilder.UseSqlite($"Filename={dbPath}");
        }

    }
}
