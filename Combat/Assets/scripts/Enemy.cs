using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    float attackTurnTime = 0.7f;
    int rotateSpeed = 120;
    int attackDistance = 17;
    int extraRunTime = 2;
    int punchDamage = 10;
    int baseShotDamage = 2;
    int attackSpeed = 1;
    int attackRotateSpeed = 20;
    float idleTime = 1.6f;
    Vector3 punchPosititon = new Vector3(0, 0, 0.7f);
    float punchRadius = 1.1f;
    float shootDistance = 10;
    AudioClip idleSound;
    AudioClip attackSound;
    private float attackAngle = 10;
    private bool isAttacking = false;
    private float lastAttack = 0;
    public Transform target;
    public CharacterController characterController;
    public Animator animator;
    float accuracy = 0.75f;
    private Vector3 spawnPosition;
    public ParticleSystem muzzleFlash;

    void Start ()
    {
        spawnPosition = transform.position;
        target = GameObject.Find("FirstPersonCharacter").transform;
        animator.Play("Standing");
    }

    private void Update()
    {
        if (idleSound && isAttacking)
        {
            //stop current audio and start other
        }
        Vector3 offset = transform.position - target.position;
        float angle = Vector3.Angle(offset, transform.forward);
        if (offset.magnitude < attackDistance)
        {
            Debug.Log("is close enough");
            isAttacking = true;
            if ((animator.GetCurrentAnimatorStateInfo(0).IsName("Standing") || offset.magnitude > punchRadius * 2) && !animator.GetCurrentAnimatorStateInfo(0).IsName("Shooting"))
            {
                animator.Play("Walking");
            }
            if (attackSound)
            {
                //stop audio and start attack sound
            }
            float time = 0;
            Vector3 direction;
            if (angle > 5 || time < attackTurnTime)
            {
                time += Time.deltaTime;
                transform.LookAt(target.transform);
                transform.rotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);
                direction = transform.TransformDirection(Vector3.forward * attackSpeed);
                characterController.SimpleMove(direction);
            }
            float timer = 0.0f;
            bool lostSight = false;
            if (timer < extraRunTime)
            {
                if (Mathf.Abs(angle) > 40)
                {
                    lostSight = true;
                }
                if (lostSight)
                {
                    timer += Time.deltaTime;
                }
                direction = transform.TransformDirection(Vector3.forward * attackSpeed);
                characterController.SimpleMove(direction);
                Vector3 pos = transform.TransformPoint(punchPosititon);
                if (Time.time > lastAttack + 0.5 && (pos - target.position).magnitude < punchRadius)
                {

                    animator.Play("Punching");
                    target.SendMessage("ApplyDamage", punchDamage);
                    lastAttack = Time.time;
                }
                else if (Time.time > lastAttack + 0.5  && (pos - target.position).magnitude < shootDistance)
                {
                    Shoot();
                    lastAttack = Time.time;
                }
            }
            isAttacking = false;
        }
        else
        {
            Debug.Log("is too far away");
            if (Time.time - lastAttack < 3)
            {
                animator.Play("Standing");
            }
            else
            {
                //go back to spawn and walk back and forth
            }
        }
        Debug.ClearDeveloperConsole();
    }

    void Shoot()
    {
        animator.Play("Shooting");
        if (Random.value > accuracy)
        {
            target.SendMessage("ApplyDamage", baseShotDamage + Random.Range(0, 5.01f));
        }
        muzzleFlash.Play();
    }

    #region oldcode
    /*IEnumerator StartFunction()
    {
        if (!target)
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }
        animator.Play("Standing");
        //yield return new WaitForSeconds(Random.value);
        while (true)
        {
            Debug.Log("starting loop");
            StartCoroutine(Idle());
            StartCoroutine(Attack());
        }
    }

    IEnumerator Idle()
    {
        animator.Play("Standing");
        if (idleSound)
        {
            //stop current audio and start other
        }
        Debug.Log("idling");
        yield return new WaitForSeconds(idleTime);
        while (true)
        {
            characterController.SimpleMove(Vector3.zero);
            Debug.Log("standing still");
            yield return new WaitForSeconds(0.2f);
            Vector3 offset = transform.position - target.position;
            if (offset.magnitude < attackDistance)
            {
                Debug.Log("is close enough");
                yield break;
            }
        }
    }

    private float RotateTowardsPosition(Vector3 targetpos)
    {
        Debug.Log("rotating");
        Vector3 relative = transform.InverseTransformPoint(targetpos);
        float angle = Mathf.Atan2(relative.x, relative.z) * Mathf.Rad2Deg;
        return angle;
    }

    IEnumerator Attack()
    {
        isAttacking = true;
        if (attackSound)
        {
            //stop audio and start attack sound
        }
        animator.CrossFade("Walking", 0);
        float angle = 180.0f;
        float time = 0;
        Vector3 direction;
        while (angle > 5 || time < attackTurnTime)
        {
            time += Time.deltaTime;
            angle = Mathf.Abs(RotateTowardsPosition(target.position));
            float move = Mathf.Clamp01((90 - angle) / 90);
            direction = transform.TransformDirection(Vector3.forward * attackSpeed * move);
            characterController.SimpleMove(direction);
        }
        float timer = 0.0f;
        bool lostSight = false;
        while (timer < extraRunTime)
        {
            angle = RotateTowardsPosition(target.position);
            if (Mathf.Abs(angle) > 40)
            {
                lostSight = true;
            }
            if (lostSight)
            {
                timer += Time.deltaTime;
            }
            direction = transform.TransformDirection(Vector3.forward * attackSpeed);
            characterController.SimpleMove(direction);
            Vector3 pos = transform.TransformPoint(punchPosititon);
            if (Time.time > lastPunchTime + 0.3 && (pos - target.position).magnitude < punchRadius)
            {
                target.SendMessage("ApplyDamage", damage);
                lastPunchTime = Time.time;
            }
            if (characterController.velocity.magnitude < attackSpeed * 0.3)
            {
                break;
            }
        }
        isAttacking = false;
        animator.CrossFade("Standing", 0);
        yield break;
    }*/
#endregion

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.TransformPoint(punchPosititon), punchRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackDistance);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, shootDistance);
    }
}
