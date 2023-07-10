using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WeatherForcast.API.Dto;
using WeatherForcast.API.Models;
using WeatherForcast.API.Repository;
using WeatherForcast.API.Repository.Contracts;

namespace WeatherForcast.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        private readonly IHotelsRepository _repository;
        private readonly IMapper _mapper;

        public HotelsController(IHotelsRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        // GET: api/Hotels
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetHotelDto>>> GetHotels()
        {
            var hotels = await _repository.GetAllAsync();
            var records = _mapper.Map<List<GetHotelDto>>(hotels);
            return Ok(records);
        }

        // GET: api/Hotel/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GetHotelDto>> GetHotel(int id)
        {
            var hotel = await _repository.GetAsync(id);

            if (hotel == null)
            {
                return NotFound("Invalid Record Id");
            }
            var records = _mapper.Map<GetHotelDto>(hotel);
            return records;
        }

        // PUT: api/Hotel/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHotel(int id, UpdateHotelDto hotelDto)
        {
            var hotel = await _repository.GetAsync(id);
            if (hotel == null)
            {
                return BadRequest("Invalid Record Id");
            }

            _mapper.Map(hotelDto, hotel); // This takes care of marking the state of the _context as being modified
            await _repository.UpdateAsync(hotel);

            return NoContent();
        }

        // POST: api/Hotel
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Country>> PostHotel(CreateHotelDto hotelDto)
        {
            var hotel = _mapper.Map<Hotel>(hotelDto);
            await _repository.AddAsync(hotel);

            return CreatedAtAction("PostHotel", new { id = hotel.Id }, hotel);
        }

        // DELETE: api/Hotel/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHotel(int id)
        {
            await _repository.DeleteAsync(id);
            return NoContent();
        }

        private async Task<bool> HotelExistsAsync(int id)
        {
            return await _repository.Exists(id);
        }
    }
}
