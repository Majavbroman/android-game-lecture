using System.Threading.Tasks;
using UnityEngine;

public interface IDataSaver
{
    Task SaveData(ref SaveData saveData);
}
