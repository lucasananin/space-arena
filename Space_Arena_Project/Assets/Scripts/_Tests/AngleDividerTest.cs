using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AngleDividerTest : MonoBehaviour
{
    public Transform target;
    public float divisionFactor = 0.5f; // Value between 0 and 1 indicating the division point
    [Space]
    [SerializeField] protected float _maxShootAngle = 10f;
    [SerializeField] protected int _projectilesPerShot = 3;

    private void Update()
    {
        var _a = _maxShootAngle * 2f;
        var _b = _a / (_projectilesPerShot - 1);

        for (int j = 0; j < _projectilesPerShot; j++)
        {
            Debug.Log($"// _ = {-_maxShootAngle + _b * j}");
        }

        return;

        if (target != null)
        {
            // Get the current angle towards the target
            Vector3 targetDir = target.position - transform.position;
            float angle = Mathf.Atan2(targetDir.y, targetDir.x) * Mathf.Rad2Deg;

            // Normalize the angle to be between 0 and 360
            if (angle < 0)
                angle += 360f;

            // Calculate the angle to divide
            float divisionAngle = Mathf.Lerp(0f, 360f, divisionFactor);

            // Calculate the angle between the current angle and the division angle
            float angleDifference = Mathf.DeltaAngle(angle, divisionAngle);

            // Determine the divided angle
            float dividedAngle = angle + angleDifference;

            Debug.Log("Divided Angle: " + dividedAngle);
        }
    }
}
