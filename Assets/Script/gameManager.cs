using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameManager : MonoBehaviour
{
    public Camera Maincamera;
    public GameObject player;
    [Range(0f,1f)] public float speedcamere;
    public ProjectileManager projectileManager;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if ((Vector2)player.transform.position != (Vector2)Maincamera.transform.position)
        {
            Vector3 v = new Vector3(player.transform.position.x * speedcamere 
                , player.transform.position.y * speedcamere , -10f);
            Maincamera.transform.position = v  ;
        }
    }

    public void shooting_Player(Vector3 pos, string prjtname)
    {
        projectileManager.sPawnPrjt_player(pos, prjtname);
    }   
    public void shooting_Enemy(Vector3 pos, string prjtname, GameObject obj, Transform posStart)
    {
        projectileManager.sPawnPrjt_enemy(pos, prjtname, obj, posStart);
    }

    public void destroyProjtofEnemy(GameObject projt, string objtag)
    {
        projectileManager.deletePrjt_Enemy(projt, objtag);
    }
    public void destroyProjtofPlayer(GameObject projt)
    {
        projectileManager.deletePrjt_Player(projt);
    }
 
}
