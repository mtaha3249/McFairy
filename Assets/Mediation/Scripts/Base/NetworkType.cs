using System;

namespace McFairy
{
    public class NetworkType
    {
        public enum InterstitialAdType
        {
            Admob = 0,
            Unity = 1,
            Facebook = 2,
            Crosspromotion = 3
        }
        public enum SceneName
        {
            MainMenu = 0,
            LevelSelection = 1,
            PlayerSelection = 2,
            Gameplay = 3,
            Store = 4,
            Paused = 5,
            LevelComplete = 6,
            LevelFail = 7,
            MidScene1 = 8,
            MidScene2 = 9,
            MidScene3 = 10
        }

        public enum Platforms
        {
            Google = 0,
            Apple = 1
        }

        public enum LogLevel
        {
            None = 0,
            Full = 1
        }

        [Serializable]
        public struct SceneSequence
        {
            public SceneName sceneName;
            public InterstitialAdType[] sequence;
            public InterstitialAdType failOver;
        }

        [Serializable]
        public struct AdmobIDs
        {
            public string Interstitial;
            public string Banner;
            public string RewardedVideo;
            public string Native;
        }

        [Serializable]
        public struct FacebookIDs
        {
            public string Interstitial;
            public string Banner;
            public string RewardedVideo;
            public string Native;
        }

        [Serializable]
        public struct UnityAdsIDs
        {
            public string AppId;
            public string InterstitialPlacementID;
            public string BannerPlacementID;
            public string RewardedPlacementID;
        }
    }
}