using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PilhaBehaviour : MonoBehaviour {
    [Range(1, 200)]
    public float recarga = 10;
    GameObject player;
    float rotacao = 1.5f;
    

    // Use this for initialization
    void Start () {
        player = GameObject.FindWithTag("Player");
        
	}

    void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown("e") && other.gameObject == player.gameObject)
        {
            player.GetComponent<LanternaBehaviour>().addCarga(recarga);
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update () {
        transform.Rotate(Vector2.up * rotacao);

    }
}
