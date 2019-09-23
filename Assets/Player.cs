using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public enum PlayerState { Standing, Walking, ShootingGun }

    SpriteRenderer _spriteRenderer;
    bool upgradedWeapon = false;

    public Sprite Standing;
    public Sprite Walking;
    public Sprite ShootingGun;
    public Sprite ShootingMachineGun;

    public PlayerState State = PlayerState.Standing;
    public bool UpgradedWeapon { get { return upgradedWeapon; } }
    public float FiringRate = 4;

    // Start is called before the first frame update
    void Start()
    {
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        switch (State)
        {
            case PlayerState.Standing:
                _spriteRenderer.sprite = Standing;
                break;
            case PlayerState.Walking:
                _spriteRenderer.sprite = Walking;
                break;
            case PlayerState.ShootingGun:
                if (UpgradedWeapon)
                {
                    _spriteRenderer.sprite = ShootingMachineGun;
                    FiringRate = 20;
                }
                else
                {
                    _spriteRenderer.sprite = ShootingGun;
                    FiringRate = 8;
                }
                break;
        }
    }

    public void UpgradeWeapon(float duration)
    {
        Debug.Log($"Weapone Upgraded");
        upgradedWeapon = true;
        StartCoroutine(Downgrade(duration));
    }

    IEnumerator Downgrade(float duration)
    {
        yield return new WaitForSeconds(duration);
        Debug.Log($"Weapone Downgraded");
        upgradedWeapon = false;
        yield break;
    }

    public void RespwanCrate(GameObject crate)
    {
        StartCoroutine(_respawnCrate(crate));
    }

    IEnumerator _respawnCrate(GameObject crate)
    {
        yield return new WaitForSeconds(60);
        Debug.Log($"Crate Respawned");
        crate.SetActive(true);
        yield break;
    }
}
