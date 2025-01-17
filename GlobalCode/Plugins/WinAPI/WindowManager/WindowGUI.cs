using System;
using System.Runtime.InteropServices;

public static class WindowGUI
{
    [DllImport("user32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
    private static extern IntPtr CreateWindowEx(int dwExStyle, string lpClassName, string lpWindowName, uint dwStyle,
        int x, int y, int nWidth, int nHeight, IntPtr hWndParent, IntPtr hMenu, IntPtr hInstance, IntPtr lpParam);

    // Crear nueva ventana
    public static IntPtr Create(string title, WindowStyle style, SexyRect rect,
        ExtendedWindowStyle exStyle = default, string className = null, IntPtr hWndParent = default,
        IntPtr hMenu = default, IntPtr hInstance = default, IntPtr lpParam = default)
    {
        if (!PlatformHelper.IsWindows)
            return IntPtr.Zero;

        className = string.IsNullOrEmpty(className) ? "static" : className;

        return CreateWindowEx((int)exStyle, className, title, (uint)style, rect.X, rect.Y,
            rect.Width, rect.Height, hWndParent, hMenu, hInstance, lpParam);
    }

    [DllImport("user32.dll", SetLastError = true)]
    private static extern bool DestroyWindow(IntPtr hWnd);

    // Destruir ventana
    public static void Destroy(IntPtr hWnd)
    {
        if (!PlatformHelper.IsWindows)
            return;

        DestroyWindow(hWnd);
    }

    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    private static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

    // Enviar mensaje
    public static IntPtr SendMsg(IntPtr hWnd, uint msgType, IntPtr wParam, IntPtr lParam = default)
    {
        return PlatformHelper.IsWindows ? SendMessage(hWnd, msgType, wParam, lParam) : IntPtr.Zero;
    }

    public static IntPtr SendMsg(IntPtr hWnd, WindowMessageType msgType, IntPtr wParam,
        IntPtr lParam = default)
    {
        return SendMsg(hWnd, (uint)msgType, wParam, lParam);
    }

    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    private static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, string lParam);

    public static IntPtr SendTxtMsg(IntPtr hWnd, uint msgType, IntPtr wParam, string lParam = null)
    {
        if (!PlatformHelper.IsWindows)
            return IntPtr.Zero;

        lParam = string.IsNullOrEmpty(lParam) ? "<Missing Msg>" : lParam;

        return SendMessage(hWnd, msgType, wParam, lParam);
    }

    public static IntPtr SendTxtMsg(IntPtr hWnd, WindowMessageType msgType, IntPtr wParam, string lParam = null)
    {
        return SendTxtMsg(hWnd, (uint)msgType, wParam, lParam);
    }

    // --- Nuevo: Procesamiento de mensajes ---
    [DllImport("user32.dll")]
    private static extern int PeekMessage(out MSG lpMsg, IntPtr hWnd, uint wMsgFilterMin, uint wMsgFilterMax, uint wRemoveMsg);

    [DllImport("user32.dll")]
    private static extern bool TranslateMessage(ref MSG lpMsg);

    [DllImport("user32.dll")]
    private static extern IntPtr DispatchMessage(ref MSG lpMsg);

    /// <summary>
    /// Procesa los mensajes de la ventana. Debe llamarse dentro de un bucle.
    /// </summary>
    public static void ProcessMessages()
    {
        if (!PlatformHelper.IsWindows)
            return;

        MSG msg;
        while (PeekMessage(out msg, IntPtr.Zero, 0, 0, 1) > 0)
        {
            TranslateMessage(ref msg);
            DispatchMessage(ref msg);
        }
    }

    [DllImport("user32.dll")]
private static extern bool UpdateWindow(IntPtr hWnd);

public static void ForceRedraw(IntPtr hWnd)
{
    if (hWnd != IntPtr.Zero)
    {
        UpdateWindow(hWnd);
    }
}


}