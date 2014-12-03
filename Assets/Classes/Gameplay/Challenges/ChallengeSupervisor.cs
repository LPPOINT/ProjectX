using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Assets.Classes.Foundation.Classes;


namespace Assets.Classes.Gameplay.Challenges
{
    public class ChallengeSupervisor : SingletonBehaviour<ChallengeSupervisor>
    {
        private static List<string> cachedChallengesIds = new List<string>(); 
        public static List<string> GetAllChallengesIds()
        {

            if (cachedChallengesIds.Any())
            {
                return cachedChallengesIds;
            }

            var types =
                Assembly.GetExecutingAssembly().GetTypes().Where(type => type == typeof (Challenge) && !type.IsAbstract);
            var attributes = new List<ChallengeAttribute>();

            foreach (var type in types)
            {
                attributes.Add(type.GetCustomAttributes(typeof(ChallengeAttribute), true).Cast<ChallengeAttribute>().FirstOrDefault());
            }

            var ids = new List<string>();

            foreach (var a in attributes)
            {
                ids.Add(a.Id);
            }

            cachedChallengesIds = ids;

            return ids;

        }


        public List<Challenge> Challenges;

        public int CompletedChallenges
        {
            get { return Challenges.Count(challenge => challenge.IsDone); }
        }

        private void Awake()
        {
            Challenges = new List<Challenge>(GetComponentsInChildren<Challenge>());
        }

    }
}
