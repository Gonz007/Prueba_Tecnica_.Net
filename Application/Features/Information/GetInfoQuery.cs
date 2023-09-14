using MediatR;

namespace UfinetPrueba.Application.Features.Information
{
    public class GetInfoQuery : IRequest<GetInfoResponseDto>
    {
        public GetInfoQuery(GetInfoDto GetInfoDto)
        {
            getInfoDto = GetInfoDto;
        }
        public GetInfoDto getInfoDto { get; set; }
    }

}
