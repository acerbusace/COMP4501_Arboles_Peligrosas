using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class UI_BuildController : MonoBehaviour {

    private GameObject canvas;
    public GameObject canvasPrefab;
    public GameObject buttonPrefab;
    private GameObject buildUnit;
    public GameObject robotPrefab;
    public GameObject wallPrefab;
    public GameObject turretPrefab;
    public GameObject buildRobotPrefab;
    public GameObject buildWallPrefab;
    public GameObject buildTurretPrefab;
    private GameObject currentBuildable;
    public ResourceController resourceController;

    private List<GameObject> buttons;

    public SelectController selectController;

    // Use this for initialization
    void Start ()
    {
        if (GameObject.FindObjectOfType<Canvas>() != null) {
            canvas = GameObject.FindObjectOfType<Canvas>().gameObject;
        } else {
            canvas = Instantiate(canvasPrefab);
        }

        buttons = new List<GameObject>();

        //createButtons();
    }

    void createButtons() {
        GameObject b1 = Instantiate(buttonPrefab, canvas.transform, false);
        b1.GetComponent<RectTransform>().Translate(new Vector3(-80, 15));
        b1.GetComponent<Button>().onClick.AddListener(buildRobot);
        b1.GetComponentInChildren<Text>().text = "Build Robot";

        GameObject b2 = Instantiate(buttonPrefab, canvas.transform, false);
        b2.GetComponent<RectTransform>().Translate(new Vector3(-80, 45));
        b2.GetComponent<Button>().onClick.AddListener(buildWall);
        b2.GetComponentInChildren<Text>().text = "Build Wall";

        GameObject b3 = Instantiate(buttonPrefab, canvas.transform, false);
        b3.GetComponent<RectTransform>().Translate(new Vector3(-80, 75));
        b3.GetComponent<Button>().onClick.AddListener(buildTurret);
        b3.GetComponentInChildren<Text>().text = "Build Turret";

        buttons.Add(b1);
        buttons.Add(b2);
        buttons.Add(b3);
    }

    void clearButtons() {
        foreach (GameObject button in buttons) {
            Destroy(button);
        }

        buttons.Clear();
    }

    // Update is called once per frame
    void Update()
    {
        //clearButtons();
        //createButtons();

        GameObject frame = selectController.getLastSelctedFrame();
        if (frame != null) {
            if (HelperFunctions.containsTag("Builder", frame.tag)) {
                if (buttons.Count == 0)
                    createButtons();
            } else {
                clearButtons();
            }
        } else {
            clearButtons();
        }

        if (Input.GetMouseButtonDown(0) && currentBuildable != null)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Vector3 v = new Vector3();
            if (Physics.Raycast(ray, out hit))
                v = transform.position = HelperFunctions.hitToVector(hit);
            v.y = 2.5f; // cause cool
            
            Instantiate(currentBuildable, v, buildUnit.transform.rotation);
            currentBuildable = null;
            Destroy(buildUnit);

            HelperFunctions.bakeNavMeshes();
        }
    }

    void buildRobot()
    {
        if (resourceController.buildingRobot()) {
            buildUnit = Instantiate(buildRobotPrefab);
            currentBuildable = robotPrefab;
        }
    }

    void buildWall()
    {
        if (resourceController.buildingWall()) {
            buildUnit = Instantiate(buildWallPrefab);
            currentBuildable = wallPrefab;
        }
    }

    void buildTurret()
    {
        if (resourceController.buildingWall()) {
            buildUnit = Instantiate(buildTurretPrefab);
            currentBuildable = turretPrefab;
        }
    }
}
