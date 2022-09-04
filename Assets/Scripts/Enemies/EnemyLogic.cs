using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyLogic : MonoBehaviour
{

    public float life = 0.5f;
    public float maxLife = 1f;
    public float fistDamage = 0.05f;
    public float kickDamage = 0.1f;

    public Slider lifeBar;
    public Animator anim;
    
    void Start()
    {
        lifeBar.value = life / maxLife;
    }

    void Update()
    {
     
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.tag == "FistDamage"){
            if (anim != null){
                anim.Play("DamagedEnemy");
            }

            life -= fistDamage;
            lifeBar.value = life;
        }

        if (other.tag == "KickDamage"){

            if (anim != null)
            {
                anim.Play("DamagedEnemy");
            }

            life -= kickDamage;
            lifeBar.value = life;
        }

        if (life <= 0){
            Destroy(gameObject);
        }
    }

}
