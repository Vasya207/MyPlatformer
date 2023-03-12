using SO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class MapDisplay : MonoBehaviour
    {
        [SerializeField] private Text levelNumber;
        [SerializeField] private Text levelDescription;
        [SerializeField] private Text playButtonText;
        [SerializeField] private Image backgroundImage;
        [SerializeField] private Button playButton;
        [SerializeField] private GameObject lockIcon;

        public void DisplayMap(SOMap soMap)
        {
            levelNumber.text = soMap.levelNumber;
            levelDescription.text = soMap.levelDescription;

            bool mapUnlocked;
            if (soMap.mapIndex == 0)
                mapUnlocked = true;
            else
                mapUnlocked = PlayerPrefs.GetInt("Level " + soMap.mapIndex) == 1;

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
            playButton.onClick.AddListener(() => SceneManager.LoadScene(soMap.sceneToLoad.name));
        }

        private void SetLevelDescriptionText()
        {
            var levelScore = PlayerPrefs.GetString(levelNumber.text);

            if (levelScore != null && levelScore.Length > 0)
                levelDescription.text = levelScore;
            else
                levelDescription.text = "FINISH THIS LEVEL TO GET SCORE";
        }
    }
}