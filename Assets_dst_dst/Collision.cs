using System.Collections.ObjectModel;
using UnityEngine;

public class Collision : MonoBehaviour
{

    public Collider2D[] moutainColliders;
    public Collider2D[] boundaryColliders;

    private void OTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            foreach (Collider2D moutain in moutainColliders)
            {
                moutain.enabled = false;
            }
            collision.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 15;
            
             foreach (Collider2D boundary in boundaryColliders)
            {
                boundary.enabled = true;
            }
            collision.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 15;
        }
    }
}