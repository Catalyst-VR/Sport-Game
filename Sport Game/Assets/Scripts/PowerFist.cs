using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerFist : MonoBehaviour
{

    [SerializeField] Collider collider;
    [SerializeField] MeshRenderer meshRenderer;

    [SerializeField] PhysicsHand physicsHand;

    // Start is called before the first frame update
    public void EnableFist()
    {
        meshRenderer.enabled = true;
        collider.enabled = true;

        physicsHand.PowerFistEnabled();

    }

    // Update is called once per frame
    public void DisableFist()
    {
        meshRenderer.enabled = false;
        collider.enabled = false;

        physicsHand.PowerFistDisabled();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Ball")
        {
            other.GetComponent<Rigidbody>().AddForce((other.transform.position - transform.position).normalized * 1, ForceMode.Impulse);
        }
    }

}
