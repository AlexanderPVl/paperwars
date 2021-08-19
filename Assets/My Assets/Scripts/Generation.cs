using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Generation : MonoBehaviour
{
/*
    [Header("Tiling")]
    [SerializeField]
    private Transform tilePrefab;
    [SerializeField]
    private int offScreenTileCnt;
    [SerializeField]
    private float tileZ;
    [SerializeField]
    private float tileY;
    [SerializeField]
    private float sameTilesTreshold = 0.1f;

    private int halfScreenTileCnt;
    private float tileWidth;
    private Camera camera;
    private float leftScreenX;
    private float rightScreenX;
    private float centerX;
    private float previousGenerationCenterX;

    private List<Transform> instantiatedTiles;

    void Start()
    {
        camera = Camera.main;
        instantiatedTiles = new List<Transform>();

        tileWidth = tilePrefab.GetComponent<RectTransform>().localScale.x * tilePrefab.GetComponent<RectTransform>().rect.width;
        rightScreenX = camera.ViewportToWorldPoint(new Vector3(1f, 1f, camera.nearClipPlane)).x;
        leftScreenX = camera.ViewportToWorldPoint(new Vector3(0f, 0f, camera.nearClipPlane)).x;
        //centerX = GetNearestTilePosition(camera.ViewportToWorldPoint(new Vector3(0.5f, 1f, camera.nearClipPlane)).x);
        //previousGenerationCenterX = centerX;
        halfScreenTileCnt  = Mathf.FloorToInt((rightScreenX - centerX) / tileWidth) + offScreenTileCnt;

        Generate();
    }

    void Update()
    { 
        if (HeroSufficientlyMoved())
        {
            UpdatePosition();
            Generate();
        }
    }

    private float GetNearestTilePosition(float xPosition)
    {
        return tileWidth * Mathf.Round(xPosition / tileWidth);
    }

    private bool HeroSufficientlyMoved()
    {
        return Mathf.Abs(camera.ViewportToWorldPoint(new Vector3(0.5f, 1f, camera.nearClipPlane)).x - previousGenerationCenterX) > tileWidth * 0.5f;
    }

    private void Generate()
    {
        Debug.Log("tiles count: " + instantiatedTiles.Count.ToString());
        previousGenerationCenterX = centerX;

        deleteExcessiveTiles();
        spawnTiles(centerX);
    }
    private void UpdatePosition()
    {
        centerX = GetNearestTilePosition(camera.ViewportToWorldPoint(new Vector3(0.5f, 1f, camera.nearClipPlane)).x);
        rightScreenX = camera.ViewportToWorldPoint(new Vector3(1f, 1f, camera.nearClipPlane)).x;
        leftScreenX = camera.ViewportToWorldPoint(new Vector3(0f, 0f, camera.nearClipPlane)).x;
    }

    private void deleteExcessiveTiles()
    {
        for (int i = 0; i < instantiatedTiles.Count; ++i)
        {
            Transform tile = instantiatedTiles[i];
            if (tile.position.x < leftScreenX - (offScreenTileCnt + 1) * tileWidth || tile.position.x > rightScreenX + (offScreenTileCnt + 1) * tileWidth)
            {
                Destroy(tile.gameObject);
                instantiatedTiles.RemoveAt(i);
            }
        }
    }

    private void spawnTiles(Transform prefab, float centerx)
    {
        float curX = 0;
        for (int i = 0; i <= halfScreenTileCnt; ++i)
        {
            curX = centerx + i * tileWidth;
            spawnTile(curX);
        }
        for (int i = 1; i <= halfScreenTileCnt; ++i)
        {
            curX = centerx - i * tileWidth;
            spawnTile(curX);
        }
    }

    private void spawnTile(Transform prefab, float xCoord)
    {
        if (instantiatedTiles.TrueForAll(tl => (Mathf.Abs(tl.position.x - xCoord)) > sameTilesTreshold * tileWidth))
        {
            instantiatedTiles.Add(Instantiate(tilePrefab, new Vector3(xCoord, 0f, tileZ), Quaternion.identity));
        }
    }
*/
}
