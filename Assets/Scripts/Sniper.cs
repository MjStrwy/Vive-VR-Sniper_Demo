using UnityEngine;
using System.Collections;

public class Sniper : MonoBehaviour {

    public GameObject bulletPrefab;
    public Transform bulletSpawnPoint;

    public SteamVR_TrackedObject rightController;

    public Camera scopeCamera;

    public const float minFOV = 1f;
    public const float maxFOV = 25f;

    // Update is called once per frame
    void Update () {
        var deviceIndex = SteamVR_Controller.GetDeviceIndex(SteamVR_Controller.DeviceRelation.Leftmost);

        //create bullet when trigger pulled and set bullet velocity
        if (SteamVR_Controller.Input(deviceIndex).GetTouchDown(SteamVR_Controller.ButtonMask.Trigger))
        {
            SteamVR_Controller.Input(deviceIndex).TriggerHapticPulse(3999);
            GameObject go = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.transform.rotation) as GameObject;
            go.GetComponent<Rigidbody>().velocity = 1000f * bulletSpawnPoint.transform.up;
        }

        //use touchpad press to zoom scope view
        if (SteamVR_Controller.Input(deviceIndex).GetPress(SteamVR_Controller.ButtonMask.Touchpad))
        {
            SteamVR_Controller.Input(deviceIndex).TriggerHapticPulse(300);
            //adjust field of view down to zoom
            float touchY = 1f * SteamVR_Controller.Input(deviceIndex).GetAxis(Valve.VR.EVRButtonId.k_EButton_Axis0).y;
            float FOV = scopeCamera.fov - touchY;
            if (FOV < minFOV)
                scopeCamera.fov = minFOV;
            else if (FOV > maxFOV)
                scopeCamera.fov = maxFOV;
            else
                scopeCamera.fov = FOV;
        }
    }
}
