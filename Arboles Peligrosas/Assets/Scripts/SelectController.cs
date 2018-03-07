using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectController : MonoBehaviour {

    private List<GameObject> selectedFrames;
    private UI_SelectedFrame sfInfo;
    private List<string> sfSupportedTags;
    private bool selectMultiple;

    private List<GameObject> sfInfoPannels;
    public GameObject sfInfoPannelPrefab;
    public GameObject UI_Canvas;

    public Text unitNameText;
    public Text unitHealthText;

    // Use this for initialization
    void Start () {
        selectedFrames = new List<GameObject>();
        sfSupportedTags = new List<string>();
        sfSupportedTags.Add("Selectable");
        sfInfoPannels = new List<GameObject>();
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

    GameObject createInfoPannel(UI_SelectedFrame sfInfo, Vector3 pos = new Vector3()) {
        GameObject pannel = Instantiate(sfInfoPannelPrefab, UI_Canvas.transform, false);
        pannel.transform.position = pos;

        Text unitNameText = pannel.transform.Find("UnitNameText").GetComponent<Text>();
        unitNameText.text = "Unit: " + sfInfo.name;

        Text unitHealthText = pannel.transform.Find("UnitHealthText").GetComponent<Text>();
        unitHealthText.text = "Health: " + sfInfo.health.ToString();

        return pannel;
    }

    void clearSelectableInfo() {
        foreach (GameObject sfInfoPannel in sfInfoPannels) {
            Destroy(sfInfoPannel);
        }

        sfInfoPannels.Clear();
    }

    void setSelectableInfo()
    {
        clearSelectableInfo();

        int i = 0;
        Vector3 pos = new Vector3(0f, 0f, 0f);

        foreach (GameObject sf in selectedFrames) {
            UI_SelectedFrame sfInfo = sf.GetComponent<Selectable>().getSFInfo();


            GameObject pannel = createInfoPannel(sfInfo, pos);
            sfInfoPannels.Add(pannel);

            RectTransform rt = pannel.GetComponent<RectTransform>();

            pos += new Vector3(0f, rt.rect.height, 0f);
            ++i;
        }


        //if (selectedFrames.Count > 0)
        //{
        //    sfInfo = selectedFrames[selectedFrames.Count - 1].GetComponent<Selectable>().getSFInfo();
            
        //    unitNameText.text = "Unit: " + sfInfo.name;
        //    unitHealthText.text = "Health: " + sfInfo.health.ToString();
        //}
    }
}
