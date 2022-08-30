using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float horizontalmove;
    public float verticalmove;
    public float rotateSpeed = 2f;
    public float playerSpeed = 10f;
    public float fallVelocity;
    public float jumpForce;
    public float gravity;

    public bool OnGround = true;

    public int playerRunSpeed = 15;

    public Camera mainCamera;
    public Animator anim;
    public CharacterController player;

    private Vector3 camForward;
    private Vector3 camRight;
    private Vector3 movePlayer;
    private Vector3 playerInput;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        //Con esto se definen los controles
        horizontalmove = Input.GetAxis("Horizontal");
        verticalmove = Input.GetAxis("Vertical");

        //Funcion para correr
        if (Input.GetKey(KeyCode.LeftShift)){
            playerSpeed = playerRunSpeed;
        }
        else
        {
            playerSpeed = 10;
        }

        //Con esto se mueve el personaje

        playerInput = new Vector3(horizontalmove, 0, verticalmove);

        CamDirection();

        movePlayer = playerInput.x * camRight + playerInput.z * camForward;

        movePlayer = movePlayer * playerSpeed;

        SetGravity();

        PlayerSkills();

        player.Move(movePlayer * Time.deltaTime);
        player.transform.Rotate(0, horizontalmove * rotateSpeed, 0);

        anim.SetFloat("VelX", horizontalmove);
        anim.SetFloat("VelY", verticalmove);

    }

    void CamDirection()
    {
        camForward = mainCamera.transform.forward;
        camRight = mainCamera.transform.right;

        camForward.y = 0;
        camRight.y = 0;

        camForward = camForward.normalized;
        camRight = camRight.normalized;
    }

    public void SetGravity(){
        if(player.isGrounded){
            fallVelocity = -gravity * Time.deltaTime; 
            movePlayer.y = fallVelocity;
        }
        else{
            fallVelocity -= gravity * Time.deltaTime; 
            movePlayer.y = fallVelocity;
        }
        
    }

    public void PlayerSkills(){
        if (player.isGrounded && Input.GetButtonDown("Jump")){
            fallVelocity = jumpForce;
            movePlayer.y = fallVelocity;
            anim.SetBool("Ground", OnGround);
        }
    }
}
