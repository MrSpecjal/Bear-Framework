using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

namespace BearFramework.Localization
{
    [AddComponentMenu("Bear Framework/Localization/Localization Manager")]
    public class LocalizationManager : MonoBehaviour
    {

        public static LocalizationManager instance;

        private Dictionary<string, string> localizedText;
        private bool isReady = false;
        private string missingTextString = "Localized text not found";

        // Use this for initialization
        void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else if (instance != this)
            {
                Destroy(gameObject);
            }

            DontDestroyOnLoad(gameObject);
        }

        public void LoadLocalizedText(string fileName)
        {
            localizedText = new Dictionary<string, string>();
            string filePath = Path.Combine(Application.streamingAssetsPath, fileName);

            if (File.Exists(filePath))
            {
                string dataAsJson = File.ReadAllText(filePath);
                LocalizationData loadedData = JsonUtility.FromJson<LocalizationData>(dataAsJson);

                for (int i = 0; i < loadedData.items.Length; i++)
                {
                    localizedText.Add(loadedData.items[i].key, loadedData.items[i].value);
                }

                Debug.Log("Data loaded, dictionary contains: " + localizedText.Count + " entries");
            }
            else
            {
                Debug.LogError("Cannot find file!");
            }

            isReady = true;
        }

        public string GetLocalizedValue(string key)
        {
            string result = missingTextString;
            if (localizedText.ContainsKey(key))
            {
                result = localizedText[key];
            }

            return result;

        }

        public bool GetIsReady()
        {
            return isReady;
        }

    }
}