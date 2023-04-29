using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class PlayerViewCheck : MonoBehaviour
{
    [SerializeField]
    private UIDocument m_UIDocument;
    //    private TemplateContainer UITreeAsset;

    public CharacterInteractionHandler chHandler;

    private string baseText;

    // Start is called before the first frame update
    void Start()
    {
        var rootElement = m_UIDocument.rootVisualElement;

        baseText = rootElement.Q<Label>("NPCInteractTipText").text;

        rootElement.Q<Label>("NPCInteractTipText").text = "";
    }

    // Update is called once per frame
    void Update()
    {

        if (chHandler.enabled == true && Input.GetKeyDown(KeyCode.E))
        {
            var rootElement = m_UIDocument.rootVisualElement;
            rootElement.Q<Label>("NPCInteractTipText").text = "";
        }

    }


    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.GetType());

        if (other is CapsuleCollider)
        {
            var rootElement = m_UIDocument.rootVisualElement;

            string UIText = baseText + other.name;

            rootElement.Q<Label>("NPCInteractTipText").text = UIText;


            chHandler.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Debug.Log(other.GetType());

        if (other is CapsuleCollider)
        {
            var rootElement = m_UIDocument.rootVisualElement;

            //            string UIText = baseText + other.name;

            //            rootElement.Q<Label>("NPCInteractTipText").text = UIText;

            rootElement.Q<Label>("NPCInteractTipText").text = "";


            chHandler.enabled = false;
        }
    }

}
