using UnityEngine;

namespace Assets.Classes.Gameplay.Model.Storage
{
    public class PrefsModelStorage : IModelStorage
    {
        public void SetFloat(string key, float value)
        {
            PlayerPrefs.SetFloat(key, value);
        }

        public float GetFloat(string key)
        {
            return PlayerPrefs.GetFloat(key);
        }

        public void SetBool(string key, bool value)
        {
            PlayerPrefs.SetInt(key, value ? 1 : 0);
        }

        public bool GetBool(string key)
        {
            return PlayerPrefs.GetInt(key) != 0;
        }

        public void SetString(string key, string value)
        {
            PlayerPrefs.SetString(key, value);
        }

        public string GetString(string key)
        {
            return PlayerPrefs.GetString(key);
        }

        public void SetInt(string key, int value)
        {
            PlayerPrefs.SetInt(key, value);
        }

        public int GetInt(string key)
        {
            return PlayerPrefs.GetInt(key);
        }

        public bool ContainsKey(string key)
        {
            return PlayerPrefs.HasKey(key);
        }

        public void RemoveKey(string key)
        {
            PlayerPrefs.DeleteKey(key);
        }
    }
}
