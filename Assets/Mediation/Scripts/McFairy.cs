using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace McFairy
{
    public class McFairy : MonoBehaviour
    {
        List<int> initializedAds = new List<int>();

        public static McFairy Instance;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
            SceneManager.sceneLoaded += OnSceneLoaded;
            InitMcFairy();
        }

        private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
        {
            LoadAds();
        }

        private void InitMcFairy()
        {
            NetworkInitization.InitializeInterstitials(() =>
            {
                InitializeAds();
            });
            Logs.ShowLog("McFairy Initialized", LogType.Log);
        }

        private void InitializeAds()
        {
            // iterate all sequences
            for (int x = 0; x < AdSequence.Instance.sequence.Length; x++)
            {
                // iterate each scene
                for (int y = 0; y < AdSequence.Instance.sequence[x].sequence.Length; y++)
                {
                    NetworkInitization.InitializeAd(AdSequence.Instance.sequence[x].sequence[y], initializedAds);
                }
                NetworkInitization.InitializeAd(AdSequence.Instance.sequence[x].failOver, initializedAds);
            }
        }

        public void LoadAds()
        {
            foreach (int adindex in initializedAds)
            {
                AdNetwork adNetwork = NetworkInitization.GetInterstitialAdNetwork((NetworkType.InterstitialAdType)adindex);
                adNetwork.LoadAd();
            }
        }

        public void ShowAd(int sceneId)
        {
            AdNetwork adNetwork;
            if (sceneId > AdSequence.Instance.sequence.Length - 1)
            {
                Logs.ShowLog("Scene Id is out of range", LogType.Error);
                return;
            }
            // running sequence
            for (int y = 0; y < AdSequence.Instance.sequence[sceneId].sequence.Length; y++)
            {
                adNetwork = NetworkInitization.GetInterstitialAdNetwork(AdSequence.Instance.sequence[sceneId].sequence[y]);
                if (adNetwork.isAdLoaded)
                {
                    adNetwork.ShowAd();
                    return;
                }
            }
            //running failover
            adNetwork = NetworkInitization.GetInterstitialAdNetwork(AdSequence.Instance.sequence[sceneId].failOver);
            if (adNetwork.isAdLoaded)
            {
                adNetwork.ShowAd();
                return;
            }
        }
    }
}