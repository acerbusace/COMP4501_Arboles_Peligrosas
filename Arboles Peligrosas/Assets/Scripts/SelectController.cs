using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectController : MonoBehaviour {

    private GameObject sf;
    private UI_SelectedFrame sfInfo;
    private List<string> sfSupportedTags;

    public Text unitNameText;
    public Text unitHealthText;

    // Use this for initialization
    void Start () {
        sfSupportedTags = new List<string>();
        sfSupportedTags.Add("Selectable");
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButtonDown(0))
        {
            //left mouse button
            setSelectableUnit(Camera.main.ScreenPointToRay(Input.mousePosition));
            setSelectableInfo();
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
                    sf = hit.transform.gameObject;
                }
            }
        }
    }

    void setSelectableInfo()
    {
        if (sf != null)
        {
            sfInfo = sf.GetComponent<Selectable>().getSFInfo();

            print("Name: " + sfInfo.name + " > " + sfInfo.health);
            unitNameText.text = sfInfo.name;
            unitHealthText.text = sfInfo.health.ToString();
        }
    }
}
