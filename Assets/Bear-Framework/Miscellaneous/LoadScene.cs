using UnityEngine;
using UnityEngine.SceneManagement;

namespace BearFramework.Miscellaneous.LevelMenagment
{
    [AddComponentMenu("Bear Framework/Miscellaneous/Load Scene")]
    public class LoadScene : MonoBehaviour
    {
        public void LoadLevel(int level)
        {
            SceneManager.LoadScene(level);
        }        
    }
}