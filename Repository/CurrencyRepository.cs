using Microsoft.EntityFrameworkCore;
using WebAPI.Entities;
using WebAPI.Interface;

namespace Web.Models.Repository
{
    public class CurrencyRepository : ICurrencyRepositoty, IDisposable
    {
        private PracticeContext db;

        public CurrencyRepository(PracticeContext _dbInstance)
        {
            db = _dbInstance;
        }

        public TbCurrency Get(string currency)
        {
            return db.TbCurrencies.FirstOrDefault(x => x.Currency == currency);
        }

        public IQueryable<TbCurrency> GetAll()
        {
            return db.TbCurrencies.OrderBy(x => x.Currency);
        }

        public void Create(TbCurrency instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }
            else
            {
                db.TbCurrencies.Add(instance);
                SaveChanges();
            }
        }

        public void Delete(TbCurrency instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }
            else
            {
                db.Entry(instance).State = EntityState.Deleted;
                SaveChanges();
            }
        }

        public void Delete(string currency)
        {
            if (string.IsNullOrEmpty(currency))
            {
                throw new ArgumentNullException("id");
            }
            else
            {
                var temp = db.TbCurrencies.FirstOrDefault(x => x.Currency == currency);
                db.Entry(temp).State = EntityState.Deleted;
                SaveChanges();
            }
        }

        public void SaveChanges()
        {
            db.SaveChanges();
        }

        public void Update(TbCurrency instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException("instance");
            }
            else
            {
                db.Entry(instance).State = EntityState.Modified;
                SaveChanges();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (db != null)
                {
                    db.Dispose();
                    db = null;
                }
            }
        }
    }
}
