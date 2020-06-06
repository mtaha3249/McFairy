namespace McFairy
{
    public abstract class AdNetwork
    {
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