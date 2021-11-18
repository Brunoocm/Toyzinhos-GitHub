using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    public string nameObj;
    public Sprite imageToy;
    public float health;
    private float m_health;
    public GameObject healthBar;
    public Slider slider;
    public Slider sliderBack;

    private Animator animator => GetComponent<Animator>();
    private float inv;
    private float max;

    private void Start()
    {
        slider.maxValue = health;
        sliderBack.maxValue = health;
        max = health;
        //healthBar.SetActive(false);
    }

    private void Update()
    {
        if (inv > 0)
        {
            inv -= Time.deltaTime;
        }

        slider.value = health;
        sliderBack.value = m_health;

        if(health > max)
        {
            health = max;
        }

        if (health > 0)
        {
            healthBar.transform.LookAt(new Vector3(Camera.main.transform.position.x, transform.position.y, Camera.main.transform.position.z));

            if (m_health > health)
            {
                m_health -= 1.2f * Time.deltaTime;
            }
        }
        if (health <= 0)
        {
            health = 0;
            m_health = 0;
            Destroy(gameObject);

        }

    }
    public void Damage(int damage)
    {
        healthBar.SetActive(true);
        m_health = health;
        health -= damage;
    }

    public void Heal(int heal)
    {
        m_health = health;
        health += heal;
    }
}
