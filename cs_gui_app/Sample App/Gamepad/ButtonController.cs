namespace TrackPlatform.App.Gui.Gamepad
{
    public class ButtonController : IUpdatable
    {
        private const bool DefaultValue = false;

        public delegate void DigitalCallback();
        public delegate bool? GetDigitalValueIsPressed();

        public event DigitalCallback Pressed;
        public event DigitalCallback Released;
        public event DigitalCallback Toggled;
        public event GetDigitalValueIsPressed ReadValue;

        private bool _oldValueIsPressed = DefaultValue;

        public void Update()
        {
            bool newValue = OnReadValue();
            bool oldValue = _oldValueIsPressed;
            _oldValueIsPressed = newValue;
            if (oldValue == newValue)
            {
                return;
            }

            OnToggled();

            if (newValue)
            {
                OnPressed();
            }
            else
            {
                OnReleased();
            }
        }

        #region Event Invocators

        protected virtual void OnPressed()
        {
            Pressed?.Invoke();
        }

        protected virtual void OnReleased()
        {
            Released?.Invoke();
        }

        protected virtual void OnToggled()
        {
            Toggled?.Invoke();
        }

        protected virtual bool OnReadValue()
        {
            return ReadValue?.Invoke() ?? DefaultValue;
        }

        #endregion
    }
}