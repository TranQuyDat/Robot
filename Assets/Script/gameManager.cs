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

    public void shooting_Player(Vector3 pos, string prjtname)
    {
        projectileManager.sPawnPrjt_player(pos, prjtname);
    }   
    public void shooting_Enemy(Vector3 pos, string prjtname, GameObject obj, Transform posStart)
    {
        projectileManager.sPawnPrjt_enemy(pos, prjtname, obj, posStart);
    }

    public void destroyProjtofEnemy(GameObject projt, GameObject obj)
    {
        projectileManager.deletePrjt_Enemy(projt, obj);
    }
    public void destroyProjtofPlayer(GameObject projt)
    {
        projectileManager.deletePrjt_Player(projt);
    }
 
}
