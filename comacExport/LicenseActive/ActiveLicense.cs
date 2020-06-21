using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using QLicense;

namespace LicenseActive
{
    public class ActiveLicense
    {
        public static Type LicenseObjectType { get; set; }
        public static byte[] CertificatePublicKeyData { private get; set; }
        private static SecureString certPwd = new SecureString();
        public static TPXNLicense licenseInfor = new TPXNLicense();

        static ActiveLicense()
        {
            
            Assembly _assembly = Assembly.GetExecutingAssembly();
            using (MemoryStream _mem = new MemoryStream())
            {
                _assembly.GetManifestResourceStream("LicenseActive.LicenseVerify.cer").CopyTo(_mem);

                CertificatePublicKeyData = _mem.ToArray();

                certPwd.AppendChar('T');
                certPwd.AppendChar('u');
                certPwd.AppendChar('n');
                certPwd.AppendChar('g');
                certPwd.AppendChar('r');
                certPwd.AppendChar('a');
                certPwd.AppendChar('u');
                certPwd.AppendChar('!');
                certPwd.AppendChar('1');
                certPwd.AppendChar('1');
            }
        }

        public static bool ValidateLicense(String liscenText , string appName)
        {
            if (string.IsNullOrWhiteSpace(liscenText))
            {
                MessageBox.Show("Please input license", string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            //generate UID
            string UID = HardwareInfo.GenerateUID(appName);


            //Check the activation string
            LicenseStatus _licStatus = LicenseStatus.UNDEFINED;
            string _msg = string.Empty;
            LicenseEntity _lic = LicenseHandler.ParseLicenseFromBASE64String(licenseInfor.GetType(), liscenText.Trim(), CertificatePublicKeyData, out _licStatus, out _msg);
            switch (_licStatus)
            {
                case LicenseStatus.VALID:
                    if (UID == _lic.UID)
                    {
                        licenseInfor = _lic as TPXNLicense;
                        return true;
                        //MessageBox.Show(_msg, "License is valid", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    return false;
                case LicenseStatus.CRACKED:
                case LicenseStatus.INVALID:
                case LicenseStatus.UNDEFINED:
                    //if (ShowMessageAfterValidation)
                    {
                        //MessageBox.Show(_msg, "License is INVALID", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }

                    return false;

                default:
                    return false;
            }
        }
    }
}
