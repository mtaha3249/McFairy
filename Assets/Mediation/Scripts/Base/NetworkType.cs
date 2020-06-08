using System;

namespace McFairy.Base
{
    public class NetworkType
    {
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
            public Interstitial interstitial;
            public Rewarded rewarded;
        }

        [Serializable]
        public struct Interstitial
        {
            public EditableScript.InterstitialAdType[] sequence;
            public EditableScript.InterstitialAdType failOver;
        }

        [Serializable]
        public struct Rewarded
        {
            public EditableScript.RewardedAdType[] sequence;
            public EditableScript.RewardedAdType failOver;
        }
    }
}