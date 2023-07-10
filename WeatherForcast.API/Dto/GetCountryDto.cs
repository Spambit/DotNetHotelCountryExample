using System;
namespace WeatherForcast.API.Dto
{
	public class GetCountryDto: CountryDtoBase
	{
		public int Id { get; set; }
	}

    public class GetCountryDetailsDto : GetCountryDto
    {
        public List<GetHotelDto> Hotels { get; set; }
    }
}

