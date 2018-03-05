using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotController : MonoBehaviour, Selectable {
    UI_SelectedFrame sfInfo;

	// Use this for initialization
	void Start () {
        sfInfo.name = "Robot";
        sfInfo.health = 100;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public UI_SelectedFrame getSFInfo() { return sfInfo; }
}
