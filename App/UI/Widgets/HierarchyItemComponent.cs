using UnityEngine;
using UnityEngine.UI;

namespace App.UI.Widgets
{
    public class HierarchyItemComponent : MonoBehaviour
    {
        public HierarchyItem item;
        public Transform childContainer;
        public Toggle toggle;

        private void Awake()
        {
            toggle = GetComponentInChildren<Toggle>();
            childContainer = transform.Find("Child");
        
            if (childContainer == null)
            {
                GameObject container = new GameObject("Child");
                childContainer = container.transform;
                childContainer.SetParent(transform);
            
                RectTransform containerRect = container.AddComponent<RectTransform>();
                containerRect.anchorMin = new Vector2(0, 0);
                containerRect.anchorMax = new Vector2(1, 1);
                containerRect.offsetMin = new Vector2(20, 0);  // 20px 들여쓰기
                containerRect.offsetMax = Vector2.zero;
            }
        }
    }
}