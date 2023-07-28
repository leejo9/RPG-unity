using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIExample : MonoBehaviour
{
    public Animator playerAnimator;
    private int damage = 20;
    bool isHit = false;

    void Start()
    {
       // playerAnimator = GetComponentInParent<Animator>();
    }

    void Update()
    {

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (!isHit)
        {
            if (collision.gameObject.CompareTag("Player") && playerAnimator.GetBool("hit1"))
            {
                GameObject bodyParent = collision.gameObject;
                bodyParent.GetComponent<Player>().TakeDamage(damage);
                isHit = true;
            }
        }
        StartCoroutine(Begin());

    }
    IEnumerator Begin()
    {
        yield return new WaitForSeconds(2.3f);
        isHit = false;
    }
}
