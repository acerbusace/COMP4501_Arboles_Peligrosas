using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_ResourceController : MonoBehaviour {

    public Text WoodText;
    public Text StoneText;

    public ResourceController resourceController;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        WoodText.text = "Wood: " + ((int)resourceController.getWood()).ToString();
        StoneText.text = "Stone: " + ((int)resourceController.getStone()).ToString();
	}
}
