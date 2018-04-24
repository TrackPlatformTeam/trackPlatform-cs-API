namespace TrackPlatform.App.Gui.Gamepad
{
    public class StickController : IUpdatable
    {
        public delegate void StickCallback(double newX, double newY, double oldX, double oldY);
        
        public event StickCallback StickChanged;
        public event AnalogController.GetAnalogValue ReadValueX;
        public event AnalogController.GetAnalogValue ReadValueY;

        private readonly AnalogController _x;
        private readonly AnalogController _y;

        private bool _isChanged = false;
        private double _newX;
        private double _newY;
        private double _oldX;
        private double _oldY;

        public StickController()
        {
            _x = new AnalogController();
            _x.ReadValue += OnReadValueX;
            _x.AnalogChanged += (value, oldValue) =>
            {
                _isChanged = true;
                _newX = value;
                _oldX = oldValue;
            };

            _y = new AnalogController();
            _y.ReadValue += OnReadValueY;
            _y.AnalogChanged += (value, oldValue) =>
            {
                _isChanged = true;
                _newY = value;
                _oldY = oldValue;
            };
        }

        public void Update()
        {
            _x.Update();
            _y.Update();
            if (_isChanged)
            {
                OnStickChanged(_newX, _newY, _oldX, _oldY);
                _isChanged = false;
            }
        }

        #region Event invocators
        
        protected virtual void OnStickChanged(double newx, double newy, double oldx, double oldy)
        {
            StickChanged?.Invoke(newx, newy, oldx, oldy);
        }

        protected virtual double? OnReadValueX()
        {
            return ReadValueX?.Invoke();
        }

        protected virtual double? OnReadValueY()
        {
            return ReadValueY?.Invoke();
        }

        #endregion
    }
}