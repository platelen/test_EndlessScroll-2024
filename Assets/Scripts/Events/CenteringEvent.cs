using UnityEngine;
using UnityEngine.Events;

namespace Events
{
    public class CenteringEvent:MonoBehaviour
    {
        public static readonly UnityEvent OnStartStopButton = new UnityEvent();
        
        public static void SendStartStopButton()
        {
            OnStartStopButton.Invoke();
        }
    }
}