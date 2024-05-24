using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
   [SerializeField] private Health playerHealth;
   [SerializeField] private Image healthbarTotal;
   [SerializeField] private Image healthbarCurrent;

   private void Start()
   {
      healthbarTotal.fillAmount = playerHealth.currentHealth/10;
   }

   private void Update()
   {
      healthbarCurrent.fillAmount = playerHealth.currentHealth/10;
   }



}
