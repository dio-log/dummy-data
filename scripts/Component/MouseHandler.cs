using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseHandler : MonoBehaviour
{
    // Start is called before the first frame update
    public InputActionAsset inputActions;
    private InputAction mousePositionAction;
    private InputAction mouseClickAction;

     private bool isDragging = false;
    private Vector2 lastMousePosition;
    private GameObject _prefabInstance;
    public GameObject PrefabInstnace
    {
        get { return _prefabInstance; }
        set { _prefabInstance = value; }
    }

     private Vector3 offset;

    public Plane dragPlane;
    public float initialY;
    public float yVelovity = 0.05f;

    

    void Awake()
    {
         var mouseMap = inputActions.FindActionMap("Mouse");
        mousePositionAction = mouseMap.FindAction("Position");
        mouseClickAction = mouseMap.FindAction("Click");
        
        mousePositionAction.Enable();
        mouseClickAction.Enable();

        mouseClickAction.started += StartDrag;
        mouseClickAction.canceled += EndDrag;

         dragPlane = new Plane(Vector3.up, Vector3.zero);
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(!IsMouseInsideScreen()) 
        {
            return;
        }
        if(_prefabInstance)
        {
            // Debug.Log(mousePositionAction.ReadValue<Vector2>());
            // Vector2 mousePos = mousePositionAction.ReadValue<Vector2>();

            Vector3 newPosition = GetMouseWorldPosition();// + offset;
            Debug.Log(GetMouseWorldPosition());

            if (Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl))
            {
                newPosition = _prefabInstance.transform.position;
                float deltaY = Input.mousePosition.y - initialY;
                newPosition.y = deltaY * yVelovity;
            }
            else
            {
                initialY = Input.mousePosition.y;
                newPosition.y = _prefabInstance.transform.position.y;

            }

            _prefabInstance.transform.position = newPosition;
            
        }

    }
    
    private void StartDrag(InputAction.CallbackContext context)
    {
        isDragging = true;
        initialY = _prefabInstance.transform.position.y;

    }

    private void EndDrag(InputAction.CallbackContext context)
    {
        isDragging = false;
    }


    Vector3 GetMouseWorldPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        float enter;
        if (dragPlane.Raycast(ray, out enter))
        {
            return ray.GetPoint(enter);
        }
        return Vector3.zero; // 레이캐스트 실패 시
    }

    bool IsMouseInsideScreen()
    {
        return Input.mousePosition.x >= 0 && Input.mousePosition.x <= Screen.width &&
               Input.mousePosition.y >= 0 && Input.mousePosition.y <= Screen.height;
    }

    


}
