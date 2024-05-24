using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private float startingHealth;
    public float currentHealth{ get; private set; }
    private Animator anim;
    private bool dead;

    [Header("iFrames")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberofFrames;
    private SpriteRenderer spriteRend;

    [Header("Components")]
    [SerializeField] private Behaviour[] components;
    private bool invulnerable;

    [Header("Death Sound")]
    [SerializeField] private AudioClip deathSound;

    [Header("Hurt Sound")]
    [SerializeField] private AudioClip hurtSound;


    


    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(float _damage)
    {
        if(invulnerable) return;
        currentHealth = Mathf.Clamp(currentHealth - _damage,0,startingHealth);
        
        if(currentHealth > 0)
        {
            anim.SetTrigger("hurt");
            SoundManager.instance.PlaySound(hurtSound);
            StartCoroutine(Invulnerability());

        }
        
        else
        {
            if(!dead)
            {
                
                // if(GetComponent<PlayerMove>() != null)
                //     GetComponent<PlayerMove>().enabled = false;

                // if(GetComponentInParent<EnemyPatrol>() != null)
                //     GetComponentInParent<EnemyPatrol>().enabled = false;
                
                // if(GetComponent<MeleeEnemy>() != null) 
                //     GetComponent<MeleeEnemy>().enabled = false;

                foreach (Behaviour component in components)
                    component.enabled = false;
                
                anim.SetBool("grounded",true);
                anim.SetTrigger("die");

                
                
                dead = true; 
                SoundManager.instance.PlaySound(deathSound);
            }
        }

    }

    public void AddHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value,0,startingHealth);
    } 
    
    public void Respawn()
    {
        dead = false;

        AddHealth(startingHealth);
        anim.ResetTrigger("die");
        anim.Play("Idle");
        StartCoroutine(Invulnerability());

        foreach (Behaviour component in components)
                    component.enabled = true;
                
    }
    private IEnumerator Invulnerability()
    {
        invulnerable = true;
        Physics2D.IgnoreLayerCollision(6,7,true);
        for(int i = 0;i<numberofFrames;i++)
        {
            spriteRend.color = new Color(1,0,0,0.5f);
            yield return new WaitForSeconds(iFramesDuration/(numberofFrames*3));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration/(numberofFrames*3));
        }
        Physics2D.IgnoreLayerCollision(6,7,false);
        invulnerable = false;

    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}
