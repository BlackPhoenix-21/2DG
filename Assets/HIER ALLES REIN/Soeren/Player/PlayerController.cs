using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterData characterData;

    private PlayerMovement movement;
    private PlayerCombat combat;

    void Awake()
    {
        movement = GetComponent<PlayerMovement>();
        combat = GetComponent<PlayerCombat>();

        movement.moveSpeed = characterData.movementStats.moveSpeed;
        combat.attack = characterData.combatStats.attack;
        combat.criticalChance = characterData.combatStats.criticalChance;
    }

    void Update()
    {
        HandleInput();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            combat.Attack();
        }
    }

    void HandleInput()
    {
        Vector3 dir = Vector3.zero;
        if (Input.GetKey(KeyCode.W)) dir = Vector3.forward;
        if (Input.GetKey(KeyCode.S)) dir = Vector3.back;
        if (Input.GetKey(KeyCode.A)) dir = Vector3.left;
        if (Input.GetKey(KeyCode.D)) dir = Vector3.right;
        movement.SetDirection(dir);
    }
}

