using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hierarchy
{
    public class HierarchyNode
    {
        public string id;
        public string name;
        public string type;
        public string parentId;
        public int depth;
        public bool isExpanded;
        public Transform transform;
        public List<HierarchyNode> children;
    }

    public class Transform
    {
        public Vector3 position;
        public Vector3 rotation;
        public Vector3 scale;
    }

    public class TreeManager
    {
        private Dictionary<string, HierarchyNode> m_nodeMap = new();
        private HierarchyNode m_root;

        public void AddNode(HierarchyNode node, string parentId = null)
        {
            m_nodeMap[node.id] = node;
            
            if (parentId == null)
            {
                m_root = node;
                node.depth = 0;
                node.parentId = null;
            }
            else
            {
                if(m_nodeMap.ContainsKey(parentId))
                {
                     throw new Exception($"Parent node with ID {parentId} not found.");
                }
                HierarchyNode parent = m_nodeMap[parentId];
                parent.children.Add(node);

                node.parentId = parentId;
                node.depth = parent.depth + 1;
            }
        }

        public HierarchyNode FindNode(string id)
        {
             return m_nodeMap.ContainsKey(id) ? m_nodeMap[id] : null;
        }

        public HierarchyNode GetRoot()
        {
            return m_root;
        }

            // 노드의 depth를 업데이트하는 메서드 (필요한 경우 사용)
        public void UpdateDepths()
        {
            UpdateDepthRecursive(m_root, 0);
        }

        private void UpdateDepthRecursive(HierarchyNode node, int depth)
        {
            node.depth = depth;
            foreach (var child in node.children)
            {
                UpdateDepthRecursive(child, depth + 1);
            }
        }

    }

    public class TreeGUI : MonoBehaviour
    {
        public TreeManager treeManager;

        void OnGUI()
        {
            DrawNode(treeManager.GetRoot(), 0);
        }

        void DrawNode(HierarchyNode node, int depth)
        {
            GUI.Label(new Rect(20 * depth, 20 * depth, 300, 20), node.name);
            //여기서 프리팹로드해서 뎁스주면서 로드하면됨
            
            foreach (var child in node.children)
            {
                DrawNode(child, depth + 1);
            }
        }
    }
}

