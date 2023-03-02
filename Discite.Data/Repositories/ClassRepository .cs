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
    public class ClassRepository : GenericRepository<ClassModel>
    {
        public override List<ClassModel> GetAll()
        {
            return dbContext.Set<ClassModel>().Include(c => c.Users).ToList();
        }
    }
}
