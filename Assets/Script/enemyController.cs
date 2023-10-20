using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyController : MonoBehaviour
{
    public GameObject player;
    public float distance;
    public float speed;

    private void Update()
    {
        Aienemy();
    }
    public void Aienemy()
    {
        float dis = Vector2.Distance(this.transform.position, player.transform.position);

        if (dis > distance) return;
        transform.position = Vector2.MoveTowards(transform.position,
            player.transform.position, speed * Time.deltaTime);

    }
}
