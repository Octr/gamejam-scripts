[System.Serializable]
public class SaveData
{
    public int SaveSlot;
    public int MaxHealth;
    public int Health;
    public int Money;
    public float Damage;
    public string Name;
    public int Level;
    public float[] Position;

    public SaveData(PlayerData playerData)
    {
        SaveSlot = playerData.SaveSlot;
        MaxHealth = playerData.MaxHealth;
        Health = playerData.Health;
        Money = playerData.Money;
        Damage = playerData.Damage;
        Name = playerData.Name;
        Level = playerData.Level;

        //Vector3 or Color
        Position = new float[3];
        Position[0] = playerData.Position.x;
        Position[1] = playerData.Position.y;
        Position[2] = playerData.Position.z;
    }
}