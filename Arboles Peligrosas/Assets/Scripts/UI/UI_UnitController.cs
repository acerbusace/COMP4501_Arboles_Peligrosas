using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_UnitController : MonoBehaviour {

    private List<GameObject> sfInfoPannels;
    private List<GameObject> sfCircles;
    public GameObject sfInfoPannelPrefab;
    public GameObject sfInfoTextPrefab;
    private GameObject canvas;
    public GameObject canvasPrefab;
    public GameObject sfCirclePrefab;

    public SelectController selectController;
    private List<GameObject> selectedFrames;
    private List<GameObject> selectedFramesNew;

	// Use this for initialization
	void Start () {
        sfInfoPannels = new List<GameObject>();
        sfCircles = new List<GameObject>();
        selectedFrames = new List<GameObject>();
        selectedFramesNew = selectController.getSelectedFrames();

        if (GameObject.FindObjectOfType<Canvas>() != null) {
            canvas = GameObject.FindObjectOfType<Canvas>().gameObject;
        } else {
            canvas = Instantiate(canvasPrefab);
        }
	}

    bool difference(List<GameObject> one, List<GameObject> two) {
        if (one.Count != two.Count) return true;
        for (int i = 0; i < one.Count; ++i) {
            if (one[i].GetInstanceID() != two[i].GetInstanceID()) return true;
        }

        return false;
    }
	
	// Update is called once per frame
	void Update () {
        if (difference(selectedFrames, selectedFramesNew)) {
            print("should be a difference");
            selectedFrames = new List<GameObject>(selectedFramesNew);

            clearSelectableInfo();
            clearSelectableCircle();

            setSelectableInfo();
            setSelectableCircle();
        }
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

    void setSelectableCircle()
    {
        foreach (GameObject sf in selectedFrames)
        {
            GameObject circle = Instantiate(sfCirclePrefab, sf.transform);
            float x = sf.GetComponent<Collider>().bounds.size.x;
            float z = sf.GetComponent<Collider>().bounds.size.z;
            circle.transform.localScale = new Vector3(
                (x + 0.5f) / circle.transform.lossyScale.x, 
                circle.transform.localScale.y / (circle.transform.lossyScale.y / circle.transform.localScale.y), 
                (z + 0.5f) / circle.transform.lossyScale.z);
            sfCircles.Add(circle);
        }
    }

    void clearSelectableCircle()
    {
        foreach (GameObject sfCircle in sfCircles)
        {
            Destroy(sfCircle);
        }
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
    }
}
