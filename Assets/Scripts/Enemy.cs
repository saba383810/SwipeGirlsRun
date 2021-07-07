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

    private void Start()
    {
        hpText.text = HP.ToString("#,0");
        anim = GetComponent<Animator>();
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

    private void OnCollisionStay(Collision other)
    {
        if(other.gameObject.CompareTag("Player")) anim.SetBool(IsBattle,true);
    }

    private void OnCollisionExit(Collision other)
    {
       if(other.gameObject.CompareTag("Player")) anim.SetBool(IsBattle, false);
    }
}

