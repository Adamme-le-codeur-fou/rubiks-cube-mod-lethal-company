using UnityEngine;

public class RotatingScrap : PhysicsProp
{
    private Vector3 rotationAngle = new Vector3(0f, 5f, 0f);
    private bool isActivated = false;

    public override void Update()
    {
        base.Update();
        if (isActivated) itemProperties.rotationOffset += rotationAngle;
    }

    public override void ItemActivate(bool used, bool buttonDown = true)
    {
        base.ItemActivate(used, buttonDown);

        if (buttonDown && playerHeldBy != null && playerHeldBy == GameNetworkManager.Instance.localPlayerController)
        {
            isActivated = !isActivated;
        }
    }

}