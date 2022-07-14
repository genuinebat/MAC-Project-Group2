// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using Vuforia;

// namespace Malorant
// {
//     public class FPSEnemy : MonoBehaviour
//     {
//         [HideInInspector]
//         public Transform ImageTarget;

//         public Malware Type;
//         public float MaxHealth;
//         public float Speed;

//         public float Health { get; private set; }

//         Vector3 targetLocation;
//         float minX, maxX, minY, maxY, minZ, maxZ;

//         void Start()
//         {
//             Health = MaxHealth;

//             SetBoundaries();
//             StartAtRandomPosition();

//             StartCoroutine(PeriodicallySetBoundaries());

//             targetLocation = transform.position;
//         }

//         void Update()
//         {
//             if (Health <= 0)
//             {
//                 Destroy(gameObject);
//             }

//             if (Vector3.Distance(transform.position, targetLocation) < 0.01f)
//             {
//                 SetNewRandomTargetLocation();
//             }
//             else 
//             {
//                 MoveToTargetLocation();
//             }
//         }

//         public void TakeDamage(bool correctWeapon)
//         {
//             Health -= correctWeapon ? 10 : 5;
//             StartCoroutine(RunAway());
//         }

//         void SetBoundaries()
//         {
//             minX = ImageTarget.position.x - 5;
//             maxX = ImageTarget.position.x + 5;
//             minY = ImageTarget.position.y -  5;
//             maxY = ImageTarget.position.y + 5;
//             minZ = ImageTarget.position.z - 3;
//             maxZ = ImageTarget.position.z;
//         }

//         IEnumerator PeriodicallySetBoundaries()
//         {
//             for (;;)
//             {
//                 yield return new WaitForSeconds(.5f);
//                 SetBoundaries();
//             }
//         }

//         void StartAtRandomPosition()
//         {
//             transform.position =
//                 new Vector3(
//                     Random.Range(minX, maxX),
//                     Random.Range(minY, maxY),
//                     Random.Range(minZ, maxZ)
//                 );
//         }

//         void MoveToTargetLocation()
//         {
//             transform.position = 
//                 Vector3.MoveTowards(
//                     transform.position,
//                     targetLocation, 
//                     Speed * Time.deltaTime
//                 );
//         }

//         void SetNewRandomTargetLocation()
//         {
//             targetLocation =
//                 new Vector3(
//                     Random.Range(minX, maxX),
//                     Random.Range(minY, maxY),
//                     Random.Range(minZ, maxZ)
//                 );
//         }

//         IEnumerator RunAway()
//         {
//             float originalSpeed = Speed;
//             Speed *= 8;

//             yield return new WaitForSeconds(1f);

//             Speed = originalSpeed;
//         }
//     }
// }
