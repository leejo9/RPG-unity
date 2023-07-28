using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public InventoryObject inventory;
    public InventoryObject equipment;
    private GroundItem hi;
    public GameObject hand;
    public Attribute[] attributes;
    public int maxHealth = 100;
    public int currentHealth;
    private Animator animator;
    public Health healthBar;
    private bool isAttacking = false;
    //public MouseItem mouseItem = new MouseItem();
    void Update()
    {
        if (hi!=null&&Input.GetKeyDown(KeyCode.E))
        {
            Destroy(hi);
            Destroy(hi.transform.parent.gameObject);
            //Destroy(hi.transform.parent.parent.gameObject);

            inventory.AddItem(new Item(hi.item), 1);
        }
        if (Input.GetMouseButtonDown(0))
        {
            OnClick();
        }
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        currentHealth = maxHealth;
        for (int i = 0; i < attributes.Length; i++)
        {
            attributes[i].SetParent(this);
        }
        for (int i = 0; i < equipment.GetSlots.Length; i++)
        {
            equipment.GetSlots[i].OnBeforeUpdate += OnBeforeSlotUpdate;
            equipment.GetSlots[i].OnAfterUpdate += OnAfterSlotUpdate;
        }
    }
    void OnClick()
    {
        animator.SetTrigger("atk");
    }
    IEnumerator InitializeAttack()
    {
        yield return new WaitForSeconds(0.1f);
        isAttacking = true;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);
        if (currentHealth < 0)
        {
            Invoke("Destroyyy", 0f);
        }
    }
    private void Destroyyy()
    {
        SceneManager.LoadScene("Loss");
        inventory.Container.Clear();
    }
    public void OnBeforeSlotUpdate(InventorySlot _slot)
    {
        if (_slot.ItemObject == null)
            return;
        switch (_slot.parent.inventory.type)
        {
            case InterfaceType.Inventory:
                break;
            case InterfaceType.Equipment:
                print(string.Concat("Removed ", _slot.ItemObject, " on ", _slot.parent.inventory.type, ", Allowed Items: ", string.Join(", ", _slot.AllowedItems)));

                for (int i = 0; i < _slot.item.buffs.Length; i++)
                {
                    for (int j = 0; j < attributes.Length; j++)
                    {
                        if (attributes[j].type == _slot.item.buffs[i].attribute)
                            attributes[j].value.RemoveModifier(_slot.item.buffs[i]);
                    }
                }

                break;
            case InterfaceType.Chest:
                break;
            default:
                break;
        }
    }
    public void OnAfterSlotUpdate(InventorySlot _slot)
    {
        if (_slot.ItemObject == null)
            return;
        switch (_slot.parent.inventory.type)
        {
            case InterfaceType.Inventory:
                break;
            case InterfaceType.Equipment:
                print(string.Concat("Placed ", _slot.ItemObject, " on ", _slot.parent.inventory.type, ", Allowed Items: ", string.Join(", ", _slot.AllowedItems)));

                for (int i = 0; i < _slot.item.buffs.Length; i++)
                {
                    for (int j = 0; j < attributes.Length; j++)
                    {
                        if (attributes[j].type == _slot.item.buffs[i].attribute)
                            attributes[j].value.AddModifier(_slot.item.buffs[i]);
                    }
                }

                break;
            case InterfaceType.Chest:
                break;
            default:
                break;
        }
    }
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("chest"))
        {
            SceneManager.LoadScene("Win");
            inventory.Container.Clear();

        }
    }
    public void OnTriggerEnter(Collider other) {

        var item = other.GetComponent<GroundItem>();
        if (item)
        {
            hi = item;

        }

    }
   public void AttributeModified(Attribute attribute)
    {
        Debug.Log(string.Concat(attribute.type, " was updated! Value is now ", attribute.value.ModifiedValue));
    }
    private void OnApplicationQuit()
    {
        inventory.Container.Clear();
    }
}
[System.Serializable]
public class Attribute
{
    [System.NonSerialized]
    public Player parent;
    public Attributes type;
    public ModifiableInt value;

    public void SetParent(Player _parent)
    {
        parent = _parent;
        value = new ModifiableInt(AttributeModified);
    }
    public void AttributeModified()
    {
        parent.AttributeModified(this);
    }
}