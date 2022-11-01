using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OuijaBoardController : MonoBehaviour
{
    [SerializeField]
    private GameObject arrowHead = null;
    private Rigidbody arrowHeadRigidbody = null;

    [SerializeField]
    private List<Transform> characterTransforms = null;

    [Space(10)]

    [SerializeField]
    private LayerMask layerMask;
    [SerializeField]
    private float transitionSpeed = 0.0f;
    private bool movementAllowed = true;

    private float arrowStayOnCharacterTime = 1.0f;
    private float arrowStayOnCharacterTimeElapsed = 0.0f;

    private int currentIndex = -1;
    private float rotationSpeed = 0.0f;

    private bool isLookingAtBoard = false;
    private float lookingAtBoardTimerTick = 0.0f;
    private bool ouijaWasThrown = false;

    private void Awake()
    {
        arrowHeadRigidbody = arrowHead.GetComponent<Rigidbody>();
    }

    private void Start()
    {
        SetNextIndex();
        SetRandomRotationSpeed();
    }

    private void Update()
    {
        ArrowMovement();
        LookingAtBoardTimer();
    }

    private void FixedUpdate()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, 2.0f, layerMask))
        {
            isLookingAtBoard = true;
        }
        else
        {
            isLookingAtBoard = false;
        }
    }

    private void ArrowMovement()
    {
        if (movementAllowed)
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

    private void LookingAtBoardTimer()
    {
        if (isLookingAtBoard && ouijaWasThrown == false)
        {
            lookingAtBoardTimerTick += Time.deltaTime;

            if (lookingAtBoardTimerTick >= 5.5f)
            {
                ThrowOuija();
            }
        }
    }

    private void ThrowOuija()
    {
        movementAllowed = false;
        AudioManager.Instance.PlayEventSound("ThrowIt");
        arrowHeadRigidbody.useGravity = true;
        arrowHeadRigidbody.AddForce((Camera.main.transform.position - transform.position) * 4.0f, ForceMode.Impulse);
        arrowHead.transform.rotation = Random.rotation;
        ouijaWasThrown = true;

        StartCoroutine(ScaryPropDoneDelay());
    }

    private IEnumerator ScaryPropDoneDelay()
    {
        yield return new WaitForSeconds(1.0f);
        PlayerProgress.Instance.IncrementEncounter();
    }
}
