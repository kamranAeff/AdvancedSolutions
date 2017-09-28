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
    }
}
