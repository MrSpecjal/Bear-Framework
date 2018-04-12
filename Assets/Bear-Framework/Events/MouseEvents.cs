using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace BearFramework.Events
{
    [AddComponentMenu("Bear Framework/Events/Mouse Events")]
    public class MouseEvents : MonoBehaviour, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler, IPointerEnterHandler, IPointerExitHandler
    {
        public UnityEvent OnMouseClick;
        public UnityEvent OnMouseDown;
        public UnityEvent OnMouseUp;
        public UnityEvent OnMouseEnter;
        public UnityEvent OnMouseExit;

        public void OnPointerClick(PointerEventData pointerEventData)
        {
            OnMouseClick.Invoke();
        }
        public void OnPointerDown(PointerEventData pointerEventData)
        {
            OnMouseDown.Invoke();
        }
        public void OnPointerUp(PointerEventData pointerEventData)
        {
            OnMouseUp.Invoke();
        }
        public void OnPointerEnter(PointerEventData pointerEventData)
        {
            OnMouseEnter.Invoke();
        }
        public void OnPointerExit(PointerEventData pointerEventData)
        {
            OnMouseExit.Invoke();
        }
    }
}