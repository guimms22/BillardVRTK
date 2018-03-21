using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChalkTriggerParticle : MonoBehaviour {

    public ParticleSystem chalk ;
    
    // Use this for initialization
    void Start()
    {
        chalk = GetComponent<ParticleSystem>();
    }

        

	
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerStay(Collider other)
    {
        Debug.Log("Hey mon ami:" + other);
        if (other.tag == "cuetip")
            chalk.Play();
        //chalk.Emit(100); 
    }
    private void OnTriggerExit(Collider other)
    {
       // chalk.Stop();
    }


}

