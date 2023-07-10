using System;
namespace WeatherForcast.API.Dto
{
	public class UpdateCountryDto: CountryDtoBase
	{
		public List<HotelDtoBase> Hotels { get; set; }
	}
}

