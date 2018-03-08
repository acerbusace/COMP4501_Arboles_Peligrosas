using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceController : MonoBehaviour {

    private float wood;
    private float stone;

    public Text WoodText;
    public Text StoneText;

    private List<GameObject> resources;
    public GameObject treePrefab;
    public GameObject stonePrefab;

    // Use this for initialization
    void Start()
    {
        wood = 69;
        stone = 69;

        resources = new List<GameObject>();
        CreateTreeResource(20);
        CreateStoneResource(15);
    }

    void CreateTreeResource(int amount)
    {
        for (int i = 0; i < amount; ++i)
        {
            GameObject tree = Instantiate(
                treePrefab, 
                new Vector3(Random.Range(-100f, 100f), 0f, Random.Range(-100f, 100f)), 
                Quaternion.identity
            );
            tree.AddComponent<Tree>();

            resources.Add(tree);
        }
    }

    void CreateStoneResource(int amount)
    {
        for (int i = 0; i < amount; ++i)
        {
            GameObject stone = Instantiate(
                stonePrefab,
                new Vector3(Random.Range(-100f, 100f), 0f, Random.Range(-100f, 100f)),
                Quaternion.identity
            );
            stone.AddComponent<Stone>();

            resources.Add(stone);
        }
    }

    // Update is called once per frame
    void Update()
    {
        WoodText.text = "Wood: " + ((int)wood).ToString();
        StoneText.text = "Stone: " + ((int)stone).ToString();
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

    public void useWood(float amount)
    {
        wood -= amount;
    }

    public void useStone(float amount)
    {
        stone -= amount;
    }
}
