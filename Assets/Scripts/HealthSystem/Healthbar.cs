using System;
using Core;
using UnityEngine;
using UnityEngine.UI;

namespace HealthSystem
{
    public class Healthbar : MonoBehaviour, IObserver
    {
        [SerializeField] private Player.Player player;
        [SerializeField] private Image totalHealthbar;
        [SerializeField] private Image currentHealthbar;
        [SerializeField] private Subject playerSubject;

        // private void Start()
        // {
        //     totalHealthbar.fillAmount = player.CurrentHealth / 10;
        // }
        //
        // // private void Update()
        // // {
        // //     currentHealthbar.fillAmount = player.CurrentHealth / 10;
        // // }

        private void OnEnable()
        {
            playerSubject.AddObserver(this);
        }

        private void OnDisable()
        {
            playerSubject.RemoveObserver(this);
        }

        public void OnNotify(float value)
        {
            currentHealthbar.fillAmount = value / 10;
        }
    }
}