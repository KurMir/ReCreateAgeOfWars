using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraEdgeDrag : MonoBehaviour
{
  public Vector2 panLimit;

  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {

    Vector3 cameraFollowPosition = transform.position;
    float moveAmount = 5f;
    float edgeSize = 20f;
    if (Input.mousePosition.x > Screen.width - edgeSize)
    {
      cameraFollowPosition.x += moveAmount * Time.deltaTime;
    }
    if (Input.mousePosition.x < edgeSize)
    {
      cameraFollowPosition.x -= moveAmount * Time.deltaTime;
    }

    cameraFollowPosition.x = Mathf.Clamp(cameraFollowPosition.x, -panLimit.x, panLimit.x);
    transform.position = cameraFollowPosition;



  }
}
