using System;
using WeatherForcast.API.Dto;
using WeatherForcast.API.Models;

namespace WeatherForcast.API.Repository.Contracts
{
	public interface ICountriesRepository: IGenericRepository<Country>
	{
        public Task<GetCountryDetailsDto> GetDetails(int? id);
    }
}

