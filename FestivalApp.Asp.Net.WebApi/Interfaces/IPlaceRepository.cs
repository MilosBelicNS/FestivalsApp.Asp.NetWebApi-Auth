using FestivalApp.Asp.Net.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FestivalApp.Asp.Net.WebApi.Interfaces
{
   public  interface IPlaceRepository
    {

        IEnumerable<Place> GetAll();
        Place GetById(int id);
        IEnumerable<Place> GetByZip(int zip);
    }
}
