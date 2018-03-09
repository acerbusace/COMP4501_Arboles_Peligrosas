using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallController : MonoBehaviour, Selectable {

    private UI_SelectedFrame sfInfo;

    private string unitName;
    private float unitHealth;

	// Use this for initialization
	void Start () {
        sfInfo.info = new Dictionary<string, string>();

        unitName = "Wall";
        unitHealth = 100f;
	}
	
	// Update is called once per frame
	void Update () {
        updateSFInfo();
	}

    public void setDestination(Ray ray) { }

    public UI_SelectedFrame getSFInfo() { return sfInfo; }

    public void updateSFInfo() {
        HelperFunctions.addToDict(sfInfo.info, "Unit", unitName);
        HelperFunctions.addToDict(sfInfo.info, "Health", ((int)unitHealth).ToString());
    }
}