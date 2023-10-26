using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SeekBehaviour : SteeringBehaviour
{
    public float targetThreshold = 0.2f;
    public bool showGizmos = true;
    bool reachedLastTarget = true;
    Vector2 targetPositionCached;
    float[] interestsTemp;
    public override (float[] danger, float[] interest) GetSteering(float[] danger, float[] interest, AiData aiData)
    {
        if (reachedLastTarget == true)
        {
            if (aiData.targets == null || aiData.targets.Count <= 0)
            {
                aiData.currentTarget = null;
                return (danger, interest);
            }
            else
            {
                reachedLastTarget = false;
                aiData.currentTarget = aiData.targets.OrderBy(target =>
                Vector2.Distance(target.position, transform.position)).FirstOrDefault();
                Vector2 v = aiData.currentTarget.position;
                //aiData.currentTarget.position = new(v.x, v.y + 0.4f);
            }
        }

        if(aiData.currentTarget != null && aiData.targets != null 
            && aiData.targets.Contains(aiData.currentTarget))
        {
            targetPositionCached = aiData.currentTarget.position; 
        }

        if(Vector2.Distance(targetPositionCached,transform.position) < targetThreshold)
        {
            reachedLastTarget = true;
            aiData.currentTarget = null;
            return (danger, interest);
        }

        Vector2 directionTotarget = (targetPositionCached - (Vector2)transform.position).normalized;
        for(int i = 0; i < interest.Length; i++)
        {
            float result = Vector2.Dot(directionTotarget, Directions.eightDirection[i]);
            if(result > 0)
            {
                float ValueToPutIn = result;
                if(ValueToPutIn > interest[i])
                {
                    interest[i] = ValueToPutIn;
                }
            }
        }
        interestsTemp = interest;
        return (danger, interest);
    }

    private void OnDrawGizmos()
    {
        if (!showGizmos) return;
        Gizmos.DrawSphere(targetPositionCached, 0.2f);
        Gizmos.DrawWireSphere(transform.position, targetThreshold);
        if(Application.isPlaying && interestsTemp != null)
        {
            Gizmos.color = Color.green;
            for(int i = 0; i < interestsTemp.Length; i++)
            {
                Gizmos.DrawRay(transform.position, Directions.eightDirection[i]*interestsTemp[i]);
            }
            if (reachedLastTarget == false)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawSphere(transform.position, 0.1f);
            }
        }
    }
}
