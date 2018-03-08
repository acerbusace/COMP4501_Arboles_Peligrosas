using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_UnitController : MonoBehaviour {

    private List<GameObject> sfInfoPannels;
    public GameObject sfInfoPannelPrefab;
    public GameObject UI_Canvas;

    public SelectController selectController;
    private List<GameObject> selectedFrames;

	// Use this for initialization
	void Start () {
        sfInfoPannels = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update () {
        clearSelectableInfo();
        setSelectableInfo();	
	}

    GameObject createInfoPannel(UI_SelectedFrame sfInfo, Vector3 pos = new Vector3()) {
        GameObject pannel = Instantiate(sfInfoPannelPrefab, UI_Canvas.transform, false);
        pannel.transform.position = pos;

        Text unitNameText = pannel.transform.Find("UnitNameText").GetComponent<Text>();
        unitNameText.text = "Unit: " + sfInfo.name;

        Text unitHealthText = pannel.transform.Find("UnitHealthText").GetComponent<Text>();
        unitHealthText.text = "Health: " + ((int)sfInfo.health).ToString();

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

        selectedFrames = selectController.getSelectedFrames();
        foreach (GameObject sf in selectedFrames) {
            UI_SelectedFrame sfInfo = sf.GetComponent<Selectable>().getSFInfo();


            GameObject pannel = createInfoPannel(sfInfo, pos);
            sfInfoPannels.Add(pannel);

            RectTransform rt = pannel.GetComponent<RectTransform>();

            pos += new Vector3(0f, rt.rect.height, 0f);
            ++i;
        }
    }
}
