using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkyBoxScript : MonoBehaviour
{
    public Material material;
    public static bool fog;
    public static float fogDensity;
    // Start is called before the first frame update
    void Start()
    {
        RenderSettings.skybox = material;
        Debug.Log("SkyBox");

        RenderSettings.fogColor = Color.white;
        RenderSettings.fogDensity = 0.001f;
        RenderSettings.fog = true;
        Debug.Log("Fog");
    }

    // Update is called once per frame
    void Update()
    {

    }
}
