using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HoloCube : MonoBehaviour, ICollectible
{
    public void OnCollect()
    {
        gameObject.SetActive(false);
    }
}
