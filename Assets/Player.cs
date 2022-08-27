using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    private bool isJumpClicked;
    private float horizontalInput;
    private Rigidbody rigitbodyComponent;
    private bool isGrounded;
    [SerializeField]private Transform groundChecker;
    private bool isCollected;
    private string text;
    [SerializeField] private Text textbox;
    private bool specialTreasure;
    // Start is called before the first frame update
    void Start()
    {
        rigitbodyComponent = GetComponent<Rigidbody>();
        text = "Find a box";
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        if (isCollected)
        {
            text = "You found a box!";
            textbox.text = text;
            Time.timeScale = 0;
        }
        if (specialTreasure)
        {
            string textspecial = "You found a hidden gold!";
            textbox.text = text + " + " + textspecial;
            Vector3 hor = new Vector3(horizontalInput * 2, 0, 0);
            transform.position = transform.position + hor * Time.deltaTime;
        }
        if (!isGrounded)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            isJumpClicked = true;
        }
    }
    //Once every physic update
    void FixedUpdate()
    {
        rigitbodyComponent.velocity = new Vector3(horizontalInput, rigitbodyComponent.velocity.y, 0);
        if (Physics.OverlapSphere(groundChecker.position, 0.1f).Length == 0)
        {
            return;
        }
        if (isJumpClicked)
        {
            rigitbodyComponent.AddForce(Vector3.up * 7, ForceMode.VelocityChange);
            isJumpClicked = false;
        }
        
    }
    void OnCollisionEnter(Collision collision)
    {
        isGrounded = true;
    }
    void OnCollisionExit(Collision collison)
    {
        isGrounded = false;
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == 6)
        {
            Destroy(other.gameObject);
            isCollected = true;
        }
        if(other.gameObject.layer == 8)
        {
            Destroy(other.gameObject);
            specialTreasure = true;
        }
    }
}
