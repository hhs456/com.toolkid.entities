//using Codice.CM.Common;
//using System.Collections;
//using System.Collections.Generic;
//using Unity.Burst;
//using Unity.Collections;
//using Unity.Jobs;
//using UnityEngine;
//namespace Toolkid.Entities {
//    public class EntityController : MonoBehaviour {
//        [SerializeField]
//        private EntitySerialization serialization;
//        [SerializeField]
//        private EntityHolder[] holders;
//        [SerializeField]
//        private List<Transform> transforms;

//        private void OnValidate() {
//            serialization.Serialize(holders);
                    
//        }
//        private void Awake() {
//            transforms = new List<Transform>();
//            foreach (var component in serialization.Transforms) {
//                transforms.Add(holders[component.Index].transform);
//            }
//        }

//        private void FixedUpdate() {
//            TransformComponent[] updateTransform = new TransformComponent[transforms.Count];

//            foreach (TransformComponent args in updateTransform) {
                
//            }

//            TransformJob transformJob = new TransformJob {                
//                updateTransforms = updateTransform,
//                objectTransforms = transforms.ToArray(),
//            };

//            JobHandle handle = transformJob.Schedule(transforms.Count, 64);
//            handle.Complete();
//        }

//        private void TransformUpdate() {

//        }

//        private void PositionUpdate() { }
//        private void RotationUpdate() { }        
//    }

//    [BurstCompile]
//    struct TransformJob : IJobParallelFor {        
//        public TransformComponent[] updateTransforms;
//        public NativeArray<Transform> objectTransforms;

//        public void Execute(int index) {
//            objectTransforms[index].position = updateTransforms[index].Position;
//            objectTransforms[index].rotation = updateTransforms[index].Rotation;            
//        }
//    }
//}
