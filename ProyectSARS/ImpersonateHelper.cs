using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace ProyectSARS
{
    public class ImpersonateHelper : IDisposable
    {
        [System.Runtime.InteropServices.DllImport("Kernel32")]
        private extern static Boolean CloseHandle(IntPtr handle);

        private IntPtr _token = IntPtr.Zero;
        private WindowsImpersonationContext _impersonatedUser = null;

        public IntPtr Token
        {
            get { return _token; }
            set { _token = value; }
        }

        public ImpersonateHelper(IntPtr token)
        {
            _token = token;
        }

        /// <summary>
        /// Switch the user to that set by the Token property
        /// </summary>
        public void Impersonate()
        {
            if (_token == IntPtr.Zero)
                _token = WindowsIdentity.GetCurrent().Token;

            _impersonatedUser = WindowsIdentity.Impersonate(_token);
        }

        /// <summary>
        /// Revert to the identity (user) before Impersonate() was called
        /// </summary>
        public void Undo()
        {
            if (_impersonatedUser != null)
                _impersonatedUser.Undo();
        }

        #region IDisposable Members
        private bool _isDisposed;

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_isDisposed)
            {
                if (disposing)
                {
                    if (_impersonatedUser != null)
                        _impersonatedUser.Dispose();

                }
                CloseHandle(_token);
                _token = IntPtr.Zero;
            }
            _isDisposed = true;
        }

        ~ImpersonateHelper()
        {
            Dispose(false);
        }
        #endregion
    }
}
