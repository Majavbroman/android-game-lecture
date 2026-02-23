using System.Threading.Tasks;
using UnityEngine;

public interface IUIObject<T>
{
    void Show();
    void Hide();
    void Refresh(T data);
}
