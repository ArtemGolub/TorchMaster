using UnityEngine;

public class WallCollision : MonoBehaviour
{
    private void Start()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, 0.01f);

        foreach (Collider collider in colliders)
        {
            if (collider.tag == "Wall")
            {
                Destroy(gameObject);
                return;
            }
        }
        GetComponent<Collider>().enabled = true;
    }
}
