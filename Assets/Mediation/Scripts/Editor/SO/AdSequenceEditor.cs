using McFairy.SO;
using UnityEditor;
using UnityEngine;

namespace McFairy.Editor.SO
{
    [CustomEditor(typeof(AdSequence))]
    public class AdSequenceEditor : UnityEditor.Editor
    {
        bool adNetworksLinks = false;
        bool validationAdmob, validationAudienceNetwork, validationUnity, validationMcFairy;

        const string PlayServicesResolver = "https://github.com/googlesamples/unity-jar-resolver/blob/master/external-dependency-manager-latest.unitypackage";
        const string AdmobAdapter = "https://drive.google.com/file/d/1gqiu2n4llU6YCHpDr-EHcrMpVv7ApQuW/view?usp=sharing";
        const string AudienceNetworkAdapter = "https://drive.google.com/file/d/1ZZMjVuqI2CjETk7SyZKPhYaQhicj21Wv/view?usp=sharing";
        const string UnityAdapter = "https://drive.google.com/file/d/1EvG9ck3eDPu2fE_Umjf-chCKYubyL4XZ/view?usp=sharing";
        const string McFairyAdsAdapter = "https://drive.google.com/file/d/12TL2U9dCWyRbK_yVxsfgrjCKLdH20Tbd/view?usp=sharing";

        SerializedProperty adIds;
        SerializedObject adTarget;
        AdSequence scriptTarget;

        public override void OnInspectorGUI()
        {
            // variables caching
            DataCaching();
            ScriptsValidation();

            EditorGUILayout.BeginVertical("box");

            EditorGUILayout.HelpBox("You must provide your ad ids and ad sequence here. Click Setting (top right corner) -> Visit Documentation to read more about McFairy Ad Mediation.", MessageType.Info);

            // initial parameters
            EditorGUILayout.PropertyField(adTarget.FindProperty("Init"), new GUIContent("Initialization Type", "Way you want to Initialize Mediation"), true);
            EditorGUILayout.PropertyField(adTarget.FindProperty("logType"), new GUIContent("Log Level", "Log Visualization"), true);
            EditorGUILayout.PropertyField(adTarget.FindProperty("GameName"), new GUIContent("Game Name", "Name of game"), true);
            EditorGUILayout.PropertyField(adTarget.FindProperty("PackageName"), new GUIContent("Package Name", "Packagename of game"), true);
            EditorGUILayout.PropertyField(adTarget.FindProperty("platform"), new GUIContent("Platform", "Launching platform"), true);
            EditorGUILayout.PropertyField(adTarget.FindProperty("hideAds"), new GUIContent("Hide All Ads", "Toogle for Hide Ads"), true);

            // layout styling
            var guiMessageStyle = new GUIStyle(GUI.skin.label);
            guiMessageStyle.wordWrap = true;
            guiMessageStyle.font = (Font)Resources.Load("Font/Font");
#if UNITY_PRO_LICENSE
            guiMessageStyle.normal.textColor = Color.white;
#else
			guiMessageStyle.normal.textColor = Color.black;
#endif
            guiMessageStyle.fontStyle = FontStyle.Bold;

            // creating ad sequence
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Ad Sequence", guiMessageStyle);
            EditorGUILayout.BeginHorizontal();
            GUILayout.Space(15);
            EditorGUILayout.PropertyField(adTarget.FindProperty("sequence"), new GUIContent("Ad Sequence", "Ad Sequence of game"), true);
            EditorGUILayout.EndHorizontal();

            // creating ad ids
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Ad Ids", guiMessageStyle);

            // warning or error for object validation ADMOB
            if (!validationAdmob && scriptTarget.validateSequence(McFairyAdsData.InterstitialAdType.Admob))
            {
                EditorGUILayout.HelpBox("Admob adapter is not imported but using Admob in sequence.", MessageType.Error);
            }
            else if (!validationAdmob && !scriptTarget.validateSequence(McFairyAdsData.InterstitialAdType.Admob))
            {
                EditorGUILayout.HelpBox("Admob adapter is missing.", MessageType.Warning);
            }
            // Admob ID's showing
            GUI.enabled = validationAdmob;
            EditorGUILayout.BeginHorizontal();
            GUILayout.Space(15);
            EditorGUILayout.PropertyField(adIds.FindPropertyRelative("admobIds"), new GUIContent("Admob Ids", "Ad Ids of Admob"), true);
            EditorGUILayout.EndHorizontal();
            GUI.enabled = !validationAdmob;

            GUI.enabled = true;
            // warning or error for object validation Audience Network
            if (!validationAudienceNetwork && scriptTarget.validateSequence(McFairyAdsData.InterstitialAdType.AudienceNetwork))
            {
                EditorGUILayout.HelpBox("Audience Network adapter is not imported but using Audience Network in sequence.", MessageType.Error);
            }
            else if (!validationAudienceNetwork && !scriptTarget.validateSequence(McFairyAdsData.InterstitialAdType.AudienceNetwork))
            {
                EditorGUILayout.HelpBox("Audience Network adapter is missing.", MessageType.Warning);
            }
            // Audience Network ID's showing
            GUI.enabled = validationAudienceNetwork;
            EditorGUILayout.BeginHorizontal();
            GUILayout.Space(15);
            EditorGUILayout.PropertyField(adIds.FindPropertyRelative("audienceNetworkIds"), new GUIContent("Audience Network Ids", "Ad Ids of Audience Network"), true);
            EditorGUILayout.EndHorizontal();
            GUI.enabled = !validationAudienceNetwork;

            GUI.enabled = true;
            // warning or error for object validation Unity Ads
            if (!validationUnity && scriptTarget.validateSequence(McFairyAdsData.InterstitialAdType.Unity))
            {
                EditorGUILayout.HelpBox("Unity Ads adapter is not imported but using Unity Ads in sequence.", MessageType.Error);
            }
            else if (!validationUnity && !scriptTarget.validateSequence(McFairyAdsData.InterstitialAdType.Unity))
            {
                EditorGUILayout.HelpBox("Unity Ads adapter is missing.", MessageType.Warning);
            }
            // Unity Ads ID's showing
            GUI.enabled = validationUnity;
            EditorGUILayout.BeginHorizontal();
            GUILayout.Space(15);
            EditorGUILayout.PropertyField(adIds.FindPropertyRelative("unityAdsIds"), new GUIContent("Unity Ads Ids", "Ad Ids of Unity Ads"), true);
            EditorGUILayout.EndHorizontal();
            GUI.enabled = !validationUnity;

            GUI.enabled = true;

            // creating ad ids
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("McFairy Ads", guiMessageStyle);
            // warning or error for object validation Unity Ads
            if (!AdSequence.Instance._interstitial && scriptTarget.validateSequence(McFairyAdsData.InterstitialAdType.McFairyAds))
            {
                EditorGUILayout.HelpBox("McFairy Interstitial sprites not defined but using McFairy Ads in sequence.", MessageType.Error);
            }
            if (!AdSequence.Instance._icon && scriptTarget.validateIconAd())
            {
                EditorGUILayout.HelpBox("McFairy icon sprites not defined but using McFairy icon Ads in sequence.", MessageType.Error);
            }
            // Unity Ads ID's showing
            EditorGUILayout.BeginHorizontal();
            GUILayout.Space(15);
            EditorGUILayout.BeginVertical();
            EditorGUILayout.PropertyField(adTarget.FindProperty("_icon"), new GUIContent("Icon Ad", "Icon Sprite"), true);
            EditorGUILayout.PropertyField(adTarget.FindProperty("_interstitial"), new GUIContent("Interstitial Ad", "Interstitial Sprite"), true);
            EditorGUILayout.PropertyField(adTarget.FindProperty("_url"), new GUIContent("URL", "Link to game"), true);
            EditorGUILayout.EndVertical();
            EditorGUILayout.EndHorizontal();

            GUI.enabled = true;
            EditorGUILayout.EndVertical();

            EditorGUILayout.BeginVertical("box");
            EditorGUILayout.HelpBox("You can download adapters by given links below.", MessageType.Info);
            EditorGUILayout.BeginHorizontal();
            GUILayout.Space(15);
            EditorGUILayout.BeginVertical();
            adNetworksLinks = EditorGUILayout.Foldout(adNetworksLinks, "Links");
            EditorGUILayout.EndVertical();
            EditorGUILayout.EndHorizontal();
            if (adNetworksLinks)
            {
                EditorGUILayout.BeginHorizontal();
                GUILayout.Space(15);
                GUI.enabled = !validationAdmob;
                if (GUILayout.Button("Admob", GUILayout.MinWidth(100f), GUILayout.ExpandWidth(true)))
                {
                    scriptTarget.OpenUrl(AdmobAdapter);
                }
                GUI.enabled = validationAdmob;

                GUI.enabled = !validationAudienceNetwork;
                if (GUILayout.Button("Audience Network", GUILayout.MinWidth(100f), GUILayout.ExpandWidth(true)))
                {
                    scriptTarget.OpenUrl(AudienceNetworkAdapter);
                }
                GUI.enabled = validationAudienceNetwork;
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                GUILayout.Space(15);
                GUI.enabled = !validationUnity;
                if (GUILayout.Button("Unity Ads", GUILayout.MinWidth(100f), GUILayout.ExpandWidth(true)))
                {
                    scriptTarget.OpenUrl(UnityAdapter);
                }
                GUI.enabled = validationUnity;

                GUI.enabled = !validationMcFairy;
                if (GUILayout.Button("McFairy Ads", GUILayout.MinWidth(100f), GUILayout.ExpandWidth(true)))
                {
                    scriptTarget.OpenUrl(McFairyAdsAdapter);
                }
                GUI.enabled = validationMcFairy;

                EditorGUILayout.EndHorizontal();

                GUI.enabled = true;
                EditorGUILayout.BeginHorizontal();
                GUILayout.Space(15);
                EditorGUILayout.BeginVertical();
                EditorGUILayout.HelpBox("Play Services Resolver is Renamed as Dependency Manager", MessageType.Info);
                if (GUILayout.Button("Download Play Services Resolver"))
                {
                    scriptTarget.OpenUrl(PlayServicesResolver);
                }
                EditorGUILayout.EndVertical();
                EditorGUILayout.EndHorizontal();
            }
            EditorGUILayout.EndVertical();

            adTarget.ApplyModifiedProperties();
        }

        void DataCaching()
        {
            scriptTarget = (AdSequence)target;
            adTarget = new SerializedObject(target);
            adIds = adTarget.FindProperty("adIds");
        }

        void ScriptsValidation()
        {
            validationAdmob = scriptTarget.ValidateAdmob();
            validationAudienceNetwork = scriptTarget.ValidateAudienceNetwork();
            validationUnity = scriptTarget.ValidateUnityAds();
            validationMcFairy = scriptTarget.ValidateMcFairy();
        }
    }
}