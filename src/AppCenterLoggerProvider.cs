using System;
using System.Collections.Concurrent;
using Microsoft.Extensions.Options;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using AppCenterBase = Microsoft.AppCenter.AppCenter;

namespace Microsoft.Extensions.Logging.AppCenter
{
    public class AppCenterLoggerProvider : ILoggerProvider, ISupportExternalScope
    {
        private readonly IOptionsMonitor<AppCenterLoggerOptions> _options;
        private readonly ConcurrentDictionary<string, AppCenterLogger> _loggers;

        private IDisposable _optionsReloadToken;
        private IExternalScopeProvider _scopeProvider = NullExternalScopeProvider.Instance;

        public AppCenterLoggerProvider(IOptionsMonitor<AppCenterLoggerOptions> options)
        {
            _options = options;
            _loggers = new ConcurrentDictionary<string, AppCenterLogger>();
            AppCenterBase.LogLevel = _options.CurrentValue.AppCenterLogLevel;
            AppCenterBase.Start(_options.CurrentValue.AppCenterSecret, typeof(Analytics), typeof(Crashes));
            ReloadLoggerOptions(options.CurrentValue);
        }

        private void ReloadLoggerOptions(AppCenterLoggerOptions options)
        {
            foreach (var logger in _loggers)
            {
                logger.Value.Options = options;
            }

            _optionsReloadToken = _options.OnChange(ReloadLoggerOptions);
        }

        /// <inheritdoc />
        public ILogger CreateLogger(string name)
        {
            return _loggers.GetOrAdd(name, loggerName => new AppCenterLogger(name)
            {
                Options = _options.CurrentValue,
                ScopeProvider = _scopeProvider
            });
        }

        /// <inheritdoc />
        public void Dispose()
        {
            _optionsReloadToken?.Dispose();
        }

        /// <inheritdoc />
        public void SetScopeProvider(IExternalScopeProvider scopeProvider)
        {
            _scopeProvider = scopeProvider;

            foreach (var logger in _loggers)
            {
                logger.Value.ScopeProvider = _scopeProvider;
            }

        }
    }
}