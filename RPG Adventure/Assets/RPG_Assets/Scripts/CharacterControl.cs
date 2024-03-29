using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG
{
    public enum TransitionParamter
    {
        Move,
        Jump,
        ForceTransition,
        Grounded,
        Attack,
    }
    public class CharacterControl : MonoBehaviour
    {
        public Animator SkinnedMeshAnimator;
        public bool MoveRight;
        public bool MoveLeft;
        public bool MoveForward;
        public bool MoveBackward;
        public bool Jump;
        public bool Attack;

        public GameObject ColliderEdgePrefab;
        public List<GameObject> BottomSpheres = new List<GameObject>();
        public List<GameObject> FrontSpheres = new List<GameObject>();
        public List<Collider> RagdollParts = new List<Collider>();
        public List<Collider> CollidingParts = new List<Collider>();


        private Rigidbody rigid;

        private void OnTriggerEnter(Collider other)
        {
            if (RagdollParts.Contains(other))
            {
                return;
            }
            CharacterControl control = other.transform.root.GetComponent<CharacterControl>();
            if (control == null)
            {
                return;
            }
            if (other.gameObject == control.gameObject)
            {
                return;
            }
            if (!CollidingParts.Contains(other))
            {
                CollidingParts.Add(other);
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (CollidingParts.Contains(other))
            {
                CollidingParts.Remove(other); 
            }
        }
        public Rigidbody RIGID_BODY
        {
            get
            {
                if (rigid == null)
                {
                    rigid = GetComponent<Rigidbody>();
                }
                return rigid;
            }
        }
        private void Awake()
        {
            bool SwitchBack = false;
            if (!IsFacingForward())
            {
                SwitchBack = true;
            }
            FaceForward(true);
            SetRagdollParts();
            SetColliderSpheres();
            if (SwitchBack)
            {
                FaceForward(false);
            }
        }
        private void SetRagdollParts()
        {
            Collider[] colliders = this.gameObject.GetComponentsInChildren<Collider>();
            foreach(Collider c in colliders)
            {
                if(c.gameObject != this.gameObject)
                {
                    c.isTrigger = true;
                    RagdollParts.Add(c);
                }

            }
        }

       /* private IEnumerator Start()
        {
            yield return new WaitForSeconds(5f);
            RIGID_BODY.AddForce(200f * Vector3.up);
            yield return new WaitForSeconds(0.5f);
            TurnOnRagdoll();
        }*/
        public void TurnOnRagdoll()
        {
            RIGID_BODY.useGravity = false;
            RIGID_BODY.velocity = Vector3.zero;
            this.gameObject.GetComponent<BoxCollider>().enabled = false;
            SkinnedMeshAnimator.enabled = false;
            SkinnedMeshAnimator.avatar = null;

            foreach(Collider c in RagdollParts)
            {
                c.isTrigger = false;
                c.attachedRigidbody.velocity = Vector3.zero;
            }
        }
        private void SetColliderSpheres()
        {
            BoxCollider box = GetComponent<BoxCollider>();
            float bottom = box.bounds.center.y - box.bounds.extents.y;
            float top = box.bounds.center.y + box.bounds.extents.y;
            float front = box.bounds.center.z + box.bounds.extents.z;
            float back = box.bounds.center.z - box.bounds.extents.z;

            GameObject bottomFront = CreateEdgeSphere(new Vector3(this.transform.position.x, bottom, front));
            GameObject bottomBack = CreateEdgeSphere(new Vector3(this.transform.position.x, bottom, back));
            GameObject topFront = CreateEdgeSphere(new Vector3(this.transform.position.x, top, front));

            bottomFront.transform.parent = this.transform;
            bottomBack.transform.parent = this.transform;
            topFront.transform.parent = this.transform;

            BottomSpheres.Add(bottomFront);
            BottomSpheres.Add(bottomBack);

            FrontSpheres.Add(bottomFront);
            FrontSpheres.Add(topFront);

            float sec = (bottomFront.transform.position - bottomBack.transform.position).magnitude / 5f;
            CreateMiddleSpheres(bottomFront, -this.transform.forward, sec, 4, BottomSpheres);

            float versec = (bottomFront.transform.position - topFront.transform.position).magnitude / 10f;
            CreateMiddleSpheres(topFront, -this.transform.up, versec, 9, FrontSpheres);

            for (int x = 0; x < 4; x++)
            {
                Vector3 pos = bottomBack.transform.position + (Vector3.forward * sec * (x+1));

                GameObject newObj = CreateEdgeSphere(pos);
                newObj.transform.parent = this.transform;
                BottomSpheres.Add(newObj);
            }
        }

        public void CreateMiddleSpheres(GameObject start, Vector3 dir,  float sec, int iterations, List<GameObject> spheresList)
        {
            for (int x = 0; x < iterations; x++)
            {
                Vector3 pos = start.transform.position + (dir * sec * (x + 1));

                GameObject newObj = CreateEdgeSphere(pos);
                newObj.transform.parent = this.transform;
                BottomSpheres.Add(newObj);
            }
        }

        public GameObject CreateEdgeSphere(Vector3 pos)
        {
            GameObject obj = Instantiate(ColliderEdgePrefab, pos, Quaternion.identity);
            return obj;
        }
        public void MoveFront(float Speed, float SpeedGraph)
        {
            transform.Translate(Vector3.forward * Speed * SpeedGraph * Time.deltaTime);

        }
        public void FaceForward(bool forward)
        {
            if (forward)
            {
                transform.rotation = Quaternion.Euler(0f, 0f, 0f);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0f, 180f, 0f);
            }
        }
        public bool IsFacingForward()
        {
            if (transform.forward.z > 0f)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
