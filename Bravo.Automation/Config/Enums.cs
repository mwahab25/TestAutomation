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
        MacOS,
        Android,
        IOS
    }
    enum Browsers
    {
        Chrome,
        Firefox,
        Edge
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
        XPath,
        Id,
        CssLocator,
        ClassName,
        LinkText,
        Name,
        PartialLinkText
    }
}
