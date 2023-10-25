using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
      float timeLife;
    public float speed;
    [HideInInspector]
    public Vector3 posEnd;
    public Rigidbody2D rb;
    Vector3 shootDirection;
    gameManager gameManager;
    [HideInInspector]
    public GameObject parent;
    void Start()
    {
        gameManager = GameObject.FindAnyObjectByType<gameManager>().GetComponent<gameManager>();
        StartCoroutine(timelife());
        shootDirection = (posEnd - transform.position).normalized;
    }

    private void Update()
    {
        tranForm_prjt();
        //Debug.Log("direction : "+  shootDirection);
    }

    private void tranForm_prjt()
    {
        transform.position = transform.position + shootDirection * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (this.gameObject.CompareTag("bullet_player") &&
            (collision.CompareTag("wall")|| collision.CompareTag("enemy"))
            )
        {
            gameManager.destroyProjtofPlayer(this.gameObject);
        }
        
        if (this.gameObject.CompareTag("bullet_enemy") &&
            (collision.CompareTag("wall")||collision.CompareTag("Player"))
            )
        {
            gameManager.destroyProjtofEnemy(this.gameObject, parent);
        }
    }
    public IEnumerator timelife()
    {
        timeLife = Random.Range(3f, 7f);
        yield return new WaitForSeconds(timeLife);
        Debug.Log("this ok;");
        if (this.gameObject.CompareTag("bullet_player"))
        {
            gameManager.destroyProjtofPlayer(this.gameObject);
            yield return 0;
        }
        gameManager.destroyProjtofEnemy(this.gameObject, parent);
    }
}
