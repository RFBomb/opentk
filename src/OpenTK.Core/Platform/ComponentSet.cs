using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using OpenTK.Core.Utility;
using OpenTK.Mathematics;

#nullable enable

namespace OpenTK.Core.Platform
{
    /// <summary>
    /// A set of platform abstraction layer components.
    /// </summary>
    public class ComponentSet : IClipboardComponent,
                                ICursorComponent,
                                IDisplayComponent,
                                IIconComponent,
                                IKeyboardComponent,
                                IMouseComponent,
                                IOpenGLComponent,
                                ISurfaceComponent,
                                IWindowComponent,
                                IShellComponent,
                                IJoystickComponent
    {
        // This class is a bunch of boilerplate some of which has been generated by the IDE.
        private IClipboardComponent? _clipboardComponent;
        private ICursorComponent? _cursorComponent;
        private IDisplayComponent? _displayComponent;
        private IIconComponent? _iconComponent;
        private IKeyboardComponent? _keyboardComponent;
        private IMouseComponent? _mouseComponent;
        private IOpenGLComponent? _openGLComponent;
        private ISurfaceComponent? _surfaceComponent;
        private IWindowComponent? _windowComponent;
        private IShellComponent? _shellComponent;
        private IJoystickComponent? _joystickComponent;

        /// <summary>
        /// Indicated whether the component set has been initialized.
        /// </summary>
        /// <remarks>
        /// An initialized component set cannot be modified.
        /// </remarks>
        public bool Initialized { get; private set; } = false;

        /// <inheritdoc/>
        public string Name
        {
            get
            {
                HashSet<string> names = new HashSet<string>();
                if (_cursorComponent is not null)
                {
                    names.Add(_cursorComponent.Name);
                }

                if (_displayComponent is not null)
                {
                    names.Add(_displayComponent.Name);
                }

                if (_iconComponent is not null)
                {
                    names.Add(_iconComponent.Name);
                }

                if (_keyboardComponent is not null)
                {
                    names.Add(_keyboardComponent.Name);
                }

                if (_mouseComponent is not null)
                {
                    names.Add(_mouseComponent.Name);
                }

                if (_openGLComponent is not null)
                {
                    names.Add(_openGLComponent.Name);
                }

                if (_surfaceComponent is not null)
                {
                    names.Add(_surfaceComponent.Name);
                }

                if (_windowComponent is not null)
                {
                    names.Add(_windowComponent.Name);
                }

                if (_shellComponent is not null)
                {
                    names.Add(_shellComponent.Name);
                }

                if (_joystickComponent is not null)
                {
                    names.Add(_joystickComponent.Name);
                }

                return $"Component Set [{string.Join(", ", names)}]";
            }
        }

        /// <inheritdoc/>
        public PalComponents Provides => (_cursorComponent is not null ? PalComponents.MouseCursor : 0) |
                                         (_displayComponent is not null ? PalComponents.Display : 0) |
                                         (_iconComponent is not null ? PalComponents.WindowIcon : 0) |
                                         (_keyboardComponent is not null ? PalComponents.KeyboardInput : 0) |
                                         (_mouseComponent is not null ? PalComponents.MouseCursor : 0) |
                                         (_openGLComponent is not null ? PalComponents.OpenGL : 0) |
                                         (_surfaceComponent is not null ? PalComponents.Surface : 0) |
                                         (_windowComponent is not null ? PalComponents.Window : 0) |
                                         (_shellComponent is not null ? PalComponents.Shell : 0) |
                                         (_joystickComponent is not null ? PalComponents.Joystick : 0);

        private ILogger? _logger;

        /// <inheritdoc/>
        public ILogger? Logger
        {
            get => _logger;
            set
            {
                _logger = value;

#pragma warning disable SA1503 // Braces should not be omitted

                if (_clipboardComponent != null) _clipboardComponent.Logger = _logger;

                if (_cursorComponent != null) _cursorComponent.Logger = _logger;

                if (_displayComponent != null) _displayComponent.Logger = _logger;

                if (_iconComponent != null) _iconComponent.Logger = _logger;

                if (_keyboardComponent != null) _keyboardComponent.Logger = _logger;

                if (_mouseComponent != null) _mouseComponent.Logger = _logger;

                if (_openGLComponent != null) _openGLComponent.Logger = _logger;

                if (_surfaceComponent != null) _surfaceComponent.Logger = _logger;

                if (_windowComponent != null) _windowComponent.Logger = _logger;

                if (_shellComponent != null) _shellComponent.Logger = _logger;

                if (_joystickComponent != null) _joystickComponent.Logger = _logger;

#pragma warning restore SA1503 // Braces should not be omitted
            }
        }

        /// <summary>
        /// Get or set which components are in the set.
        /// </summary>
        /// <param name="which">The component group.</param>
        /// <exception cref="NotImplementedException">Not implemented, yet.</exception>
        /// <exception cref="ArgumentException">The given <paramref name="which"/> enum should only contain bit set for get.</exception>
        /// <exception cref="PalException">Raised when the set is modified after initialization.</exception>
        public IPalComponent? this[PalComponents which]
        {
            get => which switch
            {
                PalComponents.ControllerInput => throw new NotImplementedException(),
                PalComponents.Display => _displayComponent,
                PalComponents.KeyboardInput => _keyboardComponent,
                PalComponents.MiceInput => _mouseComponent,
                PalComponents.MouseCursor => _cursorComponent,
                PalComponents.Surface => _surfaceComponent,
                PalComponents.Vulkan => throw new NotImplementedException(),
                PalComponents.Window => _windowComponent,
                PalComponents.WindowIcon => _iconComponent,
                PalComponents.OpenGL => _openGLComponent,
                PalComponents.Clipboard => _clipboardComponent,
                PalComponents.Shell => _shellComponent,
                PalComponents.Joystick => _joystickComponent,
                _ => throw new ArgumentException("Components are a bitfield or out of range.", nameof(which))
            };
            set
            {
                if (Initialized)
                {
                    throw new PalException(this, "Cannot change set after components are initialized.");
                }

                if ((which & PalComponents.Display) != 0)
                {
                    _displayComponent = value as IDisplayComponent;
                }
                if ((which & PalComponents.KeyboardInput) != 0)
                {
                    _keyboardComponent = value as IKeyboardComponent;
                }
                if ((which & PalComponents.MiceInput) != 0)
                {
                    _mouseComponent = value as IMouseComponent;
                }
                if ((which & PalComponents.MouseCursor) != 0)
                {
                    _cursorComponent = value as ICursorComponent;
                }
                if ((which & PalComponents.Surface) != 0)
                {
                    _surfaceComponent = value as ISurfaceComponent;
                }
                if ((which & PalComponents.Window) != 0)
                {
                    _windowComponent = value as IWindowComponent;
                }
                if ((which & PalComponents.WindowIcon) != 0)
                {
                    _iconComponent = value as IIconComponent;
                }
                if ((which & PalComponents.OpenGL) != 0)
                {
                    _openGLComponent = value as IOpenGLComponent;
                }
                if ((which & PalComponents.Clipboard) != 0)
                {
                    _clipboardComponent = value as IClipboardComponent;
                }
                if ((which & PalComponents.Shell) != 0)
                {
                    _shellComponent = value as IShellComponent;
                }
                if ((which & PalComponents.Joystick) != 0)
                {
                    _joystickComponent = value as IJoystickComponent;
                }
            }
        }

        /// <inheritdoc/>
        void IPalComponent.Initialize(PalComponents which)
        {
            if ((which & ~Provides) != 0)
            {
                throw new PalException(this, $"Platform does not support requested features.")
                {
                    Data =
                    {
                        ["Requested"] = which,
                        ["Supported"] = Provides
                    }
                };
            }

            _cursorComponent?.Initialize(which & PalComponents.MouseCursor);
            _displayComponent?.Initialize(which & PalComponents.Display);
            _iconComponent?.Initialize(which & PalComponents.WindowIcon);
            _keyboardComponent?.Initialize(which & PalComponents.KeyboardInput);
            _mouseComponent?.Initialize(which & PalComponents.MiceInput);
            _surfaceComponent?.Initialize(which & PalComponents.Surface);
            _windowComponent?.Initialize(which & PalComponents.Window);
            _openGLComponent?.Initialize(which & PalComponents.OpenGL);
            _clipboardComponent?.Initialize(which & PalComponents.Clipboard);
            _shellComponent?.Initialize(which & PalComponents.Shell);

            Initialized = true;
        }

        IReadOnlyList<ClipboardFormat> IClipboardComponent.SupportedFormats => _clipboardComponent!.SupportedFormats;

        ClipboardFormat IClipboardComponent.GetClipboardFormat()
        {
            return _clipboardComponent!.GetClipboardFormat();
        }

        void IClipboardComponent.SetClipboardText(string text)
        {
            _clipboardComponent!.SetClipboardText(text);
        }

        string? IClipboardComponent.GetClipboardText()
        {
            return _clipboardComponent!.GetClipboardText();
        }

        AudioData? IClipboardComponent.GetClipboardAudio()
        {
            return _clipboardComponent!.GetClipboardAudio();
        }

        Bitmap? IClipboardComponent.GetClipboardBitmap()
        {
            return _clipboardComponent!.GetClipboardBitmap();
        }

        string? IClipboardComponent.GetClipboardHTML()
        {
            return _clipboardComponent!.GetClipboardHTML();
        }

        List<string>? IClipboardComponent.GetClipboardFiles()
        {
            return _clipboardComponent!.GetClipboardFiles();
        }

        /// <inheritdoc/>
        bool ICursorComponent.CanLoadSystemCursors => _cursorComponent!.CanLoadSystemCursors;

        /// <inheritdoc/>
        bool ICursorComponent.CanInspectSystemCursors => _cursorComponent!.CanInspectSystemCursors;

        /// <inheritdoc/>
        bool IIconComponent.CanLoadSystemIcon => _iconComponent!.CanLoadSystemIcon;

        /// <inheritdoc/>
        bool IWindowComponent.CanSetIcon => _windowComponent!.CanSetIcon;

        /// <inheritdoc/>
        bool IWindowComponent.CanGetDisplay => _windowComponent!.CanGetDisplay;

        /// <inheritdoc/>
        bool IWindowComponent.CanSetCursor => _windowComponent!.CanSetCursor;

        /// <inheritdoc/>
        bool IWindowComponent.CanCaptureCursor => _windowComponent!.CanCaptureCursor;

        /// <inheritdoc/>
        IReadOnlyList<PlatformEventType> IWindowComponent.SupportedEvents => _windowComponent!.SupportedEvents;

        /// <inheritdoc/>
        IReadOnlyList<WindowBorderStyle> IWindowComponent.SupportedStyles => _windowComponent!.SupportedStyles;

        /// <inheritdoc/>
        IReadOnlyList<WindowMode> IWindowComponent.SupportedModes => _windowComponent!.SupportedModes;

        /// <inheritdoc/>
        void IWindowComponent.ProcessEvents(bool waitForEvents)
        {
            _windowComponent!.ProcessEvents(waitForEvents);
        }

        /// <inheritdoc/>
        WindowHandle IWindowComponent.Create(GraphicsApiHints hints)
        {
            return _windowComponent!.Create(hints);
        }

        /// <inheritdoc/>
        void IWindowComponent.Destroy(WindowHandle handle)
        {
            _windowComponent!.Destroy(handle);
        }

        /// <inheritdoc/>
        bool IWindowComponent.IsWindowDestroyed(WindowHandle handle)
        {
            return _windowComponent!.IsWindowDestroyed(handle);
        }

        /// <inheritdoc/>
        string IWindowComponent.GetTitle(WindowHandle handle)
        {
            return _windowComponent!.GetTitle(handle);
        }

        /// <inheritdoc/>
        void IWindowComponent.SetTitle(WindowHandle handle, string title)
        {
            _windowComponent!.SetTitle(handle, title);
        }

        /// <inheritdoc/>
        IconHandle IWindowComponent.GetIcon(WindowHandle handle)
        {
            return _windowComponent!.GetIcon(handle);
        }

        /// <inheritdoc/>
        void IWindowComponent.SetIcon(WindowHandle handle, IconHandle icon)
        {
            _windowComponent!.SetIcon(handle, icon);
        }

        /// <inheritdoc/>
        void IWindowComponent.GetPosition(WindowHandle handle, out int x, out int y)
        {
            _windowComponent!.GetPosition(handle, out x, out y);
        }

        /// <inheritdoc/>
        void IWindowComponent.SetPosition(WindowHandle handle, int x, int y)
        {
            _windowComponent!.SetPosition(handle, x, y);
        }

        /// <inheritdoc/>
        void IWindowComponent.GetSize(WindowHandle handle, out int width, out int height)
        {
            _windowComponent!.GetSize(handle, out width, out height);
        }

        /// <inheritdoc/>
        void IWindowComponent.SetSize(WindowHandle handle, int width, int height)
        {
            _windowComponent!.SetSize(handle, width, height);
        }

        /// <inheritdoc/>
        void IWindowComponent.GetClientPosition(WindowHandle handle, out int x, out int y)
        {
            _windowComponent!.GetClientPosition(handle, out x, out y);
        }

        /// <inheritdoc/>
        void IWindowComponent.SetClientPosition(WindowHandle handle, int x, int y)
        {
            _windowComponent!.SetClientPosition(handle, x, y);
        }

        /// <inheritdoc/>
        void IWindowComponent.GetClientSize(WindowHandle handle, out int width, out int height)
        {
            _windowComponent!.GetClientSize(handle, out width, out height);
        }

        /// <inheritdoc/>
        void IWindowComponent.SetClientSize(WindowHandle handle, int width, int height)
        {
            _windowComponent!.SetClientSize(handle, width, height);
        }

        /// <inheritdoc/>
        void IWindowComponent.GetMaxClientSize(WindowHandle handle, out int? width, out int? height)
        {
            _windowComponent!.GetMaxClientSize(handle, out width, out height);
        }

        /// <inheritdoc/>
        void IWindowComponent.SetMaxClientSize(WindowHandle handle, int? width, int? height)
        {
            _windowComponent!.SetMaxClientSize(handle, width, height);
        }

        /// <inheritdoc/>
        void IWindowComponent.GetMinClientSize(WindowHandle handle, out int? width, out int? height)
        {
            _windowComponent!.GetMinClientSize(handle, out width, out height);
        }

        /// <inheritdoc/>
        void IWindowComponent.SetMinClientSize(WindowHandle handle, int? width, int? height)
        {
            _windowComponent!.SetMinClientSize(handle, width, height);
        }

        /// <inheritdoc/>
        DisplayHandle IWindowComponent.GetDisplay(WindowHandle handle)
        {
            return _windowComponent!.GetDisplay(handle);
        }

        /// <inheritdoc/>
        WindowMode IWindowComponent.GetMode(WindowHandle handle)
        {
            return _windowComponent!.GetMode(handle);
        }

        /// <inheritdoc/>
        void IWindowComponent.SetMode(WindowHandle handle, WindowMode mode)
        {
            _windowComponent!.SetMode(handle, mode);
        }

        /// <inheritdoc/>
        public void SetFullscreenDisplay(WindowHandle window, DisplayHandle? display)
        {
            _windowComponent!.SetFullscreenDisplay(window, display);
        }

        /// <inheritdoc/>
        public void SetFullscreenDisplay(WindowHandle window, DisplayHandle display, VideoMode videoMode)
        {
            _windowComponent!.SetFullscreenDisplay(window, display, videoMode);
        }

        /// <inheritdoc/>
        public bool GetFullscreenDisplay(WindowHandle window, [NotNullWhen(true)] out DisplayHandle? display)
        {
            return _windowComponent!.GetFullscreenDisplay(window, out display);
        }

        /// <inheritdoc/>
        WindowBorderStyle IWindowComponent.GetBorderStyle(WindowHandle handle)
        {
            return _windowComponent!.GetBorderStyle(handle);
        }

        /// <inheritdoc/>
        void IWindowComponent.SetBorderStyle(WindowHandle handle, WindowBorderStyle style)
        {
            _windowComponent!.SetBorderStyle(handle, style);
        }

        /// <inheritdoc/>
        void IWindowComponent.SetAlwaysOnTop(WindowHandle handle, bool floating)
        {
            _windowComponent!.SetAlwaysOnTop(handle, floating);
        }

        /// <inheritdoc/>
        bool IWindowComponent.IsAlwaysOnTop(WindowHandle handle)
        {
            return _windowComponent!.IsAlwaysOnTop(handle);
        }

        /// <inheritdoc/>
        public void SetHitTestCallback(WindowHandle handle, HitTest? test)
        {
            _windowComponent!.SetHitTestCallback(handle, test);
        }

        /// <inheritdoc/>
        void IWindowComponent.SetCursor(WindowHandle handle, CursorHandle? cursor)
        {
            _windowComponent!.SetCursor(handle, cursor);
        }

        /// <inheritdoc/>
        CursorCaptureMode IWindowComponent.GetCursorCaptureMode(WindowHandle handle)
        {
            return _windowComponent!.GetCursorCaptureMode(handle);
        }

        /// <inheritdoc/>
        void IWindowComponent.SetCursorCaptureMode(WindowHandle handle, CursorCaptureMode mode)
        {
            _windowComponent!.SetCursorCaptureMode(handle, mode);
        }

        /// <inheritdoc/>
        void IWindowComponent.FocusWindow(WindowHandle handle)
        {
            _windowComponent!.FocusWindow(handle);
        }

        /// <inheritdoc/>
        void IWindowComponent.RequestAttention(WindowHandle handle)
        {
            _windowComponent!.RequestAttention(handle);
        }

        /// <inheritdoc/>
        void IWindowComponent.ScreenToClient(WindowHandle handle, int x, int y, out int clientX, out int clientY)
        {
            _windowComponent!.ScreenToClient(handle, x, y, out clientX, out clientY);
        }

        /// <inheritdoc/>
        void IWindowComponent.ClientToScreen(WindowHandle handle, int clientX, int clientY, out int x, out int y)
        {
            _windowComponent!.ClientToScreen(handle, clientX, clientY, out x, out y);
        }

        void IWindowComponent.SwapBuffers(WindowHandle handle)
        {
            _windowComponent!.SwapBuffers(handle);
        }

        /// <inheritdoc/>
        IconHandle IIconComponent.Create(SystemIconType systemIcon)
        {
            return _iconComponent!.Create(systemIcon);
        }

        /// <inheritdoc/>
        IconHandle IIconComponent.Create(int width, int height, System.ReadOnlySpan<byte> data)
        {
            return _iconComponent!.Create(width, height, data);
        }

        /// <inheritdoc/>
        void ISurfaceComponent.Destroy(SurfaceHandle handle)
        {
            _surfaceComponent!.Destroy(handle);
        }

        /// <inheritdoc/>
        SurfaceType ISurfaceComponent.GetType(SurfaceHandle handle)
        {
            return _surfaceComponent!.GetType(handle);
        }

        /// <inheritdoc/>
        DisplayHandle ISurfaceComponent.GetDisplay(SurfaceHandle handle)
        {
            return _surfaceComponent!.GetDisplay(handle);
        }

        /// <inheritdoc/>
        void ISurfaceComponent.SetDisplay(SurfaceHandle handle, DisplayHandle display)
        {
            _surfaceComponent!.SetDisplay(handle, display);
        }

        /// <inheritdoc/>
        void ISurfaceComponent.GetClientSize(SurfaceHandle handle, out int width, out int height)
        {
            _surfaceComponent!.GetClientSize(handle, out width, out height);
        }

        /// <inheritdoc/>
        void IIconComponent.Destroy(IconHandle handle)
        {
            _iconComponent!.Destroy(handle);
        }

        /// <inheritdoc/>
        void IIconComponent.GetSize(IconHandle handle, out int width, out int height)
        {
            _iconComponent!.GetSize(handle, out width, out height);
        }

        /// <inheritdoc/>
        CursorHandle ICursorComponent.Create(SystemCursorType systemCursor)
        {
            return _cursorComponent!.Create(systemCursor);
        }

        /// <inheritdoc/>
        CursorHandle ICursorComponent.Create(int width, int height, ReadOnlySpan<byte> image, int hotspotX, int hotspotY)
        {
            return _cursorComponent!.Create(width, height, image, hotspotX, hotspotY);
        }

        /// <inheritdoc/>
        CursorHandle ICursorComponent.Create(int width, int height, ReadOnlySpan<byte> colorData, ReadOnlySpan<byte> maskData, int hotspotX, int hotspotY)
        {
            return _cursorComponent!.Create(width, height, colorData, maskData, hotspotX, hotspotY);
        }

        /// <inheritdoc/>
        bool ICursorComponent.IsSystemCursor(CursorHandle handle)
        {
            return _cursorComponent!.IsSystemCursor(handle);
        }

        /// <inheritdoc/>
        void ICursorComponent.Destroy(CursorHandle handle)
        {
            _cursorComponent!.Destroy(handle);
        }

        /// <inheritdoc/>
        void ICursorComponent.GetSize(CursorHandle handle, out int width, out int height)
        {
            _cursorComponent!.GetSize(handle, out width, out height);
        }

        /// <inheritdoc/>
        void ICursorComponent.GetHotspot(CursorHandle handle, out int x, out int y)
        {
            _cursorComponent!.GetHotspot(handle, out x, out y);
        }

        /// <inheritdoc/>
        bool IDisplayComponent.CanGetVirtualPosition => _displayComponent!.CanGetVirtualPosition;

        /// <inheritdoc/>
        int IDisplayComponent.GetDisplayCount()
        {
            return _displayComponent!.GetDisplayCount();
        }

        /// <inheritdoc/>
        bool IMouseComponent.CanSetMousePosition => _mouseComponent!.CanSetMousePosition;

        /// <inheritdoc/>
        void IMouseComponent.GetPosition(out int x, out int y)
        {
            _mouseComponent!.GetPosition(out x, out y);
        }

        /// <inheritdoc/>
        void IMouseComponent.SetPosition(int x, int y)
        {
            _mouseComponent!.SetPosition(x, y);
        }

        /// <inheritdoc/>
        DisplayHandle IDisplayComponent.Open(int index)
        {
            return _displayComponent!.Open(index);
        }

        /// <inheritdoc/>
        DisplayHandle IDisplayComponent.OpenPrimary()
        {
            return _displayComponent!.OpenPrimary();
        }

        /// <inheritdoc/>
        void IDisplayComponent.Close(DisplayHandle handle)
        {
            _displayComponent!.Close(handle);
        }

        /// <inheritdoc/>
        bool IDisplayComponent.IsPrimary(DisplayHandle handle)
        {
            return _displayComponent!.IsPrimary(handle);
        }

        /// <inheritdoc/>
        string IDisplayComponent.GetName(DisplayHandle handle)
        {
            return _displayComponent!.GetName(handle);
        }

        /// <inheritdoc/>
        void IDisplayComponent.GetVideoMode(DisplayHandle handle, out VideoMode mode)
        {
            _displayComponent!.GetVideoMode(handle, out mode);
        }

        /// <inheritdoc/>
        VideoMode[] IDisplayComponent.GetSupportedVideoModes(DisplayHandle handle)
        {
            return _displayComponent!.GetSupportedVideoModes(handle);
        }

        /// <inheritdoc/>
        void IDisplayComponent.GetVirtualPosition(DisplayHandle handle, out int x, out int y)
        {
            _displayComponent!.GetVirtualPosition(handle, out x, out y);
        }

        /// <inheritdoc/>
        void IDisplayComponent.GetResolution(DisplayHandle handle, out int width, out int height)
        {
            _displayComponent!.GetResolution(handle, out width, out height);
        }

        /// <inheritdoc/>
        void IDisplayComponent.GetWorkArea(DisplayHandle handle, out Box2i area)
        {
            _displayComponent!.GetWorkArea(handle, out area);
        }

        /// <inheritdoc/>
        void IDisplayComponent.GetRefreshRate(DisplayHandle handle, out float refreshRate)
        {
            _displayComponent!.GetRefreshRate(handle, out refreshRate);
        }

        /// <inheritdoc/>
        void IDisplayComponent.GetDisplayScale(DisplayHandle handle, out float scaleX, out float scaleY)
        {
            _displayComponent!.GetDisplayScale(handle, out scaleX, out scaleY);
        }

        /// <inheritdoc/>
        bool IKeyboardComponent.SupportsLayouts => _keyboardComponent!.SupportsLayouts;

        /// <inheritdoc/>
        bool IKeyboardComponent.SupportsIme => _keyboardComponent!.SupportsIme;

        /// <inheritdoc/>
        string IKeyboardComponent.GetActiveKeyboardLayout(WindowHandle? handle)
        {
            return _keyboardComponent!.GetActiveKeyboardLayout(handle);
        }

        /// <inheritdoc/>
        string[] IKeyboardComponent.GetAvailableKeyboardLayouts()
        {
            return _keyboardComponent!.GetAvailableKeyboardLayouts();
        }

        /// <inheritdoc/>
        Scancode IKeyboardComponent.GetScancodeFromKey(Key key)
        {
            return _keyboardComponent!.GetScancodeFromKey(key);
        }

        /// <inheritdoc/>
        Key IKeyboardComponent.GetKeyFromScancode(Scancode scancode)
        {
            return _keyboardComponent!.GetKeyFromScancode(scancode);
        }

        /// <inheritdoc/>
        void IKeyboardComponent.BeginIme(WindowHandle window)
        {
            _keyboardComponent!.BeginIme(window);
        }

        /// <inheritdoc/>
        void IKeyboardComponent.SetImeRectangle(WindowHandle window, int x, int y, int width, int height)
        {
            _keyboardComponent!.SetImeRectangle(window, x, y, width, height);
        }

        /// <inheritdoc/>
        void IKeyboardComponent.EndIme(WindowHandle window)
        {
            _keyboardComponent!.EndIme(window);
        }

        /// <inheritdoc/>
        bool IOpenGLComponent.CanShareContexts => _openGLComponent!.CanShareContexts;

        /// <inheritdoc/>
        bool IOpenGLComponent.CanCreateFromWindow => _openGLComponent!.CanCreateFromWindow;

        /// <inheritdoc/>
        bool IOpenGLComponent.CanCreateFromSurface => _openGLComponent!.CanCreateFromSurface;

        /// <inheritdoc/>
        OpenGLContextHandle IOpenGLComponent.CreateFromSurface()
        {
            return _openGLComponent!.CreateFromSurface();
        }

        /// <inheritdoc/>
        OpenGLContextHandle IOpenGLComponent.CreateFromWindow(WindowHandle handle)
        {
            return _openGLComponent!.CreateFromWindow(handle);
        }

        /// <inheritdoc/>
        void IOpenGLComponent.DestroyContext(OpenGLContextHandle handle)
        {
            _openGLComponent!.DestroyContext(handle);
        }

        /// <inheritdoc/>
        IBindingsContext IOpenGLComponent.GetBindingsContext(OpenGLContextHandle handle)
        {
            return _openGLComponent!.GetBindingsContext(handle);
        }

        /// <inheritdoc/>
        IntPtr IOpenGLComponent.GetProcedureAddress(OpenGLContextHandle handle, string procedureName)
        {
            return _openGLComponent!.GetProcedureAddress(handle, procedureName);
        }

        /// <inheritdoc/>
        OpenGLContextHandle? IOpenGLComponent.GetCurrentContext()
        {
            return _openGLComponent!.GetCurrentContext();
        }

        /// <inheritdoc/>
        bool IOpenGLComponent.SetCurrentContext(OpenGLContextHandle? handle)
        {
            return _openGLComponent!.SetCurrentContext(handle);
        }

        /// <inheritdoc/>
        OpenGLContextHandle? IOpenGLComponent.GetSharedContext(OpenGLContextHandle handle)
        {
            return _openGLComponent!.GetSharedContext(handle);
        }

        /// <inheritdoc/>
        void IOpenGLComponent.SetSwapInterval(int interval)
        {
            _openGLComponent!.SetSwapInterval(interval);
        }

        /// <inheritdoc/>
        int IOpenGLComponent.GetSwapInterval()
        {
            return _openGLComponent!.GetSwapInterval();
        }

        /// <inheritdoc/>
        SurfaceHandle ISurfaceComponent.Create()
        {
            return _surfaceComponent!.Create();
        }

        /// <inheritdoc/>
        void IShellComponent.AllowScreenSaver(bool allow)
        {
            _shellComponent!.AllowScreenSaver(allow);
        }

        /// <inheritdoc/>
        BatteryStatus IShellComponent.GetBatteryInfo(out BatteryInfo batteryInfo)
        {
            return _shellComponent!.GetBatteryInfo(out batteryInfo);
        }

        /// <inheritdoc/>
        ThemeInfo IShellComponent.GetPreferredTheme()
        {
            return _shellComponent!.GetPreferredTheme();
        }

        /// <inheritdoc/>
        SystemMemoryInfo IShellComponent.GetSystemMemoryInformation()
        {
            return _shellComponent!.GetSystemMemoryInformation();
        }

        /// <inheritdoc/>
        float IJoystickComponent.LeftDeadzone => _joystickComponent!.LeftDeadzone;

        /// <inheritdoc/>
        float IJoystickComponent.RightDeadzone => _joystickComponent!.RightDeadzone;

        /// <inheritdoc/>
        float IJoystickComponent.TriggerThreshold => _joystickComponent!.TriggerThreshold;

        /// <inheritdoc/>
        bool IJoystickComponent.IsConnected(int index)
        {
            return _joystickComponent!.IsConnected(index);
        }

        /// <inheritdoc/>
        JoystickHandle IJoystickComponent.Open(int index)
        {
            return _joystickComponent!.Open(index);
        }

        /// <inheritdoc/>
        void IJoystickComponent.Close(JoystickHandle handle)
        {
            _joystickComponent!.Close(handle);
        }

        /// <inheritdoc/>
        Guid IJoystickComponent.GetGuid(JoystickHandle handle)
        {
            return _joystickComponent!.GetGuid(handle);
        }

        /// <inheritdoc/>
        string IJoystickComponent.GetName(JoystickHandle handle)
        {
            return _joystickComponent!.GetName(handle);
        }

        /// <inheritdoc/>
        float IJoystickComponent.GetAxis(JoystickHandle handle, JoystickAxis axis)
        {
            return _joystickComponent!.GetAxis(handle, axis);
        }

        /// <inheritdoc/>
        bool IJoystickComponent.GetButton(JoystickHandle handle, JoystickButton button)
        {
            return _joystickComponent!.GetButton(handle, button);
        }

        /// <inheritdoc/>
        bool IJoystickComponent.SetVibration(JoystickHandle handle, float lowFreqIntensity, float highFreqIntensity)
        {
            return _joystickComponent!.SetVibration(handle, lowFreqIntensity, highFreqIntensity);
        }

        /// <inheritdoc/>
        bool IJoystickComponent.TryGetBatteryInfo(JoystickHandle handle, out GamepadBatteryInfo batteryInfo)
        {
            return _joystickComponent!.TryGetBatteryInfo(handle, out batteryInfo);
        }
    }
}
