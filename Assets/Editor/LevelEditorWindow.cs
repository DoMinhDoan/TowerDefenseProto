using UnityEditor;
using UnityEngine;

public class LevelEditorWindow : EditorWindow
{
    int selectedAction = 0;

    [MenuItem ("Window/Level Editor")]
    static void Init()
    {
        LevelEditorWindow window = (LevelEditorWindow)EditorWindow.GetWindow(typeof(LevelEditorWindow));
        window.Show();
    }

    private void OnGUI()
    {
        float width = position.width - 5;   //to leave a little bit space
        float height = 30;
        string[] actionLabel = {"X", "Create Spot", "Delete Spot"};
        selectedAction = GUILayout.SelectionGrid(selectedAction, actionLabel, 3, GUILayout.Width(width), GUILayout.Height(height));
    }

    private void OnEnable()
    {
        SceneView.onSceneGUIDelegate -= OnScene;
        SceneView.onSceneGUIDelegate += OnScene;
    }

    void OnScene(SceneView sceneView)
    {
        Event e = Event.current;
        if(e.type == EventType.MouseUp)
        {
            //To make a ray, you use the helper method ScreenPointToRay(), which you call with the mouse position stored in the event
            //As the y-coordinate is reversed between camera and event coordinate system, you transform it by deducting the original y-coordinate from the height of the current view.
            Ray ray = Camera.current.ScreenPointToRay(new Vector3(e.mousePosition.x, Camera.current.pixelHeight - e.mousePosition.y));
            if (selectedAction == 1)
            {
                // create the spot
                Debug.Log("create the spot");

                RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity, 1 << LayerMask.NameToLayer("Background"));
                if (hit)
                {
                    GameObject spotPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Prefabs/Openspot.prefab");

                    GameObject spotGO = Instantiate(spotPrefab, GameObject.Find("Openspots").transform);                    
                    spotGO.transform.position = hit.point;

                    Undo.RegisterCreatedObjectUndo(spotGO, "Create Spot");
                }
            }
            else if (selectedAction == 2)
            {
                // del the spot
                Debug.Log("del the spot");

                RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity, 1 << LayerMask.NameToLayer("Spot"));
                if (hit)
                {
                    Undo.DestroyObjectImmediate(hit.collider.gameObject);
                }
            }
        }
    }
}
