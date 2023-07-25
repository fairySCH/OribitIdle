using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainTabs : MonoBehaviour
{
    public GameObject tabbutton1;
    public GameObject tabbutton2;
    public GameObject tabbutton3;
    public GameObject tabbutton4;
    public GameObject tabbutton5;

    public GameObject tabcontent1;
    public GameObject tabcontent2;
    public GameObject tabcontent3;
    public GameObject tabcontent4;
    public GameObject tabcontent5;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void HideAllTabs()
    {
        tabcontent1.SetActive(false);
        tabcontent2.SetActive(false);
        tabcontent3.SetActive(false);
        tabcontent4.SetActive(false);
        tabcontent5.SetActive(false);
    }

    public void ShowTab1()
    {
        HideAllTabs();
        tabcontent1.SetActive(true);
    }
    public void ShowTab2()
    {
        HideAllTabs();
        tabcontent2.SetActive(true);
    }
    public void ShowTab3()
    {
        HideAllTabs();
        tabcontent3.SetActive(true);
    }
    public void ShowTab4()
    {
        HideAllTabs();
        tabcontent4.SetActive(true);
    }
    public void ShowTab5()
    {
        HideAllTabs();
        tabcontent5.SetActive(true);
    }
}
