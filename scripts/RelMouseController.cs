using UnityEngine;
using System.Collections.Generic;

public class RelMouseController : MonoBehaviour
{
    public List<GameObject> selectableObjects = new List<GameObject>();
    private List<GameObject> selectedObjects = new List<GameObject>();
    private List<Vector3> initialPositions = new List<Vector3>();
    private bool isFloating = false;
    private Camera mainCamera;
    public float yOffset = 0f; // Y축 오프셋 (바닥에서 얼마나 띄울지)

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (!Input.GetKey(KeyCode.LeftControl))
            {
                DeselectAll();
            }
            SelectObject();
        }

        if (isFloating)
        {
            MoveSelectedObjects();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ResetPositions();
        }

        if (Input.GetMouseButtonDown(0) && isFloating)
        {
            FixPositionsAndDeselect();
        }

        if (Input.GetKeyDown(KeyCode.T))
        {
            ToggleFloating();
        }
    }

    void SelectObject()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            GameObject hitObject = hit.collider.gameObject;
            if (selectableObjects.Contains(hitObject) && !selectedObjects.Contains(hitObject))
            {
                selectedObjects.Add(hitObject);
                initialPositions.Add(hitObject.transform.position);
            }
        }
    }

    void DeselectAll()
    {
        selectedObjects.Clear();
        initialPositions.Clear();
        isFloating = false;
    }

    void MoveSelectedObjects()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane plane;

        if (Input.GetKey(KeyCode.LeftControl))
        {
            // Y축 이동
            plane = new Plane(Vector3.forward, Vector3.zero);
        }
        else
        {
            // XZ 평면 이동
            plane = new Plane(Vector3.up, new Vector3(0, yOffset, 0));
        }

        float distance;
        if (plane.Raycast(ray, out distance))
        {
            Vector3 hitPoint = ray.GetPoint(distance);

            for (int i = 0; i < selectedObjects.Count; i++)
            {
                if (Input.GetKey(KeyCode.LeftControl))
                {
                    // Y축만 변경
                    selectedObjects[i].transform.position = new Vector3(
                        selectedObjects[i].transform.position.x,
                        hitPoint.y,
                        selectedObjects[i].transform.position.z
                    );
                }
                else
                {
                    // XZ 평면 변경, Y는 고정
                    selectedObjects[i].transform.position = new Vector3(
                        hitPoint.x,
                        yOffset,
                        hitPoint.z
                    );
                }
            }
        }
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
    }
}