using FestivalApp.Asp.Net.WebApi.Interfaces;
using FestivalApp.Asp.Net.WebApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace FestivalApp.Asp.Net.WebApi.Controllers
{
    public class PlacesController : ApiController
    {


        public IPlaceRepository _repository { get; set; }

        public PlacesController(IPlaceRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Place> GetAll()
        {
            return _repository.GetAll();
        }


        [Authorize]
        [ResponseType(typeof(Place))]
        public IHttpActionResult GetById(int id)
        {
            var place = _repository.GetById(id);

            if (place == null)
            {
                return NotFound();
            }

            return Ok(place);
        }


        [Authorize]
        [ResponseType(typeof(Place))]
        public IEnumerable<Place> GetByZipCode(int zip)
        {
            return _repository.GetByZip(zip);
        }

    }
}
