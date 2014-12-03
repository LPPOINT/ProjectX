using Assets.Classes.Gameplay.Model;

namespace Assets.Classes.Gameplay.Challenges
{
    public abstract class IncrementalChallenge : Challenge
    {
        public int MaxValue;
        public int CurrentValue { get; private set; }

        public void Increment()
        {
            if(IsDone)
                return;
            CurrentValue++;
            if (CurrentValue == MaxValue)
            {
                OnChallengeDone();
            }
        }

        public override void FromModel()
        {
            base.FromModel();
            if (IsDone)
            {
                CurrentValue = MaxValue;
            }
            CurrentValue = GameModel.Instance.GetIncrementalChallengeProgress(Id);
        }
    }
}
