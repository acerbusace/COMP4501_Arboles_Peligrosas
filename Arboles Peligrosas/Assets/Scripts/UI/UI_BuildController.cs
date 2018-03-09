using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_BuildController : MonoBehaviour {

    //public GameObject robotPrefab;
    //public GameObject wallPrefab;
    //public GameObject turretPrefab;
    public GameObject buttonPrefab;
    private GameObject UI_Canvas;
    public GameObject dummyPrefab;
    private GameObject dummyObject;
    public GameObject robotPrefab;
    public GameObject wallPrefab;
    public GameObject turretPrefab;
    private GameObject currentBuildable;

    // Use this for initialization
    void Start ()
    {
        UI_Canvas = GameObject.Find("UI_Canvas");

        GameObject b1 = Instantiate(buttonPrefab, UI_Canvas.transform, false);
        b1.GetComponent<RectTransform>().Translate(new Vector3(-80, 15));
        b1.GetComponent<Button>().onClick.AddListener(buildRobot);
        b1.GetComponentInChildren<Text>().text = "Build Robot";

        GameObject b2 = Instantiate(buttonPrefab, UI_Canvas.transform, false);
        b2.GetComponent<RectTransform>().Translate(new Vector3(-80, 45));
        b2.GetComponent<Button>().onClick.AddListener(buildWall);
        b2.GetComponentInChildren<Text>().text = "Build Wall";

        GameObject b3 = Instantiate(buttonPrefab, UI_Canvas.transform, false);
        b3.GetComponent<RectTransform>().Translate(new Vector3(-80, 75));
        b3.GetComponent<Button>().onClick.AddListener(buildTurret);
        b3.GetComponentInChildren<Text>().text = "Build Turret";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && currentBuildable != null)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Vector3 v = new Vector3();
            if (Physics.Raycast(ray, out hit))
                v = transform.position = HelperFunctions.hitToVector(hit);
            v.y += 2; // cause cool
            Instantiate(currentBuildable, v, Quaternion.identity);
            currentBuildable = null;
            Destroy(dummyObject);
        }
    }

    void buildRobot()
    {
        dummyObject = Instantiate(dummyPrefab);
        currentBuildable = robotPrefab;
    }

    void buildWall()
    {
        dummyObject = Instantiate(dummyPrefab);
        currentBuildable = wallPrefab;
    }

    void buildTurret()
    {
        dummyObject = Instantiate(dummyPrefab);
        currentBuildable = turretPrefab;
    }
}
