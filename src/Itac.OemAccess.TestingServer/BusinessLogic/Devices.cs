using System;
using System.Collections.Generic;
using System.Linq;

namespace Itac.OemAccess.TestingServer.BusinessLogic
{

    public class Devices
    {
        private readonly Func<Device> _deviceFactory;
        private readonly Dictionary<string, Device> _devices = new Dictionary<string, Device>(StringComparer.OrdinalIgnoreCase);

        public Devices(Func<Device> deviceFactory)
        {
            _deviceFactory = deviceFactory;
        }

        public void AddNew(string serialNumber, string sharedKey)
        {
            if (!_devices.ContainsKey(serialNumber))
            {
                var dev = _deviceFactory();
                dev.SerialNumber = serialNumber;
                dev.SharedKey = sharedKey;
                dev.SaveBootConfig(null);
                dev.SavePlatformConfig(null);
                dev.SaveAppConfig(null);
                _devices.Add(serialNumber, dev);
            }
            else
            {
                _devices[serialNumber].SharedKey = sharedKey;
            }
        }

        public Device this[string serialNumber] => _devices[serialNumber];

        public List<Device> GetAll()
        {
            return _devices.Values.ToList();
        }
    }
}
