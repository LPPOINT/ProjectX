using UnityEngine;

namespace Assets.Classes.Infrastructure
{
    public static class GameInfrastructure
    {
        public static readonly string HostTag = "InfrastructureHost";
        public static readonly string InfrastructureInitializedEventName = "InfrastructureInitialized";


        public static GameObject Host { get; private set; }


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
