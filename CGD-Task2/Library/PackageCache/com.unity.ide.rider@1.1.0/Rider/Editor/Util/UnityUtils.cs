using System;
using System.Linq;
using UnityEngine;

<<<<<<< HEAD:CGD-Task2/Library/PackageCache/com.unity.ide.rider@1.1.0/Rider/Editor/Util/UnityUtils.cs
namespace Packages.Rider.Editor.Util
=======
namespace Packages.Rider.Editor
>>>>>>> SceneManeger:CGD-Task2/Library/PackageCache/com.unity.ide.rider@1.0.8/Rider/Editor/UnityUtils.cs
{
  public static class UnityUtils
  {
    internal static readonly string UnityApplicationVersion = Application.unityVersion;
    
    public static Version UnityVersion
    {
      get
      {
        var ver = UnityApplicationVersion.Split(".".ToCharArray()).Take(2).Aggregate((a, b) => a + "." + b);
        return new Version(ver);
      }
    }
  }
}