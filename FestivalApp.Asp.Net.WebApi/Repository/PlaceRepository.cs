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
    public class PlaceRepository :IDisposable, IPlaceRepository
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

        public IEnumerable<Place> GetAll()
        {
            return db.Places;
        }

        public Place GetById(int id)
        {
            return db.Places.Find(id);
        }

        public IEnumerable<Place> GetByZip(int zip)
        {
            return db.Places.Where(x => x.Zip < zip).OrderBy(x => x.Zip);
        }

       


    }
}