using UnityEngine;

public abstract class CollectibleBase : MonoBehaviour
{
    [SerializeField] protected AudioSource _sound;
    private bool _collected = false;

    public void Collect()// Appelé par le joueur lors de la collecte
    {
        if (_collected)
            return;
        Debug.Log($"Collectible {gameObject.name} collected.");
        _collected = true;

        // Effet spécifique du collectible
        OnCollect();

        // Son
        if (_sound != null)
            AudioSource.PlayClipAtPoint(_sound.clip, transform.position);

        // Désactivation immédiate pour éviter les doubles hits
        gameObject.SetActive(false);

        // Destruction après le son
        Destroy(gameObject, _sound != null ? _sound.clip.length : 0.1f);
    }

    // Méthode que les enfants doivent implémenter
    protected abstract void OnCollect();

}