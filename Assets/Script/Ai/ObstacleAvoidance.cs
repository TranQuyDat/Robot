using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleAvoidance : SteeringBehaviour
{
    public float radius  = 2f, agentColliderSize = 0.6f;
    float[] dangerResultTemp = null;
    public bool showGizmos = true;

    public override (float[] danger, float[] interest) GetSteering(float[] danger, float[] interest, AiData aiData)
    {
        foreach(Collider2D obs in aiData.obstacles)
        {
            Vector2 directionToObstacle = 
                obs.ClosestPoint(transform.position) - (Vector2)transform.position;
            float distanceToObstacle = directionToObstacle.magnitude;

            float weight = distanceToObstacle < agentColliderSize ? 1 :
                (radius - distanceToObstacle) / radius;

            Vector2 directionToObstacleNormalized = directionToObstacle.normalized;
            for (int i = 0; i < Directions.eightDirection.Count; i++)
            {
                float result = 
                    Vector2.Dot(directionToObstacleNormalized, Directions.eightDirection[i]);
                if(result > danger[i])
                {
                    danger[i] = result;
                }
            }
        }
        dangerResultTemp = danger;
        return (danger, interest);
    }

    public void OnDrawGizmos()
    {
        if (!showGizmos) return;

        if (Application.isPlaying && dangerResultTemp != null)
        {
            Gizmos.color = Color.red;
            for (int i = 0; i < dangerResultTemp.Length; i++)
            {
                Gizmos.DrawRay(transform.position,
                    dangerResultTemp[i] * Directions.eightDirection[i]);
            }
        }

        else
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawSphere(transform.position, radius);
        }
    }
}
static class Directions
{
    public static List<Vector2> eightDirection = new List<Vector2>()
    {
        new Vector2(0,1).normalized,
        new Vector2(0,-1).normalized,
        new Vector2(1,0).normalized,
        new Vector2(-1,0).normalized,
        new Vector2(1,1).normalized,
        new Vector2(1,-1).normalized,
        new Vector2(-1,1).normalized,
        new Vector2(-1,-1).normalized,
    };
} 