
using System;
using System.Linq;
using Assets.Classes.Gameplay.Model;
using UnityEngine;

namespace Assets.Classes.Gameplay.Challenges
{
    public abstract class Challenge : MonoBehaviour
    {

        private void Awake()
        {
            try
            {
                Id =
                    GetType()
                        .GetCustomAttributes(typeof (ChallengeAttribute), true)
                        .Cast<ChallengeAttribute>()
                        .FirstOrDefault()
                        .Id;
            }
            catch 
            {
                Debug.Log("Challenge id initialization error.");
            }
        }

        public string Id { get; private set; }

        public string Name;
        public string Description;


        public event EventHandler Done;
        public bool IsDone { get; private set; }
        public void OnChallengeDone()
        {
            IsDone = true;
            var handler = Done;
            if (handler != null) handler(this, EventArgs.Empty);
            GameModel.Instance.RegisterChallengeCompleted(Id);
        }

        protected virtual void UpdateChallenge()
        {
            
        }
        private void Update()
        {
            if (!IsDone)
            {
                UpdateChallenge();
            }
        }

        public virtual void FromModel()
        {
            IsDone = GameModel.Instance.IsChallengeCompleted(Id);
        }

    }
}
