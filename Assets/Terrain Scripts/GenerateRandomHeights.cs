using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TerrainTextureData
{
    public Texture2D terrainTexture;
    public Vector2 tileSize;
}

[System.Serializable]
public class TreeData
{
    public GameObject treeMesh;
    public float minHeight;
    public float maxHeight;
}

public class GenerateRandomHeights : MonoBehaviour
{
    private Terrain terrain;
    private TerrainData terrainData;

    [SerializeField]
    [Range(0f, 1f)]
    private float minRandomHeightRange = 0f;

    [SerializeField]
    [Range(0f, 1f)]
    private float maxRandomHeightRange = 0.1f;

    [SerializeField] private bool flattenTerrain = true;
    [Header("Perlin")]

    [SerializeField] private bool perlinNoise = false;

    [SerializeField] private float perlinNoiseWidthScale = 0.01f;
    [SerializeField] private float perlinNoiseHeightScale = 0.01f;

    [Header("Texture Data")]
    [SerializeField] private List<TerrainTextureData> terrainTextureData;

    [SerializeField] private bool addTerrainTexture = false;

    [Header("Tree Data")]
    [SerializeField] private List<TreeData> treeData;

    [SerializeField] private int maxTree = 2000;

    [SerializeField] private int treeSpacing = 10;

    [SerializeField] private bool addTrees = false;
    [SerializeField] private int terrainLayersIndex;


    [Header("Water")]
    [SerializeField] private GameObject water;
    [SerializeField] private float waterHeight = 0.3f;



    // Start is called before the first frame update
    void Start()
    {
        if (terrain == null)
        {
            terrain = this.GetComponent<Terrain>();
        }
        if (terrainData == null)
        {
            terrainData = Terrain.activeTerrain.terrainData;
        }
        GenerateHeights();
        AddTerrainTexture();
        AddTrees();
        AddWater();



    }



    private void GenerateHeights()
    {
        float[,] heightMap = new float[terrainData.heightmapResolution, terrainData.heightmapResolution];

        for (int width = 0; width < terrainData.heightmapResolution; width++)
        {
            for (int height = 0; height < terrainData.heightmapResolution; height++)
            {   /*
                if (perlinNoise)
                {
                    heightMap[width, height] = Mathf.PerlinNoise(width * perlinNoiseWidthScale, height * perlinNoiseHeightScale);
                }
                else
                {
                    heightMap[width, height] = Random.Range(minRandomHeightRange, maxRandomHeightRange);
                }
                */
                heightMap[width, height] = Random.Range(minRandomHeightRange, maxRandomHeightRange);
                heightMap[width, height] += Mathf.PerlinNoise(width * perlinNoiseWidthScale, height * perlinNoiseHeightScale);
            }
        }

        terrainData.SetHeights(0, 0, heightMap);
    }

    private void FlattenTerrain()
    {
        float[,] heightMap = new float[terrainData.heightmapResolution, terrainData.heightmapResolution];

        for (int width = 0; width < terrainData.heightmapResolution; width++)
        {
            for (int height = 0; height < terrainData.heightmapResolution; height++)
            {
                heightMap[width, height] = 0;
            }
        }

        terrainData.SetHeights(0, 0, heightMap);
    }

    private void AddTerrainTexture()
    {
        TerrainLayer[] terrainLayers = new TerrainLayer[terrainTextureData.Count];

        for (int i = 0; i < terrainTextureData.Count; i++)
        {
            if (addTerrainTexture)
            {
                terrainLayers[i] = new TerrainLayer();
                terrainLayers[i].diffuseTexture = terrainTextureData[i].terrainTexture;
                terrainLayers[i].tileSize = terrainTextureData[i].tileSize;
            }
            else
            {
                terrainLayers[i] = new TerrainLayer();
                terrainLayers[i].diffuseTexture = null;
            }
        }
        terrainData.terrainLayers = terrainLayers;
    }

    private void AddTrees()
    {
        TreePrototype[] trees = new TreePrototype[treeData.Count];

        for (int i = 0; i < treeData.Count; i++)
        {
            trees[i] = new TreePrototype();
            trees[i].prefab = treeData[i].treeMesh;
        }

        terrainData.treePrototypes = trees;
        List<TreeInstance> treeInstancesList = new List<TreeInstance>();

        if (addTrees)
        {
            for (int z = 0; z < terrainData.size.z; z += treeSpacing)
            {
                for (int x = 0; x < terrainData.size.x; x += treeSpacing)
                {
                    for (int treeIndex = 0; treeIndex < trees.Length; treeIndex++)
                    {
                        if (treeInstancesList.Count < maxTree)
                        {
                            float currentHeight = terrainData.GetHeight(x, z) / terrainData.size.y;

                            if (currentHeight >= treeData[treeIndex].minHeight && currentHeight <= treeData[treeIndex].maxHeight)
                            {
                                float randomX = (x + Random.Range(-5.0f, 5.0f)) / terrainData.size.x;
                                float randomZ = (z + Random.Range(-5.0f, 5.0f)) / terrainData.size.z;

                                Vector3 treePosition = new Vector3(randomX * terrainData.size.x,
                                                                        currentHeight * terrainData.size.y,
                                                                        randomZ * terrainData.size.z) + this.transform.position;

                                RaycastHit raycastHit;
                                int layerMask = 1 << terrainLayersIndex;
                                if (Physics.Raycast(treePosition, -Vector3.up, out raycastHit, 100, layerMask) ||
                                    Physics.Raycast(treePosition, Vector3.up, out raycastHit, 100, layerMask))
                                {
                                    float treeDistance = (raycastHit.point.y - this.transform.position.y) / terrainData.size.y;

                                    TreeInstance treeInstance = new TreeInstance();

                                    treeInstance.position = new Vector3(randomX, treeDistance, randomZ);
                                    treeInstance.rotation = Random.Range(0, 360);
                                    treeInstance.prototypeIndex = treeIndex;
                                    treeInstance.color = Color.white;
                                    treeInstance.lightmapColor = Color.white;
                                    treeInstance.heightScale = 0.95f;
                                    treeInstance.widthScale = 0.95f;

                                    treeInstancesList.Add(treeInstance);
                                }


                            }
                        }
                    }
                }
            }
        }
        terrainData.treeInstances = treeInstancesList.ToArray();
    }

    private void AddWater()
    {
        GameObject waterGameObject = Instantiate(water, this.transform.position, this.transform.rotation);
        waterGameObject.name = "Water";
        waterGameObject.transform.position = this.transform.position + new Vector3(terrainData.size.x / 2, waterHeight * terrainData.size.y,
        terrainData.size.z / 2);
        waterGameObject.transform.localScale = new Vector3(terrainData.size.z, 1, terrainData.size.z);
    }


    private void OnDestroy()
    {
        if (flattenTerrain)
        {
            FlattenTerrain();
        }
    }
}
