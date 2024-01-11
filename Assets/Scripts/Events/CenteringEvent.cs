using UnityEngine;
using UnityEngine.Events;

namespace Events
{
    public class CenteringEvent:MonoBehaviour
    {
        public static readonly UnityEvent OnStartCenteringContent = new UnityEvent();
        
        public static void SendStartCenteringContent()
        {
            OnStartCenteringContent.Invoke();
        }
    }
}