using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using WeatherForcast.API.Dto;
using WeatherForcast.API.Models;

namespace WeatherForcast.API.Repository.Contracts
{
    public interface IHotelsRepository: IGenericRepository<Hotel>
    {
    }
}

