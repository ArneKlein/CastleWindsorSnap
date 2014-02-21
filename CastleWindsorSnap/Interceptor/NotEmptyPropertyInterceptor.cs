
namespace CastleWindsorSnap.Interceptor
{
    using System;
    using System.Reflection;
    using Castle.DynamicProxy;
    using Snap;

    /// <summary>
    /// Ein Interceptor um leere Elemente abzufangen
    /// </summary>
    public class NotEmptyPropertyInterceptor : MethodInterceptor
    {
        public override void BeforeInvocation()
        {
            //Console.WriteLine("this is executed before your method");
            base.BeforeInvocation();
        }

        public override void InterceptMethod(IInvocation invocation, MethodBase method, Attribute attribute)
        {
            // Just keep running for this demo.  
            invocation.Proceed(); // the underlying method call
        }

        public override void AfterInvocation()
        {
            //Console.WriteLine("this is executed after your method");
            base.AfterInvocation();
        }
    }
}
