using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class CharacterInteractionHandler : MonoBehaviour
{

    [SerializeField]
    private UIDocument m_UIDocument;

    public GameObject target;

    public bool enabled = false;

    // Start is called before the first frame update
    void Start()
    {

        m_UIDocument.enabled = false;

    }

    // Update is called once per frame
    void Update()
    {
        if (enabled && Input.GetKeyDown(KeyCode.E))
        {

            m_UIDocument.enabled = true;



        }
    }
}
