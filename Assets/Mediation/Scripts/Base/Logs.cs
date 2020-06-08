using UnityEngine;
using McFairy.SO;
using McFairy.Base;

namespace McFairy.Logger
{
    public class Logs
    {
        public static void ShowLog(string message, LogType logType)
        {
            // check log type
            switch (AdSequence.Instance.logType)
            {
                case NetworkType.LogLevel.None:
                    break;
                case NetworkType.LogLevel.Full:
                    switch (logType)
                    {
                        // message type
                        case LogType.Log:
                            Debug.Log(message);
                            break;
                        // warning type
                        case LogType.Warning:
                            Debug.LogWarning(message);
                            break;
                    }
                    break;
            }
            // error type its written outside of block because we don't want error hide from log level. as Error must shown on every case
            switch (logType)
            {
                case LogType.Error:
                    Debug.LogError(message);
                    break;
            }
        }
    }
}