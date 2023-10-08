using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Fusion;

public class RotationEqualizer : NetworkBehaviour
{
    public void OriginalRotation()
    {
        transform.rotation = GetComponentInParent<PlayerModel>().transform.rotation;
    }
}
