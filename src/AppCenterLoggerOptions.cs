using System;
using System.Collections.Generic;
using System.Text;

namespace Microsoft.Extensions.Logging
{
    public class AppCenterLoggerOptions
    {
        public bool IncludeScopes { get; set; }
        public string AppCenteriOSSecret { get; set; }
        public string AppCenterAndroidSecret { get; set; }
        public string AppCenterUWPSecret { get; set; }

        public string AppCenterSecret
        {
            get
            {
                var sb = new StringBuilder();

                if (!string.IsNullOrEmpty(AppCenterAndroidSecret))
                {
                    sb.Append($"android={AppCenterAndroidSecret}");
                }

                if (!string.IsNullOrEmpty(AppCenteriOSSecret))
                {
                    if (sb.Length > 0)
                    {
                        sb.Append(";");
                    }
                    sb.Append($"ios={AppCenteriOSSecret}");
                }

                if (!string.IsNullOrEmpty(AppCenterUWPSecret))
                {
                    if (sb.Length > 0)
                    {
                        sb.Append(";");
                    }
                    sb.Append($"uwp={AppCenterUWPSecret}");
                }

                return sb.ToString();
            }
        }
    }
}
