using FestivalApp.Asp.Net.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FestivalApp.Asp.Net.WebApi.Interfaces
{
    public interface IFestivalRepository
    {
        IEnumerable<Festival> GetAll();
        Festival GetById(int id);
        IEnumerable<Festival> Search(Filter filter);
        void Delete(Festival festival);
        void Create(Festival festival);
        void Update(Festival festival);
    }
}
