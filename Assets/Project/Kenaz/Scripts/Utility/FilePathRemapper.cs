using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;


namespace Kenaz
{
    public class FilePathRemapper : MonoBehaviour {

        FilePathMapDataCollection mapData;
        
        readonly string JSON_PATH = "filePathMap.json";
        bool isInit = false;

        void Awake()
        {
            Toolbox.RegisterComponent<FilePathRemapper>();
            //Init();
        }

        public void Init()
        {   
            if(isInit)
            {
                return;
            }
            if (mapData == null)
            {
                if(EasyIO.CheckFileExist(JSON_PATH))
                {
                    mapData = JsonUtility.FromJson<FilePathMapDataCollection>(EasyIO.ReadData(JSON_PATH));
                }
                else
                {
                    mapData = new FilePathMapDataCollection();
                }
            }
            isInit = true;
        }

        public string GetFilePath(string fileName)
        {
            for(int i = 0; i < mapData.datas.Length; i++)
            {
                if(mapData.datas[i].originalPath.Equals(fileName))
                {
                    return mapData.datas[i].newPath;
                }
            }
            return null;
        }

        public FilePathMapDataCollection json
        {
            get
            {   
                return mapData;
            }
        }

        //@incomplete
        public string AddFileAndGetNewPath(string fileName)
        {
            return string.Empty;
        }

        bool hasSameFileName(string fileName)
        {
            for(int i = 0; i < mapData.datas.Length; i++)
            {
                if(mapData.datas[i].originalPath.Equals(fileName))
                {
                    return true;
                }
            }
            return false;
        }

        public void SaveJson()
        {
            EasyIO.WriteData(JSON_PATH, JsonUtility.ToJson(mapData));
        }
    }

    
}

[Serializable]
public class FilePathMapDataCollection
{
    public FilePathMapData [] datas;
}

[Serializable]
public class FilePathMapData
{
    public string originalPath;
    public string newPath;
}
