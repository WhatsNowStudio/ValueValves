using UnityEngine;
using System.IO;
using System.Text;
using System.Collections;

static public class EasyIO 
{

    public static void CopyFilesRecursively(DirectoryInfo source, DirectoryInfo target)
    {
        foreach (DirectoryInfo dir in source.GetDirectories())
            CopyFilesRecursively(dir, target.CreateSubdirectory(dir.Name));
        foreach (FileInfo file in source.GetFiles())
            file.CopyTo(Path.Combine(target.FullName, file.Name));
    }

    public static Texture2D GetTexture2D(string filePath)
    {
        Texture2D tex2D = null;
        if (File.Exists(filePath))
        {
            if (Application.platform == RuntimePlatform.WindowsEditor)
            {
                //WindowsEditor認不得中文路徑
                byte[] bytes = File.ReadAllBytes(filePath);
                tex2D = new Texture2D(1, 1);
                tex2D.LoadImage(bytes);
            }
            else
            {
                WWW www = new WWW("file://" + filePath);
                while (!www.isDone) { }
                tex2D = www.texture;
            }
        }
        return tex2D;
    }
    
    public static Texture GetTexture(string filePath)
    {
        Texture tex = null;
        if (File.Exists(filePath))
        {
            if (Application.platform == RuntimePlatform.WindowsEditor)
            {
                //WindowsEditor認不得中文路徑
                byte[] bytes = File.ReadAllBytes(filePath);
                Texture2D tex2D = new Texture2D(1, 1);
                tex2D.LoadImage(bytes);
                tex = (Texture)tex2D;
            }
            else
            {
                WWW www = new WWW("file://" + filePath);
                while (!www.isDone) { }
                tex = www.texture as Texture;
            }
        }
        return tex;
    }
    
    static public bool CheckFileExist(string fileName, string filePath)
    {
        return File.Exists(Path.Combine(filePath, fileName));
    }

    static public bool CheckFileExist(string fileName)
    {
        return File.Exists(Path.Combine(Application.persistentDataPath, fileName));
    }

    static public void CreateDirectory(string folderPath)
    {
        if (Directory.Exists(folderPath) == false) { Directory.CreateDirectory(folderPath); }
    }

    static public void WriteData(string fileName, string filePath, string data)
    {
        //write json data to txt file
        //刪除文件.
        DeleteFile(filePath, fileName);

        //創建文件.
        CreateFile(filePath, fileName, data);
        //得到文本中每一行的內容.
        //infoall = LoadFile(Application.persistentDataPath,"FileName.txt"); 
    }

    static public void WriteData(string fileName, string data)
    {
        //write json data to txt file
        //刪除文件.
        DeleteFile(Application.persistentDataPath, fileName);
        
        //創建文件.
        CreateFile(Application.persistentDataPath, fileName, data);
        //得到文本中每一行的內容.
        //infoall = LoadFile(Application.persistentDataPath,"FileName.txt"); 
    }

    static public void SaveFile(string fileName, string filePath, byte[] bytes)
    {
        File.WriteAllBytes(Path.Combine(filePath, fileName), bytes);
    }

    static public void SaveFile(string fileName, byte[] bytes)
    {
        // Debug.LogFormat("save path: {0}",Path.Combine(Application.persistentDataPath, fileName));
        File.WriteAllBytes (Path.Combine(Application.persistentDataPath, fileName), bytes);
    }
    
    static public void LoadImage(string fileName, System.Action<Sprite> action)
    {
        //string filePath = Path.Combine(Application.persistentDataPath, fileName);
        //string filePath = Application.persistentDataPath + "//" + fileName;
#if UNITY_STANDALONE_OSX
        string filePath = Path.Combine(Application.persistentDataPath, fileName);
#else
        string filePath = Application.persistentDataPath + "/" + fileName;
#endif
        /*
#if UNITY_IOS
        filePath = string.Format("file://{0}", filePath);
#endif
        */
        // Debug.LogFormat("file path : '{0}'", filePath);
        if (File.Exists(filePath)){
            //StartCoroutine(StartLoadSprite(filePath, action));
            StartLoadSprite(filePath, action);
        }
        else
        {
            action(null);
        }
    }
    
    static void StartLoadSprite(string filePath, System.Action<Sprite> action)
    {
        byte[] data = File.ReadAllBytes(filePath);
        var split = filePath.Split('.');
        var fileType = split[split.Length-1].ToLower();
        TextureFormat format;
        if (fileType.Equals("png"))
        {
            format = TextureFormat.ARGB32;
        }
        else
        {
            format = TextureFormat.RGB24;
        }
        Texture2D texture = new Texture2D(1024, 1024, format, false);
        if(texture.LoadImage(data))
        {
            // Debug.Log("isLoaded = " + isLoaded);
            texture.name = Path.GetFileNameWithoutExtension(filePath);
            texture.Compress(false);
            Sprite newImg = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
            // www.Dispose();
            //yield return null;
            action(newImg);
            // data = null;
        }
    }

    public static Sprite StartLoadSpriteByBytes(byte[] bytes, string fileName)
    {
        var split = fileName.Split('.');
        var fileType = split[split.Length - 1].ToLower();
        TextureFormat format;
        if (fileType.Equals("png"))
        {
            format = TextureFormat.ARGB32;
        }
        else
        {
            format = TextureFormat.RGB24;
        }
        Texture2D texture = new Texture2D(2048, 2048, format, false);
        /*var isLoaded = */
        texture.LoadImage(bytes);
        //print("isLoaded = " + isLoaded);
        texture.name = Path.GetFileNameWithoutExtension(fileName);
        texture.Compress(false);
        return Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
        // www.Dispose();
        //yield return null;
    }

    static public string ReadData(string fileName)
    {
        return LoadFile(Application.persistentDataPath, fileName); 
    }

    /**
   * path：文件創建目錄.
   * name：文件的名稱.
   * info：寫入的內容.
   */
    static public void CreateFile(string path,string name,string info) 
    { 
        //文件流信息.
        StreamWriter sw; 
        FileInfo t = new FileInfo(path+"//"+ name); 
        if(!t.Exists) 
        { 
            //如果文件不存在則創建.
            sw = t.CreateText();
        }
        else
        {
            //如果此文件存在則打開.
            sw = t.AppendText();
        }
        //以行的形式寫入信息.
        sw.WriteLine(info);
        //關閉流.
        sw.Close();
        //銷毀流.
        sw.Dispose(); 
    } 
    
    
    /**
   * path：讀取文件的路徑.
   * name：讀取文件的名稱.
   */
    static public string LoadFile(string path,string name) 
    { 
        //使用流的形式讀取.
        StreamReader sr =null;
        try{
            sr = File.OpenText(path+"//"+ name); 
        }catch(System.Exception e)
        {

            Debug.Log(e.Message);
            //路徑與名稱未找到文件則直接回傳null.
            return null;
        }
        string line;
        line = sr.ReadToEnd ();
        //關閉流.
        sr.Close();
        //銷毀流.
        sr.Dispose();
        //將數組鏈表容器返回.
        return line;
    } 
    
    /**
   * path：刪除文件的路徑.
   * name：刪除文件的名稱.
   */    
    static public void DeleteFile(string path,string name)
    {
        if(File.Exists(path+"//"+ name))
        {
            File.Delete(path+"//"+ name);
        }
    }


}
