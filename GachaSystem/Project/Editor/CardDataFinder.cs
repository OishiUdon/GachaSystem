using UnityEngine;
using UnityEditor;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using UnityEditor.VersionControl;

/// <summary>
/// 指定したフォルダ内のScriptableObjectを検索して表示するエディタ拡張
/// </summary>
public class CardDataFinder : EditorWindow
{
    private string searchFolder = "Assets/Datas/New/CardData"; // ここに探索するフォルダのパスを指定
    private Vector2 displayScrollPosition = Vector2.zero;
    private Vector2 selectedScrollPosition = Vector2.zero;
    private List<ScriptableObject> foundDatas = new List<ScriptableObject>();
    private ScriptableObject selectedObject;
    private string dataName = "NewAssetData";

    /// <summary>
    /// ウィンドウを表示する
    /// </summary>
    [MenuItem("Custom Tools/Card Data Finder")]
    public static void ShowWindow()
    {
        CardDataFinder window = (CardDataFinder)GetWindow(typeof(CardDataFinder));
        window.Show();
    }

    private void OnGUI()
    {
        GUILayout.Label("Find CardDatas in Folder and Subfolders", EditorStyles.boldLabel);
        searchFolder = EditorGUILayout.TextField("Search Folder Path:", searchFolder);

        if (GUILayout.Button("Find CardDatas"))
        {
            foundDatas.Clear();
            FindObjectsRecursive(searchFolder);
            SortObjects();
        }

        using (new GUILayout.HorizontalScope())
        {
            DisplayFoundObjects();
            DisplaySelectObject();
        }
    }
    
    /// <summary>
    /// 再帰的にScriptableObjectを検索する
    /// </summary>
    /// <param name="folderPath">検索するフォルダのパス</param>
    private void FindObjectsRecursive(string folderPath)
    {
        string[] subFolders = Directory.GetDirectories(folderPath);

        string[] guids = AssetDatabase.FindAssets("t:ScriptableObject", new string[] { folderPath });
        foreach (string guid in guids)
        {
            string assetPath = AssetDatabase.GUIDToAssetPath(guid);
            ScriptableObject scriptableObject = AssetDatabase.LoadAssetAtPath<ScriptableObject>(assetPath);
            if (scriptableObject != null && !foundDatas.Contains(scriptableObject))
            {
                foundDatas.Add(scriptableObject);
            }
        }
        foreach (string subFolder in subFolders)
        {
            FindObjectsRecursive(subFolder);
        }
    }

    /// <summary>
    /// データリストを名前順にソートする
    /// </summary>
    private void SortObjects()
    {
        foundDatas.Sort((x, y) => string.Compare(x.name, y.name));
    }

    /// <summary>
    /// 検索したオブジェクトをディスプレイに表示する
    /// </summary>
    private void DisplayFoundObjects()
    {
        GUILayout.Space(10);
        using (GUILayout.ScrollViewScope scroll = new GUILayout.ScrollViewScope(displayScrollPosition, EditorStyles.helpBox, GUILayout.Width(200)))
        {
            GUILayout.Label("Found CardDatas: " + foundDatas.Count, EditorStyles.boldLabel);
            displayScrollPosition = scroll.scrollPosition;

            // Display all objects
            for (int i = 0; i < foundDatas.Count; i++)
            {
                ScriptableObject obj = foundDatas[i];
                if (GUILayout.Button(obj.name) && Event.current.button == 0)
                {
                    selectedObject = obj;
                    dataName = obj.name;
                }
            }

            GUILayout.Space(15);
            if (GUILayout.Button("Add Data") && Event.current.button == 0)
            {
                CardData newCardData = CreateInstance<CardData>();
                string path = AssetDatabase.GenerateUniqueAssetPath(searchFolder + "/NewCardData.asset");
                AssetDatabase.CreateAsset(newCardData, path);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
                foundDatas.Add(newCardData);
                selectedObject = newCardData;
            }
            if (GUILayout.Button("Delete Data") && Event.current.button == 0)
            {
                var path = AssetDatabase.GetAssetPath(selectedObject);
                AssetDatabase.DeleteAsset(path);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
                foundDatas.Remove(selectedObject);
                selectedObject = null;
            }
        }
    }

    /// <summary>
    /// データリストから選択したオブジェクトをディスプレイに表示する
    /// </summary>
    private void DisplaySelectObject()
    {
        GUILayout.Space(10);
        using (GUILayout.ScrollViewScope scroll = new GUILayout.ScrollViewScope(selectedScrollPosition, EditorStyles.helpBox))
        {
            EditorGUILayout.LabelField("Selected CardData:", EditorStyles.boldLabel);
            selectedScrollPosition = scroll.scrollPosition;

            if (selectedObject != null)
            {
                ShowSelectedProperties(selectedObject);
            }
            else
            {
                EditorGUILayout.LabelField("No CardData selected.");
            }
        }
    }

    /// <summary>
    /// 選択したオブジェクトの詳細を表示する
    /// </summary>
    /// <param name="obj"></param>
    private void ShowSelectedProperties(ScriptableObject obj)
    {
        var path = AssetDatabase.GetAssetPath(obj);
        dataName = EditorGUILayout.TextField("CardData Name: ", dataName);
        if (GUILayout.Button("Rename CardData"))
        {
            AssetDatabase.RenameAsset(path, dataName);
            SortObjects();
        }
        if (GUILayout.Button("Move To Inspector"))
        {
            Selection.activeObject = obj;
        }

        GUILayout.Space(15);
        SerializedObject serializedObject = new SerializedObject(obj);
        SerializedProperty prop = serializedObject.GetIterator();
        prop.Next(true);

        while (prop.NextVisible(false))
        {
            EditorGUILayout.PropertyField(prop, true);
        }

        serializedObject.ApplyModifiedProperties();
    }
}
