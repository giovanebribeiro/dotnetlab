using Lab.Net.StoreApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Lab.Net.StoreApp.Tests
{
    class TestStoreAppContext : IStoreAppContext
    {
        public DbSet<Product> Products
        {
            get; set;
        }

        public TestStoreAppContext()
        {
            this.Products = new TestProductDbSet();
        }

        public void Dispose() { }

        public void MarkAsModified(Product item) { }

        public int SaveChanges()
        {
            return 0;
        }
    }
}
