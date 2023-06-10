using System;

namespace EsotericDevZone.Core
{
    public static partial class Validation
    {
        public static void Assert(bool condition) => ThrowIf(!condition, "Assertion failed");

        public static void ThrowIf(bool condition, Exception exception)
        {
            if (condition)
                throw exception;
        }

        public static void ThrowIf(bool condition, string message)
        {
            if (condition)
                throw new ValidationException(message);
        }

        public static void ThrowIfNull<T>(T value, Exception exception) where T : class => ThrowIf(value == null, exception);
        public static void ThrowIfNull<T>(T value, string message) where T : class => ThrowIf(value == null, new ArgumentNullException(message));

    }
}
