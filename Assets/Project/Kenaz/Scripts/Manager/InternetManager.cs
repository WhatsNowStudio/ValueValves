using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class InternetManager : MonoBehaviour {

    readonly string DomainUrl = "https://www.google.com";

    
    static public InternetManager Instance
    {
        get
        {
            return Toolbox.Instance.GetOrAddComponent<InternetManager>();
        }
    }

    // Use this for initialization
    void Awake()
    {
        Toolbox.RegisterComponent<InternetManager>();
    }
    
    public void CheckConnection(System.Action<bool> action)
    {
        StartCoroutine(StartCheckConnection(action));
    }
    
    IEnumerator StartCheckConnection(System.Action<bool> action)
    {
        /*
        if(Application.isMobilePlatform && Application.internetReachability== NetworkReachability.NotReachable)
        {
            Debug.Log("Don't have internet");
            action (false);
        }
        else
        {
            Ping ping = new Ping(IPAdress);

            int nTime = 0;
            var waitingTime = new WaitForSeconds(1f);
            bool isConnect = true;
            while(!ping.isDone)
            {
                yield return waitingTime;

                if(nTime > 100)    // time 10 sec, OverTime
                {
                    //nTime = 0;
                    Debug.Log("connect failed : " + ping.time);
                    isConnect = false;
                    break;
                }
                nTime++;
            }
            yield return ping.time;

            action (isConnect);
            */
            
            WWW www = new WWW(System.Uri.EscapeUriString(DomainUrl)); 
            float progressTime = 0f;        
            var lastTime = Time.realtimeSinceStartup;
            bool timeout = false;
            while (!www.isDone)
            {
                var deltaTime = Time.realtimeSinceStartup - lastTime;
                lastTime = Time.realtimeSinceStartup;
                progressTime += deltaTime;
                if(progressTime > 10f)
                {
                    timeout = true;
                    break;
                }
                // ProgressController.Instance.UpdateProgress(www.progress);            
                yield return null;
            }
            
            if (string.IsNullOrEmpty(www.error) && !timeout) {
                action (true);
            } else {
                Debug.Log("www.error : " + www.error);
                action (false);
            }
        //}
    }
}
