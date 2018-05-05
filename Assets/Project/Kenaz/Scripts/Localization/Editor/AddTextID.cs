using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEditor;

public class AddTextID {

    [MenuItem("Custom/Kenaz/AddTextID")]
    static void AddTexViewID()
    {
        //Debug.Log("add");
        var txts = GameObject.FindObjectsOfType<Text>();
        //Debug.Log("txts count = " + txts.Length);

        foreach(var txt in txts)
        {
            if(txt.gameObject.GetComponent<TextViewContent>() == null)
            {
                var tvc = txt.gameObject.AddComponent<TextViewContent>();
                tvc.ContentID = string.Format("{0}_{1}_{2}", SceneManager.GetActiveScene().name, txt.name, txt.GetInstanceID().ToString());
            }
        }
    }

}
