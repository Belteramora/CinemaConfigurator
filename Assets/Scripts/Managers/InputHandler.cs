using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public LayerMask selectableLayer;
    public UI ui;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(1))
        {
            var screenRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            if(Physics.Raycast(screenRay, out RaycastHit hitInfo, Camera.main.farClipPlane, selectableLayer))
            {
                var hittedObject = hitInfo.collider.gameObject.GetComponent<ItemObject>();
                ui.ShowContextMenuAtPosition(Input.mousePosition, hittedObject);
            }
            else
            {
                ui.HideContextMenu();
            }
        }

        if (Input.GetMouseButton(0))
            ui.HideContextMenu();
    }
}
