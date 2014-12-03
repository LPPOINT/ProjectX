using System.Linq;
using Assets.Classes.Foundation.Attributes;
using UnityEngine;

namespace Assets.Classes.Foundation.Classes
{
    public class SingletonBehaviour<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;

        private static object _lock = new object();

        public static bool IsExist { get; private set; }

        public static T Instance
        {
            get
            {
                if (applicationIsQuitting)
                {
                    Debug.LogWarning("[Singleton] Instance '" + typeof(T) +
                                     "' already destroyed on application quit." +
                                     " Won't create again - returning null.");
                    return null;
                }

                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = (T)FindObjectOfType(typeof(T));

#if UNITY_EDITOR
                        if (FindObjectsOfType(typeof(T)).Length > 1)
                        {
                            Debug.LogError("[Singleton] Something went really wrong " +
                                           " - there should never be more than 1 singleton!" +
                                           " Reopenning the scene might fix it.");
                            return _instance;
                        }

#endif
                        if (_instance == null)
                        {
                            var singleton = new GameObject();
                            _instance = singleton.AddComponent<T>();
                            singleton.name = "(singleton) " + typeof(T).ToString();
                            DontDestroyOnLoad(singleton);

                            var a =
                                _instance.GetType()
                                    .GetCustomAttributes(typeof (SingletonMetadataAttribute), true)
                                    .FirstOrDefault() as SingletonMetadataAttribute;
                            if (a != null)
                            {
                                a.ApplyMetadataTo(singleton);
                            }

                        }
                    }
                    IsExist = true;
                    return _instance;
                }
            }
        }

        private static bool applicationIsQuitting = false;


        public void OnDestroy()
        {
            IsExist = false;
            applicationIsQuitting = true;
        }
    }
}