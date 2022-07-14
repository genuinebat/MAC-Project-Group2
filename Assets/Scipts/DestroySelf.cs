using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySelf : MonoBehaviour
{
    public float delayTime;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(destroySelf(delayTime));
    }

    IEnumerator destroySelf(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}
