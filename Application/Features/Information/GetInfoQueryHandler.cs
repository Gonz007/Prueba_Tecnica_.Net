using MediatR;
using Microsoft.EntityFrameworkCore;
using UfinetPrueba.Domain.Entities;
using UfinetPrueba.Domain.Interfaces;

namespace UfinetPrueba.Application.Features.Information
{
    public class GetInfoQueryHandler : IRequestHandler<GetInfoQuery, GetInfoResponseDto>
    {
        private readonly IRepository<Domain.Entities.Countries> _Countries;
        private readonly IRepository<Hotel> _hotel;
        private readonly IRepository<Restaurant> _restaurant;
        private readonly ApplicationDbContext _dbContext;

        public GetInfoQueryHandler(
            IRepository<Countries> Countries,
            IRepository<Hotel> hotel,
            IRepository<Restaurant> restaurant,
            ApplicationDbContext dbContext)
        {
            _Countries = Countries;
            _hotel = hotel;
            _restaurant = restaurant;
            _dbContext = dbContext;
        }

        public async Task<GetInfoResponseDto> Handle(GetInfoQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var queryCountries = _Countries.GetAsync().Result.ToList();

                if (request.getInfoDto.Filter != null)
                {
                    if (!string.IsNullOrEmpty(request.getInfoDto.Filter.NameCountries))
                    {
                        //StringComparison.OrdinalIgnoreCase se usa para no diferencias mayúsculas y minúsculas
                        queryCountries = queryCountries
                            .Where(c => c.name.IndexOf(request.getInfoDto.Filter.NameCountries, StringComparison.OrdinalIgnoreCase) >= 0)
                            .ToList();
                    }

                    if (!string.IsNullOrEmpty(request.getInfoDto.Filter.isocode))
                    {
                        queryCountries = queryCountries
                            .Where(c => c.isocode.IndexOf(request.getInfoDto.Filter.isocode, StringComparison.OrdinalIgnoreCase) >= 0)
                            .ToList();
                    }
                }

                GetInfoResponseDto getInfoResponseDto = new GetInfoResponseDto
                {
                    Total = queryCountries.Count(),
                    GetInfoDetail = new List<GetInfoDetail>()
                };

                var queryCountriesPag = queryCountries
                    .OrderByDescending(c => c.name)
                    .Skip((request.getInfoDto.pageNumber - 1) * request.getInfoDto.rowsPerPage)
                    .Take(request.getInfoDto.rowsPerPage)
                    .ToList();

                //dbContextLock se usa para mostrar multiples registros en un proceso asincronico
                object dbContextLock = new object();
                Parallel.ForEach(queryCountriesPag, item =>
                {
                    lock (dbContextLock) { 
                        var getInfoDetail = new GetInfoDetail
                    {
                        id = item.id,
                        name = item.name,
                        isocode = item.isocode,
                        population = item.population,
                        GetRestaurantDetail = new List<GetRestaurantDetail>(),
                        GetHotelDetail = new List<GetHotelDetail>()
                    };

                    var restaurants = _dbContext.Restaurants
                        .Where(r => r.Countries.Contains(item))
                        .ToList();

                    foreach (var restaurant in restaurants)
                    {
                        getInfoDetail.GetRestaurantDetail.Add(new GetRestaurantDetail
                        {
                            id = restaurant.id,
                            name = restaurant.name,
                            type = restaurant.type
                        });
                    }

                    var hotels = _dbContext.Hotels
                        .Where(h => h.Countries.Contains(item))
                        .ToList();

                    foreach (var hotel in hotels)
                    {
                        getInfoDetail.GetHotelDetail.Add(new GetHotelDetail
                        {
                            id = hotel.id,
                            name = hotel.name,
                            stars = hotel.stars
                        });
                    }

                    getInfoResponseDto.GetInfoDetail.Add(getInfoDetail);
                    }
                });



                return getInfoResponseDto;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
