using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;

public class TextViewManager : MonoBehaviour {

    //static public string LanguagePref = "lang";
    
    List<TextViewContent> tvcs = new List<TextViewContent>();
    string[,] csvData;
    string curSceneName = "";

    string curLang = "en";

    Dictionary<string, string> curDict;

    // Use this for initialization
    void Awake()
    {   
        Toolbox.RegisterComponent<TextViewManager>();
        //curLang = Language.ZH_TW;
        //SceneManager.sceneLoaded += SetTextLocalize;
    }

    public string GetLanguage()
    {
        return curLang;
    }

    public void SetLanguage(string lang)
    {
        curLang = lang;
    }

    public void Add(TextViewContent tvc)
    {
        tvcs.Add(tvc);
    }

    public void Remove(TextViewContent tvc)
    {
        tvcs.Remove(tvc);
    }

    public void Clear()
    {
        tvcs.Clear();
    }

    public string GetTextContent(string contentID)
    {
        return curDict[contentID];
    }

    public void SetTextLocalizeTest()
    {
        SetAllTextLocalize(SceneManager.GetActiveScene(), curLang);
    }

    public void SetTextLocalize(Scene scene)
    {
        SetAllTextLocalize(scene, curLang);
    }

    public void SetAllTextLocalize(Scene scene, string lang)
    {
        if(!curSceneName.Equals(scene.name))
        {
            var data = Resources.Load<TextAsset>(scene.name);
            if(data == null)
            {
                Debug.LogWarningFormat("can't find data {0}.csv!", scene.name);
                return;
            }
            csvData = CSVReader.SplitCsvGrid(data.text);
        }
        curDict = new Dictionary<string, string>();
        //find target data row
        for(int i = 1; i < csvData.GetLength(0)-1; i++)
        {
            if(csvData[i, 0].Equals(lang))
            {
                for(int j = 1; j < csvData.GetUpperBound(1); j++)
                {
                    if(!string.IsNullOrEmpty(csvData[i, j]))
                    {
                        var newStr = csvData[i, j].Replace("\"", "");
                        //Debug.LogFormat("{0}={1}", csvData[i, j], newStr);
                        if(!curDict.ContainsKey(csvData[0, j]))
                        {
                            curDict.Add(csvData[0, j], newStr);
                        }                        
                    }
                }
                break;
            }
        }

        foreach(var tvc in tvcs)
        {
            if(curDict.ContainsKey(tvc.ContentID))
            {
                if(!string.IsNullOrEmpty(curDict[tvc.ContentID]))
                {
                    tvc.text = curDict[tvc.ContentID];
                }
            }
        }
    }
}
