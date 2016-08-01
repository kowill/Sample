using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Runtime.InteropServices.ComTypes;
using static DirectShowSample.Api.Definition;

namespace DirectShowSample.Api
{
    public static class Device
    {
        public static object CoCreateInstance(string clsid)
        {
            return Activator.CreateInstance(Type.GetTypeFromCLSID(new Guid(clsid)));
        }
        public static void Release(object obj)
        {
            if (obj != null)
            {
                Marshal.ReleaseComObject(obj);
            }
        }
        public static List<DeviceInf> GetFilterList(Guid category)
        {
            ICreateDevEnum device = null;
            IEnumMoniker enumrator = null;
            var list = new List<DeviceInf>();
            try
            {
                device = (ICreateDevEnum)Activator.CreateInstance(Type.GetTypeFromCLSID(new Guid(Definition.CLSID_SystemDeviceEnum)));

                device.CreateClassEnumerator(ref category, out enumrator, 0);

                var moniker = new IMoniker[1];
                var fetched = IntPtr.Zero;

                Func<IPropertyBag, string, string> readProperty = (bag, propertyName) =>
                {
                    try
                    {
                        object tmp = null;
                        bag.Read(propertyName, out tmp, 0);
                        return (string)tmp;
                    }
                    catch
                    {
                        return "";
                    }
                };


                while (enumrator.Next(moniker.Length, moniker, fetched) == 0)
                {
                    IPropertyBag bag = null;
                    object propertyObject = null;
                    var propertyGuid = new Guid(Definition.PropertyBag);
                    moniker[0].BindToStorage(null, null, ref propertyGuid, out propertyObject);
                    bag = (IPropertyBag)propertyObject;

                    object filter = null;
                    var baseFilterGuid = new Guid(Definition.IID_IBaseFilter);
                    moniker[0].BindToObject(null, null, ref baseFilterGuid, out filter);

                    var inf = new DeviceInf();

                    inf.Name = readProperty(bag, "FriendlyName");
                    inf.CLSID = readProperty(bag, "CLSID");
                    inf.Filter = (IBaseFilter)filter;

                    list.Add(inf);
                }
                for (int i = 0; i < list.Count - 1; i++)
                {
                    for (int j = i + 1; j < list.Count; j++)
                    {
                        if (list[j].Name == list[i].Name)
                        {
                            list[j].Index = list[i].Index + 1;
                            break;
                        }
                    }
                }
                return list;
            }
            finally
            {
                if (enumrator != null)
                {
                    Marshal.ReleaseComObject(enumrator);
                }
                if (device != null)
                {
                    Marshal.ReleaseComObject(device);
                }
            }
        }

        public static IBaseFilter CreateFilter(string category, string clsid, int index)
        {
            IEnumMoniker enumerator = null;
            ICreateDevEnum device = null;
            var categoryGuid = new Guid(category);
            var localIndex = index;
            try
            {
                device = (ICreateDevEnum)Activator.CreateInstance(Type.GetTypeFromCLSID(new Guid(Definition.CLSID_SystemDeviceEnum)));
                device.CreateClassEnumerator(ref categoryGuid, out enumerator, 0);

                var monikers = new IMoniker[1];
                var fetched = IntPtr.Zero;

                Func<IPropertyBag, string, string> readProperty = (bag, propertyName) =>
                {
                    try
                    {
                        object tmp = null;
                        bag.Read(propertyName, out tmp, 0);
                        return (string)tmp;
                    }
                    catch
                    {
                        return "";
                    }
                };


                while (enumerator.Next(monikers.Length, monikers, fetched) == 0)
                {
                    IPropertyBag bag = null;
                    object propertyObject = null;
                    var propertyGuid = new Guid(Definition.PropertyBag);
                    monikers[0].BindToStorage(null, null, ref propertyGuid, out propertyObject);
                    bag = (IPropertyBag)propertyObject;

                    try
                    {
                        if (readProperty(bag, "CLSID") != clsid)
                        {
                            continue;
                        };
                        if (0 < localIndex)
                        {
                            localIndex--;
                            continue;
                        }

                        object filter = null;
                        var filterGuid = new Guid(Definition.IID_IBaseFilter);
                        monikers[0].BindToObject(null, null, ref filterGuid, out filter);

                        return (IBaseFilter)filter;
                    }
                    finally
                    {
                        Marshal.ReleaseComObject(bag);
                        if (monikers[0] != null)
                        {
                            Marshal.ReleaseComObject(monikers[0]);
                        }
                    }
                }
            }
            finally
            {
                if (enumerator != null)
                {
                    Marshal.ReleaseComObject(enumerator);
                }
                if (device != null)
                {
                    Marshal.ReleaseComObject(device);
                }
            }
            return null;
        }

        public static IPin FindPin(IBaseFilter filter, string name)
        {
            IEnumPins enumpins = null;
            IPin pin = null;
            try
            {
                filter.EnumPins(ref enumpins);

                int fetched = 0;
                while (enumpins.Next(1, pin, fetched) == 0)
                {
                    if (fetched == 0) break;

                    var info = new PIN_INFO();
                    try
                    {

                    }
                    finally
                    {
                        if (pin != null) Marshal.ReleaseComObject(pin);
                        if (info.Filter != null) Marshal.ReleaseComObject(info.Filter);
                    }
                }
            }
            finally
            {

            }
            return null;
        }
    }
    public class DeviceInf
    {
        public String Name { get; set; }
        public string CLSID { get; set; }
        public int Index { get; set; }

        public IBaseFilter Filter { get; set; }

        ~DeviceInf()
        {
            if (this.Filter != null)
            {
                Marshal.ReleaseComObject(this.Filter);
            }
        }

    }
}
