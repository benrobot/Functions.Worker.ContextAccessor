using Microsoft.Azure.Functions.Worker;

namespace Functions.Worker.ContextAccessor
{
    /// <summary>
    /// Accessor for <see cref="FunctionContext"/>
    /// </summary>
    public interface IFunctionContextAccessor
    {
        /// <inheritdoc cref="Microsoft.Azure.Functions.Worker.FunctionContext"/>
        FunctionContext FunctionContext { get; }
    }
}
