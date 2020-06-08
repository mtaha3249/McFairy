
namespace McFairy.Base
{
    public abstract class RewardedBase
    {
        public delegate void AdCompleted();
        public static AdCompleted OnAdCompleted;
        public delegate void AdLoaded();
        public static AdLoaded OnAdLoaded;
        public bool isAdLoaded;
        /// <summary>
        /// initialization of ad network
        /// </summary>
        /// <param name="id">interstitial id</param>
        /// <param name="appId">application id</param>
        public abstract void Initialize(string id = "", string appId = "");

        public abstract void LoadAd();

        public abstract void ShowAd();
    }
}