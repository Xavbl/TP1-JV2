using System.Linq;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

public class BossAnimations : MonoBehaviour
{
    private static readonly int EmissionColor = Shader.PropertyToID("_EmissionColor");
    private static readonly int Blend = Animator.StringToHash("Blend");

    [Header("State")]
    [SerializeField] private PlayerIndexValue playerIndex;
    [SerializeField] private IdleAnimationValue idleAnimation;
    [SerializeField] private EyeStateValue eyeState;

    [Header("Renderers")]
    [SerializeField] private Renderer[] bodyRenderers;
    [SerializeField] private Renderer eyesRenderer;

    [Header("Textures and colors")]
    [SerializeField] private Texture2D[] bodyTextures;
    [SerializeField, ColorUsage(true, true)] private Color[] eyeColors;
    [SerializeField] private Vector2[] eyesTextureOffset = { new(0, 0), new(.33f, 0), new(.66f, 0), new(.33f, .66f) };

    [Header("Animations")]
    [SerializeField, Range(0, 1)] private float walkThreshold = 0.1f;
    [SerializeField, Range(0, 1)] private float walkStartDamping = 0.3f;
    [SerializeField, Range(0, 1)] private float walkStopDamping = 0.15f;

    private Boss character;
    private Animator animator;

    public PlayerIndexValue PlayerIndex
    {
        get => playerIndex;
        set
        {
            playerIndex = value;
            UpdateVisual();
        }
    }

    public IdleAnimationValue IdleAnimation
    {
        get => idleAnimation;
        set
        {
            idleAnimation = value;
            UpdateIdleAnimation();
        }
    }

    public EyeStateValue EyeState
    {
        get => eyeState;
        set
        {
            eyeState = value;
            UpdateVisual();
        }
    }

    private void Awake()
    {
        character = GetComponent<Boss>();
        animator = GetComponent<Animator>();

        UpdateVisual();
        UpdateIdleAnimation();
    }

    private void Update()
    {
        var normalizedSpeed = Mathf.Clamp01(character.Velocity.sqrMagnitude);

        if (normalizedSpeed > walkThreshold)
            animator.SetFloat(Blend, normalizedSpeed, walkStartDamping, Time.deltaTime);
        else if (normalizedSpeed < walkThreshold)
            animator.SetFloat(Blend, normalizedSpeed, walkStopDamping, Time.deltaTime);
    }

    private void UpdateVisual()
    {
        // Visual values.
        var visualIndex = (int)playerIndex;
        var bodyTexture = bodyTextures[visualIndex];
        var eyeColor = eyeColors[visualIndex];
        var eyeTextureOffset = eyesTextureOffset[(int)eyeState];

        // Update body renderers
        for (var i = 0; i < bodyRenderers.Length; i++)
        {
            bodyRenderers[i].material.mainTexture = bodyTexture;
        }

        // Update eyes renderer.
        eyesRenderer.material.SetColor(EmissionColor, eyeColor);
        eyesRenderer.material.mainTextureOffset = eyeTextureOffset;
    }

    private void UpdateIdleAnimation()
    {
        animator.SetTrigger(idleAnimation.ToString());
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

        UpdateVisual();
        UpdateIdleAnimation();
    }
#endif

    public enum PlayerIndexValue
    {
        Player1 = 0,
        Player2 = 1,
        Player3 = 2,
        Player4 = 3,
    }

    public enum IdleAnimationValue
    {
        Normal = 0,
        Happy = 1,
        Angry = 2,
        Scared = 3
    }

    public enum EyeStateValue
    {
        Normal = 0,
        Happy = 1,
        Angry = 2,
        Scared = 3
    }
}