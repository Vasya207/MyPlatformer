using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapDisplay : MonoBehaviour
{
    [SerializeField] private Text levelNumber;
    [SerializeField] private Text levelDescription;
    [SerializeField] private Text playButtonText;
    [SerializeField] private Image backgroundImage;
    [SerializeField] private Button playButton;
    [SerializeField] private GameObject lockIcon;
    [SerializeField] private UIMenuManager menuUI;

    public void DisplayMap(Map _map)
    {
        levelNumber.text = _map.levelNumber;
        levelDescription.text = _map.levelDescription;

        bool mapUnlocked;
        if (_map.mapIndex == 0)
        {
            mapUnlocked = true;
        }
        else
        {
            mapUnlocked = PlayerPrefs.GetInt("Level " + _map.mapIndex) == 1;
        }

        lockIcon.SetActive(!mapUnlocked);
        playButton.interactable = mapUnlocked;

        if (mapUnlocked)
        {
            backgroundImage.color = Color.white;
            levelNumber.color = Color.white;
            levelDescription.color = Color.white;
            playButtonText.color = Color.white;
            SetLevelDescriptionText();
        }

        else
        {
            backgroundImage.color = Color.grey;
            levelNumber.color = Color.grey;
            levelDescription.color = Color.grey;

            var transparentWhiteColor = new Color(1f, 1f, 1f, 0.5f);
            playButtonText.color = transparentWhiteColor;
        }

        playButton.onClick.RemoveAllListeners();
        playButton.onClick.AddListener(() => SceneManager.LoadScene(_map.sceneToLoad.name));
    }

    void SetLevelDescriptionText()
    {
        string levelScore = PlayerPrefs.GetString(levelNumber.text);
        //Debug.Log("Yes" + levelScore);

        if (levelScore != null && levelScore.Length > 0)
        {
            levelDescription.text = levelScore;
        }
        else
        {
            levelDescription.text = "FINISH THIS LEVEL TO GET SCORE";
        }
    }
}
