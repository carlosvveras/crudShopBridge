using Microsoft.EntityFrameworkCore;
using Shopbridge_base.Data;
using Shopbridge_base.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shopbridge_Test
{
    public class DummyData
    {
        public DummyData()
        {
        }

        public void Seed(Shopbridge_Context context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            context.Product.AddRange(
                new Product() { Name = "CSHARP", Description = "csharp", Price = 10 },
                new Product() { Name = "VISUAL STUDIO", Description = "visualstudio", Price = 15 },
                new Product() { Name = "ASP.NET CORE", Description = "aspnetcore", Price = 25 },
                new Product() { Name = "SQL SERVER", Description = "sqlserver", Price = 35 }
            );

            //context.Product.AsNoTracking();

            context.SaveChanges();
        }

    }


}
