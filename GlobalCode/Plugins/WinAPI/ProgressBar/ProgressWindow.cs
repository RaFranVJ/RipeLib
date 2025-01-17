using System;
using System.Runtime.InteropServices;
using System.Threading;

public class ProgressWindow
{
    // Main Window
    protected IntPtr mainWindow;

    // Progress Bar
    protected IntPtr progressBar;

    // Text Display for Progress
    protected IntPtr progressText;

    // Window Thread
    private Thread progressThread;

    // Control Lifetime for this Window
    private volatile bool isRunning;

    [DllImport("comctl32.dll") ]
    private static extern void InitCommonControls();

    public ProgressWindow(string title, UIComponentParams mainCfg = null, UIBarComponentParams barCfg = null,
        UIBarComponentParams textCfg = null, IntPtr parent = default)
    {
        progressThread = new( () =>
        {
            InitializeProgressWindow(title, mainCfg, barCfg, textCfg, parent);
            RunMessageLoop();
        });

        if (!PlatformHelper.IsWindows)
            return;

        #pragma warning disable CA1416 // Validate platform compatibility
        progressThread.SetApartmentState(ApartmentState.STA); // GUI thread

        isRunning = true;
        progressThread.Start();
    }

    // Quick Constructor
    public static ProgressWindow CreateNew(ProgressBarTextConfig cfg = null)
    {
        if (cfg == null)
            return null;

        return new(cfg.Title, parent: cfg.ParentWindowPtr);
    }

    private void InitializeProgressWindow(string title, UIComponentParams mainCfg, UIBarComponentParams barCfg,
        UIBarComponentParams textCfg, IntPtr parent)
    {
        title = string.IsNullOrEmpty(title) ? ProgressBarTextConfig.GetDefaultTitle() : title;
        mainCfg ??= new();
        barCfg ??= new(false);
        textCfg ??= new(true);

        InitCommonControls();

        mainWindow = WindowGUI.Create(title, mainCfg.Style | WindowStyle.WS_VISIBLE, mainCfg.Dimensions,
            mainCfg.ExtendedStyle, mainCfg.ClassName, parent);

        if (mainWindow == IntPtr.Zero)
            return;

        progressBar = WindowGUI.Create(null, barCfg.Style, barCfg.Dimensions, barCfg.ExtendedStyle,
            barCfg.ClassName, mainWindow); // Error: Bar not visible

        progressText = WindowGUI.Create(null, textCfg.Style, textCfg.Dimensions,
            textCfg.ExtendedStyle, textCfg.ClassName, mainWindow); // Text is displayed as "Title" (twice)

        SetText("Starting...");
        SetProgress(0, 100);

        WindowGUI.ForceRedraw(mainWindow);
        WindowGUI.ForceRedraw(progressBar);
        WindowGUI.ForceRedraw(progressText);
    }

    private void RunMessageLoop()
    {
        while (isRunning)
        {
            WindowGUI.ProcessMessages();
            Thread.Sleep(50); // Avoid excessive CPU usage
        }
    }

    // Enqueue the progress update to be executed in the main thread
    private void EnqueueProgressUpdate(int processed, int total, string text)
    {
        // Action to update the progress bar and text
        void updateAction()
        {
            SetProgress(processed, total);  // Update the progress bar
            SetText(text);                  // Update the text
        }

        // Ensure the update is done in the main message loop thread
        if (progressThread != null && progressThread.IsAlive)
        {
            // Execute the update action within the main message thread
            lock (this)
            {
                updateAction();
            }
        }
    }

    private void SetProgress(int processed, int total)
    {
        if (progressBar == IntPtr.Zero)
            return;

        WindowGUI.SendMsg(progressBar, (uint)ProgressBarMessageType.PBM_SETRANGE, default, MakeLParam(0, total) );
        WindowGUI.SendMsg(progressBar, (uint)ProgressBarMessageType.PBM_SETPOS, processed);
    }

    public virtual void SetProgress(int processed, int total, ProgressFormat format, string text = null)
    {
        text = GetText(text, processed, total, format);

        EnqueueProgressUpdate(processed, total, text); // Use the new method to enqueue progress update
    }

    public virtual void SetText(string text)
    {
        if (progressText != IntPtr.Zero)
            WindowGUI.SendTxtMsg(progressText, (uint)WindowMessageType.WM_SETTEXT, IntPtr.Zero, text);
    }

    public void Destroy()
    {
        if (mainWindow != IntPtr.Zero)
        {
            WindowGUI.Destroy(mainWindow);
            mainWindow = default;
        }

        isRunning = false; // Stop the message loop

        if (progressThread != null && progressThread.IsAlive)
        {
            progressThread.Join(); // Wait for the thread to finish
            progressThread = null;
        }
    }

    private static IntPtr MakeLParam(int low, int high) => (IntPtr)((high << 16) | (low & 0xFFFF));

    private static string GetText(string baseText, int processed, int total, ProgressFormat format)
    {
        baseText = string.IsNullOrEmpty(baseText) ? ProgressBarTextConfig.GetDefaultMsg() : baseText;

        string displayText = total > 0 ? format switch
        {
            ProgressFormat.Percent => $"{(int)((double)processed / total * 100)}%",
            ProgressFormat.Full => $"{processed}/{total} | {(int)((double)processed / total * 100)}%",
            _ => $"{processed}/{total}"
        }
        : $"{processed}";

        return $"{baseText}... ({displayText})";
    }
}
