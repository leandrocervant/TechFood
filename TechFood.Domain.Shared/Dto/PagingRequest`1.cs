using System;

namespace TechFood.Domain.Shared.Dto
{
    public class PagingRequest<TSort> : PagingRequest where TSort : struct, IConvertible
    {
        public TSort Sort { get; set; }
    }
}
