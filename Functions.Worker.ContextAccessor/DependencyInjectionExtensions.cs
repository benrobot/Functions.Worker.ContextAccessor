using Functions.Worker.ContextAccessor.Internal;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Functions.Worker.ContextAccessor
{
    /// <summary>
    /// Dependency Injection Extensions
    /// </summary>
    public static class DependencyInjectionExtensions
    {
        /// <summary>
        /// Adds <see cref="IFunctionContextAccessor"/>. Must be used in conjunction with <see cref="UseFunctionContextAccessor"/>
        /// </summary>
        public static IServiceCollection AddFunctionContextAccessor(this IServiceCollection services)
        {
            services.AddSingleton<IFunctionContextAccessor, FunctionContextAccessor>();
            services.AddSingleton<IFunctionContextSetter, FunctionContextAccessor>();
            return services;
        }

        /// <summary>
        /// Required by <see cref="AddFunctionContextAccessor"/>
        /// </summary>
        public static IFunctionsWorkerApplicationBuilder UseFunctionContextAccessor(
            this IFunctionsWorkerApplicationBuilder builder)
        {
            builder.UseMiddleware<FunctionContextAccessorMiddleware>();
            return builder;
        }
    }
}
