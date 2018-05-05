using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class TextViewContent : MonoBehaviour {
    
    Text txt;

    public string ContentID;

    TextViewManager textViewMgr;
    
    //public string[] contents;

    void Awake()
    {
        txt = GetComponent<Text>();
        textViewMgr = Toolbox.Instance.GetOrAddComponent<TextViewManager>();
        textViewMgr.Add(this);
    }

    public void GetContent()
    {
        txt.text = textViewMgr.GetTextContent(ContentID);
    }

    void OnDestroy()
    {
        txt = null;
        if(textViewMgr != null)
        {
            textViewMgr.Remove(this);
        }
    }
    
    public Color color
    {
        set{ txt.color = value; }
        get{ return txt.color; }
    }

    public string text
    {
        set{ txt.text = value; }
        get{ return txt.text; }
    }
}
