using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SelectController : MonoBehaviour {

    private List<GameObject> selectedFrames;
    private string sfTag;
    // used for multi select and queueing actions
    private bool shiftModifier;

    // Use this for initialization
    void Start () {
        selectedFrames = new List<GameObject>();

        sfTag = "Selectable";

        shiftModifier = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.LeftShift))
            shiftModifier = true;
        if (Input.GetKeyUp(KeyCode.LeftShift))
            shiftModifier = false;

        // remove selected units which no longer exist
        selectedFrames.RemoveAll(GameObject => GameObject == null);

        //left mouse button
        if (Input.GetMouseButtonDown(0)) {
            if (!IsPointerOverUIObject())
                setSelectableUnit(Camera.main.ScreenPointToRay(Input.mousePosition));
        }
    }

    void setSelectableUnit(Ray ray)
    {
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (HelperFunctions.containsTag(sfTag, hit.transform.gameObject.tag))
            {
                if (!shiftModifier)
                    selectedFrames.Clear();
                // don't select the same object twice
                if (!selectedFrames.Contains(hit.transform.gameObject))
                    selectedFrames.Add(hit.transform.gameObject);
            } else {
                selectedFrames.Clear();
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

    private bool IsPointerOverUIObject() {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
}
