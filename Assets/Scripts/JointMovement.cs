using UnityEngine;

public class JointMovement : MonoBehaviour 
{
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerMover _)) {
            collision.transform.SetParent(transform);
        }    
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out PlayerMover _)) {
            collision.transform.SetParent(null);
        }
    }
}