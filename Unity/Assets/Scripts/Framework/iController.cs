using UnityEngine;
using System.Collections;

public interface iController {

     void Init();
     void OnClose();
    iView GetView();

}
