using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int HP =default;
    [SerializeField] private Text hpText =default;
    private Animator anim;
    private static readonly int IsBattle = Animator.StringToHash("isBattle");
    [SerializeField] private ParticleSystem showParticle = default;
    [SerializeField] private ParticleSystem exceptionParticle =default;
    private ParticleSystem destroyParticle;
    private void Start()
    {
        
        hpText.text = HP.ToString("#,0");
        anim = GetComponent<Animator>();
        Invoke(nameof(EnemyDestroy),15);
        var startParticle = Instantiate(showParticle);
        destroyParticle = Instantiate(exceptionParticle);
        startParticle.transform.position = transform.position; 
        startParticle.Play();
    }

    public void HpDamage(int damage)
    {
        HP -= damage;
        hpText.text = HP.ToString("#,0");
        
    }

    public int GetHp()
    {
        return HP;
            
    }

    public void SetHp(int HP)
    {
        this.HP = HP;
    }

    private void OnCollisionStay(Collision other)
    {
        if(other.gameObject.CompareTag("Player")) anim.SetBool(IsBattle,true);
    }

    private void OnCollisionExit(Collision other)
    {
       if(other.gameObject.CompareTag("Player")) anim.SetBool(IsBattle, false);
    }

    public void EnemyDestroy()
    {
        destroyParticle.transform.position = transform.position;
        destroyParticle.Play();
        Destroy(gameObject);
    }
}

