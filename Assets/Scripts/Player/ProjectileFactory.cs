using UnityEngine;

namespace Player
{
    public class ProjectileFactory: MonoBehaviour
    {
        [SerializeField] private GameObject[] arrows;
        
        public int FindArrow()
        {
            for (var i = 0; i < arrows.Length; i++)
                if (!arrows[i].activeInHierarchy)
                    return i;
            return 0;
        }
    }
}