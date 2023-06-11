using Core;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DataController : Singleton<DataController>
{
    private void OnEnable()
    {
        Signals.OnLevelFinished.AddListener(MarkLevelAsFinished);
    }

    public void MarkLevelAsFinished()
    {
        PlayerPrefs.SetInt($"Level {SceneManager.GetActiveScene().buildIndex}", 1);
        PlayerPrefs.Save();
    }
}
