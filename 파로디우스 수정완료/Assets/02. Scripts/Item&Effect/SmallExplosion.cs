using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmallExplosion : MonoBehaviour
{
    private void Update()
    {
        
        Destroy(this.gameObject, 2f);
    }
}
