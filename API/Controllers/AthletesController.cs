using API.Dtos;
using API.Models;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace API.Controllers
{
    [RoutePrefix("api/v1/Athletes")]
    public class AthletesController : ApiController
    {
        [Route("")]
        [HttpGet]
        public List<nAthleteDto> getAll()
        {
            MapperConfiguration mc = new MapperConfiguration(
                c => c.AddProfile<DtoModelMappingProfilo>()
                );
            //Mapper mapper = new Mapper(mc);

            using(var context = new OlympicsContext())
            {
                return context.nAthletes
                    .ProjectTo<nAthleteDto>(mc)
                    .ToList();
            }
        }

        [Route("{id}")]
        [HttpGet]
        public nAthleteDto getOne(int id)
        {
            MapperConfiguration mc = new MapperConfiguration(
                c => c.AddProfile<DtoModelMappingProfilo>()
                );
            Mapper mapper = new Mapper(mc);

            using (var context = new OlympicsContext())
            {
                var a = context.nAthletes
                    .FirstOrDefault(w => w.IdAthlete == id);

                return mapper.Map<nAthleteDto>(a);
            }
        }
    }
}