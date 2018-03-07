using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceController : MonoBehaviour {

    private int wood;
    private int stone;

    public Text WoodText;
    public Text StoneText;

    // Use this for initialization
    void Start()
    {
        wood = 69;
        stone = 69;
    }

    // Update is called once per frame
    void Update()
    {
        WoodText.text = "Wood: " + wood.ToString();
        StoneText.text = "Stone: " + stone.ToString();
    }

    public int getWood()
    {
        return wood;
    }

    public int getStone()
    {
        return stone;
    }

    public int addWood(int amount)
    {
        return wood += amount;
    }

    public int addStone(int amount)
    {
        return stone += amount;
    }

    public void useWood(int amount)
    {
        wood -= amount;
    }

    public void useStone(int amount)
    {
        stone -= amount;
    }
}
