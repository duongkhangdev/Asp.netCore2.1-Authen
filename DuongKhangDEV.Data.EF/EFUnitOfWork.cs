using System;
using System.Collections.Generic;
using System.Text;
using DuongKhangDEV.Infrastructure.Interfaces;

namespace DuongKhangDEV.Data.EF
{
    public class EFUnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _dbContext;

        public EFUnitOfWork(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Commit()
        {
            _dbContext.SaveChanges();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}
