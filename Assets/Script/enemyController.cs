using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyController : MonoBehaviour
{
    public GameObject player;
    public float distanceToAtk_shoot = 4;
    public float distanceToAtk_melee = 2;
    public float speed = 2;
    public List<SteeringBehaviour> steeringBehaviours;
    public ContextSolver contexsolver;
    public List<Detector> detectors;
    public AiData aiData;

    public float timedelay  = 0.05f;
    
    public bool showgizmos = true;
    public gameManager gameManager;

    public Animator animator;

    bool isAtk = false;
    bool isAtkshoot = true;
    Collider2D trigercollider;
    Vector2 movement;
    private void Start()
    {
        InvokeRepeating("performdetection",0, timedelay);
        movement = this.transform.position;
    }
    private void Update()
    {
        
        Aienemy();
    }

    private void performdetection()
    {
        foreach(Detector detector in detectors)
        {
            detector.Detect(aiData);
        }

       
    }
    public void Aienemy()
    {
        float dis = Vector2.Distance(this.transform.position, player.transform.position);
        atk(dis);
        moving();
        flip();
    }
    public bool isrunning()
    {
        if(movement !=(Vector2) this.transform.position)
        {
            movement = this.transform.position;
            return true;
        }
        return false;
    }
    public void moving()
    {
        if (isAtk)
        {
            animator.SetBool("isrun", false);
            return;
        }
        speed = 2;
        animator.SetBool("isAtkShoot", false);
        animator.SetBool("isAtkMelee", false);
        animator.SetBool("isrun", isrunning());
        transform.Translate(contexsolver.
            GetDirectionToMove(steeringBehaviours, aiData) * speed * Time.deltaTime);
    }

    public void flip()
    {
        Vector2 direction = (player.transform.position - this.transform.position).normalized;
        float scalex = this.transform.localScale.x;
        this.transform.localScale = new Vector2((direction.x == 0) ? scalex :
            (direction.x < 0) ? Mathf.Abs(scalex) * -1 : Mathf.Abs(scalex),1);;
    }
    public void atk(float dis)
    {
        if (distanceToAtk_melee < dis && dis <= distanceToAtk_shoot)
        {
            isAtk = true;
            speed = 0;
            Atkshoot();
        }
        else if (dis > distanceToAtk_shoot)
        {
            isAtk = false;
            speed = 2;
        }
        else if (dis < distanceToAtk_melee 
            && (trigercollider == null ||!trigercollider.CompareTag("Player")))
        {
            isAtk = false;
            speed = 2;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            trigercollider = collision;
            isAtk = true;
            Atkmelee();
            Debug.Log(isAtk);
        }
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        trigercollider = null;
    }
    public void Atkshoot()
    {
        if (isAtkshoot)
        {
            isAtkshoot = false;
            animator.SetBool("isAtkMelee", false);
            animator.SetBool("isAtkShoot", true);
            Debug.Log("atk shoot");
            gameManager.shooting_enemy(player.transform.position);
            StartCoroutine(waitTosetBoolshoot(true));
        }
    }

    IEnumerator waitTosetBoolshoot(bool value)
    {
        yield return new WaitForSeconds(2f);
        isAtkshoot = value;
    }
    public void Atkmelee()
    {
        animator.SetBool("isAtkShoot", false);
        animator.SetBool("isAtkMelee", true);
        Debug.Log("atk melee");
    }

    private void OnDrawGizmos()
    {
        if (!showgizmos) return;
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position,distanceToAtk_shoot);
    
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,distanceToAtk_melee);
    }
}
