using System.Collections;
using Mirror;
using UnityEngine;

public class Bullet : NetworkBehaviour
{
    [SerializeField] private float _impulse = 100f;
    private Rigidbody _rigidbody;

    public override void OnStartClient()
    {
        _rigidbody = GetComponent<Rigidbody>();
        if (isServer)
        {
            _rigidbody.AddForce(transform.forward * _impulse, ForceMode.Impulse);
            StartCoroutine(DestroyAfterEndLifeTime());
        }
        else
        {
            _rigidbody.detectCollisions = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerHealth>(out PlayerHealth playerHealth))
        {
            playerHealth.TakeDamage(10f);
        }
        NetworkServer.Destroy(gameObject);
    }

    private IEnumerator DestroyAfterEndLifeTime()
    {
        yield return new WaitForSeconds(1f);
        NetworkServer.Destroy(gameObject);
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }
}
