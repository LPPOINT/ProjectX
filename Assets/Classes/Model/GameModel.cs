using Assets.Classes.Common;
using Assets.Classes.Infrastructure;

namespace Assets.Classes.Model
{
    public class GameModel
    {


        public static class Keys
        {
            public static readonly string LatestGameSessionTime = "LatestGameSessionTime";
            public static readonly string CurrentGameSessionTime = "CurrentGameSessionTime";
        }


        public static readonly string GameModelInitializedEvent = Identifier.DefineString();

        public static GameModel Instance { get; private set; }

        public static void Initialize()
        {
            Initialize(new PrefsModelStorage());
        }
        public static void Initialize(IModelStorage storage)
        {
            Instance = new GameModel(storage);
            Messenger.Broadcast(GameModelInitializedEvent);
        }

        public IModelStorage Storage { get; private set; }
        private GameModel(IModelStorage storage)
        {
            Storage = storage;
        }
    }
}
