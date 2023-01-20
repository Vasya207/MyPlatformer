using UnityEngine;

public class ScriptableObjectsChanger : MonoBehaviour
{
    [SerializeField] ScriptableObject[] scriptableObjects;
    [SerializeField] MapDisplay mapDisplay;
    private int currentIndex;

    private void Awake()
    {
        ChangeScriptableObject(0);
    }
    public void ChangeScriptableObject(int _change)
    {
        currentIndex += _change;

        if (currentIndex < 0) currentIndex = scriptableObjects.Length - 1;
        else if (currentIndex > scriptableObjects.Length - 1) currentIndex = 0;

        if(mapDisplay != null) mapDisplay.DisplayMap((Map)scriptableObjects[currentIndex]);
    }
}
