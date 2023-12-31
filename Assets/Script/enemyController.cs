using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyController : MonoBehaviour
{

    [Header("SteeringBehaviours,contexsolver,detectors")]
        public float distanceToAtk_shoot = 4;
        public float distanceToAtk_melee = 2;
        public List<SteeringBehaviour> steeringBehaviours;
        public ContextSolver contexsolver;
        public List<Detector> detectors;
        public AiData aiData;
    [Header("********Defaulvalue**********")]
        new  public GameObject player;
        public float speed = 2;
        public float timedelay  = 0.03f;
        public bool showgizmos = true;
        public gameManager gameManager;
        public Animator animator;
        public Transform posStart;
    [Header("********INFO**********")]
        [SerializeField] private float HP;

    EnemySpawner enemySpawner;
    bool isAtk = false;
    bool isshooted = true;
    Collider2D trigercollider;
    Vector2 movement;
    bool isdead = false;
    private void Start()
    {
        gameManager = FindAnyObjectByType<gameManager>();
        player = FindAnyObjectByType<playerController>().gameObject;
        enemySpawner = FindAnyObjectByType<EnemySpawner>();
        InvokeRepeating("performdetection",0, timedelay);
        movement = this.transform.position;
    }
    private void Update()
    {
        if(player == null)
        {
            player = new GameObject();
        }

        if (this.gameObject.active == true && isdead)
        {
            enemyRestart();
        }
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
        Vector3 direction = contexsolver.
            GetDirectionToMove(steeringBehaviours, aiData) * speed * Time.deltaTime;
        if (isAtk)
        {
            animator.SetBool("isrun", false);
            return;
        }
        if (!isshooted) return;
        speed = 2;
        animator.SetBool("isAtkShoot", false);
        animator.SetBool("isAtkMelee", false);
        animator.SetBool("isrun", isrunning());
        transform.Translate(direction);
    }

    public void flip()
    {
        Vector2 direction = (player.transform.position - this.transform.position).normalized;
        float scalex = this.transform.localScale.x;
        this.transform.localScale = new Vector2((direction.x == 0) ? scalex :
            (direction.x < 0) ? Mathf.Abs(scalex) * -1 : Mathf.Abs(scalex), this.transform.localScale.y);;
    }
    public void atk(float dis)
    {
        if (aiData.targets == null ) 
        {
            return; 
        }
        if (distanceToAtk_melee < dis && dis <= distanceToAtk_shoot
            && aiData.targets.Count>0)
        {
            isAtk = true;
            speed = 0;
            
            Invoke("Atkshoot",1f);
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

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            trigercollider = collision;
            isAtk = true;
            Atkmelee();
            Debug.Log(isAtk);
        }
        else
        {
            animator.SetBool("isAtkMelee", false);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("bullet_player"))
        {
            HP = HP - 1;
            enemyDead();
            Debug.Log(HP);
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        trigercollider = null;
    }
    public void enemyDead()
    {
        if (isdead == true) return;
        if (HP <= 0)
        {
            Debug.Log("enemy dead");
            enemySpawner.deleteEnemy(this.gameObject);
            //Destroy(this.gameObject);
            isdead = true;
        }
    }
    public void enemyRestart()
    {
        HP = 5;
        isdead = false;
    }


    public void Atkshoot()
    {
        if (!isdead)
        {
            isshooted = false;
            animator.SetBool("isAtkMelee", false);
            animator.SetBool("isAtkShoot", true);
            Invoke("shooting", 2f);
        }
        CancelInvoke("Atkshoot");
    }

    public void shooting()
    {
        animator.SetBool("isAtkShoot", false);
        gameManager.shooting_Enemy(player.transform.position, "E_projectileGreen", this.gameObject, posStart);
        isshooted = true;
        CancelInvoke("shooting");
        
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
