using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using Assets.Classes.Foundation.Classes;
using Assets.Classes.Gameplay.Challenges;
using Assets.Classes.Gameplay.Model.Constants;
using Assets.Classes.Gameplay.Model.Enums;
using Assets.Classes.Gameplay.Model.Storage;
using Assets.Classes.Infrastructure;
using UnityEngine;

namespace Assets.Classes.Gameplay.Model
{
    public class GameModel
    {


        public static class Keys
        {
            public static readonly string LatestGameSessionTime = "LatestGameSessionTime";
            public static readonly string CurrentGameSessionTime = "CurrentGameSessionTime";
            public static readonly string DiamondsCount = "DiamondsCount";
            public static readonly string AdsRemoveState = "AdsRemoveState";
            public static readonly string BestScore = "BestScore";


            public static string GetCarUnlockState(int carId)
            {
                return "Car" + carId + "Unlocked";
            }
            public static string GetCarUnlockState(CarIdentifier carId)
            {
                return GetCarUnlockState((int) carId);
            }

            public static string GetChallengeState(string id)
            {
                return "Challenge_" + id + "_State";
            }

            public static string GetIncrementalChallengeProgress(string id)
            {
                 return "Challenge_" + id + "_Progress";
            }
        }
        
        public static readonly string GameModelInitializedEvent = "GameModelInitialized";

        public static GameModel Instance { get; private set; }

        public IModelStorage Storage { get; private set; }

        #region Initialization
        private GameModel(IModelStorage storage)
        {
            Storage = storage;
            HandleSessionTime();
            HandleLaunch();
        }

        public static void Initialize()
        {
            Initialize(new PrefsModelStorage());
        }
        public static void Initialize(IModelStorage storage)
        {
            Instance = new GameModel(storage);
            Messenger.Broadcast(GameModelInitializedEvent);
        }

#endregion

        #region Time management

        private void HandleSessionTime()
        {
            if (Storage.ContainsKey(Keys.CurrentGameSessionTime))
            {        
                LastSessionStartTime = AddTimeToKey(Keys.LatestGameSessionTime, GetTimeByKey(Keys.CurrentGameSessionTime));
            }
            CurrentSessionStartTime = AddTimeToKey(Keys.CurrentGameSessionTime, DateTime.Now);
        }

        public DateTime CurrentSessionStartTime { get; private set; }
        public DateTime LastSessionStartTime { get; private set; }

        private DateTime GetTimeByKey(string key)
        {
            try
            {
                var stringValue = Storage.GetString(key);
                var longValue = Convert.ToInt64(stringValue);
                return GetDateByTimestamp(longValue);
            }
            catch
            {
                return DateTime.Now;
            }
        }
        private DateTime AddTimeToKey(string key, DateTime time)
        {
            var stringValue = GetTimestampByDate(time).ToString(CultureInfo.InvariantCulture);
            Storage.SetString(key, stringValue);
            return time;
        }

        #endregion

        #region Launch management

        public LaunchType CurrentLaunchType { get; private set; }

        public void HandleLaunch()
        {
            CurrentLaunchType = LastSessionStartTime == DateTime.Now ? LaunchType.FirstLaunch : LaunchType.DefaultLaunch;

            if(CurrentLaunchType == LaunchType.FirstLaunch) 
                FirstLaunch.Process(this);
        }

        #endregion

        #region Diamonds management

        public int DiamondsCount
        {
            get { return Storage.GetInt(Keys.DiamondsCount); }
        }

        public void IncreaseDiamonds(int by)
        {
            Storage.SetInt(Keys.DiamondsCount, DiamondsCount + by);
        }

        public void DecreaseDiamonds(int by)
        {
            Storage.SetInt(Keys.DiamondsCount, DiamondsCount - by);
        }

        #endregion

        #region Utils

        public static long GetTimestampByDate(DateTime dt)
        {
            return dt.ToBinary();
        }

        public static DateTime GetDateByTimestamp(long timestamp)
        {
            return DateTime.FromBinary(timestamp);
        }


        #endregion

        #region Car unlocks

        public List<CarIdentifier> GetUnlockedCars()
        {
            return Enum.GetValues(typeof (CarIdentifier)).Cast<CarIdentifier>().Where(IsCarUnlocked).ToList();
        }

        public bool IsCarUnlocked(CarIdentifier carId)
        {
            return Storage.GetBool(Keys.GetCarUnlockState(carId));
        }

        public void UnlockCar(CarIdentifier carId)
        {
            Storage.SetBool(Keys.GetCarUnlockState(carId), true);
        }

        #endregion

        #region Ads management

        public bool IsAdsRemove
        {
            get { return Storage.GetBool(Keys.AdsRemoveState); }
        }

        public void RemoveAds()
        {
            Storage.SetBool(Keys.AdsRemoveState, true);
        }

        #endregion

        #region Challenges management

        public List<string> CompletedChallenges
        {
            get
            {
                var allChallenges = ChallengeSupervisor.GetAllChallengesIds();

                return allChallenges.Where(IsChallengeCompleted).ToList();
            }
        }

        public bool IsChallengeCompleted(string challengeId)
        {
            return Storage.GetBool(Keys.GetChallengeState(challengeId));
        }
        public int GetIncrementalChallengeProgress(string challengeId)
        {
            return Storage.GetInt(Keys.GetIncrementalChallengeProgress(challengeId));
        }

        public void RegisterChallengeCompleted(string challengeId)
        {
            Storage.SetBool(Keys.GetChallengeState(challengeId), true);
        }
        public void RegisterIncrementalChallengeProgress(string challengeId, int progress)
        {
            Storage.SetInt(Keys.GetIncrementalChallengeProgress(challengeId), progress);
        }

        #endregion

        #region Score management

        public int BestScore
        {
            get { return Storage.GetInt(Keys.BestScore); }
        }

        public bool ProcessScore(int score)
        {
            if (score > BestScore)
            {
                Storage.SetInt(Keys.BestScore, score);
                return true;
            }
            return false;
        }

        #endregion

    }
}
