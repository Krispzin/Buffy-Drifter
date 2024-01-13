using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Vector3 offset;
    public Transform player;

    private PlayerScript playerScript;

    public Vector3 origCamPos;
    public Vector3 boostCamPos;

    // Start is called before the first frame update
    void Start()
    {
        playerScript = player.GetComponent<PlayerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.position + offset;
        transform.rotation = Quaternion.Slerp(transform.rotation, player.rotation, 3 * Time.deltaTime);

        if(playerScript.BoostTime > 0)
        {
            transform.GetChild(0).localPosition = Vector3.Lerp(transform.GetChild(0).localPosition, boostCamPos, 3 * Time.deltaTime);
        }
        else
        {
            transform.GetChild(0).localPosition = Vector3.Lerp(transform.GetChild(0).localPosition, origCamPos, 3 * Time.deltaTime);
        }
    }
}
