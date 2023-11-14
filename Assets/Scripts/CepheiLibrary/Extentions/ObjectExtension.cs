namespace Cephei
{
    public static class ObjectExtension
    {
        public static T[] GetArray<T>(this T obj) => new T[] { obj };
    }
}