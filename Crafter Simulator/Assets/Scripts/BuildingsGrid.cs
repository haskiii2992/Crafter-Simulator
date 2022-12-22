using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class BuildingsGrid : MonoBehaviour
{
    public Vector2Int GridSize = new Vector2Int(10, 10);

    private Buildings[,] grid;
    private Buildings FlyingBuildings;
    private Camera MainCamera;

    private bool MovementMode = true;

    private void Awake()
    {
        grid = new Buildings[GridSize.x, GridSize.y];

        MainCamera = Camera.main;
    }

    public void StartPlacingBuildings(Buildings BuildingPrefab)
    {
        MovementMode = false;
        EventBus.OnGoing?.Invoke(MovementMode);
        if (FlyingBuildings != null)
        {
            Destroy(FlyingBuildings.gameObject);
        }

        FlyingBuildings = Instantiate(BuildingPrefab);
    }

    private void Update()
    {
        if (FlyingBuildings != null)
        {
            MovementMode = false;
            EventBus.OnGoing?.Invoke(MovementMode);
            var groundPlane = new Plane(Vector3.up, Vector3.zero);
            Ray ray = MainCamera.ScreenPointToRay(Input.mousePosition);

            if (groundPlane.Raycast(ray, out float position))
            {
                Vector3 WorldPosition = ray.GetPoint(position);

                int y = Mathf.RoundToInt(WorldPosition.z);
                int x = Mathf.RoundToInt(WorldPosition.x);

                bool available = true;

                if (x < 0 || x > GridSize.x - FlyingBuildings.Size.x) available = false;
                if (y < 0 || y > GridSize.x - FlyingBuildings.Size.y) available = false;

                if (available && IsPlaceTaken(x, y)) available = false;

                FlyingBuildings.transform.position = new Vector3(x, 0, y);
                FlyingBuildings.SetTransperent(available);

                if (available && Input.GetMouseButtonDown(0))
                {                   
                    PlaceFlyingBuild(x, y);
                }

            }
        }
    }

    private bool IsPlaceTaken(int placeX, int placeY)
    {
        for (int x = 0; x < FlyingBuildings.Size.x; x++)
        {
            for (int y = 0; y < FlyingBuildings.Size.x; y++)
            {
                if(grid[placeX + x, placeY + y] !=  null) return true;
            }
        }

        return false;
    }

    public void PlaceFlyingBuild(int placeX, int placeY)
    {
        for (int x = 0; x < FlyingBuildings.Size.x; x++)
        {
            for (int y = 0; y < FlyingBuildings.Size.x; y++)
            {
                grid[placeX + x, placeY + y] = FlyingBuildings;
            }
        }

        FlyingBuildings.SetNormal();
        FlyingBuildings = null;

        MovementMode = true;
        StartCoroutine(constructionModeSend());
    }

    IEnumerator constructionModeSend()
    {
        yield return new WaitForSeconds(0.1f);
        EventBus.OnGoing?.Invoke(MovementMode);
    }
}
