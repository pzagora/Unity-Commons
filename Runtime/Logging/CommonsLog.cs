using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Commons
{
    /// <summary>
    /// Logging for the Commons packages. Routes through the Reporter package (<c>UnityEngine.Report</c>) when it is in the
    /// project (<c>PACKAGE_REPORTER</c>, set by the assembly's version defines) and falls back to <see cref="Debug"/>
    /// otherwise, so no Commons package depends on Reporter.
    /// </summary>
    internal static class CommonsLog
    {
#if !PACKAGE_REPORTER
        private const string FALLBACK_FORMAT = "[{0}] {1}";
#endif

        [HideInCallstack]
        public static void Event(string source, object content, bool? passed = null, Object context = null)
        {
#if PACKAGE_REPORTER
            Report.Event(content, passed, sourceName: source, context: context);
#else
            Debug.Log(Format(source, content), context);
#endif
        }

        [HideInCallstack]
        public static void Warning(string source, object content, Object context = null)
        {
#if PACKAGE_REPORTER
            Report.Warning(content, sourceName: source, context: context);
#else
            Debug.LogWarning(Format(source, content), context);
#endif
        }

        [HideInCallstack]
        public static void Error(string source, object content, Object context = null)
        {
#if PACKAGE_REPORTER
            Report.Error(content, sourceName: source, context: context);
#else
            Debug.LogError(Format(source, content), context);
#endif
        }

        [HideInCallstack]
        public static void Exception(string source, Exception exception, Object context = null)
        {
#if PACKAGE_REPORTER
            Report.Exception(exception, source, context: context);
#else
            Debug.LogException(exception, context);
#endif
        }

#if !PACKAGE_REPORTER
        private static string Format(string source, object content)
            => string.IsNullOrEmpty(source) ? content?.ToString() : string.Format(FALLBACK_FORMAT, source, content);
#endif
    }
}
