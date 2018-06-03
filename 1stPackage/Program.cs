using Constellation;
using Constellation.Package;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1stPackage
{
    public class Program : PackageBase 
    {
        static void Main(string[] args)
        {
            PackageHost.Start<Program>(args);
        }

        public override void OnStart()
        {
            PackageHost.WriteInfo("Coucou :3 IsRunning: {0} - IsConnected: {1}", PackageHost.IsRunning, PackageHost.IsConnected);
            if (PackageHost.IsConnected == false)
            {
                PackageHost.WriteError("Euh, y'a quelqu'un ?????");
                PackageHost.WriteWarn("Je me sens seul :'(");
            }
                
            else
            {
            PackageHost.WriteInfo("Youpi youpi je suis connecté :D");
            }

            Random rnd = new Random();

            Task.Factory.StartNew(async () =>
            {
                while(PackageHost.IsRunning)
                {
                    var myData = new TempHumdi()
                    {
                        Temperature = rnd.Next(0,50),
                        Humidity = rnd.Next(10,90)
                    };
                    PackageHost.PushStateObject("TemperatureHumidity", myData,lifetime:15);
                    await Task.Delay (PackageHost.GetSettingValue<Int32>("Interval"));
                    
                }
            });

            
        }

        /// <summary>
        /// Permet de faire bipper
        /// </summary>
        /// <param name="frequence">Frequence en Hz</param>
        /// <param name="duree">Duree en ms</param>

        [MessageCallback]
        public void Beep(int frequence, int duree)
        {
            Console.Beep(frequence, duree);

        }


        public override void OnPreShutdown()
        {
            base.OnPreShutdown();
        }
    }
}
