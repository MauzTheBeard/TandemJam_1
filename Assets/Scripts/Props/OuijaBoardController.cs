using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OuijaBoardController : MonoBehaviour
{
    [SerializeField]
    private GameObject arrowHead = null;

    [SerializeField]
    private List<Transform> characterTransforms = null;

    [Space(10)]

    [SerializeField]
    private float transitionSpeed = 0.0f;

    private float arrowStayOnCharacterTime = 1.0f;
    private float arrowStayOnCharacterTimeElapsed = 0.0f;

    private int currentIndex = -1;
    private float rotationSpeed = 0.0f;

    private void Start()
    {
        SetNextIndex();
        SetRandomRotationSpeed();
    }

    private void Update()
    {
        Movement();
    }

    private void Movement()
    {
        if (Vector3.Distance(arrowHead.transform.position, characterTransforms[currentIndex].position) > 0.005f)
        {
            float step = transitionSpeed * Time.deltaTime;
            arrowHead.transform.position = Vector3.MoveTowards(arrowHead.transform.position, characterTransforms[currentIndex].position, step);
            arrowHead.transform.Rotate(0, rotationSpeed, 0);
        }
        else
        {
            StayOnCharTimer();
        }
    }

    private void SetRandomRotationSpeed()
    {
        rotationSpeed = Random.Range(-2.5f, 2.5f);
    }

    private void StayOnCharTimer()
    {
        if (arrowStayOnCharacterTimeElapsed >= arrowStayOnCharacterTime)
        {
            arrowStayOnCharacterTimeElapsed = 0.0f;
            arrowStayOnCharacterTime = Random.Range(0.75f, 1.25f);
            SetNextIndex();
            SetRandomRotationSpeed();
        }
        else
        {
            arrowStayOnCharacterTimeElapsed += Time.deltaTime;
        }
    }

    private void SetNextIndex()
    {
        if (currentIndex == characterTransforms.Count - 1)
        {
            currentIndex = 0;
        }
        else
        {
            currentIndex++;
        }
    }
}
