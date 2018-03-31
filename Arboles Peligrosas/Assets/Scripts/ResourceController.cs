using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceController : MonoBehaviour {

    private float wood;
    private float stone;

    private List<GameObject> resources;
    public GameObject treePrefab;
    public GameObject stonePrefab;

    // Use this for initialization
    void Start()
    {
        wood = 69;
        stone = 69;

        resources = new List<GameObject>();
        CreateTreeResource(40);
        CreateStoneResource(25);
    }

    void CreateTreeResource(int amount)
    {
        for (int i = 0; i < amount; ++i)
        {
            GameObject tree = Instantiate(
                treePrefab, 
                new Vector3(Random.Range(-200, 200f), 0f, Random.Range(-200f, 200f)), 
                Quaternion.identity
            );
            Tree treeComp = tree.AddComponent<Tree>();

            float remaining = UnityEngine.Random.Range(5f, 20f);
            treeComp.setRemaining(remaining);
            float scale = 1f + (remaining / 20f) * 3f;
            tree.transform.localScale = new Vector3(scale, scale, scale);

            resources.Add(tree);
        }
    }

    void CreateStoneResource(int amount)
    {
        for (int i = 0; i < amount; ++i)
        {
            GameObject stone = Instantiate(
                stonePrefab,
                new Vector3(Random.Range(-200, 200f), 0f, Random.Range(-200f, 200f)), 
                Quaternion.identity
            );
            stone.AddComponent<Stone>();

            resources.Add(stone);
        }
    }

    public float getWood()
    {
        return wood;
    }

    public float getStone()
    {
        return stone;
    }

    public float addWood(float amount)
    {
        return wood += amount;
    }

    public float addStone(float amount)
    {
        return stone += amount;
    }

    public bool buildingRobot()
    {
        if (wood > 10f)
        {
            stone -= 5f;
            return true;
        }
        return false;
    }

    public bool buildingWall()
    {
        if (stone > 5f)
        {
            wood -= 8f;
            return true;
        }
        return false;
    }

    public bool buildingTurret()
    {
        if (wood > 10f && stone > 10f)
        {
            wood -= 10f;
            stone -= 7f;
            return true;
        }
        return false;
    }
}
