using UnityEngine;
using System.Collections;

public class TextMeshContent : MonoBehaviour {

    TextMesh txt;

    [SerializeField] string[] languages = {"zh", "cn", "en"};
    [SerializeField] string[] contents;

    // Use this for initialization
    void Start () {

        txt = GetComponent<TextMesh>();
        
        var lang = Toolbox.Instance.GetOrAddComponent<TextViewManager>().GetLanguage();
        for(int i = 0; i < languages.Length; i++)
        {
            if(lang.Equals(languages[i]))
            {
                txt.text = contents[i];
                break;
            }
        }
    }
    
    // Update is called once per frame
    void Update () {
    
    }
}
