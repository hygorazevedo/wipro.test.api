using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using wipro.teste.gateway;

namespace wipro.teste.api.Controllers.PostItem
{
    [ApiController]
    [Route("api/[controller]")]
    public class ItemController : ControllerBase
    {
        private readonly IConectorFila _conectorFila;
        private readonly IMapper _mapper;
        private PostItemResponse _postItemResponse;

        public ItemController(IConectorFila conectorFila, IMapper mapper)
        {
            _conectorFila = conectorFila;
            _mapper = mapper;
            _postItemResponse = new PostItemResponse();
        }

        [HttpPost(Name = "AddItemFila")]
        public async Task<IActionResult> AddItemFilaAsync([FromBody]IList<PostItemRequest> requestItems)
        {
            ItemFilaValidator validator = new ItemFilaValidator();
            var validationResult = validator.Validate(requestItems);

            if (!validationResult.IsValid)
            {
                validationResult.Errors.ForEach(error => _postItemResponse.AddNotification(error.ErrorMessage));

                return BadRequest(_postItemResponse);
            }

            var itensFila = _mapper.Map<IList<ItemFila>>(requestItems);
            await _conectorFila.Postar(itensFila);

            return Accepted(_postItemResponse);
        }
    }
}