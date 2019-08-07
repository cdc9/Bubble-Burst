using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BossHealth : MonoBehaviour
{
    //Configs

    public Image healthImage;

    public float health;
    private float maxHealth;
    // Start is called before the first frame update
    void Start()
    {

        //Boss Health
        maxHealth = 100f;
        health = maxHealth;

        healthImage = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
         healthImage.fillAmount = health / maxHealth;
    }

    public void TakeDamage()
    {
        health = health - 10;
    }
}
