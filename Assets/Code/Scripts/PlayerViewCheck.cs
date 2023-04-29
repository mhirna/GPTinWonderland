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

    private string baseText;

    // Start is called before the first frame update
    void Start()
    {
        var rootElement = m_UIDocument.rootVisualElement;

        baseText = rootElement.Q<Label>("NPCInteractTipText").text;
    }

    // Update is called once per frame
    //void Update()
    //{   
    //}


    private void OnTriggerEnter(Collider other)
    {
        var rootElement = m_UIDocument.rootVisualElement;

        string UIText = baseText + other.name;

        rootElement.Q<Label>("NPCInteractTipText").text = UIText;

    }

}
