using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContextSolver : MonoBehaviour
{
    public bool showgizmos;

    float[] interestgizmos;
    Vector2 resultDirection = Vector2.zero;
    float rayLeng = 1;

    private void Start()
    {
        interestgizmos = new float[8];
    }

    public Vector2 GetDirectionToMove(List<SteeringBehaviour> behaviours,AiData aidata)
    {
        float[] danger = new float[8];
        float[] interest = new float[8];
        foreach(SteeringBehaviour behaviour in behaviours)
        {
            (danger, interest) = behaviour.GetSteering(danger, interest, aidata);
        }

        for(int i = 0; i < 8; i++)
        {
            interest[i] = Mathf.Clamp01(interest[i] - danger[i]);
        }
        interestgizmos = interest;
        Vector2 outPutDirection = Vector2.zero;
        for(int i = 0;i<8; i++)
        {
            outPutDirection += Directions.eightDirection[i] * interest[i];
        }
        outPutDirection = outPutDirection.normalized;
        resultDirection = outPutDirection;
        return resultDirection;
    }

    private void OnDrawGizmos()
    {
        if (showgizmos == false || Application.isPlaying == false) return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position, resultDirection);
    }

}

