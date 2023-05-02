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

    }
}
