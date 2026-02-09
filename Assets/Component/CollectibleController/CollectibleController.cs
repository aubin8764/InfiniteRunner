using UnityEngine;

public class CollectibleController : MonoBehaviour
{
    private void Start()
    {
        Debug.Log($" CollectibleController actif sur {gameObject.name}");
    }
    private void OnTriggerEnter(Collider other)
    {
        PlayerCollisionController player = other.GetComponent<PlayerCollisionController>();
        Debug.Log($" Trigger détecté avec : {other.gameObject.name}");
        if (player != null)
        {
            Debug.Log("Joueur detecté par le cristal, destruction du cristal");
            // Détruit le cristal
            Destroy(gameObject);
        }
    }
}
