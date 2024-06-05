using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Orbs : MonoBehaviour
{
    public float forceJump;
    private Vector3 originalScale;
    private Vector3 scaleTo;
  public enum TypeOrbs
    {
       purple,
       yellow,
       blue,
    }

    private void Start()
    {
        originalScale = transform.localScale;
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
                rb.velocity = Vector3.zero;
                break;
            default:
                break;
        }
        GetComponent<BoxCollider>().enabled = false;
        Animation();
    }


    private void Animation()
    {

        scaleTo = originalScale * 1.3f;

        transform.DOScale(scaleTo, 0.1f)
       .OnComplete(() =>
       {


           transform.DOScale(originalScale, 0.5f);
       });

    }
}
