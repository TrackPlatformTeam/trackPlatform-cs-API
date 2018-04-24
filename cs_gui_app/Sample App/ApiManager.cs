using System;
using TrackPlatform.Api;
using TrackPlatform.Other;

namespace TrackPlatform.App.Gui
{
    public class ApiManager : IDisposable
    {
        public delegate void SensorCallback(int sensorIndex, uint value);

        public event SensorCallback DistanceCallback;
        public event SensorCallback LineCallback;

        private readonly GamepadManager _gamepad = new GamepadManager();

        private Manager _manager;

        public ApiManager(SensorCallback distanceCallback, SensorCallback lineCallback)
        {
            DistanceCallback += distanceCallback;
            LineCallback += lineCallback;
        }

        /// <summary>
        /// Connect to target device
        /// </summary>
        /// <param name="comAddress">Address of COM port to connect</param>
        /// <param name="speed">COM port speed</param>
        /// <returns>true, if connectio was successful, else false</returns>
        public bool ConnectToDevice(string comAddress, uint speed)
        {
            if (_manager != null)
            {
                return false;
            }

            CommunicationInfoStruct info = new CommunicationInfoStruct
            {
                SerialInfo =
                {
                    Baudrate = (int) speed,
                    RxPort = comAddress,
                    TxPort = comAddress,
                }
            };
            _manager = new Manager(ConnectionModes.Bluetooth, info);
            return _manager != null;
        }

        /// <summary>
        /// Disconnect from target device
        /// </summary>
        public void Disconnect()
        {
            if (_manager == null) return;

            _manager.Connector.Disconnect();
            _manager.Dispose();
            _manager = null;
        }

        private void ReleaseUnmanagedResources()
        {
            Disconnect();
        }

        public void Dispose()
        {
            ReleaseUnmanagedResources();
            GC.SuppressFinalize(this);
        }

        ~ApiManager()
        {
            ReleaseUnmanagedResources();
        }

        #region Event invocators

        protected virtual void OnDistanceCallback(int sensorindex, uint value)
        {
            DistanceCallback?.Invoke(sensorindex, value);
        }

        protected virtual void OnLineCallback(int sensorindex, uint value)
        {
            LineCallback?.Invoke(sensorindex, value);
        }

        #endregion
    }
}