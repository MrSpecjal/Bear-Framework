using UnityEngine;
using UnityEngine.UI;

namespace BearFramework.Localization
{
    [AddComponentMenu("Bear Framework/Localization/Localized Text")]
    public class LocalizedText : MonoBehaviour
    {
        public string key;

        void Start()
        {
            Text text = GetComponent<Text>();
            text.text = LocalizationManager.instance.GetLocalizedValue(key);
        }

    }
}