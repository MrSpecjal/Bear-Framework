using UnityEngine;
using UnityEngine.Events;

namespace BearFramework.Events
{
    [AddComponentMenu("Bear Framework/Events/Basic Methods Events")]
    public class MethodsEvents : MonoBehaviour
    {
        public UnityEvent OnAwake;
        public UnityEvent OnStart;
        public UnityEvent OnUpdate;
        public UnityEvent OnFixedUpdate;
        public UnityEvent OnLateUpdate;
        public UnityEvent Enable;
        public UnityEvent Disable;

        private void Awake()
        {
            OnAwake.Invoke();
        }

        private void Start()
        {
            OnStart.Invoke();
        }

        private void Update()
        {
            OnUpdate.Invoke();
        }

        private void FixedUpdate()
        {
            OnFixedUpdate.Invoke();
        }

        private void LateUpdate()
        {
            OnLateUpdate.Invoke();
        }

        private void OnEnable()
        {
            Enable.Invoke();
        }

        private void OnDisable()
        {
            Disable.Invoke();
        }
    }
}