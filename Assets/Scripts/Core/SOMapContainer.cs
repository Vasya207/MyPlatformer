using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Core
{
    [CreateAssetMenu(fileName = "SOMapContainer", menuName = "Scriptable Objects/MapContainer")]
    public class SOMapContainer : ScriptableObject
    {
        public List<Map> mapContainer;
    }
}
