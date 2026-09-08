using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class CalculateLaunchAngle : MonoBehaviour
{
    [SerializeField] private Transform LaunchObject;
    [SerializeField] private float radius;
    [SerializeField] private float yCutOff;

    private Vector2 circlePoint; 
    private LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.enabled = false;
        lineRenderer.positionCount = 2;
    }
    void Update()
    {
        if (Mouse.current.leftButton.isPressed)
        {
            Vector2 clickPos = Mouse.current.position.ReadValue();
            clickPos = Camera.main.ScreenToWorldPoint(new Vector3(clickPos.x, yCutOff, Camera.main.nearClipPlane));
            circlePoint = GetPointOnACircle(2*LaunchObject.position - (Vector3)clickPos, LaunchObject.position); 
            DrawLine(LaunchObject.position, circlePoint);
        }
        if(Mouse.current.leftButton.wasReleasedThisFrame){
            LaunchObject.position=circlePoint;
            lineRenderer.enabled = false;
        }
    }
    private void DrawLine(Vector2 pos0, Vector2 pos1)
    {
        lineRenderer.enabled = true;
        lineRenderer.SetPosition(0, pos0);
        lineRenderer.SetPosition(1, pos1);
    }
    private Vector2 GetPointOnACircle(Vector2 point, Vector2 center)
    {
        Vector2 diff = point-center;        
    
        double distance = Math.Sqrt(Math.Pow(diff.x,2) + Math.Pow(diff.y,2));

        Vector2 result =
        new Vector2(
            (float)(center.x + (diff.x / distance) * radius),
            (float)(center.y + (diff.y / distance) * radius)
        );

        return result;
    }
}
