using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManager : MonoBehaviour
{
    public ProjectileManager projectileManager;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void shooting_player(Vector3 pos)
    {
        projectileManager.sPawnPrjt(pos);
    }
}
