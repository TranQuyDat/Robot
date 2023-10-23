using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyController : MonoBehaviour
{
    public GameObject player;
    public float distance;
    public float speed;
    public List<SteeringBehaviour> steeringBehaviours;
    public ContextSolver contexsolver;
    public List<Detector> detectors;
    public AiData aiData;
    public float timedelay  = 0.05f;

    private void Start()
    {
        InvokeRepeating("performdetection",0, timedelay);
    }
    private void Update()
    {
        Aienemy();
    }

    private void performdetection()
    {
        foreach(Detector detector in detectors)
        {
            detector.Detect(aiData);
        }

       
    }
    public void Aienemy()
    {
        float dis = Vector2.Distance(this.transform.position, player.transform.position);

        if (dis > distance) return;
        transform.Translate(contexsolver.GetDirectionToMove(steeringBehaviours, aiData)*speed*Time.deltaTime);

    }
}
