using UnityEngine;

namespace Bootstrap.Player
{
    public class SaveManager
    {
        public void Save<T>(string key, T saveData)
        {
            string jsonData = JsonUtility.ToJson(saveData, true);
            PlayerPrefs.SetString(key, jsonData);
        }

        public T Load<T>(string key) where T : new()
        {
            if (PlayerPrefs.HasKey(key))
            {
                string jsonData = PlayerPrefs.GetString(key);
                return JsonUtility.FromJson<T>(jsonData);
            }

            return new T();
        }
    }
}