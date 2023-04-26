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
    public class RunRepository : GenericRepository<RunModel>
    {
        public override List<RunModel> GetAll()
        {
            return dbContext.Set<RunModel>().Include(r => r.Enemies).Include(r => r.User).ToList();
        }
    }
}
