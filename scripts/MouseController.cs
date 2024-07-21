using UnityEngine;
using System.Collections.Generic;

public class MouseController : MonoBehaviour
{
    public List<GameObject> selectableObjects = new List<GameObject>();
    private List<GameObject> selectedObjects = new List<GameObject>();
    private List<Vector3> initialPositions = new List<Vector3>();

    private Vector3 lastMousePosition;

    public float mouseSensitivity = 0.1f;
    
    private bool isFloating = false;
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        // 1. 객체 선택
        if (Input.GetMouseButtonDown(0))
        {
            if (!Input.GetKey(KeyCode.LeftControl))
            {
                DeselectAll();
            }
            SelectObject();
        }

        // 2. 객체 이동
        if (isFloating)
        {
            MoveSelectedObjects();
        }

        // 3. ESC로 초기 위치로 돌아가기
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ResetPositions();
        }

        // 4. 마우스 왼쪽 버튼으로 위치 고정 및 선택 해제
        if (Input.GetMouseButtonDown(0) && isFloating)
        {
            FixPositionsAndDeselect();
        }

        // 객체 떠다니기 시작/종료
        if (Input.GetKeyDown(KeyCode.T))
        {
            ToggleFloating();
            Debug.Log(selectedObjects.Count);
            
        }
    }

    void SelectObject()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            GameObject hitObject = hit.collider.gameObject;
            Debug.Log(hitObject.name);
            if (selectableObjects.Contains(hitObject) && !selectedObjects.Contains(hitObject))
            {
                hitObject.GetComponent<Outline>().enabled = true;
                selectedObjects.Add(hitObject);
                initialPositions.Add(hitObject.transform.position);
            }
        }
    }

    void DeselectAll()
    {
        foreach (GameObject obj in selectedObjects)
        {
            obj.GetComponent<Outline>().enabled = false;
        }
        selectedObjects.Clear();
        initialPositions.Clear();
        isFloating = false;
    }


void MoveSelectedObjects()
    {
        Vector3 currentMousePosition = Input.mousePosition;
        
        if (lastMousePosition == Vector3.zero)
        {
            lastMousePosition = currentMousePosition;
            return;
        }

        Vector3 mouseDelta = currentMousePosition - lastMousePosition;
        mouseDelta *= mouseSensitivity;

        Vector3 movement = new Vector3(mouseDelta.x, 0, mouseDelta.y);

        if (Input.GetKey(KeyCode.LeftControl))
        {
            movement = new Vector3(0, mouseDelta.y, 0);
        }

        for (int i = 0; i < selectedObjects.Count; i++)
        {
            selectedObjects[i].transform.position += movement;
        }

        lastMousePosition = currentMousePosition;
    }


    void ResetPositions()
    {
        for (int i = 0; i < selectedObjects.Count; i++)
        {
            selectedObjects[i].transform.position = initialPositions[i];
        }
        isFloating = false;
    }

    void FixPositionsAndDeselect()
    {
        DeselectAll();
    }

    void ToggleFloating()
    {
        isFloating = !isFloating;
        lastMousePosition = Vector3.zero;
    }
}