﻿using EShop.DataAccess.Data;
using EShop.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Linq.Expressions;

namespace EShop.DataAccess.Repository;

public class Repository<T> : IRepository<T> where T : class
{
    private readonly ApplicationDbContext _db;
    internal DbSet<T> dbSet;
    public Repository(ApplicationDbContext db)
    {
        _db = db;
        this.dbSet = _db.Set<T>();
        _db.Products.Include(p => p.Category).Include(p => p.CategoryId);
    }
    public void Add(T entity)
    {
        dbSet.Add(entity);
    }

    public T Get(Expression<Func<T, bool>> filter, string? includeProperties = null, bool tracked = false)
    {

        IQueryable<T> query = tracked ? dbSet : dbSet.AsNoTracking();

        query = query.Where(filter);
        if (!string.IsNullOrEmpty(includeProperties))
        {
            foreach (var property in includeProperties
                .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(property);
            }
        }
        return query.FirstOrDefault();
    }

    public IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter, string? includeProperties = null, bool tracked = false)
    {
        IQueryable<T> query = tracked ? dbSet : dbSet.AsNoTracking();

        if(filter != null)
        {
            query = query.Where(filter);
        }
        
        if (!string.IsNullOrEmpty(includeProperties))
        {
            foreach (var property in includeProperties
                .Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(property);
            }
        }
        return query.ToList();
    }

    public void Remove(T entity)
    {
        dbSet.Remove(entity);
    }

    public void RemoveRange(IEnumerable<T> entities)
    {
        dbSet.RemoveRange(entities);
    }
}

