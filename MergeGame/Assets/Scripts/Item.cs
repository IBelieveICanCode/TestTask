using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [Header("Item characteristics")]
    public int 
        Index,
        Level,
        Score;

    private Vector3 previousPosition;
    private Vector3 mouseOffset;


    private void OnMouseDown()
    {
        mouseOffset = gameObject.transform.position - GetMouseWorldPos();
        previousPosition = transform.position;
    }

    private void OnMouseDrag()
    {
        transform.position = GetMouseWorldPos() + mouseOffset;
    }

    private void OnMouseUp()
    {
        AlignOnGrid();
        CheckSimilarities();
    }

    private Vector3 GetMouseWorldPos()
    {
        Vector3 mouseInput = Input.mousePosition;
        return Camera.main.ScreenToWorldPoint(mouseInput);
    }

    private void AlignOnGrid()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        { 
            if (hit.collider.tag == "Map")
            {
                Vector3 alignedPosition = transform.position;
                alignedPosition.x = Mathf.Round(transform.position.x);
                alignedPosition.y = Mathf.Round(transform.position.y);
                transform.position = alignedPosition;
            }
        }
        else
            transform.position = previousPosition;
    }

    private void CheckSimilarities()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.tag == "Item" && name == hit.collider.name)
                GameController.Instance.itemController.UpgradeElement(this, hit.collider.GetComponent<Item>());
            else
                transform.position = previousPosition;
        }
    }   
}
