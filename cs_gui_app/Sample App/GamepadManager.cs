using System.Collections.Generic;
using System.Linq;
using System.Threading;
using BrandonPotter.XBox;

namespace TrackPlatform.App.Gui
{
    public class GamepadManager
    {
        public const int UpdateInfoPeriodInMs = 40;

        private Thread _backThread;
        private List<XBoxController> _controllers;
        private XBoxControllerWatcher _watcher = new XBoxControllerWatcher();

        public GamepadManager()
        {
            _backThread = new Thread(ThreadCallback)
            {
                IsBackground = true
            };

            _watcher.ControllerConnected += (c) => { _controllers.Add(c); };
            _watcher.ControllerDisconnected += (c) => { _controllers.Remove(c); };

            _controllers = XBoxController.GetConnectedControllers().ToList();
        }

        private void ThreadCallback()
        {
            while (true)
            {
                if (_controllers.Count > 0)
                {
                    //TODO: send api commands
                }
                else
                {
                    Thread.Sleep(UpdateInfoPeriodInMs);
                }
            }
        }
    }
}