using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MysteryBox : Target
{
   public int effectId;


    // Start is called before the first frame update


    // Update is called once per frame
    void Update() {}


    

    public void TriggerEffect()
    {
        Destroy(gameObject);
        Instantiate(explosiveParticle, transform.position, explosiveParticle.transform.rotation);
        effectManager.ShowEffect(effectId);
    }

    
}
