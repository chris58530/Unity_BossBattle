using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossState_0 : StateMachineBehaviour
{
    int mode = 0;

    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        BossController.instance.followPlayer = true;
        BossController.instance.canShoot = true;
    }

    override public void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
       
    }

    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

    }
  
    
}
