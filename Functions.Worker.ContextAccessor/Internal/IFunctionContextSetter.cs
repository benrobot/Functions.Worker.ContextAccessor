using Microsoft.Azure.Functions.Worker;

namespace Functions.Worker.ContextAccessor.Internal
{
    internal interface IFunctionContextSetter
    {
        FunctionContext FunctionContext { get; set; }
    }
}
