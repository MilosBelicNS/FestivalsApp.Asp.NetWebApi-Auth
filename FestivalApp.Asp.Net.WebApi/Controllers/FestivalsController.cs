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
    public class FestivalsController : ApiController
    {

        public IFestivalRepository   _repository { get; set; }

        public FestivalsController(IFestivalRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<Festival> GetAll()
        {
            return _repository.GetAll();
        }
        [Authorize]
        [Route("api/festivals/search")]
        public IEnumerable<Festival> Search(Filter filter)
        {
            return _repository.Search(filter);
        }

        [Authorize]
        [ResponseType(typeof(Festival))]
        public IHttpActionResult GetById(int id)
        {
            var festival = _repository.GetById(id);

            if (festival == null)
            {
                return NotFound();
            }

            return Ok(festival);
        }

        [Authorize]
        [ResponseType(typeof(Festival))]
        public IHttpActionResult Delete(int id)
        {
            var festival = _repository.GetById(id);

            if (festival == null)
            {
                return NotFound();
            }

            _repository.Delete(festival);

            return Ok();
        }

        [Authorize]
        [ResponseType(typeof(Festival))]
        public IHttpActionResult Post(Festival festival)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _repository.Create(festival);

            return CreatedAtRoute("DefaultApi", new { Id = festival.Id }, festival);
        }

        [Authorize]
        [ResponseType(typeof(Festival))]
        public IHttpActionResult Put(int id, Festival festival)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (festival.Id != id)
            {
                return BadRequest();
            }

            try
            {
                _repository.Update(festival);
            }
            catch
            {
                return BadRequest();
            }

            return Ok(festival);
        }




    }
}
