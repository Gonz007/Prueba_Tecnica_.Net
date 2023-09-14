using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace UfinetPrueba.Application.Features.Information
{

    [ApiController]
    [Route("api/[controller]")]
    public class GetInfoController : ControllerBase
    {
        private readonly IMediator _mediator;

        public GetInfoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Consulta con filtros.
        /// </summary>
        /// <param name="getInfoDto">DTO con filtros de consulta</param>
        /// <returns>Resultado de la consulta</returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<GetInfoResponseDto> Get([FromQuery] GetInfoDto getInfoDto)
        {

            GetInfoQuery query = new GetInfoQuery(getInfoDto);
            var result = await _mediator.Send(query);


            return result;

        }
    }
}
