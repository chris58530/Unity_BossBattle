using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHealth : MonoBehaviour
{

    public static int health = 10;
    public static bool canHurt = true;

    
    private void Update()
    {
        if (health <= 0)
        {
            PlayerController plr = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
            plr.PlayerDie();
        }
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Bullet" && canHurt)
        {
            canHurt = false;
            health -= 1;
            Debug.Log(health);
            SoundManager.instance.BGM();
            SoundManager.instance.Hurt();

        }
    }
}
