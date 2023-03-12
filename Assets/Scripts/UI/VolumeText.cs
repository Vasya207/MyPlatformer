using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class VolumeText : MonoBehaviour
    {
        [SerializeField] string volumeName;
        [SerializeField] string textIntro;
        private Text txt;

        private void Awake()
        {
            txt = GetComponent<Text>();
        }

        private void Update()
        {
            UpdateVolume();
        }

        private void UpdateVolume()
        {
            var volumeValue = PlayerPrefs.GetFloat(volumeName) * 100;
            txt.text = textIntro + volumeValue;
        }
    }
}
