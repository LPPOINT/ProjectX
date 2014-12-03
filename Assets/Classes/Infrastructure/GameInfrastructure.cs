using Assets.Classes.Foundation.Classes;
using UnityEngine;

namespace Assets.Classes.Infrastructure
{
    public static class GameInfrastructure
    {
        public static readonly string HostTag = "InfrastructureHost";
        public static readonly string InfrastructureInitializedEventName = "InfrastructureInitialized";


        public static GameObject Host { get; private set; }


        public static T AddInfrastructureComponent<T>() where T : Component
        {

            if (DebugAssert.IsTrue(Host != null, "GameInfrastructure(): Host not initialized!"))
            {
                return default(T);
            }

            var c = Host.AddComponent<T>();
            Object.DontDestroyOnLoad(c);
            return c;
        }
        public static void RemoveInfrastructureComponent<T>() where T : Component
        {
            if (DebugAssert.IsTrue(Host != null, "GameInfrastructure(): Host not initialized!"))
            {
                return;
            }

            var c = Host.GetComponent<T>();
            if (c != null)
            {
                Object.Destroy(c);
            }

        }
        public static T GetInfrastructureComponent<T>() where T : Component
        {
            return DebugAssert.IsTrue(Host != null, "GameInfrastructure(): Host not initialized!") ? default (T) : Host.GetComponent<T>();
        }


        private static bool wasInitialized;
        public static void Initialize()
        {
            if (wasInitialized)
                return;
            wasInitialized = true;

            Host = GameObject.FindGameObjectWithTag(HostTag);

            if (Host == null)
            {
                Debug.LogWarning("GameInfrastructure Host not found");
                return;
            }

            foreach (Transform child in Host.transform)
            {
                Object.DontDestroyOnLoad(child.gameObject);
            }

            Object.DontDestroyOnLoad(Host);
            Messenger.Broadcast(InfrastructureInitializedEventName);
        }

    }
}
