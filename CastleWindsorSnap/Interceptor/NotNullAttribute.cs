
namespace CastleWindsorSnap.Interceptor
{
    using Snap;

    /// <summary>
    /// Dieses Attribut wird über einer Methode angebracht um zu verhindern das ein Parameter Null ist.
    /// </summary>
    public class NotNullAttribute : MethodInterceptAttribute
    {
    }
}
