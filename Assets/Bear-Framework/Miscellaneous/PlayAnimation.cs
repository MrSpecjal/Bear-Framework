using UnityEngine;

namespace BearFramework.Miscellaneous
{
    [AddComponentMenu("Bear Framework/Miscellaneous/Play Animation")]
    public class PlayAnimation : MonoBehaviour
    {
        public Animator _mecanim;        
        public void Play(string name)
        {
            _mecanim.SetTrigger(name);
        }
    }
}