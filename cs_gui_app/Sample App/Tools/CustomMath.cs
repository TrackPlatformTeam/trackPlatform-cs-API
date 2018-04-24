using System;

namespace TrackPlatform.App.Gui.Tools
{
    public class CustomMath
    {
        private const double Epsilon = 1e-12;

        public static double Remap(
            double oldValue,
            double oldBegin, double oldEnd,
            double newBegin, double newEnd
            )
        {

            if (Math.Abs(oldEnd - oldBegin) < Epsilon)
            {
                throw new ArithmeticException("/ 0");
            }

            double offset = newBegin;
            double ratio = (newEnd - newBegin) / (oldEnd - oldBegin);
            return ratio * (oldValue - oldBegin) + offset;
        }
    }
}