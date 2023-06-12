using System;
using Core;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataController : Singleton<DataController>
{
    private void OnEnable()
    {
        Signals.OnLevelFinished.AddListener(MarkLevelAsFinished);
    }

    private void OnDisable()
    {
        Signals.OnLevelFinished.RemoveListener(MarkLevelAsFinished);
    }

    public void MarkLevelAsFinished()
    {
        PlayerPrefs.SetInt($"Level {SceneManager.GetActiveScene().buildIndex}", 1);
        PlayerPrefs.Save();
    }

    public void SaveScore(string key, int score)
    {
        if (PlayerPrefs.GetInt(key) < score)
        {
            PlayerPrefs.SetInt(key,score);
            PlayerPrefs.Save();
        }
    }

    public void SaveMenuText(string key, string menuText)
    {
        PlayerPrefs.SetString(key, menuText);
        PlayerPrefs.Save();
    }
}
