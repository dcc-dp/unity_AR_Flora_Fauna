using UnityEngine;

public class PermissionHandler : MonoBehaviour
{
    void Start()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            string[] permissions = {
                "android.permission.WRITE_EXTERNAL_STORAGE",
                "android.permission.READ_EXTERNAL_STORAGE"
            };
            foreach (string permission in permissions)
            {
                if (!UnityEngine.Android.Permission.HasUserAuthorizedPermission(permission))
                {
                    UnityEngine.Android.Permission.RequestUserPermission(permission);
                }
            }
        }
    }
}
