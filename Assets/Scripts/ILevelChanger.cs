using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILevelChanger
{
    void ChangeLevel();
    void EasyLevel();
    void MediumLevel();
    void HardLevel();
    void RestartLevel();
    void End();

}
