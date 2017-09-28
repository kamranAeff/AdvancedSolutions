using ConsoleApp.C7;
using System;
using System.Threading;

namespace ConsoleApp.C1
{
    class Program
    {
        static int length = 10;
        static void Main(string[] args)
        {
            Console.Title = "Chapter 7 (#70-483)";

            /*
             Threading
             */

            //Example.Sample1();

            //Example.Sample2();

            //Example.Sample3();

            /*
             TPL - Task Paralel Library 
             */

            //TPL.Sample1();

            //TPL.Sample2();

            //TPL.Sample3();            
            
            //TPL.Sample4();

            TPL.Sample5();

            Console.ReadLine();
        }

        
    }
}
