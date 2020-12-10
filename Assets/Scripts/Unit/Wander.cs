using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace Unit
{
   public class Wander : UnitAction
   {
      public float wanderSpeed = 5;
      public float wanderRange = 20;
      public float coolDown = 3;
      private float _currentCooldown;
      private float _originalSpeed;
      private Vector3 _startPos;

      
      private void Start()
      {
         _startPos = transform.position;
      }
      public override bool IsPossible()
      {
         return true;
      }

      public override bool Enter()
      {
         _originalSpeed = GetComponent<NavMeshAgent>().speed;
         GetComponent<NavMeshAgent>().speed = wanderSpeed;
         return true;
      }

      private void Update()
      {
         if (_currentCooldown > 0) _currentCooldown -= Time.deltaTime;
      }

      public override bool DoUpdate()
      {
         if (_currentCooldown <= 0) Move();
         return true;
      }

      private void Move()
      {
         _currentCooldown = coolDown;
         var x = _startPos.x + Random.Range(-wanderRange, wanderRange);
         var z = _startPos.z + Random.Range(-wanderRange, wanderRange);
         var movePos = new Vector3(x, transform.position.y, z);
         unit.MoveTo(_startPos + movePos);
      }

      public override bool Exit()
      {
         GetComponent<NavMeshAgent>().speed = _originalSpeed;
         return true;
      }

      //private void OnDrawGizmos()
      //{
      //   if (!EditorApplication.isPlaying)
      //      Gizmos.DrawWireCube(transform.position, new Vector3(wanderRange, 0.1f, wanderRange) * 2);
      //   else
      //      Gizmos.DrawWireCube(_startPos, new Vector3(wanderRange, 0.1f, wanderRange) * 2);
      //}
   }
}

