using System.Collections;
using System.Collections.Specialized;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProjectileManager : MonoBehaviour
{
    public Transform posStart_player;
    public Transform posStart_enemy;
    public ListDictionary list_prjt = new ListDictionary();
    public List<GameObject> prjts;

    private void Start()
    {
        foreach (GameObject obj in prjts)
        {
            list_prjt.Add(obj.name, obj);
            Debug.Log(list_prjt["projectileBlue"]);
            
        }
    }
    private void Update()
    {
        
    }

    public void sPawnPrjt_player( Vector3 pos,string prjtname)
    {
        posStart_player.position = 
            new Vector3(posStart_player.position.x, posStart_player.position.y + 0.8f, 0);

        GameObject projt = Instantiate((GameObject)list_prjt[prjtname],
            posStart_player.position, Quaternion.identity,transform.parent);

        ProjectileController prjtCtl =  projt.GetComponent<ProjectileController>();
        prjtCtl.posEnd = pos;

    }

    public void sPawnPrjt_enemy(Vector3 pos , string prjtname)
    {
        GameObject projt = Instantiate((GameObject)list_prjt[prjtname],
            posStart_enemy.position, Quaternion.identity, transform.parent);

        ProjectileController prjtCtl = projt.GetComponent<ProjectileController>();
        pos.y = pos.y + 0.8f;
        prjtCtl.posEnd = pos;
    }

    

}
