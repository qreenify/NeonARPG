using UnityEngine;

public class Obstacle : MonoBehaviour
{
   public Vector3 startPos;
   public Vector3 endPos;
   public float cycleTime;
   private float _currentTime;

   private void Update()
   {
      Move();
   }

   private void Move()
   {
      _currentTime += Time.deltaTime / cycleTime;
      transform.position = Vector3.Lerp(startPos, endPos, _currentTime);
      if (transform.position != endPos) return;
      var start = startPos;
      startPos = endPos;
      endPos = start;
      _currentTime = 0;
   }

   [ContextMenu("SaveStartPos")]
   private void SaveStartPos()
   {
      startPos = transform.position;
   }
   
   [ContextMenu("SaveEndPos")]
   private void SaveEndPos()
   {
      endPos = transform.position;
   }
}
