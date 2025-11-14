using UnityEngine;

public class Exit_Collision : MonoBehaviour
{

    public Collider2D[] moutainColliders;
    public Collider2D[] boundaryColliders;

    private void OTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            foreach (Collider2D moutain in moutainColliders)
            {
                moutain.enabled = true;
            }
            collision.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 15;
            
             foreach (Collider2D boundary in boundaryColliders)
            {
                boundary.enabled = false;
            }
            collision.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 15;
        }
    }
}