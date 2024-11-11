using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableOnPlayerDeath : MonoBehaviour
{
    private void OnEnable()
    {
        PlayerHealth.OnPlayerDead += PlayerHealth_OnPlayerDead;
    }

    private void OnDisable()
    {
        PlayerHealth.OnPlayerDead -= PlayerHealth_OnPlayerDead;
    }

    private void PlayerHealth_OnPlayerDead(PlayerHealth obj)
    {
        gameObject.SetActive(false);
    }
}
