
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public float radius = 5f;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}
