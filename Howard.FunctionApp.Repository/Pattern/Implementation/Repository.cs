using Microsoft.EntityFrameworkCore;
using Howard.FunctionApp.Repository.Context;
using Howard.FunctionApp.Repository.Pattern.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Howard.FunctionApp.Repository.Pattern.Implementation
{
    public class Repository<T> : IRepository<T> where T : class, IEntity
    {
        protected readonly DatabaseContext _dbContext;
        private readonly DbSet<T> _entities;

        public Repository(DatabaseContext dbContext)
        {
            _dbContext = dbContext;
            _entities = dbContext.Set<T>();
        }

        /// <inheritdoc/>
        public async Task<T> Add(T entity)
        {
            T doesExit = await _entities.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (doesExit != null)
            {
                return default;
            }

            await _entities.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        /// <inheritdoc/>
        public async Task<bool> Delete(Guid id)
        {
            T entity = await _entities.FirstOrDefaultAsync(x => x.Id == id);

            if (entity == null)
            {
                return false;
            }

            _entities.Remove(entity);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        /// <inheritdoc/>
        public async Task<T> Get(Guid id)
        {
            return await _entities.FirstOrDefaultAsync(x => x.Id == id);
        }

        /// <inheritdoc/>
        public async Task<IList<T>> GetAll()
        {
            return await _entities.ToListAsync();
        }

        /// <inheritdoc/>
        public T GetWithExpression(Func<T, bool> predicate)
        {
            return _entities.Where(predicate).FirstOrDefault();
        }

        /// <inheritdoc/>
        public List<T> GetAllWithExpression(Func<T, bool> predicate)
        {
            return _entities.Where(predicate).ToList();
        }

        /// <inheritdoc/>
        public async Task<T> Update(T entity)
        {
            T doesExit = await _entities.FirstOrDefaultAsync(x => x.Id == entity.Id);

            if (doesExit == null)
            {
                return default;
            }

            _entities.Update(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }
    }
}
