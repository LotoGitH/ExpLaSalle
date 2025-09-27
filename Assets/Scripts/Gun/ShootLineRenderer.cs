using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(LineRenderer))]
public class ShootLineRenderer : MonoBehaviour
{
    public float rayLength = 10f;
    private LineRenderer lineRenderer;

    void Start()
    {
        // Asegúrate de que el objeto tiene LineRenderer adjunto
        lineRenderer = GetComponent<LineRenderer>();
        if (lineRenderer == null)
        {
            Debug.LogError("LineRenderer not found on this object.");
            return;
        }

        // Desactiva la línea al inicio
        lineRenderer.enabled = false;
    }

    void Update()
    {
        // Crea un rayo desde la posición del objeto hacia adelante
        Ray ray = new Ray(transform.position, transform.right);

        // Actualiza la posición final del rayo
        Vector3 rayEnd = transform.position + transform.right * rayLength;

        // Muestra el rayo utilizando LineRenderer
        ShowRay(ray.origin, rayEnd);

        // También puedes realizar interacciones o lógica basada en el rayo aquí
        // Por ejemplo, detectar objetos que el rayo atraviesa
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, rayLength))
        {
            // Haz algo con el objeto golpeado (hit.collider.gameObject)
        }
    }

    void ShowRay(Vector3 start, Vector3 end)
    {
        // Activa la línea y establece sus puntos de inicio y fin
        lineRenderer.enabled = true;
        lineRenderer.SetPosition(0, start);
        lineRenderer.SetPosition(1, end);
    }
}
