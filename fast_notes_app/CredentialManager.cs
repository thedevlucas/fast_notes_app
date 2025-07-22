using System;
using System.Runtime.InteropServices;
using System.Text;

namespace fast_notes_app
{
    public static class CredentialManager
    {
        private const string TARGET_NAME = "FastNotesApp_UserCredentials";

        [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        private static extern bool CredWrite(ref CREDENTIAL userCredential, uint flags);

        [DllImport("advapi32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        private static extern bool CredRead(string target, uint type, uint reservedFlag, out IntPtr credentialPtr);

        [DllImport("advapi32.dll", SetLastError = true)]
        private static extern bool CredDelete(string target, uint type, uint reservedFlag);

        [DllImport("advapi32.dll", SetLastError = true)]
        private static extern void CredFree(IntPtr cred);

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        private struct CREDENTIAL
        {
            public uint Flags;
            public uint Type;
            public IntPtr TargetName;
            public IntPtr Comment;
            public System.Runtime.InteropServices.ComTypes.FILETIME LastWritten;
            public uint CredentialBlobSize;
            public IntPtr CredentialBlob;
            public uint Persist;
            public uint AttributeCount;
            public IntPtr Attributes;
            public IntPtr TargetAlias;
            public IntPtr UserName;
        }

        public static bool SaveCredentials(string username, int userId)
        {
            try
            {
                // Create credential data (username:userId)
                string credentialData = $"{username}:{userId}";
                byte[] credentialBytes = Encoding.UTF8.GetBytes(credentialData);

                var credential = new CREDENTIAL
                {
                    Type = 1, // CRED_TYPE_GENERIC
                    TargetName = Marshal.StringToCoTaskMemUni(TARGET_NAME),
                    UserName = Marshal.StringToCoTaskMemUni(username),
                    CredentialBlob = Marshal.AllocCoTaskMem(credentialBytes.Length),
                    CredentialBlobSize = (uint)credentialBytes.Length,
                    Persist = 2 // CRED_PERSIST_LOCAL_MACHINE
                };

                Marshal.Copy(credentialBytes, 0, credential.CredentialBlob, credentialBytes.Length);

                bool result = CredWrite(ref credential, 0);

                // Clean up allocated memory
                Marshal.FreeCoTaskMem(credential.TargetName);
                Marshal.FreeCoTaskMem(credential.UserName);
                Marshal.FreeCoTaskMem(credential.CredentialBlob);

                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving credentials: {ex.Message}");
                return false;
            }
        }

        public static (bool success, string username, int userId) GetSavedCredentials()
        {
            try
            {
                if (CredRead(TARGET_NAME, 1, 0, out IntPtr credentialPtr))
                {
                    var credential = Marshal.PtrToStructure<CREDENTIAL>(credentialPtr);

                    byte[] credentialBytes = new byte[credential.CredentialBlobSize];
                    Marshal.Copy(credential.CredentialBlob, credentialBytes, 0, (int)credential.CredentialBlobSize);

                    string credentialData = Encoding.UTF8.GetString(credentialBytes);
                    string[] parts = credentialData.Split(':');

                    CredFree(credentialPtr);

                    if (parts.Length == 2 && int.TryParse(parts[1], out int userId))
                    {
                        return (true, parts[0], userId);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error retrieving credentials: {ex.Message}");
            }

            return (false, string.Empty, 0);
        }

        public static bool DeleteSavedCredentials()
        {
            try
            {
                return CredDelete(TARGET_NAME, 1, 0);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting credentials: {ex.Message}");
                return false;
            }
        }

        public static bool HasSavedCredentials()
        {
            try
            {
                if (CredRead(TARGET_NAME, 1, 0, out IntPtr credentialPtr))
                {
                    CredFree(credentialPtr);
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error checking credentials: {ex.Message}");
            }

            return false;
        }
    }
}
