using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.ComponentModel;
using QLicense;

namespace LicenseActive
{
    public class TPXNLicense: QLicense.LicenseEntity
    {
        [DisplayName("ExpiredDateTime")]
        [Category("License Options")]
        [XmlElement("Expire")]
        [ShowInLicenseInfo(true, "Expire Time", ShowInLicenseInfoAttribute.FormatType.String)]
        public DateTime ExpireDateTime { get; set; }

        public override LicenseStatus DoExtraValidation(out string validationMsg)
        {
            validationMsg = string.Empty;
            return LicenseStatus.VALID;
        }
    }
}
