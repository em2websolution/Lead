using Domain.Models;
using Domain.Services;
using Lead.Backend.Dtos;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Lead.Backend.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("[controller]")]
    public class LeadsController : ControllerBase
    {
        private ILeadsServices _leadServices;

        public LeadsController(ILeadsServices leadServices)
        {
            _leadServices = leadServices;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(new { Message = "This is a sample response" });
        }

        [HttpGet("{id}")]
        public IActionResult GetById([FromRoute] Guid id)
        {
            var result = _leadServices.Get(id);

            if (result == null)
                return NotFound("User not found");

            return Ok(result);
        }

        [HttpGet("GetAll")]
        public IActionResult GetAll()
        {
            var result = _leadServices.GetAll();

            if (result == null)
                return NotFound("User not found");

            return Ok(result);
        }


        [HttpPost]
        public IActionResult Insert([FromBody] LeadsDto leadDto)
        {
            if (leadDto == null)
                throw new ArgumentNullException("Invalid data");

            var lead = new Leads
            {
                FirsName = leadDto.FirsName,
                LastName = leadDto.LastName,
                Email = leadDto.Email,
                Company = leadDto.Company,
                Created = DateTime.Now
            };
            var result = _leadServices.Insert(lead);

            return Ok(new { Id = result });

        }

        [HttpPut("{id}")]
        public IActionResult Update([FromRoute] Guid id, [FromBody] LeadsDto leadDto)
        {
            if (leadDto == null)
                throw new ArgumentNullException("Invalid data");

            var lead = new Leads
            {
                Id = id,
                FirsName = leadDto.FirsName,
                LastName = leadDto.LastName,
                Email = leadDto.Email,
                Company = leadDto.Company,
                Created = DateTime.Now
            };

            _leadServices.Update(lead);

            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            if (id == null)
                throw new ArgumentNullException("Invalid data");

            _leadServices.Delete(id);

            return Ok();
        }

    }
}