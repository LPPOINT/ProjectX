using System;

namespace Assets.Classes.Gameplay.Challenges
{
    public class ChallengeAttribute : Attribute
    {
        public ChallengeAttribute(string id)
        {
            Id = id;
        }

        public string Id { get; private set; }
    }
}
