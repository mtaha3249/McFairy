#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using McFairy.Base;

namespace McFairy.SO
{
    [CreateAssetMenu(fileName = "Ad Data", menuName = "McFairy/Create Ad Data", order = 1)]
#if UNITY_EDITOR
    [HelpURL("https://sites.google.com/view/mtahabinfarooq")]
#endif
    public class AdSequence : ScriptableObject
    {
        #region Singelton Instance
        const string databaseName = "Ad Sequence";
        static AdSequence instance;
        public static AdSequence Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = Resources.Load(databaseName) as AdSequence;
                }
                return instance;
            }
        }
        #endregion

        #region Editor
#if UNITY_EDITOR
        [MenuItem("Window/McFairy/Show Ad Sequence #s")]
        public static void Edit()
        {
            Selection.activeObject = Instance;
        }
#endif
        #endregion

        public NetworkType.InitializiationType Init;
        public NetworkType.LogLevel logType;
        public string GameName;
        public string PackageName;
        public NetworkType.Platforms platform;
        public NetworkType.SceneSequence[] sequence;
        public EditableScript.AdIds adIds;

        public Sprite _icon;
        public Sprite _interstitial;
        public string _url;

        private const string docUrl = "";

        #region External Calls
        public void OpenUrl(string url)
        {
            Application.OpenURL(url);
        }

        public GameObject CreateInterstitial(Transform Parent)
        {
            GameObject Interstitial = Instantiate((GameObject)Resources.Load("Prefabs/Interstitial"), Parent);
            return Interstitial;
        }

        public bool ValidateAdmob()
        {
            return Validator.validateScript(EditableScript._namespace + "." + EditableScript._namespaceAdapters + "." + EditableScript._namespaceAdaptersAdmob, EditableScript._admobInterstitialClassName);
        }

        public bool ValidateAudienceNetwork()
        {
            return Validator.validateScript(EditableScript._namespace + "." + EditableScript._namespaceAdapters + "." + EditableScript._namespaceAdaptersAudienceNetwork, EditableScript._audienceNetworkInterstitialClassName);
        }

        public bool ValidateUnityAds()
        {
            return Validator.validateScript(EditableScript._namespace + "." + EditableScript._namespaceAdapters + "." + EditableScript._namespaceAdaptersUnityAds, EditableScript._unityInterstitialClassName);
        }

        public bool validateSequence(EditableScript.InterstitialAdType interstitialAdType)
        {
            for (int x = 0; x < sequence.Length; x++)
            {
                for (int y = 0; y < sequence[x].interstitial.sequence.Length; y++)
                {
                    if (sequence[x].interstitial.sequence[y] == interstitialAdType)
                    {
                        return true;
                    }
                }
                if (sequence[x].interstitial.failOver == interstitialAdType)
                {
                    return true;
                }
            }
            return false;
        }

        public bool validateIconAd()
        {
            for (int x = 0; x < sequence.Length; x++)
            {
                if (sequence[x].iconAd._enable)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }

        [ContextMenu("Visit Documentation")]
        public void GoToDoc()
        {
            OpenUrl(docUrl);
        }
        #endregion
    }
}