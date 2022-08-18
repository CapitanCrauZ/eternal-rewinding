using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimiento : MonoBehaviour
{
    public float velocidad = 7f;
    public float rotacion = 12f;
    // private Animator anim;
    private Rigidbody player;
    private Vector3 desplazamiento;


    // Start is called before the first frame update
    void Start()
    {
        // anim = GetComponent<Animator>();
        player = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        movimientoPlayer(horizontal, vertical);
        // animPlayer(horizontal, vertical);

    }

    void movimientoPlayer(float horizontal, float vertical)
    {      
        desplazamiento.Set(horizontal, 0f, vertical);
        desplazamiento = desplazamiento.normalized * velocidad * Time.deltaTime;

        player.MovePosition(transform.position + desplazamiento);

        if (horizontal != 0f || vertical != 0f){
            rotacionPlayer(horizontal, vertical);
        }
    }

    void rotacionPlayer(float horizontal, float vertical)
    {
        Vector3 targetDirection = new Vector3(horizontal, 0f, vertical);
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);
        Quaternion newRotation = Quaternion.Lerp(GetComponent<Rigidbody>().rotation, targetRotation, rotacion * Time.deltaTime);
        GetComponent<Rigidbody>().MoveRotation(newRotation);
    }
    
    // void animPlayer(float horizontal, float vertical){
    //     bool correr = horizontal != 0f || vertical != 0f;
    //     anim.SetBool("corriendo", correr);
    // }}
}
