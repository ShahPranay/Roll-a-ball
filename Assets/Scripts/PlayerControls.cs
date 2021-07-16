using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerControls : MonoBehaviour
{
    public float speed = 0;
    public TextMeshProUGUI CountText;
    public GameObject winTextObject;

    public InputAct Controls;
    private Rigidbody rb;
    private bool isMoving=false;
    private bool isgrounded;
    private float movementX,movementY;
    private float delay=0.1f;
    private int count;


    void Awake(){
        Controls = new InputAct();
        Controls.Player.Movement.performed += ctx => Move(ctx.ReadValue<Vector2>());
        Controls.Player.Movement.canceled += ctx => StopMove();
        Controls.Player.Jump.performed += Ctx => Jumped();
    }
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        count=0;
        SetCountText();
        winTextObject.SetActive(false);
    }

    void OnEnable(){
        Controls.Enable();
    }

    void OnDisable(){
        Controls.Disable();
    }
    IEnumerator whileMoving(){
        isMoving=true;
        Debug.Log("started");
        while(isMoving){
            Vector3 movement= new Vector3(movementX, 0.0f, movementY);
            rb.AddForce(movement*speed);
            yield return new WaitForSeconds(delay);
        }
    }
    void Move(Vector2 movementvector)
    {
        movementX=movementvector.x;
        movementY=movementvector.y;
        if(!isMoving)
        StartCoroutine(whileMoving());
    }
    void StopMove(){
        if(isMoving){
            isMoving=false;
            Debug.Log("stopped");
            StopCoroutine(whileMoving());
        }
    }
    void Jumped(){
        Vector3 jump= new Vector3(0.0f,300f,0.0f);
        if(isgrounded)
            rb.AddForce(jump);
    }
    void SetCountText(){
        CountText.text="Count: "+count.ToString();
        if(count>=12){
            winTextObject.SetActive(true);
        }
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("PickUp"))
        {
            other.gameObject.SetActive(false);
            count+=1;
            speed+=count;
            FindObjectOfType<AudioManager>().play("PickUp");
            SetCountText();
        }
    }
    void OnCollisionEnter(Collision theCollision){
        if(theCollision.gameObject.name == "Ground"){
            isgrounded=true;
        }
    }
    void OnCollisionExit(Collision theCollision){
        if(theCollision.gameObject.name == "Ground"){
            isgrounded=false;
        }
    }
}
