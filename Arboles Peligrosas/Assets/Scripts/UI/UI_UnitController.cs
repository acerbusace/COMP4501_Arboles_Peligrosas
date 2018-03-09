using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_UnitController : MonoBehaviour {

    private List<GameObject> sfInfoPannels;
    public GameObject sfInfoPannelPrefab;
    public GameObject sfInfoTextPrefab;
    private GameObject canvas;
    public GameObject canvasPrefab;

    public SelectController selectController;
    private List<GameObject> selectedFrames;

	// Use this for initialization
	void Start () {
        sfInfoPannels = new List<GameObject>();
        canvas = Instantiate(canvasPrefab);
	}
	
	// Update is called once per frame
	void Update () {
        clearSelectableInfo();
        setSelectableInfo();	
	}

    GameObject createInfoPannel(UI_SelectedFrame sfInfo, Vector3 pannelPos = new Vector3()) {
        GameObject pannel = Instantiate(sfInfoPannelPrefab, canvas.transform, false);
        pannel.transform.position = pannelPos;

        Vector3 textPos = new Vector3();
        foreach (string key in sfInfo.info.Keys) {
            GameObject name = Instantiate(sfInfoTextPrefab, pannel.transform, false);
            name.GetComponent<Text>().text = key + ": " + sfInfo.info[key];
            name.transform.position -= textPos;
            textPos.y += name.GetComponent<Text>().preferredHeight;
        }

        RectTransform rt = pannel.GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(150f, textPos.y + 10);

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
