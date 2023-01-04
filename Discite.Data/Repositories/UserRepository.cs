using Discite.Data.Models;
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
        public UserRepository()
        {

        }

        public UserRepository(DisciteDbContext dbContext) : base(dbContext)
        { }
    }
}
