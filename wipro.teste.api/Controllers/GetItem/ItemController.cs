using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using wipro.teste.gateway;

namespace wipro.teste.api.Controllers.GetItem
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IConectorFila _conectorFila;
        private readonly IMapper _mapper;
        private GetItemResponse _getItemResponse;

        public ItemController(IConectorFila conectorFila, IMapper mapper)
        {
            _conectorFila = conectorFila;
            _mapper = mapper;
            _getItemResponse = new GetItemResponse();
        }

        [HttpGet(Name = "GetItemFila")]
        public async Task<IActionResult> GetItemFilaAsync()
        {
            var output = await _conectorFila.Obter();

            if (output.HasNotifications)
            {
                output.Notifications.ToList().ForEach(notification => _getItemResponse.AddNotification(notification));

                return UnprocessableEntity(_getItemResponse);
            }

            _getItemResponse = _mapper.Map<GetItemResponse>(output.ItemFila);

            return Accepted(_getItemResponse);
        }
    }
}
