using System.Linq.Expressions;
using Bulky.DataAccess.Data;
using Bulky.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace Bulky.DataAccess.Repository;

public class Repository<T> : IRepository<T> where T : class
{
    internal DbSet<T> DbSet;
    private readonly ApplicationDbContext _dbContextContext;

    public Repository(ApplicationDbContext dbContextContext)
    {
        _dbContextContext = dbContextContext;
        DbSet = _dbContextContext.Set<T>();
    }

    public IEnumerable<T> GetAll()
    {
        IQueryable<T> query = DbSet;
        return query.ToList();
    }

    public T Get(Expression<Func<T, bool>> filter)
    {
        IQueryable<T> query = DbSet;
        query = query.Where(filter);
        return query.FirstOrDefault()!;
    }

    public void Add(T entity)
    {
        DbSet.Add(entity);
    }

    // public void Update(T entity)
    // {
    //     DbSet.Update(entity);
    // }

    public void Remove(T entity)
    {
        DbSet.Remove(entity);
    }

    public void RemoveRange(IEnumerable<T> entities)
    {
        DbSet.RemoveRange(entities);
    }
}