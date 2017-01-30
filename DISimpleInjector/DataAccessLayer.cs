using System;
using System.Runtime.ConstrainedExecution;

namespace DISimpleInjector
{
    public class DataAccessLayer : ICart
    {
        public string AddToCart()
        {
            string val = "Simple Injector is fastest DI as compare to";
            Console.WriteLine(val);
            return val;
        }
    }
}