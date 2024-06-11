using System;

public static class GlobalEvents
{
    public static event Action<bool> ChangeRenderState;
    public static event Action RenderImages;
    public static event Action UnRenderImages;
    public static event Action SaveWhenBuffUnrender;
    public static event Action<bool> SwitchMoveImagePosibility;
    public static bool isRenderNow = true;

    public static void InvokeRenderState()
    {
        ChangeRenderState?.Invoke(isRenderNow);
        isRenderNow = !isRenderNow;
    }

    public static void InvokeRenderImages()
    {
        RenderImages?.Invoke();

        isRenderNow = true;
    }
    public static void InvokeUnRenderImages()
    {
        UnRenderImages?.Invoke();

        isRenderNow = false;

    }

    public static void InvokeSaveBuffDataWhenUnRender()
    {
        SaveWhenBuffUnrender?.Invoke();

    }

    public static void InvokeSwitchMoveImagePosibility(bool canControlSizePositionIcon)
    {
        SwitchMoveImagePosibility?.Invoke(canControlSizePositionIcon);
    }
}
