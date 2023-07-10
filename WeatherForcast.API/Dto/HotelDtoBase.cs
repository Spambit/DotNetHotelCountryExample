using System;
using System.ComponentModel.DataAnnotations;

namespace WeatherForcast.API.Dto
{
	public class HotelDtoBase
	{
        [Required]
        public string Name { get; set; }

        public string Address { get; set; }

        public double Rating { get; set; }
    }

    public class GetHotelDto : HotelDtoBase
    {
        public object Id { get; set; }
    }

    public class CreateHotelDto : HotelDtoBase
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int CountryId { get; set; }
    }
}

