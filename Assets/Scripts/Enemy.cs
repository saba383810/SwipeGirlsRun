using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int HP =default;
    [SerializeField] private Text hpText =default;

    private void Start()
    {
        hpText.text = HP.ToString("#,0");
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
    
    
}

