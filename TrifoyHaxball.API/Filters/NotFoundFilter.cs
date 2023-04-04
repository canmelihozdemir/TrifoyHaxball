using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TrifoyHaxball.Core.DTOs;
using TrifoyHaxball.Core.Services;
using TrifoyHaxball.Entity;

namespace TrifoyHaxball.API.Filters
{
    public class NotFoundFilter<T,T2> : IAsyncActionFilter where T:BaseEntity where T2 : BaseDto
    {
        private readonly IService<T,T2> _service;

        public NotFoundFilter(IService<T,T2> service)
        {
            _service = service;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var idValue=context.ActionArguments.Values.FirstOrDefault();

            if (idValue is null)
            {
                await next.Invoke();
                return;
            }

            var id = (int)idValue;
            var anyEntity = await _service.AnyAsync(x=>x.Id==id);

            if (anyEntity)
            {
                await next.Invoke();
                return;
            }

            context.Result = new NotFoundObjectResult(CustomResponseDto<NoContentDto>.Fail(404,$"{typeof(T).Name}({id}) not found!"));
        }
    }
}
