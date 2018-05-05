using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Collections.Generic;

public class APIDataDownloader
{
    public APIDataDownloader(List<string> urls)
    {
        //use tool box for coroutine
        Toolbox.Instance.StartCoroutine(StartDownload(urls));
    }

    //should be changed to use UnityWebRequest or .net http request
    IEnumerator StartDownload(List<string> urls)
    {
        for(int i = 0; i < urls.Count; i++)
        {
            var url = urls[i];
            var www = new WWW(url);
            yield return www;

            var fileName = GetFileNameFromUrl(urls[i]);
            EasyIO.SaveFile(fileName, www.bytes);
            Debug.LogFormat("downloaded file: {0}", fileName);
        }
    }

    IEnumerator StartDownLoadByWebRequest(List<string> urls)
    {
        for(int i = 0; i < urls.Count; i++)
        {
            using(var request = UnityWebRequest.Get(urls[i]))
            {
                var download = new DownloadHandlerBuffer();
                request.downloadHandler = download;
                yield return request.Send();
                var fileName = GetFileNameFromUrl(urls[i]);
                EasyIO.SaveFile(fileName, download.data);
                Debug.LogFormat("downloaded file: {0}", fileName);
            }
        }
    }

    string GetFileNameFromUrl(string url)
    {
        var fileSplit = url.Split('/');
        var fileName = fileSplit[fileSplit.Length-1];
        var fileNameSplit = fileName.Split('.');
        return string.Format("{0}.{1}", fileNameSplit[0], fileNameSplit[1]);

    }
}