using UnityEngine;

namespace BearFramework.Miscellaneous
{
    [AddComponentMenu("Bear Framework/Miscellaneous/Play Animation")]
    public class PlayAnimation : MonoBehaviour
    {
        public Animator animator;        
        public void Play(string name)
        {
            animator.SetTrigger(name);
        }
    }
}