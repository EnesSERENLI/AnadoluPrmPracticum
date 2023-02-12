using AnadoluPrmPracticum.Data.Context;
using AnadoluPrmPracticum.Data.Model;
using AnadoluPrmPracticum.Data.Repository.Abstract;
using AnadoluPrmPracticum.Data.UnitOfWork.Abstract;
using AnadoluPrmPracticum.Service.Repository.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AnadoluPrmPracticum.Service.UnitOfWork.Concrete
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _dbContext;
        public bool disposed;

        public UnitOfWork(AppDbContext dbContext)
        {
            this._dbContext = dbContext;

            UserRepository = new GenericRepository<User>(dbContext);
            GameRepository = new GenericRepository<Game>(dbContext);
        }


        public IGenericRepository<User> UserRepository { get; private set; }

        public IGenericRepository<Game> GameRepository { get; private set; }

        public async Task CompleteAsync() 
        {
            await _dbContext.SaveChangesAsync();
        }

        protected virtual void Clean(bool disposing) //Cache temizleme işlemler
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Clean(true);
            GC.SuppressFinalize(this);
        }
    }
}
