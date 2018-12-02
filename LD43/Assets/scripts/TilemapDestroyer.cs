using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilemapDestroyer : MonoBehaviour {

    void OnTriggerEnter2D(Collider2D col) {
        if (col is TilemapCollider2D) {
            
        }
    }
}
