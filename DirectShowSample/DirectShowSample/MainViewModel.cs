using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Livet;
using System.Collections.ObjectModel;
using DirectShowSample.Api;

namespace DirectShowSample
{
    public class MainViewModel : ViewModel
    {
        public MainViewModel()
        {
            GetDevices();
        }

        private void GetDevices()
        {
            Devices.Clear();
            foreach (var item in Device.GetFilterList(new Guid(Definition.CLSID_VideoInputDeviceCategory)))
            {
                Devices.Add(item);
            }
            //RaisePropertyChanged(nameof(Devices));
        }

        public ObservableCollection<DeviceInf> Devices { get; } = new ObservableCollection<DeviceInf>();


    }
}
