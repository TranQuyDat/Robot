using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    [Header("********Default values*******")]
    public Animator animator;
    public Rigidbody2D rb;
    public float speed;
    public gameManager gameManager;
    public GameObject weaponObj;
    [Header("*********INFO***********")]
    [SerializeField] private float HP;
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
        weaponFlmouse();
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
            , this.transform.localScale.y);
    }

    public void shooter()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            pos.z = 0f;
            gameManager.shooting_Player(pos, "P_projectileRed");
        }
    }

    public void weaponFlmouse()
    {
        Vector3 dirTomouse = (Camera.main.ScreenToWorldPoint(Input.mousePosition)- this.transform.position).normalized;
        dirTomouse.z = 0f;
        dirTomouse.x = Mathf.Clamp(dirTomouse.x, -0.2f, 0.2f) ;
        dirTomouse.y = Mathf.Clamp(dirTomouse.y, -0.2f, 0.2f);
        Vector3 newpos = dirTomouse + this.transform.position;
        weaponObj.transform.position = new Vector2(newpos.x, newpos.y+0.2f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("bullet_enemy")|| collision.CompareTag("E_hitbox"))
        {
            HP = HP - 1;
            playerDead();

        }
    }

    public void playerDead()
    {
        if (HP <= 0)
        {
            Debug.Log("player dead");
            Destroy(this.gameObject);
        }
    }
}
