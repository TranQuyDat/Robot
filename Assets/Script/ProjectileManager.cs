using System.Collections;
using System.Collections.Specialized;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class poolingProjt
{
    public List<GameObject> lisactive;
    public List<GameObject> lisNotactive;
    public Transform posStart;
    public GameObject obj = null;


    public poolingProjt()
    {
        lisactive = new List<GameObject>();
        lisNotactive = new List<GameObject>();
    }
    public void createPrjt(ListDictionary prjts , string prjtname,Vector2 pos)
    {
        GameObject projt =  GameObject.Instantiate((GameObject)prjts[prjtname],
            posStart.position, Quaternion.identity);

        ProjectileController prjtCtl = projt.GetComponent<ProjectileController>();
        prjtCtl.posEnd = pos;
        prjtCtl.parent = obj;
        lisactive.Add(projt);
    }

    public void getPrjt(ListDictionary prjts, string prjtname, Vector2 pos)
    {
        if (obj == null || pos == null) return;
        GameObject projt;
        if (lisNotactive == null || lisNotactive.Count<=0)
        {
            createPrjt(prjts, prjtname, pos);
            return;
            
        }

        projt = lisNotactive[0];
        projt.SetActive(true);
        projt.transform.position = posStart.position;
        ProjectileController prjtCtl = projt.GetComponent<ProjectileController>();
        prjtCtl.posEnd = pos;
        lisactive.Add(projt);
        lisNotactive.Remove(projt);

        

    }
    public void delete(GameObject projt)
    {
        lisNotactive.Add(projt);
        lisactive.Remove(projt);
        projt.SetActive(false);
    }
}
public class ProjectileManager : MonoBehaviour
{
    //public Transform posStart_player;
    //public Transform posStart_enemy;
    public ListDictionary P_listPrjt = new ListDictionary();
    public ListDictionary E_listPrjt = new ListDictionary();
    public List<GameObject> P_prjts;
    public List<GameObject> E_prjts;


    public List<poolingProjt> E_poolingProjts;
    public poolingProjt P_poolingProjt  ;

    private void Start()
    {
        E_poolingProjts = new List<poolingProjt>();
        initprjt(P_prjts, P_listPrjt);
        initprjt(E_prjts, E_listPrjt);
    }
    private void Update()
    {
        
        
    }

    public void initprjt(List<GameObject> prjts , ListDictionary dic)
    {
        foreach (GameObject obj in prjts)
        {
            dic.Add(obj.name, obj);
            Debug.Log(dic.Count);

        }
    }

    public void sPawnPrjt_player( Vector3 pos,string prjtname )
    {
        /*posStart_player.position = 
            new Vector3(posStart_player.position.x, posStart_player.position.y + 0.8f, 0);

        GameObject projt = Instantiate((GameObject)P_listPrjt[prjtname],
            posStart_player.position, Quaternion.identity,transform.parent);

        ProjectileController prjtCtl =  projt.GetComponent<ProjectileController>();
        prjtCtl.posEnd = pos;*/
        P_poolingProjt.posStart.position = 
            new Vector2(P_poolingProjt.posStart.position.x, 
            P_poolingProjt.posStart.position.y + 0.8f);

        P_poolingProjt.getPrjt(P_listPrjt, prjtname, pos);

    }

    public void sPawnPrjt_enemy(Vector3 pos, string prjtname, GameObject obj, Transform posStart)
    {
        /* GameObject projt = Instantiate((GameObject)E_listPrjt[prjtname],
             posStart_enemy.position, Quaternion.identity, transform.parent);

         ProjectileController prjtCtl = projt.GetComponent<ProjectileController>();
         pos.y = pos.y + 0.8f;
         prjtCtl.posEnd = pos;*/
        pos.y = pos.y + 0.8f;
        bool washasPool = false;
        poolingProjt pooling = new poolingProjt(); ;
        foreach (poolingProjt pool in E_poolingProjts)
        {
            if (E_poolingProjts == null || E_poolingProjts.Count <= 0) break;
            if (pool.obj.tag == obj.tag)
            {
                pooling = pool;
                washasPool = true;
                break;
            }
        }

        if (washasPool)
        {
            pooling.posStart = posStart;
            pooling.getPrjt(E_listPrjt, prjtname, pos);
            return;
        }
        E_poolingProjts.Add(pooling);
        pooling.obj = obj;
        pooling.posStart = posStart;
        pooling.getPrjt(E_listPrjt, prjtname, pos);
        
    }

    public void deletePrjt_Player(GameObject projt)
    {
        P_poolingProjt.delete(projt);
    }

    public void deletePrjt_Enemy(GameObject projt,GameObject obj)
    {
        foreach (poolingProjt pool in E_poolingProjts)
        {
            if (pool.obj.tag == obj.tag)
            {
                pool.delete(projt);
                return;
            }
        }
    }

}
