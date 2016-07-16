using UnityEngine;
using System.Collections;

public class AirplaneHit : MonoBehaviour {

    public GameObject explosion;

	void OnTriggerEnter()
    {
        Instantiate(explosion, transform.position, Quaternion.identity);
        Destroy(explosion);
    }
}
