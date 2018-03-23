using System;
using System.Collections.Generic;

namespace Itac.OemAccess.TestingServer.BuisnessLogic
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
                _devices.Add(serialNumber, dev);
            }
            else
            {
                _devices[serialNumber].SharedKey = sharedKey;
            }
        }

        public Device this[string serialNumber] => _devices[serialNumber];
    }
}
