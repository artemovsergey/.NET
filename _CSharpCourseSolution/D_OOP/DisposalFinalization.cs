using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace D_OOP
{
    public class ResourceHolder2 : IDisposable
    {
        private readonly SafeHandle managedResource;

        public ResourceHolder2()
        {
            managedResource = new SafeFileHandle(new IntPtr(), true);
        }

        /// <summary>
        /// temporal coupling between calls
        /// </summary>
        public void Dispose()
        {
            managedResource?.Dispose();
            GC.SuppressFinalize(this);
        }
    }

    public class ResourceHolder : IDisposable
    {
        private readonly IntPtr unmanagedResource;
        private readonly SafeHandle managedResource;

        public ResourceHolder()
        {
            unmanagedResource = Marshal.AllocHGlobal(sizeof(int));
            managedResource = new SafeFileHandle(new IntPtr(), true);
        }

        /// <summary>
        /// temporal coupling between calls
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool isManualDisposing)
        {
            ReleaseUnmanagedResourse(unmanagedResource);
            if (isManualDisposing)
            {
                ReleaseManagedResources(managedResource);
            }
        }

        private void ReleaseManagedResources(SafeHandle safeHandle)
        {
            safeHandle?.Dispose();
        }

        private void ReleaseUnmanagedResourse(IntPtr intPtr)
        {
            Marshal.FreeHGlobal(intPtr);
        }

        ~ResourceHolder()
        {
            Dispose(false);
        }
    }
}
