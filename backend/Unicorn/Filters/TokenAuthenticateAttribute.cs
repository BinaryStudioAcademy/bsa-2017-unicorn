using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Filters;
using Unicorn.Core.Interfaces;
using Unicorn.Filters.Helpers;

namespace Unicorn.Filters
{
    public class TokenAuthenticateAttribute : Attribute, IAuthenticationFilter
    {
        public bool AllowMultiple => false;

        public async Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        {
            try
            {
                IAuthService _authService = context.ActionContext.ControllerContext.Configuration
                                  .DependencyResolver.GetService(typeof(IAuthService)) as IAuthService;

                HttpRequestMessage request = context.Request;
                AuthenticationHeaderValue authorization = request.Headers.Authorization;

                if (authorization == null)
                {
                    context.ErrorResult = new AuthenticationFailureResult("Missing authorization header", request);
                    return;
                }
                if (authorization.Scheme != "Bearer")
                {
                    context.ErrorResult = new AuthenticationFailureResult("Invalid authorization scheme", request);
                    return;
                }
                if (string.IsNullOrEmpty(authorization.Parameter))
                {
                    context.ErrorResult = new AuthenticationFailureResult("Missing Token", request);
                    return;
                }

                bool correctToken = await _authService.ValidateTokenAsync(authorization.Parameter);
                if (!correctToken)
                {
                    context.ErrorResult = new AuthenticationFailureResult("Invalid Token", request);
                }
            }
            catch (Exception ex)
            {
                context.ErrorResult = new AuthenticationFailureResult("Exception: \n" + ex.Message, context.Request);
            }
        }

        public Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        {
            return Task.FromResult<object>(null);
        }
    }
}