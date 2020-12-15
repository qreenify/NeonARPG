using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Unit {
    public class DrawAttackLine : MonoBehaviour
    {
        public LineRenderer lineRenderer;
        private Vector3 playerPosition;
        private Vector3 EnemyPosition;

        private Unit unit;
        // Start is called before the first frame update
        private void Awake()
        {
            lineRenderer = lineRenderer.GetComponent<LineRenderer>();
            lineRenderer.positionCount = 2;
            unit = GetComponent<Unit>();
        }

        public void DrawLine (Transform transform)
        {
            playerPosition.x = transform.position.x;
            playerPosition.y = 0f;
            playerPosition.z = transform.position.z;

            lineRenderer.SetPosition(0, new Vector3(playerPosition.x, playerPosition.y, playerPosition.z));
            EnemyPosition = transform.position;
            lineRenderer.SetPosition(1, new Vector3(EnemyPosition.x, EnemyPosition.y, EnemyPosition.z));
        }
       
    }
}