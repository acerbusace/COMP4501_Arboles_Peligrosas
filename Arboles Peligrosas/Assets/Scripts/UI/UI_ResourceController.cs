using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_ResourceController : MonoBehaviour {

    public GameObject canvasPrefab;
    public GameObject resourcePannelPrefab;
    private Text WoodText;
    private Text StoneText;

    public ResourceController resourceController;

	// Use this for initialization
	void Start () {
        GameObject canvas = Instantiate(canvasPrefab);
        GameObject resourcePannel = Instantiate(resourcePannelPrefab, canvas.transform, false);
        //GameObject canvas = new GameObject("ResourceCanvas");
        //GameObject resourcePannel = Instantiate(resourcePannelPrefab);
        WoodText = resourcePannel.transform.Find("WoodText").GetComponent<Text>();
        StoneText = resourcePannel.transform.Find("StoneText").GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        WoodText.text = "Wood: " + ((int)resourceController.getWood()).ToString();
        StoneText.text = "Stone: " + ((int)resourceController.getStone()).ToString();
	}
}
