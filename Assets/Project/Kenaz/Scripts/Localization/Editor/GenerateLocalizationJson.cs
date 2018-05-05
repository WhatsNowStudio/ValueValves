using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Text;
using System.IO;

public class GenerateLocalizationFile
{
    [MenuItem("Custom/Kenaz/Generate Localization File")]
    static void GenerateFile()
    {
        var tvcs = GameObject.FindObjectsOfType<TextViewContent>();
        StringBuilder sb = new StringBuilder();

        sb.Append("ID,original,tw,ch,en\n");
        //save content to csv
        foreach(var tvc in tvcs)
        {
            var content = tvc.gameObject.GetComponent<Text>().text;
            if(string.IsNullOrEmpty(tvc.ContentID))
            {
                continue;
            }
            if(!string.IsNullOrEmpty(content))
            {
                sb.Append(string.Format("{0},{1},\"\",\"\",\"\"\n", tvc.ContentID, content));
            }
        }

        var nowTime = System.DateTime.Now;
        var fileName = string.Format("{0}_{1}{2}{3}_{4}{5}.csv", UnityEngine.SceneManagement.SceneManager.GetActiveScene().name, nowTime.Year, nowTime.Month, nowTime.Day, nowTime.Hour, nowTime.Minute);

        if(!System.IO.Directory.Exists(Application.dataPath+"/Resources"))
        {
            System.IO.Directory.CreateDirectory(Application.dataPath+"/Resources");
        }

        EasyIO.WriteData(fileName, Application.dataPath+"/Resources", sb.ToString());
        Debug.Log("generate complete");

        var projWindowType = System.Type.GetType("UnityEditor.ProjectWindowCallback,UnityEditor");
        var windows = Resources.FindObjectsOfTypeAll(projWindowType);
        if (windows != null && windows.Length > 0)
        {
            var window = (EditorWindow)windows[0];
            if (window)
                window.Repaint();
        }
    }
}