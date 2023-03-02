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
    public class EnemyRepository : GenericRepository<EnemyModel>
    {
        public override List<EnemyModel> GetAll()
        {
            return dbContext.Set<EnemyModel>().Include(e => e.Runs).ThenInclude(e => e.Run).ToList();
        }
    }
}
