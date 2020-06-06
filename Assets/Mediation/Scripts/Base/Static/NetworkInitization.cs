using System;
using System.Collections.Generic;
using UnityEngine;

namespace McFairy
{
    public class NetworkInitization
    {
        const string _namespace = "McFairy";
        const string _admobInterstitialClassName = "AdmobInterstitial";
        const string _facebookInterstitialClassName = "FacebookInterstitial";
        const string _unityInterstitialClassName = "UnityInterstitial";

        static Dictionary<int, AdNetwork> dictionaryInterstitial =
            new Dictionary<int, AdNetwork>();

        /// <summary>
        /// Initialize ad network object
        /// </summary>
        public static void InitializeInterstitials(Action OnComplete = null)
        {
            dictionaryInterstitial.Add((int)NetworkType.InterstitialAdType.Admob, Validator.validateScript(_namespace, _admobInterstitialClassName));
            dictionaryInterstitial.Add((int)NetworkType.InterstitialAdType.Facebook, Validator.validateScript(_namespace, _facebookInterstitialClassName));
            dictionaryInterstitial.Add((int)NetworkType.InterstitialAdType.Unity, Validator.validateScript(_namespace, _unityInterstitialClassName));

            if (OnComplete != null)
            {
                OnComplete.Invoke();
            }
        }

        /// <summary>
        /// get object of intialized ad network
        /// </summary>
        /// <param name="networkName">network name to get</param>
        /// <returns>object of network</returns>
        public static AdNetwork GetInterstitialAdNetwork(NetworkType.InterstitialAdType networkName)
        {
            AdNetwork adNetwork;
            dictionaryInterstitial.TryGetValue((int)networkName, out adNetwork);
            return adNetwork;
        }

        /// <summary>
        /// Initialize Ad base on interstitialAdType
        /// </summary>
        /// <param name="interstitialAdType">type of ad to initialzed</param>
        /// <param name="initializedAds">list of ads initialized</param>
        /// <returns>true if initialzed, false if already initialzed</returns>
        public static bool InitializeAd(NetworkType.InterstitialAdType interstitialAdType, List<int> initializedAds)
        {
            if (!initializedAds.Contains((int)interstitialAdType) && GetInterstitialAdNetwork(interstitialAdType) != null)
            {
                GetInterstitialAdNetwork(interstitialAdType).Initialize(getId(interstitialAdType), getAppId(interstitialAdType));
                Logs.ShowLog(interstitialAdType.ToString() + " Initialized", LogType.Log);
                initializedAds.Add((int)interstitialAdType);
                return true;
            }
            else
                return false;
        }

        /// <summary>
        /// find ad id from SO ad sequence and give it back
        /// </summary>
        /// <param name="interstitialAdType">ad type</param>
        /// <returns>id of ad</returns>
        static string getId(NetworkType.InterstitialAdType interstitialAdType)
        {
            string id = "";
            switch (interstitialAdType)
            {
                case NetworkType.InterstitialAdType.Admob:
                    id = AdSequence.Instance.AdmobIds.Interstitial;
                    break;
                case NetworkType.InterstitialAdType.Facebook:
                    id = AdSequence.Instance.FacebookIds.Interstitial;
                    break;
                case NetworkType.InterstitialAdType.Unity:
                    id = "";
                    break;
                case NetworkType.InterstitialAdType.Crosspromotion:
                    break;
            }
            return id;
        }

        /// <summary>
        /// find app id from SO ad sequence and give it back
        /// </summary>
        /// <param name="interstitialAdType">ad type</param>
        /// <returns>id of ad</returns>
        static string getAppId(NetworkType.InterstitialAdType interstitialAdType)
        {
            string id = "";
            switch (interstitialAdType)
            {
                case NetworkType.InterstitialAdType.Admob:
                    id = "";
                    break;
                case NetworkType.InterstitialAdType.Facebook:
                    id = "";
                    break;
                case NetworkType.InterstitialAdType.Unity:
                    id = AdSequence.Instance.unityAdsIds.AppId;
                    break;
                case NetworkType.InterstitialAdType.Crosspromotion:
                    break;
            }
            return id;
        }
    }
}