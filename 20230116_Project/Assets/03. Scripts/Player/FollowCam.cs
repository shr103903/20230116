using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCam : MonoBehaviour
{
    public bool isShaking = false;

    [SerializeField]
    private Transform player = null;

    [SerializeField]
    private float height = 12f;

    [SerializeField]
    private float weight = 2f;

    private Vector3 currentPos = Vector3.zero;

    private Vector3 randomVec = Vector2.zero;

    private IEnumerator shakeCor = null;

    private WaitForSeconds shakeDelay = new WaitForSeconds(0.05f);

    private void Update()
    {
        if (!isShaking)
        {
            //transform.position = Vector3.Slerp(transform.position, player.position + Vector3.up * height - Vector3.forward * weight, 0.1f);
        }
        transform.position = Vector3.Slerp(transform.position, player.position + Vector3.up * height - Vector3.forward * weight, 0.1f);

        if (Input.GetKeyDown(KeyCode.K))
        {
            isShaking = false;
            Shake();
        }
    }

    public void Shake()
    {
        if(shakeCor != null)
        {
            StopCoroutine(shakeCor);
        }
        shakeCor = CorShake();
        StartCoroutine(shakeCor);
    }

    private IEnumerator CorShake()
    {
        int count = 3;
        currentPos = transform.position;

        while (count > 0)
        {
            randomVec = Random.insideUnitSphere;
            randomVec.y = 0;
            transform.position = currentPos + randomVec * 0.1f;
            count--;

            yield return shakeDelay;
        }

        isShaking = false;
        StopCoroutine(shakeCor);
    }
}
