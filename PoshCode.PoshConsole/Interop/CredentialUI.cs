/*
 * CredentialUI.cs - Windows Credential UI Helper
 * 
 * License: Public Domain
 * 
 */

using System;
using System.ComponentModel;
using System.Management.Automation;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace PoshCode.Interop
{
    /// <summary>
    /// Credential UI Helper
    /// </summary>
    /// <example>
    /// var credentials = CredentialUI.Prompt("Caption", "Message", "DOMAIN\\KazariUiharu", "P@ssw0rd1"); // Vista or 7: PromptForWindowsCredentials / 2000 or XP or 2003: PromptForCredentials
    /// var credentials2 = CredentialUI.PromptForWindowsCredentials("Caption", "Message");
    /// Console.WriteLine("UserName: {0}", credentials2.UserName);
    /// Console.WriteLine("DomainName: {0}", credentials2.DomainName);
    /// Console.WriteLine("Password: {0}", credentials2.Password);
    /// </example>
    public static class CredentialUI
    {
        /// <summary>
        /// Show dialog box for generic credential.
        /// </summary>
        /// <param name="caption"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static PromptCredentialsResult Prompt(string caption, string message)
        {
            return Prompt(caption, message, null, null);
        }
        /// <summary>
        /// Show dialog box for generic credential.
        /// </summary>
        /// <param name="caption"></param>
        /// <param name="message"></param>
        /// <param name="hwndParent"></param>
        /// <returns></returns>
        public static PromptCredentialsResult Prompt(string caption, string message, IntPtr hwndParent)
        {
            return Prompt(caption, message, hwndParent, null, null);
        }
        /// <summary>
        /// Show dialog box for generic credential.
        /// </summary>
        /// <param name="caption"></param>
        /// <param name="message"></param>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static PromptCredentialsResult Prompt(string caption, string message, string userName, string password)
        {
            return Prompt(caption, message, IntPtr.Zero, userName, password);
        }
        /// <summary>
        /// Show dialog box for generic credential.
        /// </summary>
        /// <param name="caption"></param>
        /// <param name="message"></param>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="hwndParent"></param>
        /// <returns></returns>
        public static PromptCredentialsResult Prompt(string caption, string message, IntPtr hwndParent, string userName, string password)
        {
            if (Environment.OSVersion.Version.Major >= 6)
            {
                // Windows Vista or 2008 or 7 or later
                return PromptForWindowsCredentials(caption, message, hwndParent, userName, password);
            }
            else
            {
                // Windows 2000 or 2003 or XP
                return PromptForCredentials(Environment.UserDomainName, caption, message, hwndParent, userName, password);
            }
        }
        /// <summary>
        /// Show dialog box for generic credential.
        /// </summary>
        /// <param name="caption"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static PromptCredentialsSecureStringResult PromptWithSecureString(string caption, string message)
        {
            return PromptWithSecureString(caption, message, IntPtr.Zero);
        }
        /// <summary>
        /// Show dialog box for generic credential.
        /// </summary>
        /// <param name="caption"></param>
        /// <param name="message"></param>
        /// <param name="hwndParent"></param>
        /// <returns></returns>
        public static PromptCredentialsSecureStringResult PromptWithSecureString(string caption, string message, IntPtr hwndParent)
        {
            return PromptWithSecureString(caption, message, IntPtr.Zero, null, null);
        }
        /// <summary>
        /// Show dialog box for generic credential.
        /// </summary>
        /// <param name="caption"></param>
        /// <param name="message"></param>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static PromptCredentialsSecureStringResult PromptWithSecureString(string caption, string message, SecureString userName, SecureString password)
        {
            return PromptWithSecureString(caption, message, IntPtr.Zero, userName, password);
        }
        /// <summary>
        /// Show dialog box for generic credential.
        /// </summary>
        /// <param name="caption"></param>
        /// <param name="message"></param>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <param name="hwndParent"></param>
        /// <returns></returns>
        public static PromptCredentialsSecureStringResult PromptWithSecureString(string caption, string message, IntPtr hwndParent, SecureString userName, SecureString password)
        {
            if (Environment.OSVersion.Version.Major >= 6)
            {
                // Windows Vista or 2008 or 7 or later
                return PromptForWindowsCredentialsWithSecureString(caption, message, hwndParent, userName, password);
            }
            else
            {
                // Windows 2000 or 2003 or XP
                return PromptForCredentialsWithSecureString(Environment.UserDomainName, caption, message, hwndParent, userName, password);
            }
        }


        #region Method: PromptForWindowsCredentials
        /// <summary>
        /// Creates and displays a configurable dialog box that allows users to supply credential information by using any credential provider installed on the local computer.
        /// </summary>
        /// <param name="caption"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static PromptCredentialsResult PromptForWindowsCredentials(string caption, string message)
        {
            return PromptForWindowsCredentials(caption, message, IntPtr.Zero, string.Empty, string.Empty);
        }
        /// <summary>
        /// Creates and displays a configurable dialog box that allows users to supply credential information by using any credential provider installed on the local computer.
        /// </summary>
        /// <param name="caption"></param>
        /// <param name="message"></param>
        /// <param name="hwndParent"></param>
        /// <returns></returns>
        public static PromptCredentialsResult PromptForWindowsCredentials(string caption, string message, IntPtr hwndParent)
        {
            return PromptForWindowsCredentials(caption, message, hwndParent, string.Empty, string.Empty);
        }
        /// <summary>
        /// Creates and displays a configurable dialog box that allows users to supply credential information by using any credential provider installed on the local computer.
        /// </summary>
        /// <param name="caption"></param>
        /// <param name="message"></param>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static PromptCredentialsResult PromptForWindowsCredentials(string caption, string message, string userName, string password)
        {
            return PromptForWindowsCredentials(caption, message, IntPtr.Zero, userName, password);
        }
        /// <summary>
        /// Creates and displays a configurable dialog box that allows users to supply credential information by using any credential provider installed on the local computer.
        /// </summary>
        /// <param name="caption"></param>
        /// <param name="message"></param>
        /// <param name="hwndParent"></param>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static PromptCredentialsResult PromptForWindowsCredentials(string caption, string message, IntPtr hwndParent, string userName, string password)
        {
            var options = new PromptForWindowsCredentialsOptions(caption, message)
            {
                HwndParent = hwndParent,
                IsSaveChecked = false
            };
            return PromptForWindowsCredentials(options, userName, password);

        }
        /// <summary>
        /// Creates and displays a configurable dialog box that allows users to supply credential information by using any credential provider installed on the local computer.
        /// </summary>
        /// <param name="options"></param>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static PromptCredentialsResult PromptForWindowsCredentials(PromptForWindowsCredentialsOptions options, string userName, string password)
        {
            if (string.IsNullOrEmpty(userName) && string.IsNullOrEmpty(password))
            {
                return PromptForWindowsCredentialsInternal<PromptCredentialsResult>(options, null, null);
            }

            using (var userNameS = new SecureString())
            using (var passwordS = new SecureString())
            {
                if (!string.IsNullOrEmpty(userName))
                {
                    foreach (var c in userName)
                    {
                        userNameS.AppendChar(c);
                    }
                }
                if (!string.IsNullOrEmpty(password))
                {
                    foreach (var c in password)
                    {
                        passwordS.AppendChar(c);
                    }
                }

                userNameS.MakeReadOnly();
                passwordS.MakeReadOnly();
                return PromptForWindowsCredentialsInternal<PromptCredentialsResult>(options, userNameS, passwordS);
            }
        }
        /// <summary>
        /// Creates and displays a configurable dialog box that allows users to supply credential information by using any credential provider installed on the local computer.
        /// </summary>
        /// <param name="caption"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static PromptCredentialsSecureStringResult PromptForWindowsCredentialsWithSecureString(string caption, string message)
        {
            return PromptForWindowsCredentialsWithSecureString(caption, message, IntPtr.Zero, null, null);
        }
        /// <summary>
        /// Creates and displays a configurable dialog box that allows users to supply credential information by using any credential provider installed on the local computer.
        /// </summary>
        /// <param name="caption"></param>
        /// <param name="message"></param>
        /// <param name="hwndParent"></param>
        /// <returns></returns>
        public static PromptCredentialsSecureStringResult PromptForWindowsCredentialsWithSecureString(string caption, string message, IntPtr hwndParent)
        {
            return PromptForWindowsCredentialsWithSecureString(caption, message, hwndParent, null, null);
        }
        /// <summary>
        /// Creates and displays a configurable dialog box that allows users to supply credential information by using any credential provider installed on the local computer.
        /// </summary>
        /// <param name="caption"></param>
        /// <param name="message"></param>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static PromptCredentialsSecureStringResult PromptForWindowsCredentialsWithSecureString(string caption, string message, SecureString userName, SecureString password)
        {
            return PromptForWindowsCredentialsWithSecureString(caption, message, IntPtr.Zero, userName, password);
        }
        /// <summary>
        /// Creates and displays a configurable dialog box that allows users to supply credential information by using any credential provider installed on the local computer.
        /// </summary>
        /// <param name="caption"></param>
        /// <param name="message"></param>
        /// <param name="hwndParent"></param>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static PromptCredentialsSecureStringResult PromptForWindowsCredentialsWithSecureString(string caption, string message, IntPtr hwndParent, SecureString userName, SecureString password)
        {
            var options = new PromptForWindowsCredentialsOptions(caption, message)
            {
                HwndParent = hwndParent,
                IsSaveChecked = false
            };
            return PromptForWindowsCredentialsWithSecureString(options, userName, password);
        }
        /// <summary>
        /// Creates and displays a configurable dialog box that allows users to supply credential information by using any credential provider installed on the local computer.
        /// </summary>
        /// <param name="options"></param>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static PromptCredentialsSecureStringResult PromptForWindowsCredentialsWithSecureString(PromptForWindowsCredentialsOptions options, SecureString userName, SecureString password)
        {
            return PromptForWindowsCredentialsInternal<PromptCredentialsSecureStringResult>(options, userName, password);
        }

        private static T PromptForWindowsCredentialsInternal<T>(PromptForWindowsCredentialsOptions options, SecureString userName, SecureString password) where T : class, IPromptCredentialsResult
        {
            var creduiInfo = new NativeMethods.CREDUI_INFO()
            {
                pszCaptionText = options.Caption,
                pszMessageText = options.Message,
                hwndParent = options.HwndParent,
                hbmBanner = options.HbmBanner
            };

            var credentialsFlag = options.Flags;

            var userNamePtr = IntPtr.Zero;
            var passwordPtr = IntPtr.Zero;
            var authPackage = 0;
            var outAuthBuffer = IntPtr.Zero;
            var outAuthBufferSize = 0;
            var inAuthBuffer = IntPtr.Zero;
            var inAuthBufferSize = 0;
            var save = options.IsSaveChecked;
            try
            {
                if (userName != null || password != null)
                {
                    if (userName == null)
                    {
                        userName = new SecureString();
                    }

                    if (password == null)
                    {
                        password = new SecureString();
                    }

                    userNamePtr = Marshal.SecureStringToCoTaskMemUnicode(userName);
                    passwordPtr = Marshal.SecureStringToCoTaskMemUnicode(password);
                }

                // prefilled with UserName or Password
                if (userNamePtr != IntPtr.Zero || passwordPtr != IntPtr.Zero)
                {
                    inAuthBufferSize = 1024;
                    inAuthBuffer = Marshal.AllocCoTaskMem(inAuthBufferSize);
                    if (
                        !NativeMethods.CredPackAuthenticationBuffer(0x00, userNamePtr, passwordPtr, inAuthBuffer,
                                                            ref inAuthBufferSize))
                    {
                        var win32Error = Marshal.GetLastWin32Error();
                        if (win32Error == 122 /*ERROR_INSUFFICIENT_BUFFER*/)
                        {
                            inAuthBuffer = Marshal.ReAllocCoTaskMem(inAuthBuffer, inAuthBufferSize);
                            if (
                                !NativeMethods.CredPackAuthenticationBuffer(0x00, userNamePtr, passwordPtr, inAuthBuffer,
                                                                    ref inAuthBufferSize))
                            {
                                throw new Win32Exception(Marshal.GetLastWin32Error());
                            }
                        }
                        else
                        {
                            throw new Win32Exception(win32Error);
                        }
                    }
                }

                var retVal = NativeMethods.CredUIPromptForWindowsCredentials(creduiInfo,
                                                                     options.AuthErrorCode,
                                                                     ref authPackage,
                                                                     inAuthBuffer,
                                                                     inAuthBufferSize,
                                                                     out outAuthBuffer,
                                                                     out outAuthBufferSize,
                                                                     ref save,
                                                                     credentialsFlag
                                                                     );

                switch (retVal)
                {
                    case NativeMethods.CredUIPromptReturnCode.Cancelled:
                        return null;
                    case NativeMethods.CredUIPromptReturnCode.Success:
                        break;
                    default:
                        throw new Win32Exception((int)retVal);
                }


                if (typeof(T) == typeof(PromptCredentialsSecureStringResult))
                {
                    var credResult = NativeMethods.CredUnPackAuthenticationBufferWrapSecureString(true, outAuthBuffer, outAuthBufferSize);
                    credResult.IsSaveChecked = save;
                    return credResult as T;
                }
                else
                {
                    var credResult = NativeMethods.CredUnPackAuthenticationBufferWrap(true, outAuthBuffer, outAuthBufferSize);
                    credResult.IsSaveChecked = save;
                    return credResult as T;
                }
            }
            finally
            {
                if (inAuthBuffer != IntPtr.Zero)
                {
                    Marshal.ZeroFreeCoTaskMemUnicode(inAuthBuffer);
                }

                if (outAuthBuffer != IntPtr.Zero)
                {
                    Marshal.ZeroFreeCoTaskMemUnicode(outAuthBuffer);
                }

                if (userNamePtr != IntPtr.Zero)
                {
                    Marshal.ZeroFreeCoTaskMemUnicode(userNamePtr);
                }

                if (passwordPtr != IntPtr.Zero)
                {
                    Marshal.ZeroFreeCoTaskMemUnicode(passwordPtr);
                }
            }
        }
        #endregion

        #region Method: PromptForCredentials
        /// <summary>
        /// Creates and displays a configurable dialog box that accepts credentials information from a user.
        /// </summary>
        /// <param name="targetName"></param>
        /// <param name="caption"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static PromptCredentialsResult PromptForCredentials(string targetName, string caption, string message)
        {
            return PromptForCredentials(new PromptForCredentialsOptions(targetName, caption, message));
        }
        /// <summary>
        /// Creates and displays a configurable dialog box that accepts credentials information from a user.
        /// </summary>
        /// <param name="targetName"></param>
        /// <param name="caption"></param>
        /// <param name="message"></param>
        /// <param name="hwndParent"></param>
        /// <returns></returns>
        public static PromptCredentialsResult PromptForCredentials(string targetName, string caption, string message, IntPtr hwndParent)
        {
            return PromptForCredentials(targetName, caption, message, hwndParent);
        }
        /// <summary>
        /// Creates and displays a configurable dialog box that accepts credentials information from a user.
        /// </summary>
        /// <param name="targetName"></param>
        /// <param name="caption"></param>
        /// <param name="message"></param>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static PromptCredentialsResult PromptForCredentials(string targetName, string caption, string message, string userName, string password)
        {
            return PromptForCredentials(targetName, caption, message, IntPtr.Zero, userName, password);
        }
        /// <summary>
        /// Creates and displays a configurable dialog box that accepts credentials information from a user.
        /// </summary>
        /// <param name="targetName"></param>
        /// <param name="caption"></param>
        /// <param name="message"></param>
        /// <param name="hwndParent"></param>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static PromptCredentialsResult PromptForCredentials(string targetName, string caption, string message, IntPtr hwndParent, string userName, string password)
        {
            return PromptForCredentials(new PromptForCredentialsOptions(targetName, caption, message) { HwndParent = hwndParent }, userName, password);
        }
        /// <summary>
        /// Creates and displays a configurable dialog box that accepts credentials information from a user.
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public static PromptCredentialsResult PromptForCredentials(PromptForCredentialsOptions options)
        {
            return PromptForCredentials(options, null, null);
        }
        /// <summary>
        /// Creates and displays a configurable dialog box that accepts credentials information from a user.
        /// </summary>
        /// <param name="options"></param>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static PromptCredentialsResult PromptForCredentials(PromptForCredentialsOptions options, string userName, string password)
        {
            using (var userNameS = new SecureString())
            using (var passwordS = new SecureString())
            {
                if (!string.IsNullOrEmpty(userName))
                {
                    foreach (var c in userName)
                    {
                        userNameS.AppendChar(c);
                    }
                }
                if (!string.IsNullOrEmpty(password))
                {
                    foreach (var c in password)
                    {
                        passwordS.AppendChar(c);
                    }
                }

                userNameS.MakeReadOnly();
                passwordS.MakeReadOnly();
                return PromptForCredentialsInternal<PromptCredentialsResult>(options, userNameS, passwordS);
            }
        }
        /// <summary>
        /// Creates and displays a configurable dialog box that accepts credentials information from a user.
        /// </summary>
        /// <param name="targetName"></param>
        /// <param name="caption"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public static PromptCredentialsSecureStringResult PromptForCredentialsWithSecureString(string targetName, string caption, string message)
        {
            return PromptForCredentialsWithSecureString(new PromptForCredentialsOptions(targetName, caption, message));
        }
        /// <summary>
        /// Creates and displays a configurable dialog box that accepts credentials information from a user.
        /// </summary>
        /// <param name="targetName"></param>
        /// <param name="caption"></param>
        /// <param name="message"></param>
        /// <param name="hwndParent"></param>
        /// <returns></returns>
        public static PromptCredentialsSecureStringResult PromptForCredentialsWithSecureString(string targetName, string caption, string message, IntPtr hwndParent)
        {
            return PromptForCredentialsWithSecureString(targetName, caption, message, hwndParent, null, null);
        }
        /// <summary>
        /// Creates and displays a configurable dialog box that accepts credentials information from a user.
        /// </summary>
        /// <param name="targetName"></param>
        /// <param name="caption"></param>
        /// <param name="message"></param>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static PromptCredentialsSecureStringResult PromptForCredentialsWithSecureString(string targetName, string caption, string message, SecureString userName, SecureString password)
        {
            return PromptForCredentialsWithSecureString(targetName, caption, message, IntPtr.Zero, userName, password);
        }
        /// <summary>
        /// Creates and displays a configurable dialog box that accepts credentials information from a user.
        /// </summary>
        /// <param name="targetName"></param>
        /// <param name="caption"></param>
        /// <param name="message"></param>
        /// <param name="hwndParent"></param>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static PromptCredentialsSecureStringResult PromptForCredentialsWithSecureString(string targetName, string caption, string message, IntPtr hwndParent, SecureString userName, SecureString password)
        {
            return PromptForCredentialsWithSecureString(new PromptForCredentialsOptions(targetName, caption, message) { HwndParent = hwndParent }, userName, password);
        }
        /// <summary>
        /// Creates and displays a configurable dialog box that accepts credentials information from a user.
        /// </summary>
        /// <param name="options"></param>
        /// <returns></returns>
        public static PromptCredentialsSecureStringResult PromptForCredentialsWithSecureString(PromptForCredentialsOptions options)
        {
            return PromptForCredentialsInternal<PromptCredentialsSecureStringResult>(options, null, null);
        }
        /// <summary>
        /// Creates and displays a configurable dialog box that accepts credentials information from a user.
        /// </summary>
        /// <param name="options"></param>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static PromptCredentialsSecureStringResult PromptForCredentialsWithSecureString(PromptForCredentialsOptions options, SecureString userName, SecureString password)
        {
            return PromptForCredentialsInternal<PromptCredentialsSecureStringResult>(options, userName, password);
        }

        private static T PromptForCredentialsInternal<T>(PromptForCredentialsOptions options, SecureString userName, SecureString password) where T : class, IPromptCredentialsResult
        {
            if (options == null)
            {
                throw new ArgumentNullException(nameof(options));
            }

            if (userName != null && (userName.Length > NativeMethods.CREDUI_MAX_USERNAME_LENGTH))
            {
                throw new ArgumentOutOfRangeException(nameof(userName), "CREDUI_MAX_USERNAME_LENGTH");
            }

            if (password != null && (password.Length > NativeMethods.CREDUI_MAX_PASSWORD_LENGTH))
            {
                throw new ArgumentOutOfRangeException(nameof(password), "CREDUI_MAX_PASSWORD_LENGTH");
            }

            var creduiInfo = new NativeMethods.CREDUI_INFO()
            {
                pszCaptionText = options.Caption,
                pszMessageText = options.Message,
                hwndParent = options.HwndParent,
                hbmBanner = options.HbmBanner
            };
            var userNamePtr = IntPtr.Zero;
            var passwordPtr = IntPtr.Zero;
            var save = options.IsSaveChecked;
            try
            {
                // The maximum number of characters that can be copied to (pszUserName|szPassword) including the terminating null character.
                if (userName == null)
                {
                    userNamePtr = Marshal.AllocCoTaskMem((NativeMethods.CREDUI_MAX_USERNAME_LENGTH + 1) * sizeof(short));
                    Marshal.WriteInt16(userNamePtr, 0, 0x00);
                }
                else
                {
                    userNamePtr = Marshal.SecureStringToCoTaskMemUnicode(userName);
                    userNamePtr = Marshal.ReAllocCoTaskMem(userNamePtr, (NativeMethods.CREDUI_MAX_USERNAME_LENGTH + 1) * sizeof(short));
                }

                if (password == null)
                {
                    passwordPtr = Marshal.AllocCoTaskMem((NativeMethods.CREDUI_MAX_PASSWORD_LENGTH + 1) * sizeof(short));
                    Marshal.WriteInt16(passwordPtr, 0, 0x00);
                }
                else
                {
                    passwordPtr = Marshal.SecureStringToCoTaskMemUnicode(password);
                    passwordPtr = Marshal.ReAllocCoTaskMem(passwordPtr, (NativeMethods.CREDUI_MAX_PASSWORD_LENGTH + 1) * sizeof(short));
                }
                Marshal.WriteInt16(userNamePtr, NativeMethods.CREDUI_MAX_USERNAME_LENGTH * sizeof(short), 0x00);
                Marshal.WriteInt16(passwordPtr, NativeMethods.CREDUI_MAX_PASSWORD_LENGTH * sizeof(short), 0x00);

                var retVal = NativeMethods.CredUIPromptForCredentials(creduiInfo,
                                                              options.TargetName,
                                                              IntPtr.Zero,
                                                              options.AuthErrorCode,
                                                              userNamePtr,
                                                              NativeMethods.CREDUI_MAX_USERNAME_LENGTH,
                                                              passwordPtr,
                                                              NativeMethods.CREDUI_MAX_PASSWORD_LENGTH,
                                                              ref save,
                                                              options.Flags);
                switch (retVal)
                {
                    case NativeMethods.CredUIPromptReturnCode.Cancelled:
                        return null;
                    case NativeMethods.CredUIPromptReturnCode.InvalidParameter:
                        throw new Win32Exception((int)retVal);
                    case NativeMethods.CredUIPromptReturnCode.InvalidFlags:
                        throw new Win32Exception((int)retVal);
                    case NativeMethods.CredUIPromptReturnCode.Success:
                        break;
                    default:
                        throw new Win32Exception((int)retVal);
                }


                if (typeof(T) == typeof(PromptCredentialsSecureStringResult))
                {
                    return new PromptCredentialsSecureStringResult
                    {
                        UserName = NativeMethods.PtrToSecureString(userNamePtr),
                        Password = NativeMethods.PtrToSecureString(passwordPtr),
                        IsSaveChecked = save
                    } as T;
                }
                else
                {
                    return new PromptCredentialsResult
                    {
                        UserName = Marshal.PtrToStringUni(userNamePtr),
                        Password = Marshal.PtrToStringUni(passwordPtr),
                        IsSaveChecked = save
                    } as T;
                }
            }
            finally
            {
                if (userNamePtr != IntPtr.Zero)
                {
                    Marshal.ZeroFreeCoTaskMemUnicode(userNamePtr);
                }

                if (passwordPtr != IntPtr.Zero)
                {
                    Marshal.ZeroFreeCoTaskMemUnicode(passwordPtr);
                }
            }
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        [Flags]
        public enum PromptForWindowsCredentialsFlag
        {
            /// <summary>
            /// Plain text username/password is being requested
            /// </summary>
            CREDUIWIN_GENERIC = 0x00000001,
            /// <summary>
            /// Show the Save Credential checkbox
            /// </summary>
            CREDUIWIN_CHECKBOX = 0x00000002,
            /// <summary>
            /// Only Cred Providers that support the input auth package should enumerate
            /// </summary>
            CREDUIWIN_AUTHPACKAGE_ONLY = 0x00000010,
            /// <summary>
            /// Only the incoming cred for the specific auth package should be enumerated
            /// </summary>
            CREDUIWIN_IN_CRED_ONLY = 0x00000020,
            /// <summary>
            /// Cred Providers should enumerate administrators only
            /// </summary>
            CREDUIWIN_ENUMERATE_ADMINS = 0x00000100,
            /// <summary>
            /// Only the incoming cred for the specific auth package should be enumerated
            /// </summary>
            CREDUIWIN_ENUMERATE_CURRENT_USER = 0x00000200,
            /// <summary>
            /// The Credui prompt should be displayed on the secure desktop
            /// </summary>
            CREDUIWIN_SECURE_PROMPT = 0x00001000,
            /// <summary>
            /// Tell the credential provider it should be packing its Auth Blob 32 bit even though it is running 64 native
            /// </summary>
            CREDUIWIN_PACK_32_WOW = 0x10000000
        }

        /// <summary>
        /// 
        /// </summary>
        [Flags]
        public enum PromptForCredentialsFlag
        {
            /// <summary>
            /// indicates the username is valid, but password is not
            /// </summary>
            CREDUI_FLAGS_INCORRECT_PASSWORD = 0x00001,
            /// <summary>
            /// Do not show "Save" checkbox, and do not persist credentials
            /// </summary>
            CREDUI_FLAGS_DO_NOT_PERSIST = 0x00002,
            /// <summary>
            /// Populate list box with admin accounts
            /// </summary>
            CREDUI_FLAGS_REQUEST_ADMINISTRATOR = 0x00004,
            /// <summary>
            /// do not include certificates in the drop list
            /// </summary>
            CREDUI_FLAGS_EXCLUDE_CERTIFICATES = 0x00008,
            /// <summary>
            /// 
            /// </summary>
            CREDUI_FLAGS_REQUIRE_CERTIFICATE = 0x00010,
            /// <summary>
            /// 
            /// </summary>
            CREDUI_FLAGS_SHOW_SAVE_CHECK_BOX = 0x00040,
            /// <summary>
            /// 
            /// </summary>
            CREDUI_FLAGS_ALWAYS_SHOW_UI = 0x00080,
            /// <summary>
            /// 
            /// </summary>
            CREDUI_FLAGS_REQUIRE_SMARTCARD = 0x00100,
            /// <summary>
            /// 
            /// </summary>
            CREDUI_FLAGS_PASSWORD_ONLY_OK = 0x00200,
            /// <summary>
            /// 
            /// </summary>
            CREDUI_FLAGS_VALIDATE_USERNAME = 0x00400,
            /// <summary>
            /// 
            /// </summary>
            CREDUI_FLAGS_COMPLETE_USERNAME = 0x00800,
            /// <summary>
            /// Do not show "Save" checkbox, but persist credentials anyway
            /// </summary>
            CREDUI_FLAGS_PERSIST = 0x01000,
            /// <summary>
            /// 
            /// </summary>
            CREDUI_FLAGS_SERVER_CREDENTIAL = 0x04000,
            /// <summary>
            /// do not persist unless caller later confirms credential via CredUIConfirmCredential() api
            /// </summary>
            CREDUI_FLAGS_EXPECT_CONFIRMATION = 0x20000,
            /// <summary>
            /// Credential is a generic credential
            /// </summary>
            CREDUI_FLAGS_GENERIC_CREDENTIALS = 0x40000,
            /// <summary>
            /// Credential has a username as the target
            /// </summary>
            CREDUI_FLAGS_USERNAME_TARGET_CREDENTIALS = 0x80000,
            /// <summary>
            /// don't allow the user to change the supplied username
            /// </summary>
            CREDUI_FLAGS_KEEP_USERNAME = 0x100000
        }

        /// <summary>
        /// 
        /// </summary>
        public class PromptForWindowsCredentialsOptions
        {
            private string _caption;
            private string _message;
            public string Caption
            {
                get { return _caption; }
                set
                {
                    if (value.Length > NativeMethods.CREDUI_MAX_CAPTION_LENGTH)
                    {
                        throw new ArgumentOutOfRangeException(nameof(value));
                    }

                    _caption = value;
                }
            }
            public string Message
            {
                get { return _message; }
                set
                {
                    if (value.Length > NativeMethods.CREDUI_MAX_MESSAGE_LENGTH)
                    {
                        throw new ArgumentOutOfRangeException(nameof(value));
                    }

                    _message = value;
                }
            }
            public IntPtr HwndParent { get; set; }
            public IntPtr HbmBanner { get; set; }
            public bool IsSaveChecked { get; set; }
            public PromptForWindowsCredentialsFlag Flags { get; set; }
            public int AuthErrorCode { get; set; }
            public PromptForWindowsCredentialsOptions(string caption, string message)
            {
                if (string.IsNullOrEmpty(caption))
                {
                    throw new ArgumentNullException(nameof(caption));
                }

                if (string.IsNullOrEmpty(message))
                {
                    throw new ArgumentNullException(nameof(message));
                }

                Caption = caption;
                Message = message;
                Flags = PromptForWindowsCredentialsFlag.CREDUIWIN_GENERIC;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        public class PromptForCredentialsOptions
        {
            private string _caption;
            private string _message;
            public string Caption
            {
                get { return _caption; }
                set
                {
                    if (value.Length > NativeMethods.CREDUI_MAX_CAPTION_LENGTH)
                    {
                        throw new ArgumentOutOfRangeException(nameof(value));
                    }

                    _caption = value;
                }
            }
            public string Message
            {
                get { return _message; }
                set
                {
                    if (value.Length > NativeMethods.CREDUI_MAX_MESSAGE_LENGTH)
                    {
                        throw new ArgumentOutOfRangeException(nameof(value));
                    }

                    _message = value;
                }
            }
            public string TargetName { get; set; }
            public IntPtr HwndParent { get; set; }
            public IntPtr HbmBanner { get; set; }
            public bool IsSaveChecked { get; set; }
            public PromptForCredentialsFlag Flags { get; set; }
            public int AuthErrorCode { get; set; }
            public PromptForCredentialsOptions(string targetName, string caption, string message)
            {
                if (targetName == null)
                {
                    targetName = string.Empty; // new ArgumentNullException(nameof(targetName));
                }

                if (string.IsNullOrEmpty(caption))
                {
                    throw new ArgumentNullException(nameof(caption));
                }

                if (string.IsNullOrEmpty(message))
                {
                    throw new ArgumentNullException(nameof(message));
                }

                TargetName = targetName;
                Caption = caption;
                Message = message;
                Flags = PromptForCredentialsFlag.CREDUI_FLAGS_GENERIC_CREDENTIALS | PromptForCredentialsFlag.CREDUI_FLAGS_DO_NOT_PERSIST;
            }
        }

        private static class NativeMethods
        {
            public const int CREDUI_MAX_MESSAGE_LENGTH = 32767;
            public const int CREDUI_MAX_CAPTION_LENGTH = 128;
            public const int CRED_MAX_USERNAME_LENGTH = (256 + 1 + 256);
            public const int CREDUI_MAX_USERNAME_LENGTH = CRED_MAX_USERNAME_LENGTH;
            public const int CREDUI_MAX_PASSWORD_LENGTH = (512 / 2);

            public enum CredUIPromptReturnCode
            {
                Success = 0,
                Cancelled = 1223,
                InvalidParameter = 87,
                InvalidFlags = 1004
            }

            [StructLayout(LayoutKind.Sequential)]
            public class CREDUI_INFO
            {
                public int cbSize;
                public IntPtr hwndParent;
                [MarshalAs(UnmanagedType.LPWStr)]
                public string pszMessageText;
                [MarshalAs(UnmanagedType.LPWStr)]
                public string pszCaptionText;
                public IntPtr hbmBanner;

                public CREDUI_INFO()
                {
                    cbSize = Marshal.SizeOf(typeof(CREDUI_INFO));
                }
            }

            //
            // CredUIPromptForCredentials -------------------------------------
            //
            [DllImport("credui.dll", CharSet = CharSet.Unicode)]
            public static extern CredUIPromptReturnCode CredUIPromptForCredentials(
                CREDUI_INFO pUiInfo,
                string pszTargetName,
                IntPtr Reserved,
                int dwAuthError,
                IntPtr pszUserName,
                int ulUserNameMaxChars,
                IntPtr pszPassword,
                int ulPasswordMaxChars,
                ref bool pfSave,
                PromptForCredentialsFlag dwFlags
            );

            //
            // CredUIPromptForWindowsCredentials ------------------------------
            //
            [DllImport("credui.dll", CharSet = CharSet.Unicode)]
            public static extern CredUIPromptReturnCode
            CredUIPromptForWindowsCredentials(
                CREDUI_INFO pUiInfo,
                int dwAuthError,
                ref int pulAuthPackage,
                IntPtr pvInAuthBuffer,
                int ulInAuthBufferSize,
                out IntPtr ppvOutAuthBuffer,
                out int pulOutAuthBufferSize,
                ref bool pfSave,
                PromptForWindowsCredentialsFlag dwFlags
            );

            [DllImport("credui.dll", CharSet = CharSet.Unicode, SetLastError = true)]
            public static extern bool CredPackAuthenticationBuffer(
                int dwFlags,
                string pszUserName,
                string pszPassword,
                IntPtr pPackedCredentials,
                ref int pcbPackedCredentials
            );
            [DllImport("credui.dll", CharSet = CharSet.Unicode, SetLastError = true)]
            public static extern bool CredPackAuthenticationBuffer(
                int dwFlags,
                IntPtr pszUserName,
                IntPtr pszPassword,
                IntPtr pPackedCredentials,
                ref int pcbPackedCredentials
            );

            [DllImport("credui.dll", CharSet = CharSet.Unicode, SetLastError = true)]
            public static extern bool CredUnPackAuthenticationBuffer(
                int dwFlags,
                IntPtr pAuthBuffer,
                int cbAuthBuffer,
                StringBuilder pszUserName,
                ref int pcchMaxUserName,
                StringBuilder pszDomainName,
                ref int pcchMaxDomainame,
                StringBuilder pszPassword,
                ref int pcchMaxPassword
            );
            [DllImport("credui.dll", CharSet = CharSet.Unicode, SetLastError = true)]
            public static extern bool CredUnPackAuthenticationBuffer(
                int dwFlags,
                IntPtr pAuthBuffer,
                int cbAuthBuffer,
                IntPtr pszUserName,
                ref int pcchMaxUserName,
                IntPtr pszDomainName,
                ref int pcchMaxDomainame,
                IntPtr pszPassword,
                ref int pcchMaxPassword
            );

            public static PromptCredentialsResult CredUnPackAuthenticationBufferWrap(bool decryptProtectedCredentials, IntPtr authBufferPtr, int authBufferSize)
            {
                var sbUserName = new StringBuilder(255);
                var sbDomainName = new StringBuilder(255);
                var sbPassword = new StringBuilder(255);
                var userNameSize = sbUserName.Capacity;
                var domainNameSize = sbDomainName.Capacity;
                var passwordSize = sbPassword.Capacity;

                //#define CRED_PACK_PROTECTED_CREDENTIALS      0x1
                //#define CRED_PACK_WOW_BUFFER                 0x2
                //#define CRED_PACK_GENERIC_CREDENTIALS        0x4

                var result = CredUnPackAuthenticationBuffer((decryptProtectedCredentials ? 0x1 : 0x0),
                                                                authBufferPtr,
                                                                authBufferSize,
                                                                sbUserName,
                                                                ref userNameSize,
                                                                sbDomainName,
                                                                ref domainNameSize,
                                                                sbPassword,
                                                                ref passwordSize
                                                                );
                if (!result)
                {
                    var win32Error = Marshal.GetLastWin32Error();
                    if (win32Error == 122 /*ERROR_INSUFFICIENT_BUFFER*/)
                    {
                        sbUserName.Capacity = userNameSize;
                        sbPassword.Capacity = passwordSize;
                        sbDomainName.Capacity = domainNameSize;
                        result = CredUnPackAuthenticationBuffer((decryptProtectedCredentials ? 0x1 : 0x0),
                                                                authBufferPtr,
                                                                authBufferSize,
                                                                sbUserName,
                                                                ref userNameSize,
                                                                sbDomainName,
                                                                ref domainNameSize,
                                                                sbPassword,
                                                                ref passwordSize
                                                                );
                        if (!result)
                        {
                            throw new Win32Exception(Marshal.GetLastWin32Error());
                        }
                    }
                    else
                    {
                        throw new Win32Exception(win32Error);
                    }
                }

                return new PromptCredentialsResult
                {
                    UserName = sbUserName.ToString(),
                    DomainName = sbDomainName.ToString(),
                    Password = sbPassword.ToString()
                };
            }

            public static PromptCredentialsSecureStringResult CredUnPackAuthenticationBufferWrapSecureString(bool decryptProtectedCredentials, IntPtr authBufferPtr, int authBufferSize)
            {
                var userNameSize = 255;
                var domainNameSize = 255;
                var passwordSize = 255;
                var userNamePtr = IntPtr.Zero;
                var domainNamePtr = IntPtr.Zero;
                var passwordPtr = IntPtr.Zero;
                try
                {
                    userNamePtr = Marshal.AllocCoTaskMem(userNameSize);
                    domainNamePtr = Marshal.AllocCoTaskMem(domainNameSize);
                    passwordPtr = Marshal.AllocCoTaskMem(passwordSize);

                    //#define CRED_PACK_PROTECTED_CREDENTIALS      0x1
                    //#define CRED_PACK_WOW_BUFFER                 0x2
                    //#define CRED_PACK_GENERIC_CREDENTIALS        0x4

                    var result = CredUnPackAuthenticationBuffer((decryptProtectedCredentials ? 0x1 : 0x0),
                                                                    authBufferPtr,
                                                                    authBufferSize,
                                                                    userNamePtr,
                                                                    ref userNameSize,
                                                                    domainNamePtr,
                                                                    ref domainNameSize,
                                                                    passwordPtr,
                                                                    ref passwordSize
                                                                    );
                    if (!result)
                    {
                        var win32Error = Marshal.GetLastWin32Error();
                        if (win32Error == 122 /*ERROR_INSUFFICIENT_BUFFER*/)
                        {
                            userNamePtr = Marshal.ReAllocCoTaskMem(userNamePtr, userNameSize);
                            domainNamePtr = Marshal.ReAllocCoTaskMem(domainNamePtr, domainNameSize);
                            passwordPtr = Marshal.ReAllocCoTaskMem(passwordPtr, passwordSize);
                            result = CredUnPackAuthenticationBuffer((decryptProtectedCredentials ? 0x1 : 0x0),
                                                                    authBufferPtr,
                                                                    authBufferSize,
                                                                    userNamePtr,
                                                                    ref userNameSize,
                                                                    domainNamePtr,
                                                                    ref domainNameSize,
                                                                    passwordPtr,
                                                                    ref passwordSize);
                            if (!result)
                            {
                                throw new Win32Exception(Marshal.GetLastWin32Error());
                            }
                        }
                        else
                        {
                            throw new Win32Exception(win32Error);
                        }
                    }

                    return new PromptCredentialsSecureStringResult
                    {
                        UserName = PtrToSecureString(userNamePtr, userNameSize),
                        DomainName = PtrToSecureString(domainNamePtr, domainNameSize),
                        Password = PtrToSecureString(passwordPtr, passwordSize)
                    };
                }
                finally
                {
                    if (userNamePtr != IntPtr.Zero)
                    {
                        Marshal.ZeroFreeCoTaskMemUnicode(userNamePtr);
                    }

                    if (domainNamePtr != IntPtr.Zero)
                    {
                        Marshal.ZeroFreeCoTaskMemUnicode(domainNamePtr);
                    }

                    if (passwordPtr != IntPtr.Zero)
                    {
                        Marshal.ZeroFreeCoTaskMemUnicode(passwordPtr);
                    }
                }
            }

            #region Utility Methods
            public static SecureString PtrToSecureString(IntPtr p)
            {
                var s = new SecureString();
                var i = 0;
                while (true)
                {
                    var c = (char)Marshal.ReadInt16(p, ((i++) * sizeof(short)));
                    if (c == '\u0000')
                    {
                        break;
                    }

                    s.AppendChar(c);
                }
                s.MakeReadOnly();
                return s;
            }
            public static SecureString PtrToSecureString(IntPtr p, int length)
            {
                var s = new SecureString();
                for (var i = 0; i < length; i++)
                {
                    s.AppendChar((char)Marshal.ReadInt16(p, i * sizeof(short)));
                }

                s.MakeReadOnly();
                return s;
            }
            #endregion
        }
    }

    /// <summary>
    /// 
    /// </summary>
    public interface IPromptCredentialsResult
    {
    }
    /// <summary>
    /// 
    /// </summary>
    public class PromptCredentialsResult : IPromptCredentialsResult
    {
        public string UserName { get; internal set; }
        public string DomainName { get; internal set; }
        public string Password { get; internal set; }
        public bool IsSaveChecked { get; set; }
        
        public static implicit operator PSCredential(PromptCredentialsResult credentials)
        {
            if (string.IsNullOrEmpty(credentials?.UserName))
            {
                return null;
            }

            return new PSCredential(string.IsNullOrEmpty(credentials.DomainName) ? credentials.UserName : $"{credentials.DomainName}\\{credentials.UserName}", credentials.Password?.ConvertToSecureString());
        }
    }
    /// <summary>
    /// 
    /// </summary>
    public class PromptCredentialsSecureStringResult : IPromptCredentialsResult
    {
        public SecureString UserName { get; internal set; }
        public SecureString DomainName { get; internal set; }
        public SecureString Password { get; internal set; }
        public bool IsSaveChecked { get; set; }

        public static implicit operator PSCredential(PromptCredentialsSecureStringResult credentialsSecureStringResult)
        {
            if (credentialsSecureStringResult == null)
            {
                return null;
            }

            var userName = credentialsSecureStringResult.DomainName != null && credentialsSecureStringResult.DomainName.Length > 0
                            ? credentialsSecureStringResult.UserName.ConvertToString()
                            : $@"{credentialsSecureStringResult.DomainName.ConvertToString()}\{credentialsSecureStringResult.UserName.ConvertToString()}";

            return new PSCredential( userName, credentialsSecureStringResult.Password);
        }
    }


    internal static class SecureStringHelper
    {
        // Methods
        internal static SecureString ConvertToSecureString(this string plainString)
        {
            var result = new SecureString();
            if (!string.IsNullOrEmpty(plainString))
            {
                foreach (var c in plainString.ToCharArray())
                {
                    result.AppendChar(c);
                }
            }
            result.MakeReadOnly();
            return result;
        }

        internal static SecureString CreateSecureString(IntPtr ptrToString, int length = 0)
        {
            var password = length > 0
                 ? Marshal.PtrToStringUni(ptrToString, length)
                 : Marshal.PtrToStringUni(ptrToString);
            return ConvertToSecureString(password);
        }


        internal static string ConvertToString(this SecureString secureString)
        {
            var str = string.Empty;
            var pointer = IntPtr.Zero;
            if ((secureString != null) && (secureString.Length > 0))
            {

                try
                {
                    pointer = Marshal.SecureStringToBSTR(secureString);
                    str = Marshal.PtrToStringBSTR(pointer);
                }
                finally
                {
                    if (pointer != IntPtr.Zero)
                    {
                        Marshal.ZeroFreeBSTR(pointer);
                    }
                }
            }
            return str;
        }
    }

}

