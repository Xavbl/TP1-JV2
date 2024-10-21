using System.Linq;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

public class AlienAnimations : MonoBehaviour
{
    private static readonly int EmissionColor = Shader.PropertyToID("_EmissionColor");
    private static readonly int Blend = Animator.StringToHash("Blend");

    [Header("Renderers")]
    [SerializeField] private Renderer[] bodyRenderers;
    [SerializeField] private Renderer eyesRenderer;

    [Header("Textures and colors")]
    [SerializeField] private Texture2D bodyTextures;

    [Header("Animations")]
    [SerializeField, Range(0, 1)] private float walkThreshold = 0.1f;
    [SerializeField, Range(0, 1)] private float walkStartDamping = 0.3f;
    [SerializeField, Range(0, 1)] private float walkStopDamping = 0.15f;

    private Alien character;
    private Animator animator;


    private void Awake()
    {
        character = GetComponent<Alien>();
        animator = GetComponent<Animator>();

    }

    private void Update()
    {
        var normalizedSpeed = Mathf.Clamp01(character.Velocity.sqrMagnitude);

        if (normalizedSpeed > walkThreshold)
            animator.SetFloat(Blend, normalizedSpeed, walkStartDamping, Time.deltaTime);
        else if (normalizedSpeed < walkThreshold)
            animator.SetFloat(Blend, normalizedSpeed, walkStopDamping, Time.deltaTime);
    }

#if UNITY_EDITOR
    [ContextMenu("Find Renderers")]
    private void GetRenderers()
    {
        Undo.RecordObject(this, $"Finding renderers in {gameObject.name}");
        eyesRenderer = transform.Find("HeadEyes").GetComponent<Renderer>();
        bodyRenderers = GetComponentsInChildren<Renderer>().Where(x => x != eyesRenderer).ToArray();
    }

    private void OnValidate()
    {
        if (!EditorApplication.isPlaying) return;
        if (animator is null) return;

    }
#endif
}