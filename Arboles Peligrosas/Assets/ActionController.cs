using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionController : MonoBehaviour {

    private List<string> resourseTags;
    private bool shiftModifier;
    public SelectController selectController;

	// Use this for initialization
	void Start () {
        resourseTags = new List<string>();
        resourseTags.Add("Tree");
        resourseTags.Add("Stone");

        shiftModifier = false;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.LeftShift))
            shiftModifier = true;
        if (Input.GetKeyUp(KeyCode.LeftShift))
            shiftModifier = false;

        List<GameObject> selectedFrames = selectController.getSelectedFrames();

        if (Input.GetMouseButtonDown(1)) { //right mouse button

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (HelperFunctions.containsTag(resourseTags, hit.transform.gameObject.tag))
                {
                    if (shiftModifier)
                    {
                        foreach (GameObject sf in selectedFrames)
                        {
                            if (HelperFunctions.containsTag("Gatherable", sf.gameObject.tag))
                                sf.GetComponent<Moveable>().queueGather(hit);
                        }
                    }
                    else
                    {
                        foreach (GameObject sf in selectedFrames)
                        {
                            if (HelperFunctions.containsTag("Gatherable", sf.gameObject.tag))
                                sf.GetComponent<Moveable>().gather(hit);
                        }
                    }
                }
                else
                {
                    if (shiftModifier)
                    {
                        foreach (GameObject sf in selectedFrames)
                        {
                            if (HelperFunctions.containsTag("Moveable", sf.gameObject.tag))
                                sf.GetComponent<Moveable>().queueMove(hit);
                        }
                    }
                    else
                    {
                        foreach (GameObject sf in selectedFrames)
                        {
                            if (HelperFunctions.containsTag("Moveable", sf.gameObject.tag))
                                sf.GetComponent<Moveable>().move(hit);
                        }
                    }
                }
            }
        }
	}
}
