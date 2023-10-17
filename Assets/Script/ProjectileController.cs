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
    void Start()
    {
        StartCoroutine(timelife());
        shootDirection = (posEnd - transform.position).normalized;
    }

    private void Update()
    {
        tranForm_prjt();
        Debug.Log("direction : "+  shootDirection);
    }

    private void tranForm_prjt()
    {
        transform.position = transform.position + shootDirection * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("wall"))
        {
            Destroy(this.gameObject);
        }
    }
    public IEnumerator timelife()
    {
        timeLife = Random.Range(3f, 7f);
        yield return new WaitForSeconds(timeLife);
        Debug.Log("this ok;");
        Destroy(this.gameObject);
    }
}
