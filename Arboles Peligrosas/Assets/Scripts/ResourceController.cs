using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceController : MonoBehaviour {

    private float wood;
    private float stone;

    private List<GameObject> resources;
    public List<GameObject> treePrefab;
    public List<GameObject> stonePrefab;
    public Transform level_resources;

    // Use this for initialization
    void Start()
    {
        wood = 69;
        stone = 69;

        resources = new List<GameObject>();
        CreateTreeResource(120);
        CreateStoneResource(25);

        //HelperFunctions.bakeNavMeshes();
    }

    void CreateTreeResource(int amount)
    {
        for (int i = 0; i < amount; ++i)
        {
            int random = (int) UnityEngine.Random.Range(0, treePrefab.Count - 1);
            GameObject tree = Instantiate(
                treePrefab[random],
                level_resources
            );
            //tree.transform.position = new Vector3(Random.Range(-200, 200f), 0f, Random.Range(-200f, 200f));
            tree.transform.position = HelperFunctions.randomPosition(200f, 200f, 1.5f);
            Tree treeComp = tree.AddComponent<Tree>();

            float remaining = UnityEngine.Random.Range(5f, 20f);
            treeComp.setRemaining(remaining);
            float scale = ((remaining - 5f) / 15f) * 3f;
            tree.transform.localScale = tree.transform.localScale + new Vector3(scale, scale, scale);

            resources.Add(tree);
        }
    }

    void CreateStoneResource(int amount)
    {
        for (int i = 0; i < amount; ++i)
        {
            int random = (int)UnityEngine.Random.Range(0, stonePrefab.Count - 1);
            GameObject stone = Instantiate(
                stonePrefab[random],
                level_resources
            );
            //stone.transform.position = new Vector3(Random.Range(-200, 200f), 0f, Random.Range(-200f, 200f));
            stone.transform.position = HelperFunctions.randomPosition(200f, 200f, 1.5f);
            Stone stoneComp = stone.AddComponent<Stone>();

            float remaining = UnityEngine.Random.Range(5f, 15f);
            stoneComp.setRemaining(remaining);
            float scale = ((remaining - 5f) / 10f) * 100f;
            stone.transform.localScale = stone.transform.localScale + new Vector3(scale, scale, scale);

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
