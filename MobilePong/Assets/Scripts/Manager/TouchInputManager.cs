using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchInputManager : MonoBehaviour
{
    private GameObject playerLeftHit = null;
    private GameObject playerRightHit = null;


    private void Update()
    {
        Touch[] myTouches = Input.touches;

        for (int i = 0; i < myTouches.Length; i++)
        {
            if (myTouches[i].phase == TouchPhase.Began)
            {
                RaycastHit hit;
                Ray touchRay = Camera.main.ScreenPointToRay(myTouches[i].position);

                if (Physics.Raycast(touchRay, out hit))
                {
                    if (hit.collider.tag == "PlayerLeft" && playerLeftHit == null)
                        playerLeftHit = hit.collider.gameObject;

                    if (hit.collider.tag == "PlayerRight" && playerRightHit == null)
                        playerRightHit = hit.collider.gameObject;
                }
            }

            if (myTouches[i].phase == TouchPhase.Ended)
            {
                if (myTouches[i].position.x < Screen.width / 2)
                    playerLeftHit = null;

                if (myTouches[i].position.x > Screen.width / 2)
                    playerRightHit = null;
            }


            if (myTouches[i].phase == TouchPhase.Moved)
            {
                if (playerLeftHit != null && myTouches[i].position.x < Screen.width / 2)
                    MovePlayer(myTouches[i], playerLeftHit);

                if (playerRightHit != null && myTouches[i].position.x > Screen.width / 2)
                    MovePlayer(myTouches[i], playerRightHit);
            }
        }
    }


    private void MovePlayer(Touch myTouch, GameObject player)
    {
        float movePlayerY = Camera.main.ScreenToWorldPoint(myTouch.position).y;

        if (movePlayerY <= -3.5f)
            movePlayerY = -3.5f;

        if (movePlayerY >= 3.5f)
            movePlayerY = 3.5f;

        player.GetComponent<Rigidbody>().MovePosition(new Vector3(player.transform.position.x, movePlayerY, player.transform.position.z));
    }
}
