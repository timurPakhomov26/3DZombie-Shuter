using UnityEngine;

public class DeviceTypes : MonoBehaviour
{
    public static DeviceTypes Instance;
    public DeviceTypeWEB CurrentDeviceType { get ; private set; }
    private void Awake()
	{
		Instance = this;
		DontDestroyOnLoad(gameObject);
   
    }

    private void Start()
    {
      if(SystemInfo.deviceType == DeviceType.Desktop)
        {
          CurrentDeviceType = DeviceTypeWEB.Desktop;
          Debug.Log("Dekstop");
        }
        else
        {
           CurrentDeviceType = DeviceTypeWEB.Mobile;
           Debug.Log("Mobile");
        }
    }
    

}
public enum DeviceTypeWEB 
{
    Mobile,
	   Desktop
 }

