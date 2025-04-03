using UnityEngine;

public class Plane : MonoBehaviour
{
    private static float GRAVITY = 9.81f;
    private static float m = 70000;//kg - mass of the plane
    private static float k = 0.06f;//luc can
    private static float v0 = 194.4f;//m/s //km/h; = 194.4 m/s - initial velocity of the plane
    private static float ro = 1.225f;//kg/m3 - density of air
    private static float S = 122.4f * 2;//m2 - area of the plane
    private static float e = (float)System.Math.E;
    private static float t = 1;//s - time step

    public float F_day;//input

    public float P;//GRAVITY*m
    public float V_duoi;
    public float V_tren;
    public float F_nang;
    public float F_can;

    private void Update()
    {
        P = GRAVITY * m;
        V_duoi = F_day / k + (v0 - F_day / k) * Mathf.Pow(e, -k * t / m);
        V_duoi = V_duoi * 3.6f; // Convert m/s to km/h
        V_tren = V_duoi * 1.1f;
        F_nang = (ro * (V_tren * V_tren - V_duoi * V_duoi) * S / (3.6f * 3.6f)) / 2;
        F_can = k * V_duoi * V_duoi;
    }
}
