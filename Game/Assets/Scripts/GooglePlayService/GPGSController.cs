using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GooglePlayGames.BasicApi;
using GooglePlayGames;

public class GPGSController : MonoBehaviour
{
    public GameObject loginBtn, achievementBtn;

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
                loginBtn.SetActive(false);
                achievementBtn.SetActive(true);
                FirstLogin();
            }
            else
            {
                achievementBtn.SetActive(false);
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
