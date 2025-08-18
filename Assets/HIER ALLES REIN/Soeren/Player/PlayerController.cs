using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [HideInInspector]
    public CharacterData characterData;

    private PlayerMovement movement;
    private PlayerCombat combat;

    void Start()
    {
        movement = GetComponent<PlayerMovement>();
        combat = GetComponent<PlayerCombat>();

        characterData = GameManager.instance.playerData;
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
        Vector2 dir = Vector2.zero;
        if (Input.GetKey(KeyCode.W)) dir = Vector2.up;
        if (Input.GetKey(KeyCode.S)) dir = Vector2.down;
        if (Input.GetKey(KeyCode.A)) dir = Vector2.left;
        if (Input.GetKey(KeyCode.D)) dir = Vector2.right;
        movement.SetDirection(dir);
    }
}

