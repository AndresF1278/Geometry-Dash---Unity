using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbs : MonoBehaviour
{
    public float forceJump;

  public enum TypeOrbs
    {
       purple,
       yellow,
       blue,
    }

    [SerializeField] public TypeOrbs typeOrb;
    public void EffectBlue(Rigidbody rb)
    {
         rb.gameObject.GetComponent<PlayerControllers>().ChangeGravityPlayer();
    }

    public void EffectYellow(Rigidbody rbPlayer)
    {
        if (Physics.gravity.y > 0)
        {
            rbPlayer.velocity = new Vector3(0, -forceJump * 1.5f, 0);
        }
        else
        {
            rbPlayer.velocity = new Vector3(0, forceJump * 1.5f, 0);
        }
    }
    public void EffectPurple(Rigidbody rbPlayer)
    {
        if (Physics.gravity.y > 0)
        {
            rbPlayer.velocity = new Vector3(0, -forceJump, 0);
        }
        else
        {
            rbPlayer.velocity = new Vector3(0, forceJump, 0);
        }
    }

    public void SelectMethodEffect( Rigidbody rb)
    {
        switch (typeOrb)
        {
            case TypeOrbs.purple:
                EffectPurple(rb);
                break;
            case TypeOrbs.yellow:
                EffectYellow(rb);
                break;
            case TypeOrbs.blue:
                EffectBlue(rb);
                break;
            default:
                break;
        }
    }


}
