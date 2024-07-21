using UnityEngine;
using System.Collections.Generic;

public class DragSelector : MonoBehaviour
{
    public RectTransform selectionBox; // UI에 추가할 선택 영역 표시용 이미지
    private Vector2 startPos;
    private Vector2 endPos;
    private bool isDragging = false;
    private List<GameObject> selectedObjects = new List<GameObject>();

    void Update()
    {
        // 마우스 왼쪽 버튼을 누르기 시작할 때
        if (Input.GetMouseButtonDown(0))
        {
            startPos = Input.mousePosition;
            isDragging = true;
            selectionBox.gameObject.SetActive(true);
        }

        // 마우스 드래그 중
        if (isDragging)
        {
            endPos = Input.mousePosition;
            Vector2 center = (startPos + endPos) / 2;
            Vector2 size = new Vector2(Mathf.Abs(startPos.x - endPos.x), Mathf.Abs(startPos.y - endPos.y));

            selectionBox.position = center;
            selectionBox.sizeDelta = size;
        }

        // 마우스 버튼을 놓았을 때
        if (Input.GetMouseButtonUp(0))
        {
            isDragging = false;
            selectionBox.gameObject.SetActive(false);
            SelectObjectsInDragArea();
        }
    }

    void SelectObjectsInDragArea()
    {
        selectedObjects.Clear();
        Rect selectionRect = new Rect(Mathf.Min(startPos.x, endPos.x), Mathf.Min(startPos.y, endPos.y),
                                      Mathf.Abs(startPos.x - endPos.x), Mathf.Abs(startPos.y - endPos.y));

        foreach (GameObject obj in GameObject.FindGameObjectsWithTag("Selectable"))
        {
            Vector3 screenPos = Camera.main.WorldToScreenPoint(obj.transform.position);
            if (selectionRect.Contains(screenPos))
            {
                selectedObjects.Add(obj);
                Outline outline = obj.GetComponent<Outline>();
                if(outline == null )
                {
                    obj.AddComponent<Outline>();
                }
                outline.enabled = true;
                // 여기에 선택된 객체에 대한 처리를 추가하세요 (예: 하이라이트 효과)
            }
        }

        Debug.Log($"선택된 객체 수: {selectedObjects.Count}");
    }
}