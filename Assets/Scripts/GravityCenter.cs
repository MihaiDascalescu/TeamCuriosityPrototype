using UnityEngine;

public class GravityCenter : MonoBehaviour
{
    [SerializeField] private Transform targetTransform;
    private Rigidbody2D targetRigidBody2d;

    [SerializeField] private float influenceRange;
    [SerializeField] private float intensity;
    
    private float distanceToTarget;

    private Vector2 pullForce;

    private void Start()
    {
        targetRigidBody2d = targetTransform.GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        distanceToTarget = Vector2.Distance(targetTransform.position, transform.position);
        if (distanceToTarget <= influenceRange)
        {
            pullForce = (transform.position - targetTransform.position).normalized / distanceToTarget * intensity;
            targetRigidBody2d.AddForce(pullForce, ForceMode2D.Force);
        }
    }
}
