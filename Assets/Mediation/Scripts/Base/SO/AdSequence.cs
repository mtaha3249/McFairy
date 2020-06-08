﻿#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using McFairy.Base;

namespace McFairy.SO
{
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

        public NetworkType.LogLevel logType;
        public string GameName;
        public string PackageName;
        public NetworkType.Platforms platform;
        [Header("Ad Sequences")]
        public NetworkType.SceneSequence[] sequence;

        [Space(5)]
        public EditableScript.AdIds adIds;
    }
}