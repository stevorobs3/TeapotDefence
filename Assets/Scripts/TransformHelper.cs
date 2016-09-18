using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

static class TransformHelper
{

    public static void LookAtTarget(Vector3 targetPosition, ref GameObject gameObject)
    {
        var dir = targetPosition - gameObject.transform.position;
        var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        gameObject.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

        var yScale = (targetPosition.x > gameObject.transform.position.x) ? 1 : -1;
        gameObject.transform.localScale = new Vector3(-1, yScale, 1);
    }
}