using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace BearFramework.Events
{
    [ExecuteInEditMode]
    [AddComponentMenu("Bear Framework/Events/Play Sound")]
    [RequireComponent(typeof(AudioSource))]
    public class PlaySound : MonoBehaviour
    {   
        private AudioSource sound;
        [ExecuteInEditMode]
        private void Awake()
        {
            sound = GetComponent<AudioSource>();
        }

        public void Play()
        {
            sound.Play();
        }
    }
}
