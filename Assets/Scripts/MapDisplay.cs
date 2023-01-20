using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MapDisplay : MonoBehaviour
{
    [SerializeField] private Text levelNumber;
    [SerializeField] private Text levelDescription;
    [SerializeField] private Text playButtonText;
    [SerializeField] private Image backgroundImage;
    [SerializeField] private Button playButton;
    [SerializeField] private GameObject lockIcon;
    public void DisplayMap(Map _map)
    {
        levelNumber.text = _map.levelNumber;
        levelDescription.text = _map.levelDescription;

        bool mapUnlocked = PlayerPrefs.GetInt("currentScene", 0) >= _map.mapIndex;

        lockIcon.SetActive(!mapUnlocked);
        playButton.interactable = mapUnlocked;

        if (mapUnlocked)
        {
            backgroundImage.color = Color.white;
            levelNumber.color = Color.white;
            levelDescription.color = Color.white;
            playButtonText.color = Color.white;
        }

        else
        {
            backgroundImage.color = Color.grey;
            levelNumber.color = Color.grey;
            levelDescription.color = Color.grey;
            playButtonText.color = Color.grey;
        }

        playButton.onClick.RemoveAllListeners();
        playButton.onClick.AddListener(() => SceneManager.LoadScene(_map.sceneToLoad.name));
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
