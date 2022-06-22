﻿using OpenTK.Core.Platform;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenTK.Platform.Native.Windows
{
    public class IconComponent : IIconComponent
    {
        public string Name => "Win32IconComponent";

        public PalComponents Provides => PalComponents.WindowIcon;

        public void Initialize(PalComponents which)
        {
            if (which != PalComponents.WindowIcon)
            {
                throw new Exception("IconComponent can only initialize the WindowIcon component.");
            }
        }

        public bool CanLoadFile => throw new NotImplementedException();

        public bool CanLoadSystemIcon => throw new NotImplementedException();

        public bool HasMipmaps => throw new NotImplementedException();

        public IconHandle Create()
        {
            return new HIcon();
        }

        public void Destroy(IconHandle handle)
        {
            HIcon hicon = handle.As<HIcon>(this);

            if (hicon.Icon != IntPtr.Zero)
            {
                bool success = Win32.DestroyIcon(hicon.Icon);
                if (success == false)
                {
                    throw new Win32Exception("DestroyIcon failed.");
                }

                // Delete the mask and color bitmaps.
                Win32.DeleteObject(hicon.MaskBitmap);
                Win32.DeleteObject(hicon.ColorBitmap);

                hicon.Icon = default;
                hicon.MaskBitmap = default;
                hicon.ColorBitmap = default;
            }
        }

        public void GenerateMipmaps(IconHandle handle)
        {
            throw new NotImplementedException();
        }

        public void GetDimensions(IconHandle handle, out int width, out int height)
        {
            throw new NotImplementedException();
        }

        public void GetDimensions(IconHandle handle, int level, out int width, out int height)
        {
            throw new NotImplementedException();
        }

        public void GetBitmap(IconHandle handle, Span<byte> data)
        {
            throw new NotImplementedException();
        }

        public void GetBitmap(IconHandle handle, int level, Span<byte> data)
        {
            throw new NotImplementedException();
        }

        public int GetBitmapSize(IconHandle handle)
        {
            throw new NotImplementedException();
        }

        public int GetBitmapSize(IconHandle handle, int level)
        {
            throw new NotImplementedException();
        }

        public unsafe void Load(IconHandle handle, int width, int height, ReadOnlySpan<byte> data)
        {
            HIcon hicon = handle.As<HIcon>(this);

            if (width < 0) throw new ArgumentOutOfRangeException($"Width cannot be negative. Value: {width}");
            if (height < 0) throw new ArgumentOutOfRangeException($"Height cannot be negative. Value: {height}");

            if (data.Length < width * height * 4) throw new ArgumentException($"The given span is too small. It must be at least {width * height * 4} long. Was: {data.Length}");

            // FIXME: Figure out if we should destroy the previous icon like this.
            Destroy(handle);

            // See https://web.archive.org/web/20080205042408/http://support.microsoft.com/kb/318876
            Win32.BITMAPV5HEADER header = default;
            header.bV5Size = (uint)sizeof(Win32.BITMAPV5HEADER);
            header.bV5Width = width;
            header.bV5Height = height;
            header.bV5Planes = 1;
            header.bV5BitCount = 32;
            header.bV5Compression = BI.Bitfields;
            header.bV5RedMask = 0x00_FF_00_00;
            header.bV5GreenMask = 0x00_00_FF_00;
            header.bV5BlueMask = 0x00_00_00_FF;
            header.bV5AlphaMask = 0xFF_00_00_00;
            // FIXME: Determine color space!
            // header.bV5CSType = LCS_sRGB?

            IntPtr hDC = Win32.GetDC(IntPtr.Zero);

            IntPtr colorBitmap = Win32.CreateDIBSection(hDC, in header, DIB.RGBColors, out IntPtr dataPtr, IntPtr.Zero, 0);
            if (colorBitmap == IntPtr.Zero)
            {
                throw new Win32Exception("CreateDIBSection failed.");
            }

            Span<byte> bitmapData = new Span<byte>(dataPtr.ToPointer(), width * height * 4);

            // Copy over image data.
            data.CopyTo(bitmapData);

            // Create an empty mask.
            IntPtr maskBitmap = Win32.CreateBitmap(width, height, 1, 1, IntPtr.Zero);
            // FIXME: The value if ERROR_INVALID_BITMAP is impossible to find.
            // It's not defined in wingdi.h and nothing online mentions it.
            if (maskBitmap == IntPtr.Zero /*|| maskBitmap == Win32.ERROR_INVALID_BITMAP*/)
            {
                throw new Win32Exception("CreateBitmap failed.");
            }

            Win32.ICONINFO iconinfo = new Win32.ICONINFO()
            {
                fIcon = true, // We are creating a icon
                xHotspot = 0,
                yHotspot = 0,
                hbmMask = maskBitmap,
                hbmColor = colorBitmap,
            };

            IntPtr hIcon = Win32.CreateIconIndirect(in iconinfo);
            if (hIcon == IntPtr.Zero)
            {
                throw new Win32Exception("CreateIconIndirect() failed.");
            }

            Win32.ReleaseDC(IntPtr.Zero, hDC);

            hicon.Icon = hIcon;
            hicon.MaskBitmap = maskBitmap;
            hicon.ColorBitmap = colorBitmap;
        }

        public void Load(IconHandle handle, int width, int height, ReadOnlySpan<byte> data, int level)
        {
            throw new NotImplementedException();
        }

        public void Load(IconHandle handle, string file)
        {
            throw new NotImplementedException();
        }

        public void Load(IconHandle handle, Stream stream)
        {
            throw new NotImplementedException();
        }

        public void Load(IconHandle handle, SystemIconType name)
        {
            throw new NotImplementedException();
        }
    }
}
