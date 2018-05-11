using System.Text;

namespace TrackPlatform.Tools
{
    public static class DebugConversionTools
    {
        public static string ToHexString(this byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
            {
                hex.AppendFormat("{0:x2} ", b);
            }

            if (hex.Length > 0)
            {
                hex.Length--;
            }

            return hex.ToString();
        }
    }
}