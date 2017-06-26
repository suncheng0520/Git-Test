using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using EFYaJia.Models;

namespace EFYaJia
{
    public partial class ContosoUniversityEntities: DbContext
    {
        public override int SaveChanges()
        {
            var entities = this.ChangeTracker.Entries();
            foreach (var item in entities)
            {
                if (item.Entity is Course)
                {

                }
                if (item.State==EntityState.Modified)
                {
                    item.CurrentValues.SetValues(new { ModifiedOn = DateTime.Now });
                }
            }
            return base.SaveChanges();
        }
    }
}
