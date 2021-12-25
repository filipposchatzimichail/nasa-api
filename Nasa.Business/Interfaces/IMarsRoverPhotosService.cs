using Nasa.DataAccess.Data.MarsRover;
using Nasa.DataAccess.Enums;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nasa.Business.Interfaces
{
    public interface IMarsRoverPhotosService
    {
        Task<List<MarsRoverPhoto>> GetMarsRoverPhotosAsync(
            MarsRover rover,
            DateTime earthTime,
            MarsRoverCamera? camera = null);
    }
}
