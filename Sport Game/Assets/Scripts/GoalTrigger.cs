using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalTrigger : MonoBehaviour
{


    [SerializeField] string assignedTeam;
    [SerializeField] Score scoreBoard;

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Ball")
        {
            other.transform.position = new Vector3(-2.4000001f, 49.0999985f, 22.8999996f);
            other.GetComponent<Rigidbody>().velocity = Vector3.zero;
            other.GetComponent<Rigidbody>().angularVelocity = Vector3.zero;

            scoreBoard.AddScore(assignedTeam, 1);
        }




    }




}
