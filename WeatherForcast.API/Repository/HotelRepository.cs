using System;
using WeatherForcast.API.Models;
using WeatherForcast.API.Repository.Contracts;

namespace WeatherForcast.API.Repository
{
	public class HotelRepository: GenericRepository<Hotel>, IHotelsRepository
	{
        public HotelRepository(HotelListingDbContext context) : base(context)
		{

        }
	}
}

