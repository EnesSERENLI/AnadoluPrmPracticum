using AnadoluPrmPracticum.Data.Context;
using AnadoluPrmPracticum.Data.Repository.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace AnadoluPrmPracticum.Service.Repository.Concrete
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        protected readonly AppDbContext _context;
        private DbSet<TEntity> _entities;

        public GenericRepository(AppDbContext dbContext)
        {
            _context = dbContext;
            _entities = _context.Set<TEntity>();
        }

        public bool Any(Expression<Func<TEntity, bool>> expression) =>  _entities.Any(expression); 

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _entities.AsNoTracking().ToListAsync();    
        }

        public async Task<TEntity> GetByIdAsync(int entityId)
        {
            return await _entities.FindAsync(entityId);
            //return await _entities.Where(entity => EF.Property<int>(entity, "ID").Equals(entityId)).SingleOrDefaultAsync();
        }

        public async Task InsertAsync(TEntity entity)
        {
            await _entities.AddAsync(entity);
        }

        public void RemoveAsync(TEntity entity)
        {
            var column = entity.GetType().GetProperty("IsDeleted");
            if (column is not null)
                entity.GetType().GetProperty("IsDeleted").SetValue(entity, true); //SoftDelete
            else
                _entities.Remove(entity); //HardDelete
        }

        public void Update(TEntity entity)
        {
            _entities.Update(entity);
        }
    }
}
