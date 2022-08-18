using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float x,y;
    public float velocidadRotación = 50;
    public float velocidadMovimiento = 10;
    public float fuerzaSalto = 8f;
    public int velocidadCorrer = 15;
    public Animator anim;
    public Rigidbody rb;
    // public Animator anim;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //Con esto se definen los controles
        x = Input.GetAxis("Horizontal");
        y = Input.GetAxis("Vertical");

        if (Input.GetKey(KeyCode.LeftShift)){
            velocidadMovimiento = velocidadCorrer;
        }
        else
        {
            velocidadMovimiento = 10;
        }

        if (Input.GetKeyDown(KeyCode.Space)){
            rb.AddForce(new Vector3(0, fuerzaSalto, 0), ForceMode.Impulse);
        }

        //Con esto se mueve el personaje
        transform.Rotate(0, x * Time.deltaTime * velocidadRotación, 0);
        transform.Translate(0, 0, y * Time.deltaTime * velocidadMovimiento);
        


        // anim.SetFloat("VelX",x);
        // anim.SetFloat("VelY",y);

        anim.SetFloat("VelX", x);
        anim.SetFloat("VelY", y);
    }
}
