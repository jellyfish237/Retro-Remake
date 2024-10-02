using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wraparound : MonoBehaviour {

    private void Update() {
        Vector3 ViewportPosition = Camera.main.WorldToViewportPoint(transform.position);

        Vector3 moveAdjustment = Vector3.zero;
        if (ViewportPosition.x < 0) {
          moveAdjustment.x += 1;
        } else if (ViewportPosition.x > 1) {
          moveAdjustment.x -= 1;
        } else if (ViewportPosition.y < 0) {
          moveAdjustment.y += 1;
        } else if (ViewportPosition.y > 1) {
          moveAdjustment.y -= 1;
        }
        transform.position = Camera.main.ViewportToWorldPoint(ViewportPosition + moveAdjustment);
    }
}