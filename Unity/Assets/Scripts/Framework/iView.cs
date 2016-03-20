using UnityEngine;
using System.Collections;

public interface iView<T>
    where T : ScriptableObject
{
    
    void Init(T so);
    void UpdateView();
    void Close();
}
