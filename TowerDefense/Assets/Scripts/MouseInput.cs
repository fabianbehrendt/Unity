using UnityEngine;

public class MouseInput : MonoBehaviour
{
    public GameObject towerPrefab;
    public Transform towerParent;


    private Vector3 clickedLocation = -Vector3.one;
    private Camera cam;


    private void Awake()
    {
        cam = Camera.main;
    }


    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            clickedLocation = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            clickedLocation = -Vector3.one;
        }
    }


    private void FixedUpdate()
    {
        if (clickedLocation != -Vector3.one)
        {
            RaycastHit hit;
            Ray ray = cam.ScreenPointToRay(clickedLocation);
            int layerMask = 1 << LayerMask.NameToLayer("Tiles");

            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask))
            {
                if (hit.transform.name == "Tile")
                {
                    Tile tile = hit.transform.GetComponent<Tile>();

                    if (!tile.IsTaken)
                    {
                        tile.IsTaken = true;
                        Instantiate(towerPrefab, hit.transform.position, Quaternion.identity, towerParent);
                    }
                }
            }
        }
    }
}
