using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectController : MonoBehaviour {

    private List<GameObject> selectedFrames;
    private UI_SelectedFrame sfInfo;
    private List<string> sfTags;
    private List<string> resourseTags;
    private bool selectMultiple;

    // Use this for initialization
    void Start () {
        selectedFrames = new List<GameObject>();

        sfTags = new List<string>();
        sfTags.Add("Selectable");

        resourseTags = new List<string>();
        resourseTags.Add("Tree");
        resourseTags.Add("Stone");

        selectMultiple = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.LeftShift))
            selectMultiple = true;
        if (Input.GetKeyUp(KeyCode.LeftShift))
            selectMultiple = false;

        if (Input.GetMouseButtonDown(0)) {
            //left mouse button
            setSelectableUnit(Camera.main.ScreenPointToRay(Input.mousePosition));
        } else if (Input.GetMouseButtonDown(1)) { //right mouse button

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (resourseTags.Contains(hit.transform.gameObject.tag))
                {
                    if (selectMultiple)
                    {
                        foreach (GameObject sf in selectedFrames)
                        {
                            sf.GetComponent<Selectable>().queueGather(hit);
                        }
                    }
                    else
                    {
                        foreach (GameObject sf in selectedFrames)
                        {
                            sf.GetComponent<Selectable>().gather(hit);
                        }
                    }
                }
                else
                {
                    if (selectMultiple)
                    {
                        foreach (GameObject sf in selectedFrames)
                        {
                            sf.GetComponent<Selectable>().queueMove(hit);
                        }
                    }
                    else
                    {
                        foreach (GameObject sf in selectedFrames)
                        {
                            sf.GetComponent<Selectable>().move(hit);
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
                if (sfTags.Contains(hit.transform.gameObject.tag))
                {
                    if (!selectMultiple)
                        selectedFrames.Clear();
                    if (!selectedFrames.Contains(hit.transform.gameObject))
                        selectedFrames.Add(hit.transform.gameObject);
                }
            }
        }
    }

    public List<GameObject> getSelectedFrames() {
        return selectedFrames;
    }

    public GameObject getSelctedFrame() {
        if (selectedFrames.Count > 0)
            return selectedFrames[selectedFrames.Count - 1];
        return null;
    }
}
