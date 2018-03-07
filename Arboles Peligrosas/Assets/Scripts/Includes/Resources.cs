using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resources : MonoBehaviour {

    private int wood;
    private int stone;

	// Use this for initialization
	void Start () {
        wood = 69;
        stone = 69;
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
