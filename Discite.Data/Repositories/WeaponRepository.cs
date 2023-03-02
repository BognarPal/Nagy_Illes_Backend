using Discite.Data.Models;
using Microsoft.EntityFrameworkCore;
using ProjectDiscite.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discite.Data.Repositories
{
    public class WeaponRepository : GenericRepository<WeaponModel>
    {
        public override List<WeaponModel> GetAll()
        {
            return dbContext.Set<WeaponModel>().Include(w => w.Runs).ToList();
        }
    }
}
