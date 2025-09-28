using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    [Header("Shooting")]
    public Transform shootPosition;
    public float shootForce;
    public BulletsPooling bulletsPooling;
    public float gunThrowDamage = 150f;

    [Header("Aim Line (LineRenderer)")]
    [Tooltip("Optional material template for the line. If provided, an instance will be created so changes don't affect the shared asset.")]
    public Material lineMaterialTemplate;
    [Tooltip("Default color of the line when idle.")]
    public Color lineDefaultColor = Color.green;
    [Tooltip("Color the line flashes when a shot is fired.")]
    public Color lineFireColor = Color.red;
    [Tooltip("How long the line stays in fire color after shooting (seconds).")]
    public float fireFlashDuration = 0.15f;
    [Tooltip("Length of the aim line in meters.")]
    public float lineLength = 5f;
    [Tooltip("Width of the aim line.")]
    public float lineWidth = 0.01f;

    private MasterSFX _masterSfx;
    private Vector3 _initialPostion;
    private Quaternion _initialRotation;
    private Rigidbody _rigidbody;

    private LineRenderer _lineRenderer;
    private Material _lineRuntimeMaterial;
    private float _flashTimer = 0f;

    void Awake()
    {
        // Ensure we have a LineRenderer on the gun
        _lineRenderer = GetComponent<LineRenderer>();
        if (_lineRenderer == null)
        {
            _lineRenderer = gameObject.AddComponent<LineRenderer>();
        }

        // Configure basic line properties
        _lineRenderer.positionCount = 2;
        _lineRenderer.useWorldSpace = true;
        _lineRenderer.startWidth = lineWidth;
        _lineRenderer.endWidth = lineWidth;
        _lineRenderer.numCornerVertices = 2;
        _lineRenderer.numCapVertices = 2;
        _lineRenderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
        _lineRenderer.receiveShadows = false;
        _lineRenderer.textureMode = LineTextureMode.Stretch;

        // Assign a runtime material instance so color changes don't affect shared asset
        if (lineMaterialTemplate != null)
        {
            _lineRuntimeMaterial = new Material(lineMaterialTemplate);
        }
        else
        {
            // Create a simple default unlit material if none provided
            Shader shader = Shader.Find("Unlit/Color");
            if (shader == null)
            {
                shader = Shader.Find("Sprites/Default");
            }
            _lineRuntimeMaterial = new Material(shader);
        }
        _lineRenderer.material = _lineRuntimeMaterial;
        SetLineColor(lineDefaultColor);
        _lineRenderer.enabled = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        _masterSfx = GameObject.FindObjectOfType<MasterSFX>();
        _initialPostion = transform.position;
        _initialRotation = transform.rotation;
        _rigidbody = GetComponent<Rigidbody>();
        // _bulletsPooling = GameObject.FindObjectOfType<BulletsPooling>();
    }

    void Update()
    {
        UpdateAimLine();
        UpdateFlashTimer();
    }

    private void UpdateAimLine()
    {
        if (_lineRenderer == null || shootPosition == null) return;

        Vector3 start = shootPosition.position;
        // The projectiles use shootPosition.right as forward; keep line consistent
        Vector3 dir = shootPosition.right;
        Vector3 end = start + dir.normalized * lineLength;

        _lineRenderer.SetPosition(0, start);
        _lineRenderer.SetPosition(1, end);

        // Keep width in sync in case it's tweaked at runtime
        _lineRenderer.startWidth = lineWidth;
        _lineRenderer.endWidth = lineWidth;
    }

    public void LineRenderOnOff(bool on)
    {
        _lineRenderer.enabled = on;
    }

    private void UpdateFlashTimer()
    {
        if (_flashTimer > 0f)
        {
            _flashTimer -= Time.deltaTime;
            if (_flashTimer <= 0f)
            {
                SetLineColor(lineDefaultColor);
            }
        }
    }

    private void SetLineColor(Color c)
    {
        if (_lineRuntimeMaterial != null)
        {
            if (_lineRuntimeMaterial.HasProperty("_Color"))
            {
                _lineRuntimeMaterial.color = c;
            }
            else if (_lineRuntimeMaterial.HasProperty("_BaseColor"))
            {
                _lineRuntimeMaterial.SetColor("_BaseColor", c);
            }
        }
    }

    public void ResetGunPosition()
    {
        _rigidbody.useGravity = false;
        _rigidbody.linearVelocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;
        transform.position = _initialPostion;
        transform.rotation = _initialRotation;
    }

    public void FireGun()
    {
        _masterSfx.PlayGunSFX("shoot");
        GameObject b = bulletsPooling.GetBullet();
        if (b)
        {
            b.transform.position = shootPosition.position;
            b.transform.rotation = shootPosition.rotation;
            b.GetComponent<Bullet>().BulletEnable();
            Rigidbody bulletRigidbody = b.GetComponent<Rigidbody>();
            bulletRigidbody.AddForce(shootPosition.right * shootForce, ForceMode.Impulse);
        }

        // Flash the aim line color on fire
        SetLineColor(lineFireColor);
        _flashTimer = fireFlashDuration;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            // other.gameObject.GetComponent<FollowPlayer>().GetHit(gunThrowDamage);
        }

        ResetGunPosition();
    }
}