using System.IO;

namespace TrackPlatform.Tools
{
    public static class StreamingTools
    {
        /// <summary>
        /// Blocking read from stream
        /// </summary>
        /// <param name="s">Stream to work with</param>
        /// <param name="buffer">Buffer to read</param>
        /// <param name="offset">Starting offset</param>
        /// <param name="count">How much need to read</param>
        /// <returns>Number of bytes that were read</returns>
        public static int ReadBlocking(this Stream s, byte[] buffer, int offset, int count)
        {
            int wereReadTotal = 0;
            int wereReadOneIteration = 1;

            while (wereReadOneIteration > 0 && wereReadTotal < count)
            {
                wereReadOneIteration = s.Read(buffer, offset + wereReadTotal, count - wereReadTotal);
                wereReadTotal += wereReadOneIteration;
            }

            return wereReadTotal;
        }
    }
}