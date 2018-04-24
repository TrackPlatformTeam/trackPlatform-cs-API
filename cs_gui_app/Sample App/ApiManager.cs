using System;
using TrackPlatform.Api;
using TrackPlatform.Other;

namespace TrackPlatform.App.Gui
{
    public class ApiManager : IDisposable
    {
        public delegate void SensorCallback(int sensorIndex, uint value);

        //private static IntPtr Connect(string comAddress, uint speed);
        //private static void Disconnect(IntPtr manager);
        //private static void SetSensorCallbacks(SensorCallback distanceSensorCallback, SensorCallback lineSensorCallback);

        public event SensorCallback _distanceCallback;
        public event SensorCallback _lineCallback;
        //private IntPtr _unmanagedPtr = IntPtr.Zero;

        private Api.Manager _manager;

        public ApiManager(SensorCallback distanceCallback, SensorCallback lineCallback)
        {
            _distanceCallback = distanceCallback;
            _lineCallback = lineCallback;
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
    }
}