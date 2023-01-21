using UnityEngine;

[CreateAssetMenu(fileName = "New Map", menuName = "Scriptable Objects/Map")]
public class Map : ScriptableObject
{
    public int mapIndex;
    public string levelNumber;
    [TextArea(10, 20)]
    public string levelDescription;
    public Object sceneToLoad;
}
