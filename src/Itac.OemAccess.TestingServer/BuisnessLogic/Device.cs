﻿using System.Collections.Generic;
using Itac.OemAccess.TestingServer.Model;
using Newtonsoft.Json.Linq;

namespace Itac.OemAccess.TestingServer.BuisnessLogic
{
    public class Device
    {
        private bool _appConfigFlag;
        private bool _platformConfigFlag;
        public string SerialNumber { get; set; }
        public string SharedKey { get; set; }

        private readonly PlatformRepository _platformRepository;
        private readonly ApplicationRepository _applicationRepository;

        public OemServerQueue<StateRequest> StateRequestQueue { get; } = new OemServerQueue<StateRequest>();
        public OemServerQueue<Command> CommandRequestQueue { get; } = new OemServerQueue<Command>();
        public OemServerQueue<JObject> ChangesQueue { get; } = new OemServerQueue<JObject>();

        public Device(PlatformRepository platformRepo, ApplicationRepository appRepo)
        {
            _platformRepository = platformRepo;
            _applicationRepository = appRepo;
        }

        public List<string> GetActivities()
        {
            var list = new List<string>();

            if (StateRequestQueue.Any())
                list.Add("states");
            if (CommandRequestQueue.Any())
                list.Add("commands");
            if (ChangesQueue.Any())
                list.Add("changes");
            if (_appConfigFlag)
                list.Add("applicationConfig");
            if (_platformConfigFlag)
                list.Add("platformConfig");

            return list;
        }

        public string LoadAppConfig()
        {
            _appConfigFlag = false;
            return _applicationRepository.Load(SerialNumber);
        }

        public void SaveAppConfig(string data)
        {           
            _applicationRepository.Save(SerialNumber,data);
            _appConfigFlag = true;
        }

        public string LoadPlatformConfig()
        {
            _platformConfigFlag = false;
            return _platformRepository.Load(SerialNumber);
        }

        public void SavePlatformConfig(string data)
        {
            _platformRepository.Save(SerialNumber, data);
            _platformConfigFlag = true;

        }

    }
}
