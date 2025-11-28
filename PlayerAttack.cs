using UnityEngine;
using System.Collections;

public class PlayerAttackController : MonoBehaviour
{
    private Animator animator;
    private bool isAttacking = false;

    private Vector3 startingPosition;

    [Header("Attack Configuration")]
    public Transform enemyTargetTransform;
    private EnemyAttack enemyAttackScript;

    [Header("Slide Properties")]
    [SerializeField] private float slideSpeed = 20f;
    [SerializeField] private float attackDistanceOffset = 2.0f;
    private const float REACH_DISTANCE = 0.1f;   // More accurate stopping distance

    void Start()
    {
        animator = GetComponent<Animator>();
        startingPosition = transform.position;

        if (enemyTargetTransform != null)
        {
            enemyAttackScript = enemyTargetTransform.GetComponent<EnemyAttack>();
        }
    }

    // Called when UI button is pressed
    public void OnAttackButton()
    {
        TryAttack();
    }

    private void TryAttack()
    {
        if (isAttacking) return;

        isAttacking = true;
        StartCoroutine(SlideAttackSequence());
    }

    private IEnumerator SlideAttackSequence()
    {
        if (enemyTargetTransform == null)
        {
            Debug.LogError("Enemy Target is not assigned!");
            EndAttack();
            yield break;
        }

        // --- 1. Move toward enemy ---
        Vector3 direction = (enemyTargetTransform.position - startingPosition).normalized;
        Vector3 attackPos = enemyTargetTransform.position - direction * attackDistanceOffset;

        yield return StartCoroutine(SlideToTarget(attackPos));

        // --- 2. Play attack animation ---
        animator.SetTrigger("Attack");
        Debug.Log("Player attack animation triggered");

        // Hit frame (adjust timing later)
        yield return new WaitForSeconds(0.4f);

        Debug.Log("Player deals damage");

        // Finish full animation
        yield return new WaitForSeconds(0.6f);

        // --- 3. Return to idle position ---
        yield return StartCoroutine(SlideToTarget(startingPosition));

        // --- 4. End Player Turn ---
        EndAttack();
    }

    private IEnumerator SlideToTarget(Vector3 targetPos)
    {
        while (Vector3.Distance(transform.position, targetPos) > REACH_DISTANCE)
        {
            transform.position = Vector3.MoveTowards(
                transform.position,
                targetPos,
                slideSpeed * Time.deltaTime
            );

            yield return null;
        }

        transform.position = targetPos;
    }

    private void EndAttack()
    {
        isAttacking = false;
        Debug.Log("Player turn finished");

        // Start slime’s turn AFTER the player fully finishes
        if (enemyAttackScript != null)
        {
            enemyAttackScript.StartEnemyAttack();
        }
    }
}
