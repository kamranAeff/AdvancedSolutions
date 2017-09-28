using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.C7
{
    class TPL
    {
        /// <summary>
        /// Delay - bu statik metod vasitesi ile mueyyen qeder gozlemeni temin etmek olar
        /// Laskin TPL-de her isi bir task ustune goturduyune gore cari programa tesir etmesini temin etmek ucun 
        /// .Wait() methodunu istifade etmek lazimdir
        /// </summary>
        public static void Sample1()
        {
            Console.WriteLine("{0:dd.MM.yyyy HH:mm:ss}", DateTime.Now);
            Task.Delay(TimeSpan.FromSeconds(10)).Wait();
            Console.WriteLine("{0:dd.MM.yyyy HH:mm:ss}", DateTime.Now);
        }

        /// <summary>
        /// ContinueWith-methodu bir taskin ardinca diger taski icra etmeni temin edir, 
        /// bele ki bir onceki taskin sehvsiz isleyib islemediyini yoxlayaraq (.IsFaulted-propertisi ile) her hansi ferqli emeliyyatlari ede bilerik
        /// </summary>
        public static void Sample2()
        {
            Task.Run(delegate
            {

                Console.WriteLine("{0:dd.MM.yyyy HH:mm:ss}", DateTime.Now);
                Task.Delay(TimeSpan.FromSeconds(10)).Wait();

            })
            .ContinueWith(pTask => {
                if (!pTask.IsFaulted)
                    Console.WriteLine("previousTask Ok");
                
                throw new Exception("Custom error");
            })
            .ContinueWith(r => nextTask(r, 1));
        }

        private static Task nextTask(Task previousTask, object arg2)
        {
            return Task.Run(delegate {

                if (previousTask.IsFaulted)
                {
                    Console.WriteLine("occured error on previousTask");
                    return;
                }
                Console.WriteLine("{0:dd.MM.yyyy HH:mm:ss}", DateTime.Now);

            });
        }

        /// <summary>
        /// Yaradilmis taskin icra edilmesi ucun .Start() Methodunu cagirmaq mutleqdir
        /// </summary>
        static public void Sample3()
        {
            var t1 = new Task(() => {
                Console.WriteLine("Wait please...");
                Task.Delay(TimeSpan.FromSeconds(10)).Wait();
                Console.WriteLine("{0:dd.MM.yyyy HH:mm:ss}", DateTime.Now);
                
            });// Anonymous methods with lambda

            t1.Start();// taskin icra edilmesi ucun mutleq vacibdir
        }

        /// <summary>
        /// .Run()-methodu qeyd olunmus emeliyyati yerine yetirmek ucun Thread poola-yeni task elave edir, ve geriye hemin taski object kimi qaytarir
        /// .Run()-methodunu icra etdikde taski .Start etmete ehtiyyac yoxdur
        /// </summary>
        static public void Sample4()
        {
            var tResult=Task.Run(() => {
                Console.WriteLine("Wait please...");
                Task.Delay(TimeSpan.FromSeconds(10)).Wait();
                Console.WriteLine("{0:dd.MM.yyyy HH:mm:ss}", DateTime.Now);
            });

            Console.WriteLine(tResult.Status);
        }

        /// <summary>
        /// .Run<TResult>()-methodu .Run()-methodundan (return Task; )ferqli olaraq  geriye netice qaytarir "Task<TResult>" tipinde
        /// .neticeni almaq ucun await acar sozunden istifade olunur
        /// await acar sozu taninmaq ucun async acar sozu ile isarelenmis methodda yazilmalidir
        /// await ile isarelenen task emeliyyatin sona catmasini gozleyir mutleq
        /// </summary>
        async static public void Sample5()
        {
            var tResult = await Task.Run<int>(() =>
            {
                Console.WriteLine("Wait please...");
                Task.Delay(TimeSpan.FromSeconds(10)).Wait();
                return 5;
            });

            Console.WriteLine(">> Result: {0}", tResult);
        }
    }
}
