using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRutine : MonoBehaviour
{

    public int rutina;
    public float cronometro;
    public Animator anim;
    public Quaternion angulo;
    public float grado;

    // Start is called before the first frame update
    void Start()
    {   
        anim = GetComponent<Animator>();
    }

    public void ComportamientoEnemigo(){
        cronometro += 1 * Time.deltaTime;
        if (cronometro >= 5)
        {
            rutina = Random.Range(0, 3);
            cronometro = 0;
        }
        switch (rutina)
        {
            case 0:
                anim.SetBool("walk", false);
                break;
            case 1:
                grado = Random.Range(0, 360);
                angulo = Quaternion.Euler(0, grado, 0);
                break;
            case 2:
                transform.rotation = Quaternion.RotateTowards(transform.rotation, angulo, 0.5f);
                transform.Translate(Vector3.forward * 2 * Time.deltaTime);
                anim.SetBool("walk", true);
                break;
        }
    
    }

    void Update()
    {
        ComportamientoEnemigo();
    }

}
