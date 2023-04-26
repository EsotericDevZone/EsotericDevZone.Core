namespace EsotericDevZone.Core.Extensions
{
    public static class IntExtensions
    {
        public static bool IsBetween(this int x, int a, int b) => a <= x && x <= b;

        public static int Clamp(this int x, int a, int b)
        {
            if (x < a) return a;
            if (x > b) return b;
            return x;
        }
    }
}
