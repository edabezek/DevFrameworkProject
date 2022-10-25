using DevFramework.Core.Aspects.PostSharp.ExceptionAspects;
using DevFramework.Core.Aspects.PostSharp.LogAspects;
using DevFramework.Core.Aspects.PostSharp.PerformanceAspects;
using DevFramework.Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// General Information about an assembly is controlled through the following
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("DevFramework.Northwind.Business")]
[assembly: AssemblyDescription("")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("")]
[assembly: AssemblyProduct("DevFramework.Northwind.Business")]
[assembly: AssemblyCopyright("Copyright ©  2022")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]
//Assembly seviyesi loglama , AttributeTargetTypes ile istediğimiz sınıfları loglayabiliyoruz,Namespace'i uyan-bütün manager'ları loglayacak
[assembly: LogAspect(typeof(FileLogger),AttributeTargetTypes= "DevFramework.Northwind.Business.Concrete.Managers.*")]
//[assembly: LogAspect(typeof(FileLogger), AttributeTargetTypes = "DevFramework.Northwind.Business.Concrete.Managers.Get*")] //yada get ile başlayan
//[assembly: LogAspect(typeof(FileLogger), AttributeTargetTypes = "DevFramework.Northwind.Business.Concrete.Managers.*Add*")] içinde add geçenler

//Exception ları uygulama boyunca uygulasın istiyoruz,managerlar için
[assembly: ExceptionLogAspect(typeof(FileLogger), AttributeTargetTypes = "DevFramework.Northwind.Business.Concrete.Managers.*")]

//performance
[assembly: PerformanceCounterAspect(AttributeTargetTypes = "DevFramework.Northwind.Business.Concrete.Managers.*")]
//zamanı burada ezebiliriz
//[assembly: PerformanceCounterAspect(2,AttributeTargetTypes = "DevFramework.Northwind.Business.Concrete.Managers.*")]


// Setting ComVisible to false makes the types in this assembly not visible
// to COM components.  If you need to access a type in this assembly from
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("117bca1e-72e9-4470-a91a-28ae04499f48")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version
//      Build Number
//      Revision
//
// You can specify all the values or you can default the Build and Revision Numbers
// by using the '*' as shown below:
// [assembly: AssemblyVersion("1.0.*")]
[assembly: AssemblyVersion("1.0.0.0")]
[assembly: AssemblyFileVersion("1.0.0.0")]
