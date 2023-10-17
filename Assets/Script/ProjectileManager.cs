using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProjectileManager : MonoBehaviour
{
    public GameObject prefap_prijectile_player;
    public Transform posStart;


    private void Update()
    {
        
    }

    public void sPawnPrjt( Vector3 pos)
    {
        GameObject projt = Instantiate(prefap_prijectile_player,
            posStart.position, Quaternion.identity,transform.parent);
        ProjectileController prjtCtl =  projt.GetComponent<ProjectileController>();
        prjtCtl.posEnd = pos;

    }

 

    

}
