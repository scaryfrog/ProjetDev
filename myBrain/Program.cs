using Constellation;
using Constellation.Package;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace myBrain
{


    public class Program : PackageBase
    {
        [StateObjectLink("1stPackage", "TemperatureHumidity")]
        public StateObjectNotifier TemperatureHumidity { get; set; }
        static void Main(string[] args)
        {
            PackageHost.Start<Program>(args);
        }

        public override void OnStart()
        {
            PackageHost.WriteInfo("Online : {0} - IsConnected: {1}", PackageHost.IsRunning, PackageHost.IsConnected);
            this.TemperatureHumidity.ValueChanged += TemperatureHumidity_ValueChanged;
        }

        private void TemperatureHumidity_ValueChanged(object sender, StateObjectChangedEventArgs e)
        {
           if(e.NewState.DynamicValue.Temperature> PackageHost.GetSettingValue<Int32>("seuil"))
            {
                PackageHost.WriteWarn("Attention température à {0}°C", TemperatureHumidity.DynamicValue.Temperature);

            }
        }
    }
}
