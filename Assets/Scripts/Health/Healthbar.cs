using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    [SerializeField] Player player;
    [SerializeField] Image totalHealthbar;
    [SerializeField] Image currentHealthbar;

    private void Start()
    {
        totalHealthbar.fillAmount = player.currentHealth / 10;
    }

    private void Update()
    {
        currentHealthbar.fillAmount = player.currentHealth / 10;
    }
}
