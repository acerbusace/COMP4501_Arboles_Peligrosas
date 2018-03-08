using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectController : MonoBehaviour {

    private List<GameObject> selectedFrames;
    private UI_SelectedFrame sfInfo;
    private string sfTag;
    private List<string> resourseTags;
    // used for multi select and queueing actions
    private bool shiftModifier;

    // Use this for initialization
    void Start () {
        selectedFrames = new List<GameObject>();

        sfTag = "Selectable";

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

        if (Input.GetMouseButtonDown(0)) {
            //left mouse button
            setSelectableUnit(Camera.main.ScreenPointToRay(Input.mousePosition));
        } else if (Input.GetMouseButtonDown(1)) { //right mouse button

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

    void setSelectableUnit(Ray ray)
    {
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.gameObject != null)
            {
                if (HelperFunctions.containsTag(sfTag, hit.transform.gameObject.tag))
                {
                    if (!shiftModifier)
                        selectedFrames.Clear();
                    // don't select the same object twice
                    if (!selectedFrames.Contains(hit.transform.gameObject))
                        selectedFrames.Add(hit.transform.gameObject);
                }
            }
        }
    }

    public List<GameObject> getSelectedFrames() {
        return selectedFrames;
    }

    public GameObject getLastSelctedFrame() {
        if (selectedFrames.Count > 0)
            return selectedFrames[selectedFrames.Count - 1];
        return null;
    }
}
