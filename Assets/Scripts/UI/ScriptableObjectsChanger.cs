using SO;
using UnityEngine;

namespace UI
{
    public class ScriptableObjectsChanger : MonoBehaviour
    {
        [SerializeField] private ScriptableObject[] scriptableObjects;
        [SerializeField] private MapDisplay mapDisplay;
        private int currentIndex;

        private void Awake()
        {
            ChangeScriptableObject(0);
        }

        public void ChangeScriptableObject(int change)
        {
            currentIndex += change;

            if (currentIndex < 0) currentIndex = scriptableObjects.Length - 1;
            else if (currentIndex > scriptableObjects.Length - 1) currentIndex = 0;

            if (mapDisplay != null) mapDisplay.DisplayMap((SOMap)scriptableObjects[currentIndex]);
        }
    }
}