using System;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Middleware;

namespace Functions.Worker.ContextAccessor.Internal
{
    internal class FunctionContextAccessorMiddleware : IFunctionsWorkerMiddleware
    {
        private IFunctionContextSetter FunctionContextSetter { get; }

        public FunctionContextAccessorMiddleware(IFunctionContextSetter setter)
        {
            FunctionContextSetter = setter;
        }

        public Task Invoke(FunctionContext context, FunctionExecutionDelegate next)
        {
            if (FunctionContextSetter.FunctionContext != null)
            {
                // This should never happen because the context should be localized to the current Task chain.
                // But if it does happen (perhaps the implementation is bugged), then we need to know immediately so it can be fixed.
                throw new InvalidOperationException($"Unable to initalize {nameof(IFunctionContextAccessor)}: context has already been initialized.");
            }

            FunctionContextSetter.FunctionContext = context;

            return next(context);
        }
    }
}
