using System;
using System.ComponentModel.DataAnnotations;

namespace WeatherForcast.API.Dto
{
	public abstract class CountryDtoBase
	{
        [Required]
        public object Name { get; set; }

        public object ShortName { get; set; }
    }
}

