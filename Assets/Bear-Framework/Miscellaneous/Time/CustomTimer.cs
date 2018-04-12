using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace BearFramework.Events
{
    [AddComponentMenu("Bear Framework/Time/Custom Timer")]
    public class CustomTimer : MonoBehaviour
    {
        [Header ("Timer Events")]
        public UnityEvent OnTimerHasEnded;
        public UnityEvent OnEveryTimerTick;
        [Header("Set Up Timer")]
        public float timerDuration;
        public bool loopTimer;
        [HideInInspector]
        public float timerCounter;
        [HideInInspector]
        public bool isTimerWorking;

        public void OnTimerEnded()
        {
            OnTimerHasEnded.Invoke();
        }

        public void OnEveryTick()
        {
            OnEveryTimerTick.Invoke();
        }

        public void Update()
        {
            if (isTimerWorking)
                ApplyTimer(); 
        }

        public void ApplyTimer()
        {
            if (!IsTimerEnded())
                timerCounter -= Time.deltaTime; 
        }

        public void BeginTimer()
        {
            timerCounter = timerDuration;
            isTimerWorking = true;
        }

        public void PauseTimer()
        {
            isTimerWorking = !isTimerWorking;
        }

        public void StopTimer()
        {
            isTimerWorking = false;
            timerCounter = 0;
        }

        public bool IsTimerEnded()
        {            
            bool isEnded = (timerCounter <= 0) ? true : false;

            if (isEnded && !loopTimer)
            {
                timerCounter = 0;
                isTimerWorking = false;
            }
            else
            {
                OnEveryTick();
            }

            if (loopTimer)
            {
                return isEnded;
            }
            else
            {
                timerCounter = timerDuration;
                return false;
            }
        }
    }
}