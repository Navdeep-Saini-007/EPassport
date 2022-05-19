using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EPassport.Models;

namespace EPassport.Data
{
    public class EPassportContext : DbContext
    {
        public EPassportContext (DbContextOptions<EPassportContext> options)
            : base(options)
        {
        }

        public DbSet<EPassport.Models.PassportOffice>? PassportOffice { get; set; }

        public DbSet<EPassport.Models.AppointmentDetail> AppointmentDetail { get; set; }

        public DbSet<EPassport.Models.ApplicationDetail> ApplicationDetail { get; set; }

        public DbSet<EPassport.Models.AddressDetail> AddressDetail { get; set; }

        public DbSet<EPassport.Models.FamilyDetail> FamilyDetail { get; set; }

        public DbSet<EPassport.Models.ReferenceDetail> ReferenceDetail { get; set; }
    }
}
