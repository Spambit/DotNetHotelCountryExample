using System.Runtime.Serialization;

namespace WeatherForcast.API.Repository
{
    [Serializable]
    internal class NotFoundException : ApplicationException
    {
        public NotFoundException(string name, object key) : base($"{name} with id ({key}) was not found")
        {
        }
    }
}