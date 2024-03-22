using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomPoistion : MonoBehaviour
{
    public Vector3 minPosition;
    public Vector3 maxPosition;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 randomPosition = new Vector3(
        Random.Range(minPosition.x, maxPosition.x),
        Random.Range(minPosition.y, maxPosition.y),
        Random.Range(minPosition.z, maxPosition.z)
    );
        transform.position = randomPosition;
    }

}
