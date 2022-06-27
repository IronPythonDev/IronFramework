$PluginName = $args[0];

$PluginsDirectory = ".\src\Plugins\${PluginName}" 

New-Item -Path $PluginsDirectory -ItemType "directory"

$PluginServerProjectName = "${PluginName}.Server";
$PluginClientProjectName = "${PluginName}.Client";
$PluginSharedProjectName = "${PluginName}.Shared";

$PluginServerFolderPath = ".\src\Plugins\${PluginName}\${PluginServerProjectName}";
$PluginClientFolderPath = ".\src\Plugins\${PluginName}\${PluginClientProjectName}";
$PluginSharedFolderPath = ".\src\Plugins\${PluginName}\${PluginSharedProjectName}";

dotnet new classlib --name $PluginServerProjectName --output $PluginServerFolderPath
dotnet new classlib --name $PluginClientProjectName --output $PluginClientFolderPath
dotnet new classlib --name $PluginSharedProjectName --output $PluginSharedFolderPath

Remove-Item "${PluginServerFolderPath}\Class1.cs";
Remove-Item "${PluginClientFolderPath}\Class1.cs";
Remove-Item "${PluginSharedFolderPath}\Class1.cs";

$CorePluginLibrary = ".\src\Core\Plugin\IronFramework.Core.Plugin\IronFramework.Core.Plugin.csproj"

dotnet add "${PluginServerFolderPath}\${PluginServerProjectName}.csproj" reference "${PluginSharedFolderPath}\${PluginSharedProjectName}.csproj"
dotnet add "${PluginServerFolderPath}\${PluginServerProjectName}.csproj" reference "${CorePluginLibrary}"
dotnet add "${PluginClientFolderPath}\${PluginClientProjectName}.csproj" reference "${PluginSharedFolderPath}\${PluginSharedProjectName}.csproj"
dotnet add "${PluginClientFolderPath}\${PluginClientProjectName}.csproj" reference "${CorePluginLibrary}"

dotnet add "${PluginServerFolderPath}\${PluginServerProjectName}.csproj" package AltV.Net.Async
dotnet add "${PluginClientFolderPath}\${PluginClientProjectName}.csproj" package AltV.Net.Client.Async

dotnet sln '.\src' add -s "Plugins/${PluginName}" $PluginServerFolderPath
dotnet sln '.\src' add -s "Plugins/${PluginName}" $PluginClientFolderPath
dotnet sln '.\src' add -s "Plugins/${PluginName}" $PluginSharedFolderPath

New-Item -Path $PluginServerFolderPath -ItemType "file" -Name "Plugin.cs" -Value "using AltV.Net;
using IronFramework.Core.Plugin;

namespace $PluginServerProjectName
{
    public class Plugin : IronPlugin
    {
        public override void OnStart()
        {
            Alt.Log(""$PluginName server plugin has started!"");
        }
    }
}";

New-Item -Path $PluginClientFolderPath -ItemType "file" -Name "Plugin.cs" -Value "using AltV.Net.Client;
using IronFramework.Core.Plugin;

namespace $PluginClientProjectName
{
    public class Plugin : IronPlugin
    {
        public override void OnStart()
        {
            Alt.Log(""$PluginName client plugin has started!"");
        }
    }
}";

dotnet add ".\src\Core\Server\IronFramework.Core.Server\IronFramework.Core.Server.csproj" reference "${PluginServerFolderPath}\${PluginServerProjectName}.csproj"
dotnet add ".\src\Core\Client\IronFramework.Core.Client\IronFramework.Core.Client.csproj" reference "${PluginClientFolderPath}\${PluginClientProjectName}.csproj"

$IncludePluginMethodName = $PluginName.Replace(".", "_")

New-Item -Force -Path ".\src\Core\Server\IronFramework.Core.Server\" -ItemType "file" -Name "IronCoreResource.${PluginName}.cs" -Value "namespace IronFramework.Core.Server
{
    public partial class IronCoreResource
    {
        public static void ${IncludePluginMethodName}_Plugin() => IncludedPlugins.Add(new ${PluginServerProjectName}.Plugin());
    }
}";

New-Item -Force -Path ".\src\Core\Client\IronFramework.Core.Client" -ItemType "file" -Name "IronCoreResource.${PluginName}.cs" -Value "namespace IronFramework.Core.Client
{
    public partial class IronCoreResource
    {
        public static void ${IncludePluginMethodName}_Plugin() => IncludedPlugins.Add(new ${PluginClientProjectName}.Plugin());
    }
}";