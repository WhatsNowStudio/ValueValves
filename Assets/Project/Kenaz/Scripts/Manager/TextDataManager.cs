using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Text;

public class TextContentData
{    
    [XmlAttributeAttribute("ContentID")]
    public string ContentID;
    
    public string ZH_TW;
    public string EN_US;
}

[XmlRootAttribute("Data")]
public class TextDataManager
{
    [XmlArrayAttribute("TextData"), XmlArrayItemAttribute("TextContentData")]
    public List<TextContentData> textDatas = new List<TextContentData>();
    
    public void Save(string path)
     {
         var serializer = new XmlSerializer(typeof(TextDataManager));
         using(var stream = new StreamWriter(new FileStream(path, FileMode.Open), Encoding.UTF8))
         {
             serializer.Serialize(stream, this);
         }
     }
     
    static public TextDataManager Load(string path)
    {
        var serializer = new XmlSerializer(typeof(TextDataManager));
         using(var stream = new FileStream(path, FileMode.Open))
         {
             return serializer.Deserialize(stream) as TextDataManager;
         }
    }
    
     //Loads the xml directly from the given string. Useful in combination with www.text.
    public static TextDataManager LoadFromText(string text) 
     {
         var serializer = new XmlSerializer(typeof(TextDataManager));
         return serializer.Deserialize(new StringReader(text)) as TextDataManager;
     }
}
