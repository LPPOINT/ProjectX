using Assets.Classes.Foundation.Classes;

namespace Assets.Classes.Gameplay
{
    public class GameSupervisor : SingletonBehaviour<GameSupervisor>
    {


        public GameScreen LastScreen { get; private set; }
        public GameScreen CurrentScreen { get; private set; }

        public bool IsLastScreenExist
        {
            get { return LastScreen != GameScreen.Unknown; }
        }
        public bool IsCurrentScreenExist
        {
            get { return CurrentScreen != GameScreen.Unknown; }
        }



        public void OnGameStarted()
        {
            LastScreen = GameScreen.Unknown;
            CurrentScreen = GameScreen.StartScreen;
        }

    }
}
