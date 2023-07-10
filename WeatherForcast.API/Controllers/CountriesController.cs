using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WeatherForcast.API.Dto;
using WeatherForcast.API.Models;
using WeatherForcast.API.Repository.Contracts;

namespace WeatherForcast.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        private readonly ICountriesRepository _repository;
        private readonly IMapper _mapper;

        public CountriesController(ICountriesRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // GET: api/Countries
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetCountryDto>>> GetCountries()
        {
            var countries = await _repository.GetAllAsync();
            var records = _mapper.Map<List<GetCountryDto>>(countries);
            return Ok(records);
        }

        // GET: api/Countries/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetCountryDetailsDto>> GetCountry(int id)
        {
            var countryDetailsDto = await _repository.GetDetails(id);

            if (countryDetailsDto == null)
            {
                return NotFound("Invalid Record Id");
            }

            return countryDetailsDto;
        }

        // PUT: api/Countries/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCountry(int id, UpdateCountryDto countryDto)
        {
            var country = await _repository.GetAsync(id);
            if (country == null)
            {
                return BadRequest("Invalid Record Id");
            }

            // _context.Entry(country).State = EntityState.Modified;
            _mapper.Map(countryDto, country); // This takes care of marking the state of the _context as being modified
            await _repository.UpdateAsync(country);

            return NoContent();
        }

        // POST: api/Countries
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Country>> PostCountry(CreateCountryDto countryDto)
        {
            var country = _mapper.Map<Country>(countryDto);
            await _repository.AddAsync(country);

            return CreatedAtAction("GetCountry", new { id = country.Id }, country);
        }

        // DELETE: api/Countries/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCountry(int id)
        {
            await _repository.DeleteAsync(id);
            return NoContent();
        }

        private async Task<bool> CountryExistsAsync(int id)
        {
            return await _repository.Exists(id);
        }
    }
}
