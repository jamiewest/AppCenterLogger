using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging.Configuration;
using Microsoft.Extensions.Logging.AppCenter;
using Microsoft.Extensions.Options;

namespace Microsoft.Extensions.Logging
{
    /// <summary>
    /// Extension methods for the <see cref="ILoggerFactory"/> class.
    /// </summary>
    public static class AppCenterLoggerFactoryExtensions
    {
        public static ILoggingBuilder AddAppCenter(this ILoggingBuilder builder, Action<AppCenterLoggerOptions> configure)
        {
            if (configure == null)
            {
                throw new ArgumentNullException(nameof(configure));
            }

            builder.Services.TryAddEnumerable(ServiceDescriptor.Singleton<ILoggerProvider, AppCenterLoggerProvider>());
            builder.Services.Configure(configure);

            return builder;
        }
    }
}
