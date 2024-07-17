using UnityEngine;
using System.Collections.Generic;

public class ObjectSelector : MonoBehaviour
{
    private Camera mainCamera;
    private List<GameObject> selectedObjects = new List<GameObject>();

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 왼쪽 마우스 버튼 클릭 감지
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                GameObject clickedObject = hit.collider.gameObject;

                if (Input.GetKey(KeyCode.LeftControl))
                {
                    // Ctrl 키를 누른 상태에서 클릭: 객체 추가 또는 제거
                    if (selectedObjects.Contains(clickedObject))
                    {
                        DeselectObject(clickedObject);
                    }
                    else
                    {
                        SelectObject(clickedObject);
                    }
                }
                else
                {
                    // Ctrl 키 없이 클릭: 기존 선택 초기화 후 새로운 객체만 선택
                    DeselectAllObjects();
                    SelectObject(clickedObject);
                }

                // 현재 선택된 모든 객체 출력
                PrintSelectedObjects();
            }
        }
    }

    void SelectObject(GameObject obj)
    {
        selectedObjects.Add(obj);
        ToggleOutline(obj, true);
        Debug.Log($"객체 선택됨: {obj.name} (ID: {obj.GetInstanceID()})");
    }

    void DeselectObject(GameObject obj)
    {
        selectedObjects.Remove(obj);
        ToggleOutline(obj, false);
        Debug.Log($"객체 선택 해제됨: {obj.name} (ID: {obj.GetInstanceID()})");
    }

    void DeselectAllObjects()
    {
        foreach (GameObject obj in selectedObjects)
        {
            ToggleOutline(obj, false);
        }
        selectedObjects.Clear();
    }

    void ToggleOutline(GameObject obj, bool enable)
    {
        Outline outline = obj.GetComponent<Outline>();
        if (outline == null && enable)
        {
            outline = obj.AddComponent<Outline>();
        }
        
        if (outline != null)
        {
            outline.enabled = enable;
        }
    }

    void PrintSelectedObjects()
    {
        Debug.Log("현재 선택된 객체들:");
        foreach (GameObject obj in selectedObjects)
        {
            Debug.Log($"- {obj.name} (ID: {obj.GetInstanceID()})");
        }
    }
}