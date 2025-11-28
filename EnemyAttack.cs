using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour
{
    private Animator animator;
    public Transform playerTargetTransform;

    // Use a hash for safety and performance
    private static readonly int AttackTriggerHash = Animator.StringToHash("Attack");

    [Header("Slide Properties")]
    [SerializeField] private float slideSpeed = 15f;
    [SerializeField] private float attackDistanceOffset = 1f;
    [SerializeField] private float attackWaitTime = 1f;

    private Vector3 startingPosition;
    private const float REACH_DISTANCE = 0.1f; // Changed to a smaller value for precision

    void Start()
    {
        animator = GetComponent<Animator>();
        startingPosition = transform.position;

        if (animator == null)
        {
            Debug.LogError("EnemyAttack requires an Animator component.");
        }
    }

    public void StartEnemyAttack()
    {
        // If an attack is already running, prevent starting a new one
        if (playerTargetTransform != null)
        {
            StartCoroutine(Co_EnemyAttackSequence());
        }
        else
        {
            Debug.LogError("Cannot start attack: playerTargetTransform is null.");
        }
    }

    private IEnumerator Co_EnemyAttackSequence()
    {
        yield return new WaitForSeconds(0.3f); // Delay before slime moves

        // --- 1. Slide toward player ---
        Vector3 dir = (playerTargetTransform.position - startingPosition).normalized;
        Vector3 slideTarget = playerTargetTransform.position - dir * attackDistanceOffset;

        yield return StartCoroutine(Co_SlideToTarget(slideTarget));

        // --- 2. Attack animation ---
        animator.SetTrigger(AttackTriggerHash); // Using the robust Hash ID
        Debug.Log("Slime attacking player...");

        yield return new WaitForSeconds(0.4f); // Hit frame timing
        Debug.Log("Player takes damage!");

        yield return new WaitForSeconds(attackWaitTime);

        // --- 3. Slide back ---
        yield return StartCoroutine(Co_SlideToTarget(startingPosition));

        Debug.Log("Enemy attack finished. Player's turn.");
    }

    private IEnumerator Co_SlideToTarget(Vector3 targetPos)
    {
        // Use a small constant for precise reaching
        while (Vector3.Distance(transform.position, targetPos) > REACH_DISTANCE)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                targetPos,
                slideSpeed * Time.deltaTime
            );

            yield return null;
        }

        // Snap to the final position to avoid the loop exiting slightly short
        transform.position = targetPos;
    }
}