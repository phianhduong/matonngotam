using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MyCodeCamp.Data;
using MyCodeCamp.Data.Entities;
using Microsoft.Extensions.Logging;
using AutoMapper;
using WebAPI.Models;
using System.Collections;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace WebAPI.Controllers
{
    [Route("api/[controller]")        ]
    public class CampsController : Controller
    {
        private ILogger<CampsController> _logger;
        private ICampRepository _repo;
        private IMapper _mapper;

        public CampsController(ICampRepository  repo, ILogger<CampsController> logger, IMapper mapper)
        {
            _repo = repo;
            _logger = logger;
            _mapper = mapper;
        }
        // GET: /<controller>/
        [HttpGet("")]
        public IActionResult Get()
        {
            var camps = _repo.GetAllCamps();
            return Ok(_mapper.Map<IEnumerable<CampModel>>(camps));
        }

        [HttpGet("{id}", Name ="CampGet")]
        public IActionResult Get(int id, bool includeSpeakers =false)
        {
            try
            {
                Camp camps = null;
                if (includeSpeakers) camps = _repo.GetCampWithSpeakers(id);
                else camps = _repo.GetCamp(id);
                if (camps == null) return NotFound($"Camp {id} was not found");

                return Ok(_mapper.Map<CampModel>(camps));
            }
            catch
            {
               
            }
            return BadRequest();
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Camp model)
        {
            try
            {
                _logger.LogInformation("Creating a new camps");
                _repo.Add(model);

                if (await _repo.SaveAllAsync())
                {
                    var newUri = Url.Link("CampGet", new { id = model.Id });
                    return Created( newUri, model);
                }
                else
                {
                    _logger.LogWarning("Could not save");
                }
            }
            catch(Exception ex )
            {
                _logger.LogError($"Threw exception :{ex}");
            }
            return BadRequest();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] Camp model)
        {
            try
            {
                var oldCamp = _repo.GetCamp(id);
                if (oldCamp == null) return NotFound($"Could not found camp with id {id}");

                oldCamp.Name = model.Name ?? oldCamp.Name;
                oldCamp.Description = model.Description ?? oldCamp.Description;
                oldCamp.Location = model.Location ?? oldCamp.Location;
                oldCamp.Length = model.Length > 0 ? model.Length : oldCamp.Length;
                oldCamp.EventDate = model.EventDate != DateTime.MinValue ? model.EventDate : oldCamp.EventDate;

                if (await _repo.SaveAllAsync())
                {
                    return Ok(oldCamp);
                }
            }
            catch (Exception ex)
            {

            }
            return BadRequest("Could not update Camps");
        }


        [HttpDelete("{id}")]
        public async Task <IActionResult>Delete(int id)
        {
            try
            {
                var oldCamp = _repo.GetCamp(id);
                if (oldCamp == null) return NotFound($"Could not found camp with id {id}");

                _repo.Delete(oldCamp);
                if (await _repo.SaveAllAsync())
                {
                    return Ok(oldCamp);
                }
            }
            catch (Exception ex)
            {

            }
            return BadRequest("Could not delete Camps");
        }
    }
}
