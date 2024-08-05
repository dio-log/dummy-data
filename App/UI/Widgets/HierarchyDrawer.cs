using System;
using System.Collections.Generic;
using UnityEngine;

namespace App.UI.Widgets
{
    public class HierarchyDrawer :MonoBehaviour
    {
        private void Start()
        {
    
            HierarchyManager manager = GetComponent<HierarchyManager>();
            List<HierarchyItem> items = new List<HierarchyItem>
            {
                new HierarchyItem { id = 1, parentId = 0, name = "부모1" },
                new HierarchyItem { id = 2, parentId = 1, name = "자식1" },
                new HierarchyItem { id = 3, parentId = 1, name = "자식2" },
                new HierarchyItem { id = 4, parentId = 0, name = "부모2" },
                new HierarchyItem { id = 5, parentId = 2, name = "손자1" }
            };
            manager.CreateHierarchy(items);
            
        }
    }
}