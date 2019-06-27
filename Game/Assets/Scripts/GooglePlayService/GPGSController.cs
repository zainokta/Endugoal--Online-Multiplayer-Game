using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames.BasicApi;
using GooglePlayGames;
using UnityEngine.UI;

public class GPGSController : MonoBehaviour
{
    public Button loginBtn, achievementBtn;

    // Start is called before the first frame update
    void Start()
    {
        Login();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Login()
    {
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder().Build();
        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.Activate();

        Social.localUser.Authenticate((bool success) =>
        {
            if (success == true)
            {
                loginBtn.interactable = false;
                achievementBtn.interactable = true;
                FirstLogin();
            }
            else
            {
                achievementBtn.interactable = false;
            }
        });
    }

    void FirstLogin()
    {
        PlayGamesPlatform.Instance.ReportProgress(GPGSIds.achievement_first_login, 100f, null);
    }

    public void ShowAchievement()
    {
        PlayGamesPlatform.Instance.ShowAchievementsUI();
    }
}
