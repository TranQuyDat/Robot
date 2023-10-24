using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetDetector : Detector
{
    public float detectRadius = 6;
    public LayerMask obstacleLayer, TargetLayer;

    public bool showGizmos = true;


    public List<Transform> colliders;
    public override void Detect(AiData aiData)
    {
        Collider2D playerCollider = Physics2D.OverlapCircle(transform.position, detectRadius,TargetLayer);
        if (playerCollider != null)
        {
            Vector2 direction = (playerCollider.transform.position - transform.position).normalized;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, detectRadius, obstacleLayer);
            if (hit.collider != null && TargetLayer.value == (1<<hit.collider.gameObject.layer))
            {
                colliders = new List<Transform>() { playerCollider.transform };
            }
            else colliders = null;
        }
        else colliders = null;
        aiData.targets = colliders;
    }

    private void OnDrawGizmosSelected()
    {
        if (!showGizmos) return;
        Gizmos.DrawWireSphere(transform.position, detectRadius);
        if (colliders == null) return;
        
        Gizmos.color = Color.magenta;
        foreach(Transform tran in colliders)
        {
            Gizmos.DrawWireSphere(tran.position, 0.3f);
        }
        
    }
}
