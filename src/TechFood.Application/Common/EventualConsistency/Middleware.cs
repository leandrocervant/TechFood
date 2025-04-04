using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TechFood.Domain.UoW;

namespace TechFood.Application.Common.EventualConsistency
{
    internal class Middleware(RequestDelegate next)
    {
        private readonly RequestDelegate _next = next;

        public async Task InvokeAsync(HttpContext context, IUnitOfWorkTransaction transaction)
        {
            context.Response.OnCompleted(async () =>
            {
                try
                {
                    if (
                        context.Items.TryGetValue(Mediator.EventsQueueKey, out var value) &&
                        value is Queue<INotification> eventsQueue)
                    {
                        var publisher = context.RequestServices.GetRequiredKeyedService<IMediator>(Mediator.ServiceKey);

                        while (eventsQueue!.TryDequeue(out var domainEvent))
                        {
                            await publisher.Publish(domainEvent);
                        }
                    }

                    await transaction.CommitAsync();
                }
                catch (Exception)
                {
                    // notify the client that even though they got a good response, the changes didn't take place
                    // due to an unexpected error

                    await transaction.RollbackAsync();
                }
                finally
                {
                }

            });

            await _next(context);
        }
    }
}
