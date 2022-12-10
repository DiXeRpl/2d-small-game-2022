using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Referencje
    private PlayManager playManager;
    public CharacterController characterController;

    private void Awake()
    {
        if (playManager == null)
        {
            playManager = FindObjectOfType<PlayManager>();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);

        if (move != Vector3.zero)
        {
            characterController.Move(move * playManager.speed * Time.deltaTime);
        }
    }

    /*
     * Kolizja playera 
     */

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag == ("Enemy"))
        {
            // Zmiana tagu 
            hit.gameObject.tag = ("Untagged");

            // Przekazanie info o kolizji z playerem i zatrzymaniu
            hit.gameObject.GetComponent<Enemy>().StopMe();

        }
    }
}
