using Nasa.Apod.DataAccess.Data;
using Nasa.Apod.DataAccess.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nasa.Apod.Business.Interfaces
{
    public interface IMarsRoverPhotosService
    {
        Task<List<MarsRoverPhoto>> GetMarsRoverPhotosAsync(
            MarsRover rover,
            DateTime earthTime,
            MarsRoverCamera? camera = null);
    }
}
