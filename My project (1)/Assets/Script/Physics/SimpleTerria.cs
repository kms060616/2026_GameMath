
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class SimpleTerria : MonoBehaviour
{
    public int width = 30;
    public int depth = 30;
    public float scale = 0.1f;
    public float heightMultiplier = 8f;
    public GameObject cubePrefabs;

    public GameObject blockWater;

    public GameObject blockGrass;

    public int WaterLevel = 5;

    int XOffset = 0;
    int ZOffset = 0;

    SimpleNoise simpleNoise;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        simpleNoise = GetComponent<SimpleNoise>();
        XOffset = Random.Range(-9999, 9999);
        ZOffset = Random.Range(-9999, 9999);
        Generate();
    }

    public void Generate()
    {
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < depth; z++)
            {
                float xCoord = (x + XOffset) * scale;
                float zCoord = (z + ZOffset) * scale;

                float noise = simpleNoise.Noise(xCoord, zCoord);

                int height = Mathf.RoundToInt(noise * heightMultiplier);
                if (height <= 0) continue;

                for (int y = 0; y <= height; y++)
                {
                    if (y == height)
                        GrassCube(x, z, y);
                    else
                        CreateCube(x, z, y);
                }

                for (int y2 = height + 1; y2 < WaterLevel; y2++)
                {
                    WaterCube(x, z, y2);
                }
            }
            
        }
    }

    void CreateCube(int x, int z, int height)
    {
        for (int y = 0; y <= height; y++)
        {
            Vector3 position = new Vector3(x, y, z);
            Instantiate(cubePrefabs, position, Quaternion.identity, transform);
        }
    }

    void GrassCube(int x, int z, int height)
    {
        for (int y = 0; y <= height; y++)
        {
            Vector3 position = new Vector3(x, y, z);
            Instantiate(blockGrass, position, Quaternion.identity, transform);
        }
    }

    void WaterCube(int x, int z, int height)
    {
        for (int y = 0; y <= height; y++)
        {
            Vector3 position = new Vector3(x, y, z);
            Instantiate(blockWater, position, Quaternion.identity, transform);
        }
    }
}
