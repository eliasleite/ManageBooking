using Booking.Business.Interfaces;
using Booking.Data.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Data.Repository
{
    public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : class, new()
    {
        protected readonly ManageBookingContext _manageBookingContext;
        protected readonly DbSet<TEntity> DbSet;

        public Repository(ManageBookingContext manageBookingContext)
        {
            _manageBookingContext = manageBookingContext;
            DbSet = manageBookingContext.Set<TEntity>();
        }

        public async Task Add(TEntity entity)
        {
            DbSet.Add(entity);
            await SaveChanges();
        }

        public async Task Delete(Guid id)
        {
            DbSet.Remove(await DbSet.FindAsync(id));
            await SaveChanges();
        }

        public void Dispose()
        {
            _manageBookingContext?.Dispose();
        }

        public async Task<List<TEntity>> GetAll()
        {
            return await DbSet.ToListAsync();
        }

        public async Task<TEntity> GetById(Guid id)
        {
            return await DbSet.FindAsync(id);
        }

        public async Task<int> SaveChanges()
        {
            return await _manageBookingContext.SaveChangesAsync();
        }

        public async Task Update(TEntity entity)
        {
            DbSet.Update(entity);
            await SaveChanges();
        }
    }
}
