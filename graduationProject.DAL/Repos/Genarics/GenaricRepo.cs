using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace graduationProject.DAL
{
    public class GenaricRepo<TEntity> : IGenaricRepo<TEntity> where TEntity : class
    {
        private readonly HospitalContext _context;

        public GenaricRepo(HospitalContext context)
        {
            _context = context;
        }
        public void Add(TEntity enity)
        {
            _context.Set<TEntity>().Add(enity);
        }

        public List<TEntity> GetAll()
        {
            return _context.Set<TEntity>().ToList();
        }

        public TEntity? GetById(int? id)
        {
            return _context.Set<TEntity>().Find(id);
        }

        public int SaveChanges()
        {
            Console.Writeline("vasdf");
            return _context.SaveChanges();
        }
    }
}
