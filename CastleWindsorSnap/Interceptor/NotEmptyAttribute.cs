namespace CastleWindsorSnap.Interceptor

{
    using Snap;

    /// <summary>
    /// Attribute, um zu verhindern, dass ein Parameter leer ist
    /// </summary>
    public class NotEmptyAttribute : MethodInterceptAttribute
    {
    }
}
