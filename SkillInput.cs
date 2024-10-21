using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public interface SkillInput
{
    void Attack(InputAction.CallbackContext context);
    void ElementalSkill(InputAction.CallbackContext context);
    void ElementalBurst(InputAction.CallbackContext context);
}



