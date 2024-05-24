using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firepoint;
    [SerializeField] private GameObject[] arrows;
    private float cooldownTimer;

    [Header("SFX")]
    [SerializeField] private AudioClip arrowSound;

    private void Attack()
    {
        SoundManager.instance.PlaySound(arrowSound);
        cooldownTimer = 0;
        arrows[Findarrow()].transform.position = firepoint.position;
        arrows[Findarrow()].GetComponent<EnemyProjectile>().ActivateProjectile();
    }

    private int Findarrow()
    {
        for(int i = 0; i<arrows.Length;i++)
        {
            if(!arrows[i].activeInHierarchy)
                return i;
        }
        return 0;
    }


    private void Update()
    {
        cooldownTimer += Time.deltaTime;
        if(cooldownTimer >= attackCooldown)
            Attack();
    }


}
