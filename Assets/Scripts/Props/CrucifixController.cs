using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrucifixController : MonoBehaviour
{
    private Rigidbody rigidBody = null;
    private GameObject player = null;

    private bool hasDropped = false;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
    }

    private void Update()
    {
        if (!hasDropped)
        {
            if (Vector3.Distance(transform.position, player.transform.position) < 3.5f)
            {
                DropIt();
            }
            else
            {
                Vector3 targetPostition = new Vector3(player.transform.position.x, this.transform.position.y, player.transform.position.z);
                transform.LookAt(targetPostition);
            }
        }        
    }

    private void DropIt()
    {
        AudioManager.Instance.PlayEventSound("DropIt");
        rigidBody.useGravity = true;
        hasDropped = true;

        StartCoroutine(ScaryPropDoneDelay());
    }

    private IEnumerator ScaryPropDoneDelay()
    {
        yield return new WaitForSeconds(1.1f);
        PlayerProgress.Instance.IncrementEncounter();
    }
}
