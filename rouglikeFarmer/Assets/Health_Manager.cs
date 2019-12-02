using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Health_Manager : MonoBehaviour
{

    public Image FlashImageRenderer;
    public Color FlashColor;
    private bool damaged = false;
    public float damageFlashSpeed;
    public int numOfHearts = 5;
    [Range(0, 10)]public double HP = 5;
    

    public Image[] hearts;
    public Sprite fullHearth;
    public Sprite halfHearth;
    public Sprite emptyHearth;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (damaged)
        {
            FlashImageRenderer.color = FlashColor;
        }
        else
        {
            FlashImageRenderer.color = Color.Lerp(FlashImageRenderer.color, Color.clear, damageFlashSpeed * Time.deltaTime);
        }

        damaged = false;
        if(HP > numOfHearts)
        {
            HP = numOfHearts;
        }

        for (int i = 0; i < hearts.Length; i++)
        {
            if(i <= HP-1)
            {
                hearts[i].sprite = fullHearth;
            }
            else
            {
                
                if( Math.Ceiling(HP)-1 >= i)
                {

                   
                    hearts[i].sprite = halfHearth;
                }else
                {
                    hearts[i].sprite = emptyHearth;
                }
                
            }
            if(i < numOfHearts)
            {
                hearts[i].enabled = true;
                hearts[i].GetComponent<LayoutElement>().ignoreLayout = false;
            }
            else
            {
                hearts[i].enabled = false;
                hearts[i].GetComponent<LayoutElement>().ignoreLayout = true;
            }
        }
    }

    public void TakeDamage(double Damage)
    {
        damaged = true;
        HP -= Damage;

    }

    public void Heal(double amount)
    {
        HP += amount;
    }
}
