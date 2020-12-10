public interface ISaveable
{
    bool Deserialize(string save);
    string Serialize();
}