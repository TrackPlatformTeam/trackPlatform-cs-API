using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using BrandonPotter.XBox;

namespace TrackPlatform.App.Gui.Gamepad
{
    public class GamepadManager
    {
        private const int UpdateInfoPeriodInMs = 40;

        private Timer _backThread;
        private readonly List<XBoxController> _controllers;
        private readonly XBoxControllerWatcher _watcher = new XBoxControllerWatcher();

        public GamepadManager()
        {
            Init();
            _backThread = new Timer(Update, null, 0, UpdateInfoPeriodInMs);

            _watcher.ControllerConnected += (c) => { _controllers.Add(c); };
            _watcher.ControllerDisconnected += (c) => { _controllers.Remove(c); };

            _controllers = XBoxController.GetConnectedControllers().ToList();
        }

        private void Init()
        {
            throw new System.NotImplementedException();
        }

        private void Update(object state)
        {
            throw new System.NotImplementedException();
        }

        private XBoxController GetController()
        {
            return _controllers.Count > 0 ? _controllers[0] : null;
        }
    }
}