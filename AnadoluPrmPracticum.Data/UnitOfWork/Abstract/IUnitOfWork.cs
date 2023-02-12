using AnadoluPrmPracticum.Data.Model;
using AnadoluPrmPracticum.Data.Repository.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace AnadoluPrmPracticum.Data.UnitOfWork.Abstract
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<User> UserRepository { get; }
        IGenericRepository<Game> GameRepository { get; }


        Task CompleteAsync();//For SaveChanges
    }
}
