using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectController : MonoBehaviour {

    private List<GameObject> selectedFrames;
    private UI_SelectedFrame sfInfo;
    private List<string> sfSupportedTags;
    private bool selectMultiple;

    public Text unitNameText;
    public Text unitHealthText;

    // Use this for initialization
    void Start () {
        selectedFrames = new List<GameObject>();
        sfSupportedTags = new List<string>();
        sfSupportedTags.Add("Selectable");
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
            setSelectableInfo();
        } else if (Input.GetMouseButtonDown(1)) { //left mouse button
            print("sf count: " + selectedFrames.Count);
            foreach (GameObject sf in selectedFrames) {
                sf.GetComponent<Selectable>().setDestination(Camera.main.ScreenPointToRay(Input.mousePosition));
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
                if (sfSupportedTags.Contains(hit.transform.gameObject.tag))
                {
                    if (!selectMultiple)
                        selectedFrames.Clear();

                    selectedFrames.Add(hit.transform.gameObject);
                }
            }
        }
    }

    void setSelectableInfo()
    {
        if (selectedFrames.Count > 0)
        {
            sfInfo = selectedFrames[selectedFrames.Count - 1].GetComponent<Selectable>().getSFInfo();
            
            unitNameText.text = "Unit: " + sfInfo.name;
            unitHealthText.text = "Health: " + sfInfo.health.ToString();
        }
    }
}
