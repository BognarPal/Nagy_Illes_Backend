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
    public class UserRepository : GenericRepository<UserModel>
    {
        public override List<UserModel> GetAll()
        {
            return dbContext.Set<UserModel>().Include(r => r.Runs).ToList();
        }
    }
}
