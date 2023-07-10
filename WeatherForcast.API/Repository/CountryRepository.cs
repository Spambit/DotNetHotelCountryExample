using System;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using WeatherForcast.API.Dto;
using WeatherForcast.API.Models;
using WeatherForcast.API.Repository.Contracts;

namespace WeatherForcast.API.Repository
{
	public class CountryRepository: GenericRepository<Country>, ICountriesRepository
	{
        public CountryRepository(HotelListingDbContext context, IMapper mapper) : base(context)
        {
            _context = context;
            _mapper = mapper;
        }

        private readonly HotelListingDbContext _context;
        private readonly IMapper _mapper;

        public async Task<GetCountryDetailsDto> GetDetails(int? id)
        {
            var country = await _context.Countries.Include(q => q.Hotels)
                .ProjectTo<GetCountryDetailsDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(q => q.Id == id);

            if (country == null)
            {
                throw new NotFoundException(nameof(GetDetails), id);
            }

            return country;
        }
    }
}

