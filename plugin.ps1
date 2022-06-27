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
$CoreServerLibrary = ".\src\Core\Server\IronFramework.Core.Server\IronFramework.Core.Server.csproj"
$CoreClientLibrary = ".\src\Core\Client\IronFramework.Core.Client\IronFramework.Core.Client.csproj"

dotnet add "${PluginServerFolderPath}\${PluginServerProjectName}.csproj" reference "${PluginSharedFolderPath}\${PluginSharedProjectName}.csproj"
dotnet add "${PluginServerFolderPath}\${PluginServerProjectName}.csproj" reference "${CorePluginLibrary}"
dotnet add "${PluginServerFolderPath}\${PluginServerProjectName}.csproj" reference "${CoreServerLibrary}"

dotnet add "${PluginClientFolderPath}\${PluginClientProjectName}.csproj" reference "${PluginSharedFolderPath}\${PluginSharedProjectName}.csproj"
dotnet add "${PluginClientFolderPath}\${PluginClientProjectName}.csproj" reference "${CorePluginLibrary}"
dotnet add "${PluginClientFolderPath}\${PluginClientProjectName}.csproj" reference "${CoreClientLibrary}"

dotnet add "${PluginServerFolderPath}\${PluginServerProjectName}.csproj" package AltV.Net.Async
dotnet add "${PluginClientFolderPath}\${PluginClientProjectName}.csproj" package AltV.Net.Client.Async

dotnet sln '.\src' add -s "Plugins/${PluginName}" $PluginServerFolderPath
dotnet sln '.\src' add -s "Plugins/${PluginName}" $PluginClientFolderPath
dotnet sln '.\src' add -s "Plugins/${PluginName}" $PluginSharedFolderPath

dotnet new sln --name $PluginName --output ".\src\Plugins\${PluginName}"
dotnet sln ".\src\Plugins\${PluginName}" add $PluginServerFolderPath
dotnet sln ".\src\Plugins\${PluginName}" add $PluginClientFolderPath
dotnet sln ".\src\Plugins\${PluginName}" add $PluginSharedFolderPath

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

$PluginClientConfigurationXML = [xml](Get-Content "${PluginClientFolderPath}\${PluginClientProjectName}.csproj")

$ClientCopyLocalLockFileAssembliesElement = $PluginClientConfigurationXML.CreateElement("CopyLocalLockFileAssemblies")
$ClientCopyLocalLockFileAssembliesElement.InnerText = "true";
$PluginClientConfigurationXML.Project.PropertyGroup.AppendChild($ClientCopyLocalLockFileAssembliesElement);

$ClientTargetElement = $PluginClientConfigurationXML.CreateElement("Target");
$ClientTargetElement.SetAttribute("Name", "CopyBuildFiles");
$ClientTargetElement.SetAttribute("AfterTargets", "build");

$PluginClientConfigurationXML.Project.AppendChild($ClientTargetElement);


$ClientTargetItemGroup = $PluginClientConfigurationXML.CreateElement("ItemGroup");
$ClientAllOutputBuildFiles = $PluginClientConfigurationXML.CreateElement("AllOutputBuildFiles");
$ClientAllOutputBuildFiles.SetAttribute("Include", '$(OutputPath)\**\*.*');
$ClientTargetItemGroup.AppendChild($ClientAllOutputBuildFiles);

$ClientTargetCopyElement = $PluginClientConfigurationXML.CreateElement("Copy");
$ClientTargetCopyElement.SetAttribute("SourceFiles", '@(AllOutputBuildFiles)');
$ClientTargetCopyElement.SetAttribute("DestinationFolder", '../../../../altv-server/resources/iron-core/client');
$ClientTargetCopyElement.SetAttribute("SkipUnchangedFiles", 'false');

$PluginClientConfigurationXML.Project.Target.AppendChild($ClientTargetItemGroup);
$PluginClientConfigurationXML.Project.Target.AppendChild($ClientTargetCopyElement);

$PluginClientConfigurationXML.Save("${PluginClientFolderPath}\${PluginClientProjectName}.csproj");




$PluginServerConfigurationXML = [xml](Get-Content "${PluginServerFolderPath}\${PluginServerProjectName}.csproj")
$ServerCopyLocalLockFileAssembliesElement = $PluginServerConfigurationXML.CreateElement("CopyLocalLockFileAssemblies")
$ServerCopyLocalLockFileAssembliesElement.InnerText = "true";
$PluginServerConfigurationXML.Project.PropertyGroup.AppendChild($ServerCopyLocalLockFileAssembliesElement);

$ServerTargetElement = $PluginServerConfigurationXML.CreateElement("Target");
$ServerTargetElement.SetAttribute("Name", "CopyBuildFiles");
$ServerTargetElement.SetAttribute("AfterTargets", "build");

$PluginServerConfigurationXML.Project.AppendChild($ServerTargetElement);

$ServerTargetItemGroup = $PluginServerConfigurationXML.CreateElement("ItemGroup");
$ServerAllOutputBuildFiles = $PluginServerConfigurationXML.CreateElement("AllOutputBuildFiles");
$ServerAllOutputBuildFiles.SetAttribute("Include", '$(OutputPath)\**\*.*');
$ServerTargetItemGroup.AppendChild($ServerAllOutputBuildFiles);

$ServerTargetCopyElement = $PluginServerConfigurationXML.CreateElement("Copy");
$ServerTargetCopyElement.SetAttribute("SourceFiles", '@(AllOutputBuildFiles)');
$ServerTargetCopyElement.SetAttribute("DestinationFolder", '../../../../altv-server/resources/iron-core/server');
$ServerTargetCopyElement.SetAttribute("SkipUnchangedFiles", 'false');

$PluginServerConfigurationXML.Project.Target.AppendChild($ServerTargetItemGroup);
$PluginServerConfigurationXML.Project.Target.AppendChild($ServerTargetCopyElement);
$PluginServerConfigurationXML.Save("${PluginServerFolderPath}\${PluginServerProjectName}.csproj");

dotnet build $PluginClientFolderPath
dotnet build $PluginServerFolderPath

dotnet add ".\src\Core\Server\IronFramework.Core.Server.Resource\IronFramework.Core.Server.Resource.csproj" reference "${PluginServerFolderPath}\${PluginServerProjectName}.csproj"
dotnet add ".\src\Core\Client\IronFramework.Core.Client.Resource\IronFramework.Core.Client.Resource.csproj" reference "${PluginClientFolderPath}\${PluginClientProjectName}.csproj"

$IncludePluginMethodName = $PluginName.Replace(".", "_")

New-Item -Force -Path ".\src\Core\Server\IronFramework.Core.Server.Resource\Plugins" -ItemType "file" -Name "IronCoreResource.${PluginName}.cs" -Value "namespace IronFramework.Core.Server.Resource
{
    public partial class IronCoreResource
    {
        public static void ${IncludePluginMethodName}_Plugin() => IncludedPlugins.Add(new ${PluginServerProjectName}.Plugin());
    }
}";


New-Item -Force -Path ".\src\Core\Client\IronFramework.Core.Client.Resource\Plugins" -ItemType "file" -Name "IronCoreResource.${PluginName}.cs" -Value "namespace IronFramework.Core.Client.Resource
{
    public partial class IronCoreResource
    {
        public static void ${IncludePluginMethodName}_Plugin() => IncludedPlugins.Add(new ${PluginClientProjectName}.Plugin());
    }
}";

dotnet build ".\src\Core\Client\IronFramework.Core.Client.Resource"
dotnet build ".\src\Core\Server\IronFramework.Core.Server.Resource"

New-Item -Force -Path "${PluginServerFolderPath}\Properties" -ItemType "file" -Name "launchSettings.json" -Value '{
  "profiles": {
    "Run Server": {
      "commandName": "Executable",
      "executablePath": ".\\altv-server.exe",
      "workingDirectory": "..\\..\\..\\..\\altv-server",
      "nativeDebugging": true
    }
  }
}';