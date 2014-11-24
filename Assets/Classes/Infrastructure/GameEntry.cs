using Assets.Classes.Common;
using Assets.Classes.Model;
using UnityEngine;

namespace Assets.Classes.Infrastructure
{
    public class GameEntry : MonoBehaviour
    {

        public static readonly string GameEntryEventName = Identifier.DefineString();

        private void Awake()
        {
            Debug.Log("GameEntry.Awake()");
            GameInfrastructure.Initialize();
            GameModel.Initialize();
            Messenger.Broadcast(GameEntryEventName);
            Destroy(this);
        }
    }
}
