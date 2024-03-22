using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    public GameObject CarPefab;
    public Vector3 minPosition;
    public Vector3 maxPosition;
    [SerializeField]
    private Vector3 carSize = new Vector3(100f, 100f, 100f);
    // Start is called before the first frame update
    void Start()
    {
        Instantiate(CarPefab);
        Vector3 randomPosition = new Vector3(
        Random.Range(minPosition.x, maxPosition.x),
        Random.Range(minPosition.y, maxPosition.y),
        Random.Range(minPosition.z, maxPosition.z)
    );
        transform.position = randomPosition;
    }


}
