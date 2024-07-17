using UnityEngine;
using UnityEditor;
using System.Collections.Generic;
using Hierarchy;

public class HierarchyView : EditorWindow
{
    private Dictionary<string, bool> foldoutStates = new Dictionary<string, bool>();
    private HierarchyNode rootNode;

    [MenuItem("Window/Custom Hierarchy")]
    public static void ShowWindow()
    {
        GetWindow<HierarchyView>("Custom Hierarchy");
    }

    void OnEnable()
    {
        // 초기 데이터 로드
        rootNode = LoadHierarchyNode();
    }

    void OnGUI()
    {
        EditorGUILayout.BeginVertical();
        DrawNode(rootNode);
        EditorGUILayout.EndVertical();
    }

    void DrawNode(HierarchyNode node)
    {
        if (!foldoutStates.ContainsKey(node.id))
        {
            foldoutStates[node.id] = false;
        }

        EditorGUILayout.BeginHorizontal();
        
        // 펼침/접힘 상태 토글
        foldoutStates[node.id] = EditorGUILayout.Foldout(foldoutStates[node.id], node.name, true);

        // 노드 선택 버튼
        if (GUILayout.Button("Select", GUILayout.Width(60)))
        {
            Debug.Log($"Selected: {node.name}");
        }

        EditorGUILayout.EndHorizontal();

        // 자식 노드 그리기
        if (foldoutStates[node.id])
        {
            EditorGUI.indentLevel++;
            foreach (var child in node.children)
            {
                DrawNode(child);
            }
            EditorGUI.indentLevel--;
        }
    }

    HierarchyNode LoadHierarchyNode()
    {
        HierarchyNode root = JsonUtility.FromJson<HierarchyNode>("Assets/Resources/dummy.json");
        return root;
    }
}


