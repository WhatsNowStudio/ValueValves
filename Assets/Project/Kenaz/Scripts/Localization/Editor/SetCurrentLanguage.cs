using UnityEngine;
using UnityEditor;
using System.Collections;

public class SetCurrentLanguage
{
    [MenuItem("Custom/Kenaz/SetCurrentLanguage")]
    static public void SetLanguage()
    {
        var window = (SetLanguageWindow)EditorWindow.GetWindow(typeof(SetLanguageWindow));
        window.Show();
    } 
}

public class SetLanguageWindow : EditorWindow
{
    //string lang = "";
    /// <summary>
    /// OnGUI is called for rendering and handling GUI events.
    /// This function can be called multiple times per frame (one call per event).
    /// </summary>
    void OnGUI()
    {
        /*
        GUILayout.Label(string.Format("Current Language is: {0}", PlayerPrefs.GetString(TextViewManager.LanguagePref, "tw")), EditorStyles.boldLabel);
        GUILayout.Label("Please Enter ", EditorStyles.boldLabel);
        lang = EditorGUILayout.TextField ("Set Language:", lang);
        if(GUILayout.Button("Save and Close"))
        {
            if(!string.IsNullOrEmpty(lang))
            {
                PlayerPrefs.SetString(TextViewManager.LanguagePref, lang);
            }
            this.Close();
        }
        */

    }
}