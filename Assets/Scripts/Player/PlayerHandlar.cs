using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHandlar : MonoBehaviour
{
    public void SetParent(Transform newParent)
    {
        transform.parent = newParent;
        //transform.localScale = origiParent.localScale;
    }
}
