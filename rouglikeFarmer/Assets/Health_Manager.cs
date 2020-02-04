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
    public ParticleSystem healParticle;

    // Start is called before the first frame update
    private void Awake()
    {
        FlashImageRenderer = GameObject.Find("FlashScreen").GetComponent<Image>();
        Debug.Log("sss");
        
    }

    public void SetHearts()
    {
        switch (mainSceneController.GameDifficulty)
        {
            case mainSceneController.Difficulty.Easy:
                numOfHearts = 10;
                break;
            case mainSceneController.Difficulty.Normal:
                numOfHearts = 5;
                break;
            case mainSceneController.Difficulty.Hard:
                numOfHearts = 3;
                break;
            default:
                numOfHearts = 5;
                break;
        }
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
        if (HP <= 0)
        {
            Die();
        }

    }

    public void Die()
    {
        mainSceneController.SaveScene();
        Loader.Load(Loader.Scene.Main, Loader.Scene.GameOver);
    }

    public void Heal(double amount)
    {
        HP += amount;
        healParticle.Play();
    }

    public void HealFull()
    {
        HP = numOfHearts;
        healParticle.Play();
    }
}
