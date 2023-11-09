using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class poolingEnemy
{
    public List<GameObject> lisactive;
    public List<GameObject> lisNotactive;
    public string tag;

    public poolingEnemy()
    {
        lisactive = new List<GameObject>();
        lisNotactive = new List<GameObject>();
    }
    public void spawnEnemy(GameObject prefap, List<Transform> posSpawn)
    {
        GameObject obj;
        int index = Random.Range(0, posSpawn.Count);
        obj = GameObject.Instantiate(prefap, posSpawn[index].position,Quaternion.identity);
        tag = obj.tag;
        lisactive.Add(obj);
    }
    public void pooling(GameObject prefap ,List<Transform> posSpawn)
    {
        GameObject obj  ;
        if(lisNotactive == null || lisNotactive.Count <= 0)
        {
            spawnEnemy(prefap, posSpawn);
            return;
        }
        obj = lisNotactive[0];
        enemyController objctl = obj.GetComponent<enemyController>();
        objctl.transform.position = posSpawn[Random.Range(0,posSpawn.Count)].position;
        obj.SetActive(true);
        lisactive.Add(obj);
        lisNotactive.Remove(obj);
    }
    public void delete(GameObject obj)
    {
        lisactive.Remove(obj);
        lisNotactive.Add(obj);
        obj.SetActive(false);
    }
}
public class EnemySpawner : MonoBehaviour
{
    public List<GameObject> prefapEnemy;
    public List<Transform> posSpawn ;
    public float timedelay;
    public int countEnemy;
    public List<poolingEnemy> poolingenemy;
    void Start()
    {
        poolingenemy = new List<poolingEnemy>();
        if (countEnemy > 1)
        {
            InvokeRepeating("spawnEnemy", timedelay, timedelay);
        }
    }
    
    public void createEnemy()
    {
        foreach (GameObject obj in prefapEnemy)
        {
            foreach (poolingEnemy pool in poolingenemy)
            {
                if (poolingenemy == null || poolingenemy.Count <= 0) break;
           
                if (pool.tag != null && pool.tag == obj.tag)
                {
                    pool.pooling(obj,posSpawn);
                    return;
                }
           
            }
            poolingEnemy newpooling = new poolingEnemy();
            newpooling.pooling(obj, posSpawn);
            poolingenemy.Add(newpooling);
        }
    }

    public void spawnEnemy()
    {
        if (countEnemy < 1)  return;
        createEnemy();
        countEnemy = countEnemy - 1;
    }

    public void deleteEnemy(GameObject obj)
    {
        foreach(poolingEnemy pool in poolingenemy)
        {
            if (poolingenemy == null || poolingenemy.Count <= 0) break;
            if(pool.tag != null && pool.tag == obj.tag)
            {
                pool.delete(obj);
                return;
            }
        }
    }
   
}
