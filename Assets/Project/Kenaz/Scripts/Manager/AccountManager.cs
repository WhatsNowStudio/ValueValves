using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AccountManager : MonoBehaviour {
    
    public enum LoginState { Success, Failed, InternetError };

    // Use this for initialization
    public readonly string AccountTag = "Account";
    public readonly string PWTag = "Password";
    public readonly string IDTag = "ID";

    public readonly string TimeTag = "LoginTime";
    public readonly string SIGN_OUT = "sign_out";

    static public bool checkEnable = true; 

    /*
    //incomplete
    JSONObject _curUserJson;
    public JSONObject curUserJson
    {
        get
        {
            if(_curUserJson == null)
            {
                _curUserJson = new JSONObject(SimpleIO.ReadData(string.Format("{0}.json", PlayerPrefs.GetString(AccountTag))));
            }

            return _curUserJson;
        }
    }
    */

    static public AccountManager Instance
    {
        get
        {
            return Toolbox.Instance.GetOrAddComponent<AccountManager>();
        }
    }

    void Awake()
    {
        Toolbox.RegisterComponent<AccountManager>();
    }
    
    public bool IsLogin()
    {
        return !string.IsNullOrEmpty(PlayerPrefs.GetString(AccountTag, string.Empty));
    }
    
    /*
    public IEnumerator CheckAccountAvailable()
    {
        yield return StartCoroutine(StartCheckLogin(PlayerPrefs.GetString(AccountTag, string.Empty), PlayerPrefs.GetString(PWTag, string.Empty), null));
    }
    */
    
    public void Logout()
    {
        // PlayerPrefs.SetString(AccountTag, string.Empty);
        //PlayerPrefs.SetString(PWTag, string.Empty);
        PlayerPrefs.SetString(TimeTag, SIGN_OUT);
    }
    
    /*
    public void CheckLogin(string account, string pw, System.Action<LoginState, string> action)
    {
        StartCoroutine(StartCheckLogin(account, pw, action));
    }

    IEnumerator StartCheckLogin(string account, string pw, System.Action<LoginState, string> action)
    {
        if(string.IsNullOrEmpty(account) || string.IsNullOrEmpty(pw))
        {
            // PlayerPrefs.SetString(AccountTag, string.Empty);
            PlayerPrefs.SetString(PWTag, string.Empty);
            PlayerPrefs.SetString(IDTag, string.Empty);
            if (null != action)
            {
                action(LoginState.Failed, "Empty e-mail or password.");
            }
        }
        else
        {
            WWWForm form = new WWWForm();
            form.AddField("email", account);
            form.AddField("password", pw);
            WWW www = new WWW(InternetManager.GetLoginUrl, form);
            //var process = UIMainController.Instance;
            //process.UpdateProgressBar(0f);
            //yield return Timing.WaitUntilDone(www);

            float progressTime = 0f;
            var lastTime = Time.realtimeSinceStartup;
            bool timeout = false;

            while (!www.isDone)
            {
                //process.UpdateProgressBar(www.progress);
                var deltaTime = Time.realtimeSinceStartup - lastTime;
                lastTime = Time.realtimeSinceStartup;
                progressTime += deltaTime;
                if (progressTime > 10f)
                {
                    timeout = true;
                    break;
                }
                yield return null;
            }
            
            if(www.isDone && string.IsNullOrEmpty(www.error) && !timeout)
            {
                //process.UpdateProgressBar(1f);
                //Debug.LogFormat("{0}", www.text);
                // System.Text.RegularExpressions.Regex.Unescape
                //var id = www.text[1].ToString();
                string jsonText;
                if(www.text.IndexOf("{") != 0)
                {
                    jsonText = string.Format("{0}", www.text.Substring(www.text.IndexOf("{")-1, www.text.Split('{')[1].Length+1));
                    Debug.LogFormat("final text : {0}", jsonText);
                }
                else
                {
                    jsonText = www.text;
                }
                var result = new JSONObject(System.Text.RegularExpressions.Regex.Unescape(jsonText));
                print("result = "+result.ToString());
                if(result.GetField("status").n == 1 && string.IsNullOrEmpty(result.GetField("errorMsg").str))
                {
                    _curUserJson = result;
                    SimpleIO.WriteData(account+".json", result.ToString());
                    
                    PlayerPrefs.SetString(IDTag, result.GetField("user_id").str);

                    if(SIGN_OUT.Equals(PlayerPrefs.GetString(TimeTag, SIGN_OUT)))
                    {
                        Debug.Log("date = " + System.DateTime.Now.ToString());
                        PlayerPrefs.SetString(TimeTag, System.DateTime.Now.ToString());
                        action(LoginState.Success, string.Empty);
                    }
                    else
                    {
                        try
                        {
                            var date = System.Convert.ToDateTime(PlayerPrefs.GetString(TimeTag, SIGN_OUT));
                            var timeSpan = System.DateTime.Now.Subtract(date);
                            
                            //if(timeSpan.Minutes >= 1) //for testing
                            if(timeSpan.Days >= 1)
                            {
                                //PlayerPrefs.SetString(TimeTag, SIGN_OUT);
                                Logout();
                                action(LoginState.Failed, "Please sign in again.");
                            }
                            else
                            {
                                action(LoginState.Success, string.Empty);
                            }
                        }
                        catch(System.FormatException e)
                        {
                            Debug.Log("error: "+e.Message);
                            Logout();
                            action(LoginState.Failed, "Please sign in again.");
                        }
                    }
                }
                else
                {
                    Debug.LogWarningFormat("Login Failed! status: {0}\nMSG: {1}", result.GetField("status").n.ToString(), result.GetField("errorMsg").str);

                    PlayerPrefs.SetString(IDTag, string.Empty);
                    
                    action(LoginState.Failed, result.GetField("errorMsg").str);
                }
            }
            else
            {
                Debug.Log("www.error : " + www.error);
                action(LoginState.InternetError, "Please, Check your internet connectivity and try agian.");
            }
        }
    }
    */
}
