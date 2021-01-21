using FestivalApp.Asp.Net.WebApi.Interfaces;
using FestivalApp.Asp.Net.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Web;

namespace FestivalApp.Asp.Net.WebApi.Repository
{
    public class FestivalRepository : IDisposable, IFestivalRepository
    {


        private ApplicationDbContext db = new ApplicationDbContext();

        protected void Dispose(bool disposing)
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

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


        public IEnumerable<Festival> GetAll()
        {
            return db.Festivals.Include(x => x.Place).OrderByDescending(x => x.TicketPrice);
        }

        public Festival GetById(int id)
        {
            return db.Festivals.Find(id);

        }

        public IEnumerable<Festival> Search(Filter filter)
        {
            var festivals = db.Festivals.Include(x=>x.Place).Where(x => x.FirstYear >= filter.Min &  x.FirstYear <= filter.Max).OrderBy(x => x.FirstYear);
            return festivals;
        }

        public void Create(Festival festival)
        {
            db.Festivals.Add(festival);
            db.SaveChanges();
        }

        public void Delete(Festival festival)
        {
            db.Festivals.Remove(festival);
            db.SaveChanges();

        }
        public void Update(Festival festival)
        {
            db.Entry(festival).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }catch(DbUpdateConcurrencyException)
            {
                throw;
            }
        }
    }
}