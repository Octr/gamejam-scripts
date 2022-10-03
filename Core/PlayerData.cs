using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public static PlayerData instance;

    public int SaveSlot;

    public int MaxHealth = 3;
    public int Health = 3;
    public int Money;
    public float Damage = 1.0f;
    public string Name = "Player";
    public int Level;
    public Vector3 Position;

    public void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            //DontDestroyOnLoad(gameObject);
        }
    }

    public void DefaultData(int slot)
    {
        SaveSlot = slot;
        MaxHealth = 3;
        Health = MaxHealth;
        Money = 0;
        Damage = 1.0f;
        Level = 0;
        Name = "Player";
        Level = 0;

        Position = new Vector3(0f, 0f, 0f); //Insert Spawn Cords

    }

    public void SetData(int slot, SaveData data)
    {
        SaveSlot = data.SaveSlot;

        SaveSlot = data.SaveSlot;
        MaxHealth = data.MaxHealth;
        Health = data.Health;
        Money = data.Money;
        Damage = data.Damage;
        Name = data.Name;
        Level = data.Level;

        //Vector3 or Color
        Position = new Vector3(data.Position[0], data.Position[1], data.Position[2]);

    }
}