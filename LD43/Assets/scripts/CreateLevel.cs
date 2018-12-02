using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class CreateLevel : MonoBehaviour {

    public Tile tile;
    public int platformGap;
    public int platformOffset;

    public MantisMove player;
    public GameObject mantisPrefab;

    Tilemap tilemap;
    int offset;

    Vector3Int positionLeftLast;
    Vector3Int positionRightLast;

	void Start () {
        tilemap = GetComponent<Tilemap>();
        //tilemap.SetTile(new Vector3Int(tilemap.cellBounds.position.x, tilemap.cellBounds.position.y, 0), tile);

        Ray ray = Camera.main.ViewportPointToRay(new Vector2(0, 0));
        // get the collision point of the ray with the z = 0 plane
        Vector3 worldPoint = ray.GetPoint(-ray.origin.z / ray.direction.z);
        Vector3Int position = tilemap.WorldToCell(worldPoint);

        offset = position.y % platformGap - platformOffset;
	}
	
	void FixedUpdate () {
        Ray ray = Camera.main.ViewportPointToRay(new Vector2(0.01f, 0));
        Vector3 worldPoint = ray.GetPoint(-ray.origin.z / ray.direction.z);
        Vector3Int position1 = tilemap.WorldToCell(worldPoint);

        ray = Camera.main.ViewportPointToRay(new Vector2(0.99f, 0));
        worldPoint = ray.GetPoint(-ray.origin.z / ray.direction.z);
        Vector3Int position2 = tilemap.WorldToCell(worldPoint);

        if (positionLeftLast != position1 || positionRightLast != position2) {
            int maxY = position1.y;

            for (int y = positionLeftLast.y - 1; y >= maxY; y--) {
                position1.y = y;
                position2.y = y;

                tilemap.SetTile(position1, tile);
                tilemap.SetTile(position2, tile);

                if ((position1.y + offset) % platformGap == 0) {

                    if ((position1.y + offset) % (platformGap * 3) == 0) {
                        Vector3Int position = new Vector3Int((position1.x + position2.x) / 2, position1.y, position1.z);
                        GiveEnergy energyGiver = Instantiate(mantisPrefab, tilemap.CellToWorld(position) + Vector3.up * 1.5f, Quaternion.identity).GetComponent<GiveEnergy>();
                        energyGiver.player = player;
                    }

                    int gap = Random.Range(position1.x + 1, position2.x - 1);
                    for (int x = position1.x + 1; x < position2.x; x++) {
                        if (x == gap || x == gap + 1) continue;
                        Vector3Int position = new Vector3Int(x, position1.y, position1.z);
                        tilemap.SetTile(position, tile);
                    }
                }
            }

        }

        ray = Camera.main.ViewportPointToRay(new Vector2(0, 1.1f));
        worldPoint = ray.GetPoint(-ray.origin.z / ray.direction.z);
        Vector3Int limit = tilemap.WorldToCell(worldPoint);
        for (int x = position1.x; x <= position2.x; x++) tilemap.SetTile(new Vector3Int(x, limit.y, limit.z), null);

        positionLeftLast = position1;
        positionRightLast = position2;
	}
}
