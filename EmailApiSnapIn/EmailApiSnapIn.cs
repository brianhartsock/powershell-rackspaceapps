using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace EmailApiSnapIn
{
    [RunInstaller(true)]
    public class EmailApiSnapIn : System.Management.Automation.PSSnapIn
    {
        public override string Description
        {
            get { return "SnapIn for the Email API";  }
        }

        public override string Name
        {
            get { return "EmailApiSnapIn"; }
        }

        public override string Vendor
        {
            get { return "Rackspace Email Hosting"; }
        }
    }
}
