using System;
using AutoMapper;
using WeatherForcast.API.Models;

namespace WeatherForcast.API.Dto
{
	public class MapperConfig: Profile
	{
		public MapperConfig()
		{
            CreateMap<Country, CreateCountryDto>().ReverseMap();
            CreateMap<Country, GetCountryDto>().ReverseMap();
            CreateMap<Country, GetCountryDetailsDto>().ReverseMap();
            CreateMap<Country, UpdateCountryDto>().ReverseMap();
            CreateMap<Hotel, GetHotelDto>().ReverseMap();
            CreateMap<Hotel, CreateHotelDto>().ReverseMap();
            CreateMap<Hotel, HotelDtoBase>().ReverseMap();
        }
	}
}

