using System;
using Mirror;
using UnityEngine;

public class PlayerHealth : NetworkBehaviour
{
    public Action<float> OnChangeHealth;
 
    [SyncVar(hook = nameof(ChangeHealthHook))]
    [SerializeField] private float _health = 100f;
 
    private void ChangeHealthHook(float oldHealth, float newHealth)
    {
        OnChangeHealth?.Invoke(newHealth);
    }
 
    [Server]
    public void TakeDamage(float damage)
    {
        _health = Mathf.Max(_health - damage, 0f);
        if (_health == 0f)
        {
            NetworkServer.Destroy(gameObject);
        }
    }
}