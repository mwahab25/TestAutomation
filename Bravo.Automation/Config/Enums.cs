using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bravo.Automation.Config
{
    enum AppTypes
    {
        Web,
        Mobile
    }
    enum OS
    {
        Windows,
        Android
    }
    enum Browsers
    {
        Chrome,
        IE,
        Firefox,
        Edge
    }
    enum MobileOS
    {
        Android,
        IOS
    }
    enum MobileAppTypes
    {
        Native,
        Hybrid,
        MobileWeb
    }
    enum Outcome
    {
        NotRun,
        Pass,
        Fail,
        Error,
        Aborted
    }
    enum LocatorTypes
    {
        xpath,
        id,
        csslocator,
        classname,
        linktext,
        name,
        partiallinktext
    }
}
