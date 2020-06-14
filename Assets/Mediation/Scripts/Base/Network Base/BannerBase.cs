namespace McFairy.Base
{
    public abstract class BannerBase
    {
        /// <summary>
        /// initialization of ad network
        /// </summary>
        /// <param name="id">interstitial id</param>
        /// <param name="appId">application id</param>
        public abstract void Initialize(string id = "", string appId = "");

        /// <summary>
        /// Load ad of ad network
        /// </summary>
        /// <param name="bannerSize">Size of Banner</param>
        /// <param name="bannerPosition">Position of Banner</param>
        public abstract void LoadAd(NetworkType.BannerSize bannerSize, NetworkType.BannerPosition bannerPosition);

        /// <summary>
        /// Hide ad of ad network
        /// </summary>
        public abstract void HideAd();

        /// <summary>
        /// Convert Adsize from McFairy To Network Type
        /// </summary>
        /// <typeparam name="T">Return type</typeparam>
        /// <typeparam name="T1">McFairy Type</typeparam>
        /// <param name="bannerSize">Banner size of McFairy to be converted</param>
        /// <returns>Banner size converted to related ad network</returns>
        public abstract T GetAdSize<T, T1>(T1 bannerSize);

        /// <summary>
        /// Convert AdPosition from McFairy To Network Type
        /// </summary>
        /// <typeparam name="T">Return type</typeparam>
        /// <typeparam name="T1">McFairy Type</typeparam>
        /// <param name="bannerPosition">Banner Positon of McFairy to be converted</param>
        /// <returns>Banner position converted to related ad network</returns>
        public abstract T GetAdPosition<T, T1>(T1 bannerPosition);
    }
}