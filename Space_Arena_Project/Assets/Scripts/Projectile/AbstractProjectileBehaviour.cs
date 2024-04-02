using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractProjectileBehaviour : MonoBehaviour
{
    public abstract void Init(AbstractWeaponBehaviour _abstractWeapon);
}
