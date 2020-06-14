using McFairy.SO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class McFairyInitEditor : MonoBehaviour
{
    [InitializeOnLoad]
    public class InitializeMcFairy
    {
        static InitializeMcFairy()
        {
            AddDefineSymbols();
        }

        [UnityEditor.Callbacks.DidReloadScripts]
        private static void OnScriptsReloaded()
        {
            if (PlayerPrefs.GetInt("McFairyInit") == 0)
            {
                if (EditorUtility.DisplayDialog("Congratulation", "McFairy Ad Mediation Installed.", "Great. Let's go."))
                {
                    Selection.activeObject = AdSequence.Instance;
                }
                PlayerPrefs.SetInt("McFairyInit", 1);
                PlayerPrefs.Save();
            }
        }

        /// <summary>
        /// Symbols that will be added to the editor
        /// </summary>
        public static readonly string[] Symbols = new string[] {
         "MCFAIRY"
     };

        /// <summary>
        /// Add define symbols as soon as Unity gets done compiling.
        /// </summary>
        static void AddDefineSymbols()
        {
            string definesString = PlayerSettings.GetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup);
            List<string> allDefines = definesString.Split(';').ToList();
            allDefines.AddRange(Symbols.Except(allDefines));
            PlayerSettings.SetScriptingDefineSymbolsForGroup(
                EditorUserBuildSettings.selectedBuildTargetGroup,
                string.Join(";", allDefines.ToArray()));
        }
    }
}
