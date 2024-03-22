using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindScript : MonoBehaviour
{
    public float windIntensity = 3.0f;
    public float swayFrequency = 1.0f;
    public Vector3 direction = new Vector3(-10, 301, 10);
    private float speed;
    private Vector3 intialEulerAngles;
    // Start is called before the first frame update
    void Start()
    {
        speed = Random.Range(2f, 2f);
        intialEulerAngles = transform.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        float rotateFactor = Mathf.Sin(Time.time * speed * swayFrequency) * windIntensity;
        Vector3 rot = new Vector3(rotateFactor, intialEulerAngles.y, rotateFactor);
        transform.eulerAngles = rot;
        Debug.Log("Wind");
    }
}
