using UnityEngine;
using System.Collections;

public interface iController {
   
     void Init( ScriptableObject setting,GameObject sender =null);
     void OnClose();

}
