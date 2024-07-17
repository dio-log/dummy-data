using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Hierarchy;

public class HierarchyMenu : MonoBehaviour
{
    public HierarchyNode rootNode;
    public GameObject nodePrefab;
    public RectTransform contentPanel;

    private Dictionary<HierarchyNode, GameObject> nodeObjects = new Dictionary<HierarchyNode, GameObject>();

    private void Start()
    {
        CreateHierarchy(rootNode, 0, contentPanel);
    }

    private void CreateHierarchy(HierarchyNode node, int depth, RectTransform parent)
    {
        GameObject nodeObject = Instantiate(nodePrefab, parent);
        nodeObjects[node] = nodeObject;

        RectTransform rectTransform = nodeObject.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(depth * 20, -rectTransform.rect.height * parent.childCount);

        Text nameText = nodeObject.GetComponentInChildren<Text>();
        nameText.text = node.name;

        Toggle toggle = nodeObject.GetComponentInChildren<Toggle>();
        toggle.isOn = node.isExpanded;
        toggle.onValueChanged.AddListener((isOn) => {
            node.isExpanded = isOn;
            ToggleChildren(node, isOn);
        });

        // 자식 노드를 위한 컨테이너 생성
        GameObject childContainer = new GameObject("ChildContainer");
        RectTransform childContainerRect = childContainer.AddComponent<RectTransform>();
        childContainerRect.SetParent(nodeObject.transform, false);
        
        if (node.isExpanded)
        {
            CreateChildren(node, depth + 1, childContainerRect);
        }
    }

    private void CreateChildren(HierarchyNode node, int depth, RectTransform parent)
    {
        foreach (HierarchyNode child in node.children)
        {
            CreateHierarchy(child, depth, parent);
        }
        UpdateLayout();
    }

    private void ToggleChildren(HierarchyNode node, bool isExpanded)
    {
        GameObject nodeObject = nodeObjects[node];
        RectTransform childContainer = nodeObject.transform.Find("ChildContainer").GetComponent<RectTransform>();

        if (isExpanded && childContainer.childCount == 0)
        {
            CreateChildren(node, GetNodeDepth(node) + 1, childContainer);
        }
        else
        {
            childContainer.gameObject.SetActive(isExpanded);
        }

        UpdateLayout();
    }

    private int GetNodeDepth(HierarchyNode node)
    {
        int depth = 0;
        GameObject obj = nodeObjects[node];
        while (obj.transform.parent != contentPanel)
        {
            depth++;
            obj = obj.transform.parent.gameObject;
        }
        return depth;
    }

    private void UpdateLayout()
    {
        // Force the layout to update
        LayoutRebuilder.ForceRebuildLayoutImmediate(contentPanel);
    }
}

// 이 개선된 버전의 주요 변경 사항:

// 각 노드 객체에 대한 참조를 Dictionary에 저장하여 빠르게 접근할 수 있게 했습니다.
// 각 노드 객체 내에 자식 노드들을 담을 컨테이너를 만들었습니다.
// 토글 동작 시 전체 하이라키를 다시 그리는 대신, 해당 노드의 자식 컨테이너만 활성화/비활성화합니다.
// 처음 펼칠 때만 자식 노드들을 생성하고, 이후에는 기존 객체를 재사용합니다.
// LayoutRebuilder.ForceRebuildLayoutImmediate를 사용하여 레이아웃을 강제로 업데이트합니다.

// 이 방식의 장점:

// 토글 동작이 더 효율적이고 빠릅니다.
// 불필요한 객체 생성과 제거가 줄어듭니다.
// 대규모 하이라키에서도 성능이 향상됩니다.

// 주의할 점:

// 이 스크립트를 사용하려면 Content Size Fitter와 Vertical Layout Group 컴포넌트를 contentPanel에 추가해야 합니다.
// 노드 프리팹에는 Layout Element 컴포넌트가 필요할 수 있습니다.

// 이 코드를 사용하면 토글 동작 시 하이라키가 부드럽게 확장되고 축소되는 것을 볼 수 있습니다. 추가적인 최적화나 기능이 필요하다면 말씀해 주세요.