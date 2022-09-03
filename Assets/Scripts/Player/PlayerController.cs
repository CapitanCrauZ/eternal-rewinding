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
    public float cooldown;

    float lastSkill;

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
        else if (Input.GetKey(KeyCode.W) && Input.GetKeyDown(KeyCode.LeftControl)){
            StartCoroutine(Dash());
            anim.SetBool("Dash", true);
        }
        // BACK DASH
        else if (Input.GetKey(KeyCode.S) && Input.GetKeyDown(KeyCode.LeftControl)){
            StartCoroutine(BackDash());
            anim.SetBool("BackDash", true);
        }
        // RUN
        else if (Input.GetKey(KeyCode.LeftShift)){
            playerSpeed = runSpeed;
            anim.SetBool("Run", true);
        }
        // KICK
        else if (Input.GetKeyDown(KeyCode.G)){
            anim.SetBool("Kick", true);
        }
        // SUPER KICK
        else if (Input.GetKeyDown(KeyCode.K)){
            anim.SetBool("SuperKick", true);
        }
        // PUNCH
        else if (Input.GetKeyDown(KeyCode.H)){
            anim.SetBool("Punch", true);
        }
        // UPPERCUT
        else if (Input.GetKeyDown(KeyCode.J)){
            anim.SetBool("Uppercut", true);
        } 

        else{
            playerSpeed = 10;
        }
    }

    IEnumerator Dash(){
        float startTime = Time.time;

        while(Time.time < startTime + dashTime){
            player.Move(camForward * dashSpeed * Time.deltaTime);
            yield return null;
        }
    }

    IEnumerator BackDash()
    {
        float startTime = Time.time;

        while(Time.time < startTime + dashTime){
            player.Move(-camForward * dashSpeed * Time.deltaTime);
            yield return null;
        }
    }

    public void SkillCoolDown(){
        if(Time.time-lastSkill<cooldown){
            return;
        }
        lastSkill = Time.time;
    }
}
