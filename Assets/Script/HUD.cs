using UnityEngine;
using Mirror;
using TMPro;

public class HUD : NetworkBehaviour
{
    [SerializeField] private TMP_Text _healthText;
    private PlayerHealth _playerHealth;

    public override void OnStartClient()
    {
        if (isLocalPlayer)
        {
            _playerHealth = GetComponentInParent<PlayerHealth>();
            _playerHealth.OnChangeHealth += OnChangeHealthHandler;
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    private void OnDestroy()
    {
        if (_playerHealth != null)
        {
            _playerHealth.OnChangeHealth -= OnChangeHealthHandler;
        }
    }

    private void OnChangeHealthHandler(float playerHealth)
    {
        _healthText.text = $"{playerHealth} / 100";
    }
}
