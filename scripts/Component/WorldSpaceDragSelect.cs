using UnityEngine;
using System.Collections.Generic;

public class WorldSpaceDragSelect : MonoBehaviour
{
    private Vector3 startPos;
    private Vector3 endPos;
    private bool isDragging = false;
    
    private Camera mainCamera;
    
    public LayerMask selectableLayer; // 선택 가능한 오브젝트의 레이어

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            startPos = GetMouseWorldPosition();
            isDragging = true;
        }
        
        if (Input.GetMouseButtonUp(0))
        {
            endPos = GetMouseWorldPosition();
            isDragging = false;
            SelectObjectsInRectangle();
        }
    }

    void OnDrawGizmos()
    {
        if (isDragging)
        {
            Vector3 currentPos = GetMouseWorldPosition();
            DrawWorldSpaceRect(startPos, currentPos);
        }
    }

    Vector3 GetMouseWorldPosition()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, Vector3.zero);
        float distance;
        if (plane.Raycast(ray, out distance))
        {
            return ray.GetPoint(distance);
        }
        return Vector3.zero;
    }

    void DrawWorldSpaceRect(Vector3 start, Vector3 end)
    {
        Vector3 corner1 = new Vector3(start.x, 0, start.z);
        Vector3 corner2 = new Vector3(end.x, 0, start.z);
        Vector3 corner3 = new Vector3(end.x, 0, end.z);
        Vector3 corner4 = new Vector3(start.x, 0, end.z);

        Gizmos.color = Color.green;
        Gizmos.DrawLine(corner1, corner2);
        Gizmos.DrawLine(corner2, corner3);
        Gizmos.DrawLine(corner3, corner4);
        Gizmos.DrawLine(corner4, corner1);
    }

//모델 세팅할 때 콜라이더 추가해야 캐스트됨
    void SelectObjectsInRectangle()
    {
        Vector3 center = (startPos + endPos) / 2;
        Vector3 size = new Vector3(Mathf.Abs(endPos.x - startPos.x), 100f, Mathf.Abs(endPos.z - startPos.z));
        
        // 박스캐스트를 위에서 아래로 수행
        RaycastHit[] hits = Physics.BoxCastAll(center + Vector3.up * 50f, size / 2, Vector3.down, Quaternion.identity, 100f, selectableLayer);

        foreach (RaycastHit hit in hits)
        {
            // 여기서 선택된 오브젝트에 대한 처리를 합니다.
            Debug.Log(hit.collider.gameObject.name + " 선택됨");
            // 예: hit.collider.gameObject.GetComponent<Renderer>().material.color = Color.red;
        }
    }
}