using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float horizontalmove;
    public float verticalmove;
    public float rotateSpeed = 2f;
    public float playerSpeed = 10f;
    public float runSpeed = 15f;
    public float fallVelocity;
    public float jumpForce;
    public float gravity;
    public float dashSpeed;
    public float dashTime;

    public bool isEnable = true;

    public Camera mainCamera;
    public Animator anim;
    public CharacterController player;
    public BoxCollider leftFistBoxCol;
    public BoxCollider rightFistBoxCol;
    public BoxCollider leftKickBoxCol;
    public BoxCollider rightKickBoxCol;

    private Vector3 camForward;
    private Vector3 camRight;
    private Vector3 movePlayer;
    private Vector3 playerInput;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponent<CharacterController>();
        DisableColliders();
        isEnable = true;
    }

    // Update is called once per frame
    void Update()
    {
        //Con esto se definen los controles
        horizontalmove = Input.GetAxis("Horizontal");
        verticalmove = Input.GetAxis("Vertical");

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
        
        // JUMP
        if (player.isGrounded && Input.GetKeyDown(KeyCode.Space) ){
            fallVelocity = jumpForce;
            movePlayer.y = fallVelocity;
            anim.SetBool("Ground", true);
        }
        // DASH
        else if (Input.GetKey(KeyCode.W) && Input.GetKeyDown(KeyCode.LeftControl) && isEnable == true){
            isEnable = false;
            StartCoroutine(Dash());
            anim.SetBool("Dash", true);
            StartCoroutine(CooldownDash());
        }
        // BACK DASH
        else if (Input.GetKey(KeyCode.S) && Input.GetKeyDown(KeyCode.LeftControl) && isEnable == true){
            isEnable = false;
            StartCoroutine(BackDash());
            anim.SetBool("BackDash", true);
            StartCoroutine(CooldownDash());
        }
        // RUN
        else if (Input.GetKey(KeyCode.LeftShift)){
            playerSpeed = runSpeed;
        }
        // KICK
        else if (Input.GetKeyDown(KeyCode.G) && isEnable == true){
            isEnable = false;
            anim.SetBool("Kick", true);
            StartCoroutine(CooldownAttacks());
        }
        // SUPER KICK
        else if (Input.GetKeyDown(KeyCode.K) && isEnable == true){
            isEnable = false;
            anim.SetBool("SuperKick", true);
            StartCoroutine(CooldownAttacks());
        }
        // PUNCH
        else if (Input.GetKeyDown(KeyCode.H) && isEnable == true){
            isEnable = false;
            anim.SetBool("Punch", true);
            StartCoroutine(CooldownAttacks());
        }
        // UPPERCUT
        else if (Input.GetKeyDown(KeyCode.J) && isEnable == true){
            isEnable = false;
            anim.SetBool("Uppercut", true);
            StartCoroutine(CooldownAttacks());
        } 

        else{
            playerSpeed = 10f;
        }
    }

    public void ActivateColliders(){
        leftFistBoxCol.enabled = true;
        rightFistBoxCol.enabled = true;
        leftKickBoxCol.enabled = true;
        rightKickBoxCol.enabled = true;
    }

    public void DisableColliders(){
        leftFistBoxCol.enabled = false;
        rightFistBoxCol.enabled = false;
        leftKickBoxCol.enabled = false;
        rightKickBoxCol.enabled = false;
    }

    IEnumerator Dash(){
        float startTime = Time.time;

        while (Time.time < startTime + dashTime)
        {
            player.Move(camForward * dashSpeed * Time.deltaTime);
            yield return null;
        }
    }


    IEnumerator BackDash(){
        float startTime = Time.time;

        while(Time.time < startTime + dashTime){
            player.Move(-camForward * dashSpeed * Time.deltaTime);
            yield return null;
        }
    }

    IEnumerator CooldownDash(){
        yield return new WaitForSeconds(0.4f);
        isEnable = true;
    }

    IEnumerator CooldownAttacks()
    {
        yield return new WaitForSeconds(0.15f);
        isEnable = true;
    }

}
