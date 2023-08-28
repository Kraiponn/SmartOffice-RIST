using Microsoft.EntityFrameworkCore;
using SmartOffice.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartOffice.Persistence
{
    public class ESmartNotiContext : DbContext
    {
        public ESmartNotiContext(DbContextOptions<ESmartNotiContext> options)
            : base(options)
        {
        }
        public virtual DbSet<DocItem> DocumentItems { get; set; }
    }
}
