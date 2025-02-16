using NaughtyAttributes;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Invokes events when a collider enters or exits the trigger.
/// </summary>
public class OnTrigger : MonoBehaviour
{
    [Header("Events")]
    [SerializeField] private UnityEvent<Collider> _onTriggerEnter;
    [SerializeField] private UnityEvent<Collider> _onTriggerExit;

    [Header("Parameters")]

    [SerializeField] private bool _tagFiltering = true;
    [SerializeField, ShowIf(nameof(_tagFiltering)), Tag] private string _tag;

    [Space(2)]
    [SerializeField] private bool _layerFiltering = true;
    [SerializeField, ShowIf(nameof(_layerFiltering))] private LayerMask _layerMask;

    //----------------------------------
    //Test
    //----------------------------------
    public bool tagFiltering { get; set; }
    public UnityEvent<Collider> OnTriggerEnterEvent
    {
        get { return _onTriggerEnter; }
        set { _onTriggerEnter = value; }
    }

    public string cTag
    {
        get { return _tag; }
        set { _tag = value; }
    }

    public LayerMask layerMask
    {
        get { return _layerMask; }
        set { _layerMask = value; }
    }

    //----------------------------------
    private void Awake()
    {
        _onTriggerEnter = new UnityEvent<Collider>();
    }



    //Test
    public void TestTriggerEnter(Collider other)
    {
        OnTriggerEnter(other);
    }
    /// <summary>
    /// Invokes the enter event if the collider is valid.
    /// </summary>
    /// <param name="other">the collider which triggered the script</param>
    private void OnTriggerEnter(Collider other)
    {
        // Checks tag and layer, based on what’s enabled
        if (_tagFiltering && (string.IsNullOrEmpty(_tag) || !other.CompareTag(_tag))) return;
        if (_layerFiltering && !(_layerMask == (_layerMask | (1 << other.gameObject.layer)))) return;

        _onTriggerEnter.Invoke(other);
    }

    /// <summary>
    /// Invokes the exit event if the collider is valid.
    /// </summary>
    /// <param name="other">the collider which triggered the script</param>
    private void OnTriggerExit(Collider other)
    {
        // Checks tag and layer, based on what’s enabled
        if (_tagFiltering && !other.CompareTag(_tag)) return;
        if (_layerFiltering && !(_layerMask == (_layerMask | (1 << other.gameObject.layer)))) return;

        _onTriggerExit.Invoke(other);
    }
}
