using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

namespace App.UI.Widgets
{
    public class HierarchyManager : MonoBehaviour
    {
        public GameObject itemPrefab;
        public Transform contentParent;

        private Dictionary<int, HierarchyItemComponent> instantiatedItems = new Dictionary<int, HierarchyItemComponent>();

        public void CreateHierarchy(List<HierarchyItem> items)
        {
            ClearHierarchy();

            // 루트 아이템 (parentId가 0 또는 -1인 아이템) 먼저 생성
            foreach (var item in items.Where(i => i.parentId <= 0))
            {
                CreateItem(item, contentParent);
            }

            // 나머지 아이템 생성
            foreach (var item in items.Where(i => i.parentId > 0))
            {
                if (instantiatedItems.ContainsKey(item.parentId))
                {
                    Transform parentTransform = instantiatedItems[item.parentId].childContainer;
                    CreateItem(item, parentTransform);
                }
            }
        }

        private void CreateItem(HierarchyItem item, Transform parent)
        {
            GameObject newItem = Instantiate(itemPrefab, parent);
            HierarchyItemComponent itemComponent = newItem.GetComponent<HierarchyItemComponent>();
            itemComponent.item = item;

            // 토글 버튼 설정
            Toggle toggle = newItem.GetComponentInChildren<Toggle>();
            Text text = toggle.GetComponentInChildren<Text>();
            text.text = item.name;

            toggle.onValueChanged.AddListener((isOn) => {
                ToggleChildren(item.id, isOn);
            });

            instantiatedItems.Add(item.id, itemComponent);
        }

        private void ToggleChildren(int parentId, bool isOn)
        {
            if (instantiatedItems.TryGetValue(parentId, out HierarchyItemComponent parentItem))
            {
                parentItem.childContainer.gameObject.SetActive(isOn);
            }
        }

        private void ClearHierarchy()
        {
            foreach (var item in instantiatedItems.Values)
            {
                Destroy(item.gameObject);
            }
            instantiatedItems.Clear();
        }
    }

    [System.Serializable]
    public class HierarchyItem
    {
        public int id;
        public int parentId;
        public string name;
    }

 
}