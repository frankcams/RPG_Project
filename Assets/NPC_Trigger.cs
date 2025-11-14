using UnityEngine;

public class NPCTrigger : MonoBehaviour
{
    private NPC npc;
    private QuestGiver questGiver;
    private ShopKeeper shopKeeper; // Optional: if you add a shop system

    private bool playerInRange = false;

    void Start()
    {
        npc = GetComponent<NPC>();
        questGiver = GetComponent<QuestGiver>();
        shopKeeper = GetComponent<ShopKeeper>(); // Optional
    }

    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            InteractWithNPC();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            Debug.Log("Press E to interact.");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    void InteractWithNPC()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        CharacterStats playerStats = player.GetComponent<CharacterStats>();

        npc?.Interact();
        questGiver?.GiveQuest(playerStats);
        shopKeeper?.OpenShop(); // Optional
    }
}