using UnityEngine;
using UnityEngine.UI;

namespace HealthSystem
{
    public class Healthbar : MonoBehaviour
    {
        [SerializeField] private Player.Player player;
        [SerializeField] private Image totalHealthbar;
        [SerializeField] private Image currentHealthbar;

        private void Start()
        {
            totalHealthbar.fillAmount = player.CurrentHealth / 10;
        }

        private void Update()
        {
            currentHealthbar.fillAmount = player.CurrentHealth / 10;
        }
    }
}