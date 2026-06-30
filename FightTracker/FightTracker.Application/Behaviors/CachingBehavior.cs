using FightTracker.Application.CachingServices;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FightTracker.Application.Behaviors
{
    public class CachingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly ICachingService cachingService;

        public CachingBehavior(ICachingService cachingService)
        {
            this.cachingService = cachingService;
        }
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (request is not ICacheable cacheable)
            {
                return await next();
            }
            var cached = await cachingService.GetAsync<TResponse>(cacheable.Key);

            if (cached != null)
            {
                return cached;
            }
            var result = await next();

            if (result != null)
            {
                await cachingService.SetAsync(cacheable.Key, result, cacheable.Expiration);
            }

            return result;
        }
    }
}
