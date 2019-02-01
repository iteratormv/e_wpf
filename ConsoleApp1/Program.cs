using EX.Client;
using EX.Client.ServiceReference1;
using EX.Service;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceExecutor serviceExecutor = new ServiceExecutor();
            serviceExecutor.statusChanged += DisplayStatus;
            Task.Factory.StartNew(serviceExecutor.Start);
            Thread.Sleep(1000);
            // for (int i = 0; i < 100; i++)
            //{
            //    Console.WriteLine(i);
            //    Thread.Sleep(1000);
            //}

            ClientExecutor clientExecutor = new ClientExecutor();
            VisitorDTO v = new VisitorDTO { Column1 = "Первая колонка" };
            VisitorDTO v1 = new VisitorDTO { Column1 = "Вторая лолонка", Column2 = "Вторая колонка" };
            clientExecutor.GetClient().AddOrUpdateVisitor(v);
            clientExecutor.GetClient().AddOrUpdateVisitor(v);
            clientExecutor.GetClient().AddOrUpdateVisitor(v1);
            Thread.Sleep(1000);
            var cv = clientExecutor.GetClient().GetAllVisitors();
            foreach (var c in cv) { Console.WriteLine(c.Column1+" - "+(c.Column2==null?"n":c.Column2)); }

            for(int i = 1; i < 200; i++) { Thread.Sleep(1000); Console.WriteLine(i); }
        }

        private static void DisplayStatus(string obj)
        {
            Console.WriteLine((string)obj);
        }
    }
}
