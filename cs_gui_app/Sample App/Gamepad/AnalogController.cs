using System;
using TrackPlatform.App.Gui.Tools;

namespace TrackPlatform.App.Gui.Gamepad
{
    public class AnalogController : IUpdatable
    {
        private const double DefaultValue = 0;
        private const double MinValue = -1;
        private const double MaxValue = 1;
        private const double MinValueInput = 0;
        private const double MaxValueInput = 100;
        private const double ComparisonTolerance = 1e-3;

        public delegate void AnalogCallback(double newValue, double oldValue);
        public delegate double? GetAnalogValue();

        public event AnalogCallback AnalogChanged;
        public event GetAnalogValue ReadValue;

        private double _oldValue = DefaultValue;

        public void Update()
        {
            double newValue = Normilize(OnReadValue());
            double oldValue = _oldValue;
            _oldValue = newValue;

            if (Math.Abs(oldValue - newValue) > ComparisonTolerance)
            {
                OnAnalogChanged(newValue, oldValue);
            }
        }

        private static double Normilize(double value)
        {
            return CustomMath.Remap(value, MinValueInput, MaxValueInput, MinValue, MaxValue);
        }

        #region Event invocators

        protected virtual double OnReadValue()
        {
            return ReadValue?.Invoke() ?? DefaultValue;
        }

        protected virtual void OnAnalogChanged(double newvalue, double oldvalue)
        {
            AnalogChanged?.Invoke(newvalue, oldvalue);
        }

        #endregion
    }
}