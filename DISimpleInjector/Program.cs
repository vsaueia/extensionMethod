using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SimpleInjector;

namespace DISimpleInjector
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = new Container();
            container.Register<ICart, DataAccessLayer>(Lifestyle.Singleton);
            var BL = container.GetInstance<BussinessLayer>();
            BL.InsertToCart();
            Console.ReadLine();            
        }
    }
}
