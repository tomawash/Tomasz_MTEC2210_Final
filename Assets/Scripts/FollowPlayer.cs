using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{

    public Transform player;

    public PlayerController script;
    float zoom = 0f;
    float maxZoom;

    float defaultSize = 5f;

    // Start is called before the first frame update
    void Start()
    {
        maxZoom = script.chargeLimit;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.position.y > -2f)
        {
            transform.position = new Vector3(transform.position.x, player.position.y + 2f, -10);
        }
        else
        {
            transform.position = new Vector3(transform.position.x, -0.2f, -10);
        }

        if(player.position.x > 6 || player.position.x < -6)
        {
            transform.position = new Vector3((Mathf.Abs(player.position.x) - 5.9f) * Mathf.Sign(player.position.x), transform.position.y, -10);
        }
        else
        {
            transform.position = new Vector3(0, transform.position.y, -10);
        }

        zoom = script.power;
        if(zoom > maxZoom)
        {
            zoom = maxZoom;
        }
        Debug.Log("Zoom : " + zoom);
        CamZoom();
    }

    public void CamZoom()
    {
        if (zoom > 0.5f)
        {
            Camera.main.orthographicSize = defaultSize - zoom / 4;
            transform.position = new Vector3(transform.position.x, transform.position.y - (zoom / 3), -10);
        }
        else
        {
            Camera.main.orthographicSize = defaultSize;
        }
    }
}
