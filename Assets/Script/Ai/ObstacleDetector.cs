using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ObstacleDetector : Detector
{
    public float detectRadius = 2;
    public LayerMask layerMask;

    public bool showGizmos = true;

    Collider2D[] colliders;
    public override void Detect(AiData aiData)
    {
        colliders = Physics2D.OverlapCircleAll(transform.position, detectRadius, layerMask);
        aiData.obstacles = colliders;
    }

    private void OnDrawGizmos()
    {
        if (!showGizmos) return;
        if(Application.isPlaying && colliders != null)
        {
            Gizmos.color = Color.red;
            foreach (Collider2D collider in colliders) 
            {
                Gizmos.DrawSphere(collider.transform.position,0.2f);
            }
        }
    }
}
