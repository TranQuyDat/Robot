using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public Animator animator;
    public Rigidbody2D rb;
    public float speed;
    public gameManager gameManager;

    private Vector2 movement;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        animator.SetBool("isrun", (movement.x != 0 || movement.y != 0));

        shooter();
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * speed * Time.deltaTime);
        flip();
    }
    public void flip()
    {
        float scalex = this.transform.localScale.x;
        this.transform.localScale = new Vector2(
            (movement.x==0 )? scalex :
            (movement.x > 0)? Mathf.Abs(scalex): Mathf.Abs(scalex) * -1
            , 1f);
    }

    public void shooter()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pos.z = 0f;
            gameManager.shooting_player(pos);
        }
    }

}
