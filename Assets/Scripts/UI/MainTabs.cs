using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainTabs : MonoBehaviour
{
    // public GameObject tabbutton1;
    // public GameObject tabbutton2;
    // public GameObject tabbutton3;
    // public GameObject tabbutton4;
    // public GameObject tabbutton5;

    // public GameObject tabcontent1;
    // public GameObject tabcontent2;
    // public GameObject tabcontent3;
    // public GameObject tabcontent4;
    // public GameObject tabcontent5;

    public GameObject[] tabButtons; // 배열로 버튼들을 관리
    public GameObject[] tabContents; // 배열로 탭 화면들을 관리
    private int activeTabIndex = -1; // 현재 활성화된 탭 인덱스를 기억

    // Start is called before the first frame update
    void Start()
    {
        // 모든 탭 화면을 숨깁니다.
        HideAllTabs();
    }

    // Update is called once per frame
    void Update()
    {
        // 터치를 감지합니다.
        if (Input.touchCount > 0)
        {
            // 첫 번째 터치를 가져옵니다.
            Touch touch = Input.GetTouch(0);

            // 터치가 발생하면 모든 탭 화면을 닫습니다.
            HideAllTabs();
        }
    }

    public void HideAllTabs()
    {
        foreach (GameObject tabContent in tabContents)
        {
            tabContent.SetActive(false);
        }
        activeTabIndex = -1; // 모든 탭을 닫았으므로 활성화된 탭 인덱스를 초기화합니다.
    }

    public void ShowTab(int tabIndex)
    {
        if (tabIndex < 0 || tabIndex >= tabContents.Length)
            return;

        // 이미 선택된 탭인지 확인합니다.
        if (tabIndex == activeTabIndex)
        {
            // 선택 해제된 경우 해당 탭을 닫습니다.
            tabContents[tabIndex].SetActive(false);
            activeTabIndex = -1; // 활성화된 탭 인덱스를 초기화합니다.
        }
        else
        {
            // 선택되지 않은 탭인 경우 해당 탭을 활성화합니다.
            HideAllTabs(); // 다른 탭들을 모두 닫습니다.
            tabContents[tabIndex].SetActive(true);
            activeTabIndex = tabIndex; // 활성화된 탭 인덱스를 기억합니다.
        }
    }
    // public void ShowTab1()
    // {
    //     HideAllTabs();
    //     tabcontent1.SetActive(true);
    // }
    // public void ShowTab2()
    // {
    //     HideAllTabs();
    //     tabcontent2.SetActive(true);
    // }
    // public void ShowTab3()
    // {
    //     HideAllTabs();
    //     tabcontent3.SetActive(true);
    // }
    // public void ShowTab4()
    // {
    //     HideAllTabs();
    //     tabcontent4.SetActive(true);
    // }
    // public void ShowTab5()
    // {
    //     HideAllTabs();
    //     tabcontent5.SetActive(true);
    // }
}
