using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace CherryBlossom.Persistent
{
    class SakuraContext : DbContext
    {
        private string _fileName;

        public SakuraContext(string fileName)
        {
            _fileName = fileName;
        }
        public DbSet<ItemModel> Items { get; set; }
        public DbSet<MorphModel> Morphs { get; set; }
        public DbSet<MessageModel> Messages { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            options.UseSqlite("Data Source=" + _fileName);
        }
    }
}
